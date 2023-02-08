using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs.Specialized;
using AzureBlob.API.Models;
using System;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace AzureBlob.API.Service
{
	public class AzureBlobService
	{
		BlobServiceClient _blobClient;
		BlobContainerClient _containerClient;
		string azureConnectionString = "<Primary Connection String>";
		public AzureBlobService()
		{
			_blobClient = new BlobServiceClient(azureConnectionString);
			_containerClient = _blobClient.GetBlobContainerClient("apiimages");
		}

		public async Task<List<Azure.Response<BlobContentInfo>>> UploadFiles(List<IFormFile> files)
		{

			var azureResponse = new List<Azure.Response<BlobContentInfo>>();
			foreach(var file in files)
			{
				string fileName = file.FileName;
				using (var memoryStream = new MemoryStream())
				{
					file.CopyTo(memoryStream);
					memoryStream.Position = 0;
					var client = await _containerClient.UploadBlobAsync(fileName, memoryStream, default);
					azureResponse.Add(client);
				}
			};

			return azureResponse;
		}

		public async Task<List<BlobItem>> GetUploadedBlobs()
		{
			var items = new List<BlobItem>();
			var uploadedFiles = _containerClient.GetBlobsAsync();
			await foreach (BlobItem file in uploadedFiles)
			{
				items.Add(file);
			}

			return items;
		}
	}
}

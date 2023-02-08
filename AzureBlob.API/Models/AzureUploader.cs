using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AzureBlob.API.Models
{
    public class AzureUploader
    {
        public string FileName { get; set; }

        public FileStream Stream { get; set; }     
    }
}
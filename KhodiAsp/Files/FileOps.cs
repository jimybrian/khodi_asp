using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace KhodiAsp.Files
{
    public class FileOps
    {
        const string blobName = "khodifiles";
        const string accessKey = "fhu1H6ZaE3EWyVkCQ+faWDbeSKHX0TlqZK4ehI3jW1LVrrge+XnCO93xx17gKnpchLVdRdvaYPeJ+AStEvWreg==";
        const string containerName = "khodi-images";
        const string connectionString = "DefaultEndpointsProtocol=https;AccountName=khodifiles;AccountKey=fhu1H6ZaE3EWyVkCQ+faWDbeSKHX0TlqZK4ehI3jW1LVrrge+XnCO93xx17gKnpchLVdRdvaYPeJ+AStEvWreg==;EndpointSuffix=core.windows.net";
            


        async public Task<string> uploadFile(HttpPostedFileBase file)
        {
            var fileLink = "";         

            try
            {
                if (file.ContentLength > 0)
                {
                    var container = new BlobContainerClient(connectionString, containerName);
                    var createResponse = await container.CreateIfNotExistsAsync();
                    if (createResponse != null && createResponse.GetRawResponse().Status == 201)
                        await container.SetAccessPolicyAsync(Azure.Storage.Blobs.Models.PublicAccessType.Blob);
                    var blob = container.GetBlobClient(file.FileName);
                    await blob.DeleteIfExistsAsync(DeleteSnapshotsOption.IncludeSnapshots);
                    using (var fileStream = file.InputStream)
                    {
                        await blob.UploadAsync(fileStream, new BlobHttpHeaders { ContentType = file.ContentType });
                    }
                    fileLink = (blob.Uri.ToString());
                }
            }catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);               
            }

            return fileLink;
        }

        async public Task<bool> deleteFile(string fileName)
        {
            var container = new BlobContainerClient(connectionString, containerName);

            var blob = container.GetBlobClient(fileName);
            var result = await blob.DeleteIfExistsAsync(DeleteSnapshotsOption.IncludeSnapshots);

            return result;
        }

    }
}
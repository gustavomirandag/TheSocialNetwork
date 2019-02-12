using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheSocialNetwork.DomainModel.Interfaces.Infrastructure.StorageService;

namespace TheSocialNetwork.AzureStorageAccount
{
    public class BlobService : IFileService
    {
        public string UploadFile(string containerName, string fileName, Stream file, string contentType)
        {
            //Cria uma referência a conta de Armazenamento do Azure
            CloudStorageAccount storageAccountClient =
                CloudStorageAccount.Parse(Properties.Settings.Default
                    .StorageAccountConnectionString);

            //Cria um cliente do Blob da conta de armazenamento
            CloudBlobClient blobClient = storageAccountClient.CreateCloudBlobClient();

            //Cria uma referência ao container desejado
            CloudBlobContainer blobContainer = blobClient
                .GetContainerReference(containerName);

            //Verifico se o container ao qual eu me referêncio já existe ou não e crio se não existe
            blobContainer.CreateIfNotExists();

            //Permite acesso anônimo a URL do blob
            blobContainer.SetPermissions(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });

            //Crio uma referência a um blob com nome específico
            CloudBlockBlob blobBlock = blobContainer.GetBlockBlobReference(fileName);

            //Defino o tipo do arquivo para abrir corretamente no navegador
            blobBlock.Properties.ContentType = contentType;

            //Faço upload do arquivo
            blobBlock.UploadFromStream(file);

            return blobBlock.Uri.AbsoluteUri;
        }        
    }
}

using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSocialNetwork.AzureStorageAccount
{
    public class QueueService
    {
        public void Enqueue(string queueName, string message)
        {
            //Cria uma referência a conta de Armazenamento do Azure
            CloudStorageAccount storageAccountClient =
                CloudStorageAccount.Parse(Properties.Settings.Default
                    .StorageAccountConnectionString);

            //Cria um objeto cliente de queue/fila a partir da conta de armazenamento
            CloudQueueClient queueClient = storageAccountClient.CreateCloudQueueClient();

            //Crio um objeto que referencia uma determinada queue/fila
            CloudQueue cloudQueue = queueClient.GetQueueReference(queueName);

            //Crio a fila se ela não existir
            cloudQueue.CreateIfNotExists();

            //Crio uma mensagem a partir do objeto serializado
            CloudQueueMessage cloudQueueMessage = new CloudQueueMessage(message);

            //Adiciono a mensagem na queue/fila
            cloudQueue.AddMessage(cloudQueueMessage);
        }

        /// <summary>
        /// Consume the first message in queue
        /// </summary>
        /// <param name="queueName"Queue Name</param>
        /// <returns>The message object serialized</returns>
        public string Dequeue(string queueName)
        {
            //Cria uma referência a conta de Armazenamento do Azure
            CloudStorageAccount storageAccountClient =
                CloudStorageAccount.Parse(Properties.Settings.Default
                    .StorageAccountConnectionString);

            //Cria um objeto cliente de queue/fila a partir da conta de armazenamento
            CloudQueueClient queueClient = storageAccountClient.CreateCloudQueueClient();

            //Crio um objeto que referencia uma determinada queue/fila
            CloudQueue cloudQueue = queueClient.GetQueueReference(queueName);

            //Crio a fila se ela não existir
            cloudQueue.CreateIfNotExists();

            //Adiciono a mensagem na queue/fila
            return cloudQueue.GetMessage().AsString;
        }
    }
}

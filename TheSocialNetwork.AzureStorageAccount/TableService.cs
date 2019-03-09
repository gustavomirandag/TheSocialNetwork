using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSocialNetwork.AzureStorageAccount
{


    public class TableService<T> where T : TableEntity, new()
    {
        public void AddEntity(T entity, string tableName)
        {
            //Obtem referência a StorageAccount do Azure
            CloudStorageAccount storageAccount = CloudStorageAccount
                .Parse(AzureStorageAccount.Properties.Settings.Default.StorageAccountConnectionString);

            //Cria um CloudTableClient a partir do StorageAccount
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            //Obtem referência a tabela de imagens
            CloudTable table = tableClient.GetTableReference(tableName);

            //Create the table if it doesn't exist.
            table.CreateIfNotExists();

            //--- Create the Batch Operation ---
            TableBatchOperation batchOperation = new TableBatchOperation();

            //Adiciona a entidade no Batch
            batchOperation.Insert(entity);
            //----------------------------------

            //Executa o BatchOperation
            table.ExecuteBatch(batchOperation);
        }

        public IEnumerable<T> GetAllEntities(string tableName, string partitionKey)
        {
            //Obtem referência a StorageAccount do Azure
            CloudStorageAccount storageAccount = CloudStorageAccount
                .Parse(AzureStorageAccount.Properties.Settings.Default.StorageAccountConnectionString);

            //Cria um CloudTableClient a partir do StorageAccount
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            CloudTable table = tableClient.GetTableReference(tableName);

            //Obtem referência a tabela da entidade
            TableQuery<T> query = new TableQuery<T>()
                .Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, partitionKey));
          
            IEnumerable<T> entities = table.ExecuteQuery(query);

            return entities;
        }

        public void DeleteEntity(string tableName, string rowKey)
        {
            //Obtem referência a StorageAccount do Azure
            CloudStorageAccount storageAccount = CloudStorageAccount
                .Parse(AzureStorageAccount.Properties.Settings.Default.StorageAccountConnectionString);

            //Cria um CloudTableClient a partir do StorageAccount
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            CloudTable table = tableClient.GetTableReference(tableName);

            //Obtem referência a tabela de imagens
            TableQuery<T> query = new TableQuery<T>()
                .Where(TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.Equal, rowKey));

            var entitiesToDelete = table.ExecuteQuery(query);

            TableBatchOperation operationToDelete = new TableBatchOperation();
            foreach (var entity in entitiesToDelete)
            {
                operationToDelete.Delete(entity);
            }
            table.ExecuteBatch(operationToDelete);
        }
    }
}

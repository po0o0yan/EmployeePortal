using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Configuration;
using System.Security.Authentication;
using Portal.Api.Models;

namespace Portal.Api
{
    public class DAL : IDisposable
    {
        //private MongoServer mongoServer = null;
        private bool disposed = false;

        internal Employee GetEmployee(string id)
        {
            var collection = GetEmployeesCollection();
            var filter = Builders<Employee>.Filter.Eq("Id", id);
            return collection.Find(filter).FirstOrDefault();
        }

        // To do: update the connection string with the DNS name
        // or IP address of your server. 
        //For example, "mongodb://testlinux.cloudapp.net
        private string userName = "pouyan";
        private string host = "pouyan.documents.azure.com";
        private string password = "hgBbvzMVZDvd1ByzuPrsg2BFn1MdhmILzY12wROuhTt4JrWHLw6zhgGvi6RwZzzijpRhKYU5V7MmwRXcbS44zg==";

        private string dbName = "Employees";
        private string collectionName = "EmployeeList";

        public List<Employee> GetAllEmployees()
        {
            try
            {
                var collection = GetEmployeesCollection();
                return collection.Find(new BsonDocument()).ToList();
            }
            catch (MongoConnectionException)
            {
                return new List<Employee>();
            }
        }

        public void CreateEmployee(Employee employee)
        {
            var collection = GetEmployeeCollection();
            try
            {
                collection.InsertOne(employee);
            }
            catch (MongoCommandException ex)
            {
                string msg = ex.Message;
            }
        }

        private IMongoCollection<Employee> GetEmployeesCollection()
        {
            MongoClientSettings settings = new MongoClientSettings();
            settings.Server = new MongoServerAddress(host, 10255);
            settings.UseSsl = true;
            settings.SslSettings = new SslSettings();
            settings.SslSettings.EnabledSslProtocols = SslProtocols.Tls12;

            MongoIdentity identity = new MongoInternalIdentity(dbName, userName);
            MongoIdentityEvidence evidence = new PasswordEvidence(password);

            settings.Credentials = new List<MongoCredential>()
            {
                new MongoCredential("SCRAM-SHA-1", identity, evidence)
            };

            MongoClient client = new MongoClient(settings);
            var database = client.GetDatabase(dbName);
            var todoTaskCollection = database.GetCollection<Employee>(collectionName);
            return todoTaskCollection;
        }

        private IMongoCollection<Employee> GetEmployeeCollection()
        {
            MongoClientSettings settings = new MongoClientSettings();
            settings.Server = new MongoServerAddress(host, 10255);
            settings.UseSsl = true;
            settings.SslSettings = new SslSettings();
            settings.SslSettings.EnabledSslProtocols = SslProtocols.Tls12;

            MongoIdentity identity = new MongoInternalIdentity(dbName, userName);
            MongoIdentityEvidence evidence = new PasswordEvidence(password);

            settings.Credentials = new List<MongoCredential>()
            {
                new MongoCredential("SCRAM-SHA-1", identity, evidence)
            };
            MongoClient client = new MongoClient(settings);
            var database = client.GetDatabase(dbName);
            var employeeCollection = database.GetCollection<Employee>(collectionName);
            return employeeCollection;
        }

        public bool FlushEmployeeCollection()
        {
            try
            {
                var collection = GetEmployeeCollection();
                collection.DeleteMany(new BsonDocument());
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        # region IDisposable

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                }
            }

            this.disposed = true;
        }

        # endregion
    }
}
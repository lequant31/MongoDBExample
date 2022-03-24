using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDBExample
{
    public class Config
    {
        public static IMongoDatabase GetDB()
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var db = client.GetDatabase("Airdrop");
            return db;
        }
       
    }
}

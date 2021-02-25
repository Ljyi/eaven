using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eaven.Ven.EntityFrameworkCore.MongoDb
{
    public interface IMogoDBContext
    {
        IMongoDatabase Database
        {
            get; set;
        }
    }
}

using System;
using Couchbase;
using Couchbase.Configuration.Client;
using System.Collections.Generic;
using Couchbase.Authentication;
using Couchbase.Core;
using System.Linq;
using Newtonsoft.Json;
using Couchbase.N1QL;
using Couchbase.Lite.Storage.SystemSQLite;

namespace couch
{
    class Program
    {
        static void Main(string[] args)
        {
            var cluster = new Cluster(new ClientConfiguration
            {
                Servers = new List<Uri> { new Uri("http://127.0.0.1:8091/") }
            });

            var authenticator = new PasswordAuthenticator("nav", "5522567n!");
            cluster.Authenticate(authenticator);
            var bucket = cluster.OpenBucket("travel-sample");
            //var bucket = cluster.OpenBucket();
            string a = "new sample";
            var document = new Document<dynamic>
            {
                Id = "Hello",
                Content = new
                {
                    name = a,
                    name2="asdsad"
                }
            };
            
            var upsert = bucket.Upsert(document);
            if (upsert.Success)
            {
                var get = bucket.GetDocument<dynamic>(document.Id);
                document = get.Document;
                var msg = string.Format("{0} {1}!", document.Id, document.Content.name);
                Console.WriteLine(msg);
            }

            //var document2 = new Document<dynamic>
            //{
            //    Id = "airline_10"

            //};
            //document2.Id = "airline_10123";
            var get2 = bucket.GetDocument<dynamic>("airline_10");
            var document2 = get2.Document;
            var msg2 = string.Format("{0} {1} {2} {3}!", document2.Id, document2.Content.name, document2.Content.country, document2.Content.type);
            Console.WriteLine(msg2);
            //string a="UPDATE `travel - sample` SET name = `change` WHERE name=`Couchbase`";
            //var document2 = new Document<dynamic>
            //document2 =bucket.GetDocument(airline_10);
            //var req = new QueryRequest().Statement("UPDATE `travel - sample` SET name = `change` WHERE name=`Couchbase`");
            //if (req.)
            //{
            //    Console.WriteLine("a");
            //}
            Console.WriteLine("Sample Select");
            var query = new QueryRequest("SELECT name FROM `travel-sample` LIMIT 4");
            foreach (var row in bucket.Query<dynamic>(query))
            {
                Console.WriteLine(JsonConvert.SerializeObject(row));
            }
            
            var query2 = new QueryRequest("UPDATE `travel-sample` SET name = `change` WHERE name=`new sample`");
            if(bucket.Query<dynamic>(query2)!=null)
            {
                Console.WriteLine("Hi");
            }
            string ab = document2.ToString();
            var document3 = new Document<dynamic>
            {
                
            };
            Console.WriteLine(ab);
            Console.Read();
        }

    }

}

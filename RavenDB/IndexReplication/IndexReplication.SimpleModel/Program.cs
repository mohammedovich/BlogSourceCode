using System.Collections.Generic;
using System.Linq;
using FizzWare.NBuilder;
using IndexReplication.SimpleModel.Domain;
using Raven.Bundles.IndexReplication.Data;
using Raven.Client.Document;
using Raven.Client.Extensions;
using Raven.Client.Indexes;

namespace IndexReplication.SimpleModel
{
    class Program
    {
        static void Main()
        {
            var store = new DocumentStore
                            {
                                Url = "http://localhost:8080",
                                DefaultDatabase = "ReplicationDB"
                            }.Initialize();

            IndexCreation.CreateIndexes(typeof(CustomersListIndex).Assembly, store);

            using (var session = store.OpenSession())
            {
                var customers = DummyCustomers(30);

                foreach (var customer in customers)
                    session.Store(customer);

                session.SaveChanges();
            }

            var replication = new IndexReplicationDestination
            {
                Id = "Raven/IndexReplication/CustomersListIndex",
                ColumnsMapping =
                    {
                        {"FullName", "FullName"},
                        {"JoinAt", "JoinedAt"},

                    },
                ConnectionStringName = "SqlReplication",
                PrimaryKeyColumnName = "Id",
                TableName = "Customers"
            };


            store.DatabaseCommands.EnsureDatabaseExists("ReplicationDB");
            using (var session = store.OpenSession())
            {
                session.Store(replication);
                session.SaveChanges();
            }

        }

        public class CustomersListIndex : AbstractIndexCreationTask<Customer>
        {
            public CustomersListIndex()
            {
                Map = customers => from c in customers
                                   select new {c.FullName, c.JoinAt};
            }
        }

        public static List<Customer> DummyCustomers(int howMany)
        {
            var customers = new List<Customer>();
            for (var i = 1; i <= howMany; i++)
            {
                var customer = Builder<Customer>.CreateNew().With(x => x.Id, null).With(x => x.CustomerId, i).With(x => x.FullName, "FullName "+ i).Build();
                customers.Add(customer);
            }
            return customers;
        }
    }
}

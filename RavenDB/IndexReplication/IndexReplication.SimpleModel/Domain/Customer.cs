using System;

namespace IndexReplication.SimpleModel.Domain
{
    public class Customer
    {
        public string Id { get; set; }
        public int CustomerId { get; set; }
        public string FullName { get; set; }
        public DateTime JoinAt { get; set; }
    }
}
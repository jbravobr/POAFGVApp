using System;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace POAFGVApp
{
    public class Address : BaseEntity
    {
        public string Burgh { get; set; }
        public string Street { get; set; }
        public int Number { get; set; }
        public string Zipcode { get; set; }
    }
}
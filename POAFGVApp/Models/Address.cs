using System;
namespace POAFGVApp
{
    public class Address
    {
        public string Bourgh { get; set; }
        public string Street { get; set; }
        public int Number { get; set; }
        public string Zipcode { get; set; }

        public bool IsMainAddress { get; set; }
    }
}
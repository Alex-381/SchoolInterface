﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolInterface.Core.Models
{
    public class Address
    {
        public int AddressID { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string PostalCode { get; set; }
    }
}

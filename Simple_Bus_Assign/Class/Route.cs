using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Simple_Bus_Assign.Interfaces;

namespace Simple_Bus_Assign.Class
{
    public class Route : Routeable
    {
        public string City { get; set; }

        public int Route_Id { get; set; }

        public string Route_Name { get; set; }

        public string State { get; set; }

        public int Zip { get; set; }
    }
}
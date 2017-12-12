using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Simple_Bus_Assign.Interfaces;

namespace Simple_Bus_Assign.Class
{
    public class Driver : Driverable
    {
        public int Driver_Id { get; set; }

        public string Driver_Name { get; set; }
    }
}
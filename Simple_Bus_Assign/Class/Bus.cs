using Simple_Bus_Assign.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Simple_Bus_Assign.Class
{
    public class Bus : Busable
    {
        public int Bus_Id { get; set; }
        public int Bus_Number { get; set; }
        public int Seat_Capacity { get; set; }     
    }
}
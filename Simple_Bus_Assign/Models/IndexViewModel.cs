using Simple_Bus_Assign.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Simple_Bus_Assign.Models
{
    public class IndexViewModel : Busable, Driverable, Routeable
    {
        public string City { get; set; }
        public int Route_Id { get; set; }
        public string Route_Name { get; set; }
        public string State { get; set; }
        public int Zip { get; set; }
        public int Driver_Id { get; set; }
        public string Driver_Name { get; set; }
        public int Bus_Id { get; set; }
        public int Bus_Number { get; set; }
        public int Seat_Capacity { get; set; }
        public int Assign_Id { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
    }
}
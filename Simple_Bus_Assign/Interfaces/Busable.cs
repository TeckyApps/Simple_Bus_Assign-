using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_Bus_Assign.Interfaces
{
   public interface Busable
    {
        int Bus_Id { get; set; }
        int Bus_Number { get; set; } 
        int Seat_Capacity { get; set; }
    }
}

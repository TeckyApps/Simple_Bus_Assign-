using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_Bus_Assign.Interfaces
{
    public interface Assignable
    {
        int Assign_Id { get; set; }
        Busable Current_Bus { get; set; }

        Driverable Current_Driver { get; set; }

        Routeable Current_Route { get; set; }

        DateTime StartDateTime { get; set; }

        DateTime EndDateTime { get; set; }
    }
}

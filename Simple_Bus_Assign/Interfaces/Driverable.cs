using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_Bus_Assign.Interfaces
{
    public interface Driverable
    {
        int Driver_Id { get; set; }

        string Driver_Name { get; set; }
    }
}

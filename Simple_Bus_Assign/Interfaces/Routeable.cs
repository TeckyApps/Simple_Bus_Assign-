using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_Bus_Assign.Interfaces
{
    public interface Routeable
    {
        int Route_Id { get; set; }
        string Route_Name { get; set; }
        string City { get; set; }
        string State { get; set; }
        int Zip { get; set; }
    }
}

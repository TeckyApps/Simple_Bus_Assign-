using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Simple_Bus_Assign.Interfaces;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using Simple_Bus_Assign.Models;

namespace Simple_Bus_Assign.Class
{
    public class Assign : Assignable
    {
        public int Assign_Id { get; set; }

        public Busable Current_Bus { get; set; }

        public Driverable Current_Driver { get; set; }

        public Routeable Current_Route { get; set; }
        
        public DateTime StartDateTime { get; set; }

        public DateTime EndDateTime { get; set; }

        public IEnumerable<Assign> Read()
        {
            return GetAll();
        }
        public Assign ConvertToAssign(IndexViewModel model)
        {
            Busable bus = new Bus();
            bus.Bus_Id = model.Bus_Id;
            Routeable route = new Route();
            route.Route_Id = model.Route_Id;
            Driverable driver = new Driver();
            driver.Driver_Id = model.Driver_Id;
            return new Assign
            {
                Assign_Id = model.Assign_Id,
                Current_Bus = bus,
                Current_Route = route,
                Current_Driver = driver,
                StartDateTime = model.StartDateTime,
                EndDateTime = model.EndDateTime
            };
        }
        // Delete the current assigned record
        public void DeleteAssigned(Assign assign)
        {
            string con = ConfigurationManager.ConnectionStrings["SQL_Connection"].ConnectionString;
            using (var conn = new SqlConnection(con))
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand("delete_Assigned"))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Connection = conn;
                    command.Parameters.AddWithValue("@assign_id", assign.Assign_Id);
                    command.ExecuteNonQuery();
                }
                conn.Close();
            }
        }

        // Update the assigned details
        public void UpdateAssigned(Assign assign)
        {
            string con = ConfigurationManager.ConnectionStrings["SQL_Connection"].ConnectionString;
            using (var conn = new SqlConnection(con))
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand("update_Assigned"))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Connection = conn;
                    command.Parameters.AddWithValue("@current_assignid",assign.Assign_Id);
                    command.Parameters.AddWithValue("@current_bus",assign.Current_Bus.Bus_Id);
                    command.Parameters.AddWithValue("@current_driver",assign.Current_Driver.Driver_Id);
                    command.Parameters.AddWithValue("@current_route",assign.Current_Route.Route_Id);
                    command.Parameters.AddWithValue("@start_date",assign.StartDateTime);
                    command.Parameters.AddWithValue("@end_date",assign.EndDateTime);
                    command.ExecuteNonQuery();
                }
                conn.Close();
            }
        }

        // Insert new assigned record
        public void InsertAssigned(Assign assign)
        {
            string con = ConfigurationManager.ConnectionStrings["SQL_Connection"].ConnectionString;
            using (var conn = new SqlConnection(con))
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand("insert_Assigned"))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Connection = conn;
                    command.Parameters.AddWithValue("@current_bus", assign.Current_Bus.Bus_Id);
                    command.Parameters.AddWithValue("@current_driver", assign.Current_Driver.Driver_Id);
                    command.Parameters.AddWithValue("@current_route", assign.Current_Route.Route_Id);
                    command.Parameters.AddWithValue("@start_date", assign.StartDateTime);
                    command.Parameters.AddWithValue("@end_date", assign.EndDateTime);
                    command.ExecuteNonQuery();
                }
                conn.Close();
            }
        }
        // Gets all assigned with details
        public IList<Assign> GetAll()
        {
            Assign assign = new Assign();
            return assign.GetAssign().Select(current_assign => new Assign
            {
                Assign_Id = current_assign.Assign_Id,
                Current_Bus = GetBus_ById(current_assign.Current_Bus.Bus_Id),
                Current_Driver = GetDriver_ById(current_assign.Current_Driver.Driver_Id),
                Current_Route = GetRoute_ById(current_assign.Current_Route.Route_Id),
                StartDateTime = current_assign.StartDateTime,
                EndDateTime = current_assign.EndDateTime
            }).ToList();
        }


        // Get list of buses full
        public List<Busable> GetBusses()
        {
            List<Busable> busList = new List<Busable>();
            string con = ConfigurationManager.ConnectionStrings["SQL_Connection"].ConnectionString;
            using (var conn = new SqlConnection(con))
            {
                conn.Open(); // open the sql connection
                // Get the data from the database using a stored proc
                using (SqlCommand command = new SqlCommand("getBuses_Full"))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Connection = conn;
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Bus bus = new Bus();
                            bus.Bus_Id = Convert.ToInt32(reader["Bus_Id"]);
                            bus.Bus_Number = Convert.ToInt32(reader["Bus_Number"]);
                            bus.Seat_Capacity = Convert.ToInt32(reader["Seat_Capacity"]);
                            busList.Add(bus);
                        }
                    }
                }
            }
            return busList;
        }
        // Get Bus by ID
        public Busable GetBus_ById(int id)
        {
            Busable bus = new Bus();
            string con = ConfigurationManager.ConnectionStrings["SQL_Connection"].ConnectionString;
            using (var conn = new SqlConnection(con))
            {
                conn.Open(); // open the sql connection
                // Get the data from the database using a stored proc
                using (SqlCommand command = new SqlCommand("getBuses_ById"))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Connection = conn;
                    command.Parameters.AddWithValue("@id",id);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            bus.Bus_Id = Convert.ToInt32(reader["Bus_Id"]);
                            bus.Bus_Number = Convert.ToInt32(reader["Bus_Number"]);
                            bus.Seat_Capacity = Convert.ToInt32(reader["Seat_Capacity"]);
                        }
                    }
                }
            }
            return bus;
        }
        // Get Driver By Id
        public Driverable GetDriver_ById(int id)
        {
            Driverable driver = new Driver();
            string con = ConfigurationManager.ConnectionStrings["SQL_Connection"].ConnectionString;
            using (var conn = new SqlConnection(con))
            {
                conn.Open(); // open the sql connection
                // Get the data from the database using a stored proc
                using (SqlCommand command = new SqlCommand("getDrivers_ById"))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Connection = conn;
                    command.Parameters.AddWithValue("@id", id);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            driver.Driver_Id = Convert.ToInt32(reader["Driver_Id"]);
                            driver.Driver_Name = reader["Driver_Name"].ToString();
                        }
                    }
                }
            }
            return driver;
        }
        // Get list of drivers full
        public List<Driverable> GetDrivers()
        {
            List<Driverable> driverList = new List<Driverable>();
            string con = ConfigurationManager.ConnectionStrings["SQL_Connection"].ConnectionString;
            using (var conn = new SqlConnection(con))
            {
                conn.Open(); // open the sql connection
                // Get the data from the database using a stored proc
                using (SqlCommand command = new SqlCommand("getDrivers_Full"))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Connection = conn;
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Driverable driver = new Driver();
                            driver.Driver_Id = Convert.ToInt32(reader["Driver_Id"]);
                            driver.Driver_Name = reader["Driver_Name"].ToString();
                            driverList.Add(driver);
                        }
                    }
                }
            }
            return driverList;
        }
        // Get Route By Id
        public Routeable GetRoute_ById(int id)
        {
            Routeable route = new Route();
            string con = ConfigurationManager.ConnectionStrings["SQL_Connection"].ConnectionString;
            using (var conn = new SqlConnection(con))
            {
                conn.Open(); // open the sql connection
                // Get the data from the database using a stored proc
                using (SqlCommand command = new SqlCommand("getRoutes_ById"))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Connection = conn;
                    command.Parameters.AddWithValue("@id", id);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            route.Route_Id = Convert.ToInt32(reader["Route_Id"]);
                            route.Route_Name = reader["Route_Name"].ToString();
                            route.City = reader["City"].ToString();
                            route.State = reader["State"].ToString();
                            route.Zip = Convert.ToInt32(reader["Zip"]);
                        }
                    }
                }
            }
            return route;
        }
        // Get list of routes full
        public List<Routeable> GetRoutes()
        {
            List<Routeable> routeList = new List<Routeable>();
            string con = ConfigurationManager.ConnectionStrings["SQL_Connection"].ConnectionString;
            using (var conn = new SqlConnection(con))
            {
                conn.Open(); // open the sql connection
                // Get the data from the database using a stored proc
                using (SqlCommand command = new SqlCommand("getRoutes_Full"))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Connection = conn;
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Routeable route = new Route();
                            route.Route_Id = Convert.ToInt32(reader["Route_Id"]);
                            route.Route_Name = reader["Route_Name"].ToString();
                            route.City = reader["City"].ToString();
                            route.State = reader["State"].ToString();
                            route.Zip = Convert.ToInt32(reader["Zip"]);
                            routeList.Add(route);
                        }
                    }
                }
            }
            return routeList;
        }
        // Get Assigned Full
        public List<Assignable> GetAssign()
        {
            List<Assignable> assignList = new List<Assignable>();
            string con = ConfigurationManager.ConnectionStrings["SQL_Connection"].ConnectionString;
            using (var conn = new SqlConnection(con))
            {
                conn.Open(); // open the sql connection
                // Get the data from the database using a stored proc
                using (SqlCommand command = new SqlCommand("getAssigned_Full"))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Connection = conn;
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Assignable assign = new Assign();
                            assign.Assign_Id = Convert.ToInt32(reader["Assign_Id"]);
                            Busable bus = new Bus();
                            bus.Bus_Id = Convert.ToInt32(reader["Current_Bus"]);
                            assign.Current_Bus = bus;
                            Routeable route = new Route();
                            route.Route_Id = Convert.ToInt32(reader["Current_Route"]);
                            assign.Current_Route = route;
                            Driverable driver = new Driver();
                            driver.Driver_Id = Convert.ToInt32(reader["Current_Driver"]);
                            assign.Current_Driver = driver;
                            assign.StartDateTime = Convert.ToDateTime(reader["StartDateTime"]);
                            assign.EndDateTime = Convert.ToDateTime(reader["EndDateTime"]);
                            assignList.Add(assign);
                        }
                    }
                }
            }
            return assignList;
        }
       public Assignable GetAssign_ById(int id)
        {
            Assignable assign = new Assign();
            string con = ConfigurationManager.ConnectionStrings["SQL_Connection"].ConnectionString;
            using (var conn = new SqlConnection(con))
            {
                conn.Open(); // open the sql connection
                // Get the data from the database using a stored proc
                using (SqlCommand command = new SqlCommand("getAssigned_ById"))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Connection = conn;
                    command.Parameters.AddWithValue("@id", id);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            assign.Assign_Id = Convert.ToInt32(reader["Assign_Id"]);
                            assign.Current_Driver.Driver_Id = Convert.ToInt32(reader["Current_Bus"]);
                            assign.Current_Bus.Bus_Id = Convert.ToInt32(reader["Current_Driver"]);
                            assign.Current_Route.Route_Id = Convert.ToInt32(reader["Current_Route"]);
                            assign.StartDateTime = Convert.ToDateTime(reader["StartDateTime"]);
                            assign.EndDateTime = Convert.ToDateTime(reader["EndDateTime"]);
                        }
                    }
                }
            }
            return assign;
        }
    }
}
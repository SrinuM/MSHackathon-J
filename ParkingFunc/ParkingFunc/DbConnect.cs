using ParkingFunc.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingFunc
{
    public class DbConnect
    {

        public BuildingResponse GetBuildingDetails(int Id)
        {
            BuildingResponse response = new BuildingResponse();
            SqlConnectionStringBuilder sConnB = new SqlConnectionStringBuilder()
            { 
                DataSource = "parkingapp01.database.windows.net",
                InitialCatalog = "ParkingApp",
                UserID = "sqladmin",
                Password = "Admin@123",
                TrustServerCertificate= false,
                ConnectTimeout= 90,
                Encrypt = true
            };

            response.IsSuccess = true;
            SqlConnection conn = new SqlConnection(sConnB.ConnectionString);
            List<Floor> floors = new List<Floor>();

            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    conn.Open();
                    cmd.CommandText = "spGetBuildingData";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = Id;
                    SqlDataReader reader= cmd.ExecuteReader();                    

                    while (reader.Read())
                    {
                        Floor bld = new Floor()
                        {
                            FloorId = (int)reader["Floorid"],
                            FloorName = (string)reader["floorname"],                           
                            Slots = (int)reader["Slotcount"]
                        };

                        floors.Add(bld);
                    }

                    response.Floors = floors;
                }
            }
            catch(Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessage = $"Failed with error {ex.Message}";
            }          

            return response;
        }

        public SlotResponse GetAvailableSlots(int buildId, int floorId)
        {
            SlotResponse response = new SlotResponse();
            SqlConnectionStringBuilder sConnB = new SqlConnectionStringBuilder()
            {
                DataSource = "parkingapp01.database.windows.net",
                InitialCatalog = "ParkingApp",
                UserID = "sqladmin",
                Password = "Admin@123",
                TrustServerCertificate = false,
                ConnectTimeout = 90,
                Encrypt=true
            };

            response.IsSuccess = true;
            SqlConnection conn = new SqlConnection(sConnB.ConnectionString);
            List<Slot> slots = new List<Slot>();

            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    conn.Open();
                    cmd.CommandText = "spGetSlotinformation";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@buildingid", SqlDbType.VarChar).Value = buildId;
                    cmd.Parameters.Add("@floorid", SqlDbType.VarChar).Value = buildId;
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Slot slot = new Slot()
                        {
                            SlotId = (int)reader["slotid"],
                            SlotName = (string)reader["slotname"],
                            Status = (int)reader["status"],                          

                        };

                        slots.Add(slot);
                    }

                    response.Slots = slots;
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessage = $"Failed with error {ex.Message}";
            }

            return response;
        }
    }
}

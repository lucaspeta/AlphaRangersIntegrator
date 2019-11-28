using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace AlphaRangersIntegrator
{
    class Flags
    {
        public int ID { get; set; }
        public bool Flag_Verde { get; set; }
        public bool Flag_Amarela { get; set; }
        public bool Flag_Vermelha { get; set; }
        public bool Flag_Desligar { get; set; }
        public bool isThereAnyFlag { get; set; }

        public Flags()
        {
        }

        public void Print()
        {
            Console.Write("F Verde -> "+Flag_Verde+", F Amarela -> "+Flag_Amarela);
            Console.Write(", F Vermelha -> " + Flag_Vermelha + ", F Desligar -> " + Flag_Desligar);
            Console.WriteLine();
        }

        public void ReadFlagsFromDB()
        {
            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = "Server=DESKTOP-N2VC4JG;Database=AlphaRangers;Trusted_Connection=true";
                connection.Open();

                string Sql_query = "SELECT TOP (1) [ID]"+
                                  ",[Green] "+
                                  ",[Yellow] "+
                                  ",[Red] "+
                                  ",[Shutdown] "+
                                  ",[WasReaded] "+
                                  ",[CreatedDate] "+
                              "FROM[AlphaRangers].[dbo].[Flags] WHERE WasReaded = 0 ORDER BY ID DESC";

                SqlCommand command = new SqlCommand(Sql_query, connection);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        ID = reader.GetInt32(reader.GetOrdinal("ID"));
                        this.Flag_Verde = reader.GetBoolean(reader.GetOrdinal("Green"));
                        this.Flag_Amarela = reader.GetBoolean(reader.GetOrdinal("Yellow"));
                        this.Flag_Vermelha = reader.GetBoolean(reader.GetOrdinal("Red"));
                        this.Flag_Desligar = reader.GetBoolean(reader.GetOrdinal("Shutdown"));
                        this.isThereAnyFlag = true;
                    }
                    else                    
                        isThereAnyFlag = false;                                        
                }                                  
            }
        }

        public void CheckAsRead()
        {
            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = "Server=DESKTOP-N2VC4JG;Database=AlphaRangers;Trusted_Connection=true";
                connection.Open();

                string sql_query = "UPDATE Flags SET WasReaded = @param1 WHERE Id = @param2";

                using (SqlCommand cmd = new SqlCommand(sql_query, connection))
                {
                    cmd.Parameters.Add("@param1", SqlDbType.Bit).Value = 1;
                    cmd.Parameters.Add("@param2", SqlDbType.Int).Value = this.ID;

                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}

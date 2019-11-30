using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace AlphaRangersIntegrator
{
    class Baja
    {
        public int Velocidade { get; set; }
        public int Temperatura { get; set; }
        public int FreioQTD { get; set; }
        public int VoltasQTD { get; set; }
        public int Tensao { get; set; }

        public Baja()
        {
        }

        public void print()
        {
            Console.WriteLine(string.Format("{0}, {1}, {2}, {3}, {4}", this.FreioQTD, this.Temperatura, this.Tensao, this.Velocidade, this.VoltasQTD));
        }

        public void InsertData()
        {
            using (SqlConnection connection = new SqlConnection())
            {
               connection.ConnectionString = "Server=DESKTOP-N2VC4JG;Database=AlphaRangers;Trusted_Connection=true";
               connection.Open();
               
                string sql = "INSERT INTO Baja(Velocidade,FreioQtd,VoltasQtd,Temperatura,Bateria,CreatedDate) VALUES(@param1,@param2,@param3,@param4,@param5,@param6)";
                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    cmd.Parameters.Add("@param1", SqlDbType.Int).Value = this.Velocidade;
                    cmd.Parameters.Add("@param2", SqlDbType.Int).Value = this.FreioQTD;
                    cmd.Parameters.Add("@param3", SqlDbType.Int).Value = this.VoltasQTD;
                    cmd.Parameters.Add("@param4", SqlDbType.Int).Value = this.Temperatura;
                    cmd.Parameters.Add("@param5", SqlDbType.Int).Value = this.Tensao;
                    cmd.Parameters.Add("@param6", SqlDbType.DateTime).Value = DateTime.Now;
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}

using Microsoft.Data.SqlClient;
using System.Data.SqlTypes;

namespace Dankwms
{
    public class WmsSQL
    {
        public static SqlConnection Connect()
        {
            SqlConnectionStringBuilder sqlStringBuilder = new();
            sqlStringBuilder.DataSource = "dank-sql.dank.net\\DANKSQL";
            sqlStringBuilder.InitialCatalog = "danknet";
            sqlStringBuilder.IntegratedSecurity = false;
            sqlStringBuilder.TrustServerCertificate = true;
            sqlStringBuilder.UserID = "blazor";
            sqlStringBuilder.Password = "bl4z0rPassword2024_serverSQL";

            SqlConnection connection = new(sqlStringBuilder.ConnectionString);
            connection.Open();
            return connection;
        }

        public static List<WmsEntry> Command(string cmd)
        {
            List<WmsEntry> entries = new();

            SqlConnection connection = Connect();
            SqlCommand sqlCmd = new(cmd, connection);
            SqlDataReader reader = sqlCmd.ExecuteReader();

            while (reader.Read())
            {
                WmsEntry wmsEntry = new(
                    Convert.ToString(reader["ID"]),
                    Convert.ToString(reader["Name"]),
                    Convert.ToString(reader["Description"]),
                    Convert.ToString(reader["Location"]),
                    Convert.ToString(reader["Picture"]),
                    Convert.ToString(reader["Keywords"])
                );

                entries.Add(wmsEntry);
            }

            WmsSQL.Disconnect(connection);
            return entries;
        }

        public static void Disconnect(SqlConnection Connection)
        {
            Connection.Close();
        }

        public static string GetImageBase64(string filePath) {
            byte[] imageArray = System.IO.File.ReadAllBytes(filePath);
            string base64 = Convert.ToBase64String(imageArray);
            return ($"data:image/jpg;base64, {base64}");
        }
    }
}

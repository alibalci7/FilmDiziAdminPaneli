using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace FilmDiziUygulamasi.Controllers
{
    public class Tools
    {
        private static SqlConnection baglanti;

        public static SqlConnection Baglanti
        {
            get
            {
                if (baglanti == null)
                {
                    //baglanti = new SqlConnection(ConfigurationManager.ConnectionStrings["Baglanti"].ConnectionString);
                    baglanti = new SqlConnection(ConfigurationManager.ConnectionStrings["BaglantiLocal"].ConnectionString);
                }
                return baglanti;
            }
        }

        public static bool Exec(SqlCommand cmd)
        {
            try
            {
                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }
                int etk = cmd.ExecuteNonQuery();
                return etk > 0 ? true : false;
            }
            catch
            {
                return false;
            }
            finally
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                {
                    cmd.Connection.Close();
                }
            }
        }

    }
}
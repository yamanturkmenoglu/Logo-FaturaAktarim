
using System.Configuration;
using System.Data.SqlClient;

namespace Tesla_CanToptan
{
   
        class SqlConnectionClass
        {
            public SqlConnection baglanti()
            {
                string connectionString = ConfigurationManager.ConnectionStrings["SqlServerConnection"].ConnectionString;
                SqlConnection baglan = new SqlConnection(connectionString);
                baglan.Open();
                return baglan;
            }
        }
}

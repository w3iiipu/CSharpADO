using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace HelloNotifyAfter
{
    public partial class About : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string cs = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;
            using (SqlConnection sourcecon = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("Select * from Products_Source", sourcecon);
                sourcecon.Open();
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    using (SqlConnection destinationcon = new SqlConnection(cs))
                    {
                        using (SqlBulkCopy bc = new SqlBulkCopy(destinationcon))
                        {
                            bc.BatchSize = 1000;
                            bc.NotifyAfter = 5000;
                            bc.SqlRowsCopied += new SqlRowsCopiedEventHandler(bc_SqlRowsCopied);
                            bc.DestinationTableName = "Products_Destination";
                            destinationcon.Open();
                            bc.WriteToServer(rdr);
                        }
                    }
                }
            }
        }

        static void bc_SqlRowsCopied(Object sender, SqlRowsCopiedEventArgs e)
        {
            Console.WriteLine(e.RowsCopied + "loaded....");
        }

    }
}
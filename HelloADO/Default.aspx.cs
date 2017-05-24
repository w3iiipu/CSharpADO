using System;
using System.Data.SqlClient;
using System.Web.UI;
using System.Configuration;

namespace HelloADO
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Create the connection object with SQL Server AUthentication
            //string ConnectionString = "data source=.; database=SampleDB; user id=MyUserName; password=MyPassword";

            //Create the connection object with windows credential
            SqlConnection con = new SqlConnection("data source=.; database=SandBox; integrated security=SSPI");

            //ALTERNATIVE
            //string cs = "data source=.; database=Sample_Test_DB; integrated security=SSPI";
            //SqlConnection con = new SqlConnection(cs);
            try
            {
                // Pass the connection to the command object, so the command object knows on which connection to execute the command
                SqlCommand cmd = new SqlCommand("Select * from Departments", con);

                // Open the connection. Otherwise you get a runtime error. An open connection is required to execute the command
                // Open connection as late as possible and inside a try catch block
                con.Open();
                GridView1.DataSource = cmd.ExecuteReader();
                GridView1.DataBind();
            }
            catch (Exception ex)
            {
                // Handle Exceptions, if any
            }
            finally
            {
                // The finally block is guarenteed to execute even if there is an exception. 
                //  This ensures connections are always properly closed.
                con.Close();
            }
        }

        protected void Page_Load1(object sender, EventArgs e)
        {
            //connection strings can be stored in configuration file
            //string cs = "data source =.; database = SandBox; integrated security = SSPI";

            string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;

            //using statement - the connection object will automatically close the connection after the finishes
            using (SqlConnection connection = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("Select * from Departments", connection);
                connection.Open();
                GridView1.DataSource = cmd.ExecuteReader();
                GridView1.DataBind();
            }

        }
    }
}
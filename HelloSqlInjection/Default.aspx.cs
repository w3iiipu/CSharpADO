using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI;

namespace HelloSqlInjection
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            ///
            ///Using Parameterized Query
            ///
            string CS = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                // Parameterized query. @ProductName is the parameter
                string Command = "Select * from Departments where Manager like @Manager";
                SqlCommand cmd = new SqlCommand(Command, con);
                // Provide the value for the parameter
                cmd.Parameters.AddWithValue("@Manager", TextBox1.Text + "%");
                con.Open();
                GridView1.DataSource = cmd.ExecuteReader();
                GridView1.DataBind();
            }

            ///
            ///Using Stored Procedure
            ///
            string CS = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                // The command, that we want to execute is a stored procedure,
                // so specify the name of the procedure as cmdText
                SqlCommand cmd = new SqlCommand("spGetManger", con);
                // Specify that the T-SQL command is a stored procedure
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                // Associate the parameter and it's value with the command object
                cmd.Parameters.AddWithValue("@Manager", TextBox1.Text + "%");
                con.Open();
                GridView1.DataSource = cmd.ExecuteReader();
                GridView1.DataBind();
            }
        }
    }
}
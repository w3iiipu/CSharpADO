using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI;

namespace HelloSqlCommand
{
    public partial class _Default : Page
    {
        /// 
        /// ExecuteReader() method returns multiple rows of data
        /// 

        protected void Page_Load(object sender, EventArgs e)
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection connection = new SqlConnection("data source=.; database=SandBox; integrated security=SSPI"))
            {
                //Create an instance of SqlCommand class, specifying the T-SQL command that 
                //we want to execute, and the connection object.
                SqlCommand cmd = new SqlCommand("Select DepartmentName, Manager from Departments", connection);
                connection.Open();
                //As the T-SQL statement that we want to execute return multiple rows of data, 
                //use ExecuteReader() method of the command object.
                GridView1.DataSource = cmd.ExecuteReader();
                GridView1.DataBind();
            }


            ///Can also create instance of SqlCommand class using the parameter less constructor
            ///and then later specify the command text and connection
            ///using the CommandText and Connection properties of the SqlCommand object 
       
            string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection connection = new SqlConnection("data source=.; database=SandBox; integrated security=SSPI"))
            {
                //Create an instance of SqlCommand class using the parameter less constructor
                SqlCommand cmd = new SqlCommand();
                //Specify the command, we want to execute using the CommandText property
                cmd.CommandText = "Select DepartmentName, Manager from Departments";
                //Specify the connection, on which we want to execute the command 
                //using the Connection property
                cmd.Connection = connection;
                connection.Open();
                //As the T-SQL statement that we want to execute return multiple rows of data, 
                //use ExecuteReader() method of the command object.
                GridView1.DataSource = cmd.ExecuteReader();
                GridView1.DataBind();
            }
        }

        ///
        ///use ExecuteScalar() method if the T-SQL statement returns a single value.
        ///

        protected void Page_Load1(object sender, EventArgs e)
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection connection = new SqlConnection("data source=.; database=SandBox; integrated security=SSPI"))
            {
                //Create an instance of SqlCommand class, specifying the T-SQL command 
                //that we want to execute, and the connection object.
                SqlCommand cmd = new SqlCommand("Select Count(DepartmentID) from Departments", connection);
                connection.Open();
                //As the T-SQL statement that we want to execute return a single value, 
                //use ExecuteScalar() method of the command object.
                //Since the return type of ExecuteScalar() is object, we are type casting to int datatype
                int TotalRows = (int)cmd.ExecuteScalar();
                Response.Write("Total Rows = " + TotalRows.ToString());
            }
        }


        ///
        ///Insert, Update and Delete operations on a SQL server database using the ExecuteNonQuery() method of the SqlCommand object.
        ///

        protected void Page_Load2(object sender, EventArgs e)
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection connection = new SqlConnection("data source=.; database=SandBox; integrated security=SSPI"))
            {
                //Create an instance of SqlCommand class, specifying the T-SQL command 
                //that we want to execute, and the connection object.
                SqlCommand cmd = new SqlCommand("insert into Departments values ('HR', Missy)", connection);
                connection.Open();
                //Since we are performing an insert operation, use ExecuteNonQuery() 
                //method of the command object. ExecuteNonQuery() method returns an 
                //integer, which specifies the number of rows inserted
                int rowsAffected = cmd.ExecuteNonQuery();
                Response.Write("Inserted Rows = " + rowsAffected.ToString() + "<br/>");

                //Set to CommandText to the update query. We are reusing the command object, 
                //instead of creating a new command object
                cmd.CommandText = "update Departments set Manager = 'boss' where Id = 1";
                //use ExecuteNonQuery() method to execute the update statement on the database
                rowsAffected = cmd.ExecuteNonQuery();
                Response.Write("Updated Rows = " + rowsAffected.ToString() + "<br/>");

                //Set to CommandText to the delete query. We are reusing the command object, 
                //instead of creating a new command object
                cmd.CommandText = "Delete from Departments where Id = 4";
                //use ExecuteNonQuery() method to delete the row from the database
                rowsAffected = cmd.ExecuteNonQuery();
                Response.Write("Deleted Rows = " + rowsAffected.ToString() + "<br/>");
            }
        }

    }
}
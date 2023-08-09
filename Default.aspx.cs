using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;

namespace BloodDonor
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {

            string userId = TextBox1.Text;
            string password = TextBox2.Text;

            string connString = "host = localhost; port = 3306; database = friends_users_db; username=root; password = ashu9839";
            MySqlConnection conn = new MySqlConnection(connString);

            //open the connection
            conn.Open();

            //pass the query
            string query = $"select * from Users where UserId = '{userId}' and PasswordHash = '{password}';";


            //create mySql command object to execute command
            MySqlCommand cmd = new MySqlCommand(query, conn);

            MySqlDataReader reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                Response.Write("<script> alert('Login Successful!!');</script>");
            }
            else
            {
                Response.Write("<script> alert('Invalid User Id or Password');</script>");
            }

            //close connection
            conn.Close();
        }
    }
}
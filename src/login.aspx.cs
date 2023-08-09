using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Friends2support
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            String userId = TextBox1.Text;
            String password = TextBox2.Text;

            string connString = "host = localhost; database = friends_users_db; username=root; password = mysql";
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
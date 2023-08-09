using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Friends2support
{
    public partial class donorRegistration : System.Web.UI.Page
    {

        private List<string> bloodTypes = new List<string>
        {
            "A+", "A-", "B+", "B-", "AB+", "AB-", "O+", "O-"
        };

        private Dictionary<string, Dictionary<string, List<string>>> indianCities = new Dictionary<string, Dictionary<string, List<string>>>
        {
            {
                "Andhra Pradesh", new Dictionary<string, List<string>>
                {
                    { "Anantapur", new List<string> { "Anantapur City", "Dharmavaram", "Hindupur" } },
                    { "Chittoor", new List<string> { "Chittoor", "Tirupati", "Madanapalle" } },
                    // ... Add more districts and cities as needed
                }
            },
            {
                "Arunachal Pradesh", new Dictionary<string, List<string>>
                {
                    { "Tawang", new List<string> { "Tawang Town", "Bomdila" } },
                    { "West Kameng", new List<string> { "Bhalukpong", "Bomdila" } },
                    // ... Add more districts and cities as needed
                }
            },
            // ... Add more states and districts as needed
        };

        private List<string> availableTypes = new List<string>
        {
            "Available", "Unavailable"
        };

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {              
                DropDownList1.DataSource = bloodTypes;
                DropDownList1.DataBind();

                DropDownList2.DataSource = indianCities.Keys;
                DropDownList2.DataBind();

                DropDownList5.DataSource = availableTypes;
                DropDownList5.DataBind();
            }
        }

        protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedState = DropDownList2.SelectedValue;
            if (indianCities.ContainsKey(selectedState))
            {
                DropDownList3.DataSource = indianCities[selectedState].Keys;
                DropDownList3.DataBind();
            }
        }

        protected void DropDownList3_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedState = DropDownList2.SelectedValue;
            string selectedDistrict = DropDownList3.SelectedValue;

            if (indianCities.ContainsKey(selectedState) && indianCities[selectedState].ContainsKey(selectedDistrict))
            {
                DropDownList4.DataSource = indianCities[selectedState][selectedDistrict];
                DropDownList4.DataBind();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            String fullname = TextBox1.Text;
            String mobileNumber = TextBox2.Text;
            string bloodGroup = DropDownList1.SelectedValue;
            string state = DropDownList2.SelectedValue;
            string district = DropDownList3.SelectedValue;
            string city = DropDownList4.SelectedValue;
            string availabilityValue = DropDownList5.SelectedValue;
            bool availability = (availabilityValue == "Available") ? true : false;

            String emailID = TextBox3.Text;
            String userID = TextBox4.Text;
            String password = TextBox5.Text;
            String reTypePassword = TextBox6.Text;

            if(password != reTypePassword)
            {
                Response.Write("<script> alert('Passwords do not match');</script>");
            }

            string connectionString = "host=localhost;username=root;password=mysql;database=friends_users_db";
            MySqlConnection conn = new MySqlConnection(connectionString);

            conn.Open();
            String query = $"insert into Users values ('{userID}', '{fullname}', {mobileNumber}, '{bloodGroup}'," +
                $" '{state}', '{district}', '{city}', {availability}, '{emailID}', '{password}');";

            MySqlCommand cmd = new MySqlCommand(query, conn);

            cmd.ExecuteNonQuery();

            Response.Write("<script> alert('Friend details saved');</script>");
        }
    }
}
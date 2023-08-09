using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;

namespace Mini_project
{
    public partial class Default : System.Web.UI.Page
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
                    DropDownList2.DataSource = indianCities[selectedState].Keys;
                    DropDownList2.DataBind();
                }
            }

            protected void DropDownList3_SelectedIndexChanged(object sender, EventArgs e)
            {
                string selectedState = DropDownList2.SelectedValue;
                string selectedDistrict = DropDownList2.SelectedValue;

                if (indianCities.ContainsKey(selectedState) && indianCities[selectedState].ContainsKey(selectedDistrict))
                {
                    DropDownList3.DataSource = indianCities[selectedState][selectedDistrict];
                    DropDownList3.DataBind();
                }
            }


            protected void Button1_Click(object sender, EventArgs e)
        {
            string blood_group = Convert.ToString(DropDownList1.Text);
            string country = Convert.ToString(TextBox2.Text);
            string state = Convert.ToString(TextBox3.Text);
            string district = Convert.ToString(TextBox4.Text);
            string city = Convert.ToString(TextBox5.Text);

            MySqlConnection conn = new MySqlConnection("host=localhost;username=root;password=Satyam@123;database=");

            conn.Open();

            string query = $"search product values ({blood_group}, '{country}', {state}, '{district}', '{city}');";

            MySqlCommand cmd = new MySqlCommand(query, conn);

            MySqlDataReader reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {

            }


            /*Response.Write("<script> alert('Blood donar found');</script>");
*/
        }
    }
}
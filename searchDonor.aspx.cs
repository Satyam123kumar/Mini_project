using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Friends2support
{
    public partial class searchDonor : System.Web.UI.Page
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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DropDownList1.DataSource = bloodTypes;
                DropDownList1.DataBind();

                DropDownList2.DataSource = indianCities.Keys;
                DropDownList2.DataBind();
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
            string bloodGroup = DropDownList1.SelectedValue;
            string state = DropDownList2.SelectedValue;
            string district = DropDownList3.SelectedValue;
            string city = DropDownList4.SelectedValue;

            MySqlConnection conn = new MySqlConnection("host=localhost;username=root;password=mysql;database=friends_users_db");

            conn.Open();
            String query = $@"
                SELECT 
                    FullName, 
                    MobileNumber, 
                    CASE 
                        WHEN Availability = 0 THEN 'Unavailable' 
                        WHEN Availability = 1 THEN 'Available'
                    END AS Availability
                FROM 
                    Users 
                WHERE 
                    BloodGroup = '{bloodGroup}' 
                    AND State = '{state}' 
                    AND District = '{district}' 
                    AND City = '{city}'
            ";

            MySqlCommand cmd = new MySqlCommand(query, conn);
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            GridView1.DataSource = dt;
            GridView1.DataBind();

            conn.Close();
        }
    }
}
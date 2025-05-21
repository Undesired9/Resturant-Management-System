using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace RestaurantManagement
{
    public partial class Tables : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadTables();
            }
        }

        private void LoadTables()
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["RestaurantDB"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Tables", conn))
                {
                    conn.Open();
                    gvTables.DataSource = cmd.ExecuteReader();
                    gvTables.DataBind();
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["RestaurantDB"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("INSERT INTO Tables (TableNumber, Capacity) VALUES (@TableNumber, @Capacity)", conn))
                {
                    cmd.Parameters.AddWithValue("@TableNumber", txtTableNumber.Text);
                    cmd.Parameters.AddWithValue("@Capacity", int.Parse(txtCapacity.Text));
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            LoadTables();
            txtTableNumber.Text = "";
            txtCapacity.Text = "";
        }

        protected void gvTables_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DeleteTable")
            {
                int id = Convert.ToInt32(e.CommandArgument);
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["RestaurantDB"].ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("DELETE FROM Tables WHERE ID = @ID", conn))
                    {
                        cmd.Parameters.AddWithValue("@ID", id);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                LoadTables();
            }
            else if (e.CommandName == "EditTable")
            {
                // Handle edit logic here
            }
        }
    }
} 
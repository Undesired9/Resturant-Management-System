using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace RestaurantManagement
{
    public partial class Staff : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadStaff();
            }
        }

        private void LoadStaff()
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["RestaurantDB"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Staff", conn))
                {
                    conn.Open();
                    gvStaff.DataSource = cmd.ExecuteReader();
                    gvStaff.DataBind();
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["RestaurantDB"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("INSERT INTO Staff (Name, Role) VALUES (@Name, @Role)", conn))
                {
                    cmd.Parameters.AddWithValue("@Name", txtName.Text);
                    cmd.Parameters.AddWithValue("@Role", txtRole.Text);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            LoadStaff();
            txtName.Text = "";
            txtRole.Text = "";
        }

        protected void gvStaff_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DeleteStaff")
            {
                int id = Convert.ToInt32(e.CommandArgument);
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["RestaurantDB"].ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("DELETE FROM Staff WHERE ID = @ID", conn))
                    {
                        cmd.Parameters.AddWithValue("@ID", id);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                LoadStaff();
            }
            else if (e.CommandName == "EditStaff")
            {
                // Handle edit logic here
            }
        }
    }
} 
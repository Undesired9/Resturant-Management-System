using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI;
namespace RestaurantManagement
{
    public partial class MenuItems : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadMenuItems();
            }
        }

        private void LoadMenuItems()
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["RestaurantDB"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM MenuItems", conn))
                {
                    conn.Open();
                    gvMenuItems.DataSource = cmd.ExecuteReader();
                    gvMenuItems.DataBind();
                }
            }
        }

        private void ShowToast(string message, string type = "success")
        {
            toast.InnerText = message;
            toast.Attributes["class"] = "toast show " + type;
            ScriptManager.RegisterStartupScript(this, GetType(), "showToast", "setTimeout(function(){ document.getElementById('toast').classList.remove('show'); }, 3000);", true);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["RestaurantDB"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("INSERT INTO MenuItems (Name, Price) VALUES (@Name, @Price)", conn))
                {
                    cmd.Parameters.AddWithValue("@Name", txtName.Text);
                    cmd.Parameters.AddWithValue("@Price", decimal.Parse(txtPrice.Text));
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            LoadMenuItems();
            txtName.Text = "";
            txtPrice.Text = "";
            ShowToast("Menu item saved successfully!", "success");
        }

        protected void gvMenuItems_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DeleteItem")
            {
                int id = Convert.ToInt32(e.CommandArgument);
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["RestaurantDB"].ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("DELETE FROM MenuItems WHERE ID = @ID", conn))
                    {
                        cmd.Parameters.AddWithValue("@ID", id);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                LoadMenuItems();
                ShowToast("Menu item deleted successfully!", "success");
            }
            else if (e.CommandName == "EditItem")
            {
                // Handle edit logic here
                ShowToast("Menu item loaded for editing.", "success");
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            txtName.Text = "";
            txtPrice.Text = "";
            lblFormTitle.Text = "Add/Edit Menu Item";
        }
    }
} 
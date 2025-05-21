using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Linq;

namespace RestaurantManagement
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadDashboardData();
                LoadRecentOrders();
            }
        }

        private void LoadDashboardData()
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["RestaurantDB"].ConnectionString))
            {
                conn.Open();

                // Load Today's Orders
                using (SqlCommand cmd = new SqlCommand(@"
                    SELECT COUNT(*) as TotalOrders, 
                           SUM(TotalAmount) as TotalRevenue 
                    FROM Orders 
                    WHERE CAST(OrderDate AS DATE) = CAST(GETDATE() AS DATE)", conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            lblTotalOrders.Text = reader["TotalOrders"].ToString();
                            lblTotalRevenue.Text = reader["TotalRevenue"] != DBNull.Value ? 
                                Convert.ToDecimal(reader["TotalRevenue"]).ToString("N2") : "0.00";
                        }
                    }
                }

                // Load Menu Items Stats
                using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM MenuItems", conn))
                {
                    lblTotalItems.Text = cmd.ExecuteScalar().ToString();
                }

                // Load Most Popular Item
                using (SqlCommand cmd = new SqlCommand(@"
                    SELECT TOP 1 m.Name 
                    FROM MenuItems m 
                    INNER JOIN OrderItems oi ON m.ID = oi.MenuItemID 
                    GROUP BY m.Name 
                    ORDER BY COUNT(*) DESC", conn))
                {
                    object result = cmd.ExecuteScalar();
                    lblPopularItem.Text = result != null ? result.ToString() : "-";
                }

                // Load Table Stats
                using (SqlCommand cmd = new SqlCommand(@"
                    SELECT 
                        COUNT(CASE WHEN Status = 'Available' THEN 1 END) as Available,
                        COUNT(CASE WHEN Status = 'Occupied' THEN 1 END) as Occupied
                    FROM Tables", conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            lblAvailableTables.Text = reader["Available"].ToString();
                            lblOccupiedTables.Text = reader["Occupied"].ToString();
                        }
                    }
                }
            }
        }

        private void LoadRecentOrders()
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["RestaurantDB"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(@"
                    SELECT TOP 10 ID, OrderDate, TotalAmount, Status 
                    FROM Orders 
                    ORDER BY OrderDate DESC", conn))
                {
                    conn.Open();
                    gvRecentOrders.DataSource = cmd.ExecuteReader();
                    gvRecentOrders.DataBind();
                }
            }
        }

        protected void btnNewOrder_Click(object sender, EventArgs e)
        {
            Response.Redirect("Orders.aspx?action=new");
        }

        protected void btnAddMenuItem_Click(object sender, EventArgs e)
        {
            Response.Redirect("MenuItems.aspx?action=new");
        }

        protected void btnManageTables_Click(object sender, EventArgs e)
        {
            Response.Redirect("Tables.aspx");
        }

        protected void btnViewReports_Click(object sender, EventArgs e)
        {
            Response.Redirect("Reports.aspx");
        }

        protected void txtSearch_TextChanged(object sender, EventArgs e)
        {
            // Implement search functionality
            LoadRecentOrders();
        }

        protected void ddlFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Implement filter functionality
            LoadRecentOrders();
        }
    }
} 
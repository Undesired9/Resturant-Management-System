using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Text;
using System.Web.UI;

namespace RestaurantManagement
{
    public partial class Orders : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadOrders();
                InjectOrdersChart();
            }
        }

        private void LoadOrders()
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["RestaurantDB"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Orders", conn))
                {
                    conn.Open();
                    gvOrders.DataSource = cmd.ExecuteReader();
                    gvOrders.DataBind();
                }
            }
        }

        private void ShowToast(string message, string type = "success")
        {
            var toast = this.FindControl("toast") as System.Web.UI.HtmlControls.HtmlGenericControl;
            if (toast != null)
            {
                toast.InnerText = message;
                toast.Attributes["class"] = "toast show " + type;
                ScriptManager.RegisterStartupScript(this, GetType(), "showToast", "setTimeout(function(){ document.getElementById('toast').classList.remove('show'); }, 3000);", true);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate input before processing
                DateTime orderDate;
                decimal totalAmount;
                
                if (!DateTime.TryParse(txtOrderDate.Text, out orderDate))
                {
                    ShowToast("Please enter a valid date format (e.g., MM/dd/yyyy)", "error");
                    return;
                }
                
                if (!decimal.TryParse(txtTotalAmount.Text, out totalAmount))
                {
                    ShowToast("Please enter a valid amount", "error");
                    return;
                }
                
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["RestaurantDB"].ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO Orders (OrderDate, TotalAmount) VALUES (@OrderDate, @TotalAmount)", conn))
                    {
                        cmd.Parameters.AddWithValue("@OrderDate", orderDate);
                        cmd.Parameters.AddWithValue("@TotalAmount", totalAmount);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                LoadOrders();
                InjectOrdersChart();
                txtOrderDate.Text = "";
                txtTotalAmount.Text = "";
                ShowToast("Order saved successfully!", "success");
            }
            catch (Exception ex)
            {
                ShowToast("Error: " + ex.Message, "error");
            }
        }

        protected void gvOrders_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DeleteOrder")
            {
                int id = Convert.ToInt32(e.CommandArgument);
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["RestaurantDB"].ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("DELETE FROM Orders WHERE ID = @ID", conn))
                    {
                        cmd.Parameters.AddWithValue("@ID", id);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                LoadOrders();
                InjectOrdersChart();
                ShowToast("Order deleted successfully!", "success");
            }
            else if (e.CommandName == "EditOrder")
            {
                // Handle edit logic here
                ShowToast("Order loaded for editing.", "success");
            }
        }

        private void InjectOrdersChart()
        {
            // Query for orders per day (last 7 days)
            StringBuilder labels = new StringBuilder();
            StringBuilder data = new StringBuilder();
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["RestaurantDB"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(@"SELECT CONVERT(varchar, OrderDate, 23) as OrderDay, COUNT(*) as OrderCount FROM Orders WHERE OrderDate >= DATEADD(day, -6, GETDATE()) GROUP BY CONVERT(varchar, OrderDate, 23) ORDER BY OrderDay", conn))
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            labels.Append("'" + Convert.ToDateTime(reader["OrderDay"]).ToString("MM-dd") + "',");
                            data.Append(reader["OrderCount"].ToString() + ",");
                        }
                    }
                }
            }
            string chartScript = @"
                var ctx = document.getElementById('ordersChart').getContext('2d');
                new Chart(ctx, {{
                    type: 'bar',
                    data: {{
                        labels: [{labels.ToString().TrimEnd(',')}],
                        datasets: [{{
                            label: 'Orders Per Day',
                            data: [{data.ToString().TrimEnd(',')}],
                            backgroundColor: '#3498db',
                            borderRadius: 6
                        }}]
                    }},
                    options: {{
                        responsive: true,
                        plugins: {{
                            legend: {{ display: false }}
                        }},
                        scales: {{
                            y: {{ beginAtZero: true }}
                        }}
                    }}
                }});
            ";
            ScriptManager.RegisterStartupScript(this, GetType(), "ordersChart", chartScript, true);
        }
    }
}
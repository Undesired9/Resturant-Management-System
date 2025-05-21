using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using System.Web.Security;

namespace RestaurantManagement
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Check if user is already logged in
                if (Session["UserID"] != null)
                {
                    Response.Redirect("Default.aspx");
                }
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if (ValidateUser(txtUsername.Text, txtPassword.Text))
            {
                // Create authentication ticket
                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
                    1,
                    txtUsername.Text,
                    DateTime.Now,
                    DateTime.Now.AddMinutes(30),
                    false,
                    "User"
                );

                // Encrypt the ticket
                string encryptedTicket = FormsAuthentication.Encrypt(ticket);

                // Create the cookie
                HttpCookie authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                Response.Cookies.Add(authCookie);

                // Store user info in session
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["RestaurantDB"].ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("SELECT ID FROM Users WHERE Username = @Username", conn))
                    {
                        cmd.Parameters.AddWithValue("@Username", txtUsername.Text);
                        conn.Open();
                        object userId = cmd.ExecuteScalar();
                        if (userId != null)
                        {
                            Session["UserID"] = userId;
                            Session["Username"] = txtUsername.Text;
                        }
                    }
                }

                Response.Redirect("Default.aspx");
            }
            else
            {
                lblMessage.Text = "Invalid username or password.";
            }
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtUsername.Text) || string.IsNullOrEmpty(txtPassword.Text))
            {
                lblMessage.Text = "Please enter both username and password.";
                return;
            }

            if (IsUserExists(txtUsername.Text))
            {
                lblMessage.Text = "Username already exists.";
                return;
            }

            if (RegisterUser(txtUsername.Text, txtPassword.Text))
            {
                lblMessage.Text = "Registration successful. Please login.";
                txtUsername.Text = "";
                txtPassword.Text = "";
            }
            else
            {
                lblMessage.Text = "Registration failed. Please try again.";
            }
        }

        private bool ValidateUser(string username, string password)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["RestaurantDB"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Users WHERE Username = @Username AND Password = @Password", conn))
                {
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@Password", password);
                    conn.Open();
                    int count = (int)cmd.ExecuteScalar();
                    return count > 0;
                }
            }
        }

        private bool IsUserExists(string username)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["RestaurantDB"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Users WHERE Username = @Username", conn))
                {
                    cmd.Parameters.AddWithValue("@Username", username);
                    conn.Open();
                    int count = (int)cmd.ExecuteScalar();
                    return count > 0;
                }
            }
        }

        private bool RegisterUser(string username, string password)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["RestaurantDB"].ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO Users (Username, Password) VALUES (@Username, @Password)", conn))
                    {
                        cmd.Parameters.AddWithValue("@Username", username);
                        cmd.Parameters.AddWithValue("@Password", password);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch
            {
                return false;
            }
        }
    }
} 
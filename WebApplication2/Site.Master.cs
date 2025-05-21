using System;

namespace RestaurantManagement
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.AppRelativeVirtualPath.ToLower().Contains("login.aspx") && Session["UserID"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            if (Session["UserID"] != null)
            {
                lblUsername.Text = Session["Username"] != null ? Session["Username"].ToString() : "User";
                imgAvatar.Visible = true;
                lblUsername.Visible = true;
            }
            else
            {
                imgAvatar.Visible = false;
                lblUsername.Visible = false;
            }
        }
    }
} 
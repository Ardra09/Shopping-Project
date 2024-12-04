using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
namespace Project
{
    public partial class Login : System.Web.UI.Page
    {
        ConClass objcls = new ConClass();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string str = "select Count(Reg_Id) from Log_tab where Username='" + TextBox1.Text + "' and Password='" + TextBox2.Text + "'";
            string cid = objcls.Fn_scalar(str);
            int cid1 = Convert.ToInt32(cid);
            if (cid1 == 1)
            {
                Label3.Visible = true;
                string str1 = "select Reg_Id from Log_tab where Username='" + TextBox1.Text + "' and Password='" + TextBox2.Text + "'";
                string regid = objcls.Fn_scalar(str1);
                Session["userid"] = regid;
                string str2 = "select Log_Type from Log_tab where Username='" + TextBox1.Text + "' and Password='" + TextBox2.Text + "'";
                string Log_Type = objcls.Fn_scalar(str2);
                if (Log_Type == "admin")
                {
                    Label3.Text = "Admin";
                    Response.Redirect("AdminHome.aspx");
                }
                else if (Log_Type == "user")
                {
                    Label3.Text = "User";
                    Response.Redirect("UserHome.aspx");
                }
                else
                {
                    Label3.Text = "Invalid Username and Password";
                }
            }
        }
    }
}
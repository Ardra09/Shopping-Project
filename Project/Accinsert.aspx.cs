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
    public partial class Accinsert : System.Web.UI.Page
    {
        ConClass objcls = new ConClass();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            int userid = Convert.ToInt32(Session["userid"]);
            string ins = "insert into Acnt_tab values(" + TextBox1.Text + "," + userid + ",'" + TextBox2.Text + "'," + TextBox3.Text + ")";
            int result = objcls.Fn_nonquery(ins);
            if(result==1)
            {
                Response.Redirect("Viewbill.aspx");
            }
            else
            {
                Label1.Text = "Error Occured";
            }
        }
    }
}
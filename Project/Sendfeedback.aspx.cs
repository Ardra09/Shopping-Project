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
    public partial class Sendfeedback : System.Web.UI.Page
    {
        ConClass objcls = new ConClass();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            int userId = Convert.ToInt32(Session["userid"]);
            string ins = "insert into Feedback_tab values(" + userId + ",'" + TextBox1.Text + "','null','active')";
            int result = objcls.Fn_nonquery(ins);
            if(result==1)
            {
                Label1.Visible = true;
                Label1.Text = "Inserted";
            }
        }
    }
}
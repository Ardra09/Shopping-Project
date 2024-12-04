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
    public partial class ViewFeedback : System.Web.UI.Page
    {
        ConClass objcls = new ConClass();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid();
            }
        }
        public void BindGrid()
        {
            int userid = Convert.ToInt32(Session["userid"]);
            string sel = "SELECT Feedback_tab.Feedback_msg,Feedback_tab.User_Id,User_reg.FirstName,User_reg.LastName,User_reg.Email FROM Feedback_tab  INNER JOIN  User_reg ON Feedback_tab.User_Id = User_reg.User_Id WHERE Feedback_status = 'active'";
            DataSet ds = objcls.fn_dataset(sel);
            GridView1.DataSource = ds;
            GridView1.DataBind();
        }

      
        protected void GridView1_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            string userid = GridView1.DataKeys[e.NewSelectedIndex].Value.ToString();
            Session["userid"] = userid;
            Response.Redirect("SendEmail.aspx");
        }
    }
}
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
    public partial class UserHome : System.Web.UI.Page
    {
        ConClass objcls = new ConClass();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                datalistbind();
            }
        }
        public void datalistbind()
        {
            string sel = "select * from Cat_tab";
            DataSet ds = objcls.fn_dataset(sel);
            DataList1.DataSource = ds;
            DataList1.DataBind();
        }

        protected void ImageButton1_Command(object sender, CommandEventArgs e)
        {
            int catid = Convert.ToInt32(e.CommandArgument);
            Session["category_selected"] = catid;
            Response.Redirect("ViewProduct.aspx");
        }
    }
}
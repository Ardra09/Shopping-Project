using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Project
{
    public partial class Admin_reg1 : System.Web.UI.Page
    {
        ConClass objcls = new ConClass();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        

        protected void Button1_Click1(object sender, EventArgs e)
        {

            string sel = "select max(Reg_Id) from Log_tab";
            string maxregid = objcls.Fn_scalar(sel);
            int reg_id = 0;
            if (maxregid == "")
            {
                reg_id = 1;
            }
            else
            {
                int newregid = Convert.ToInt32(maxregid);
                reg_id = newregid + 1;
            }
            string ins = "insert into Admin_reg values(" + reg_id + ",'" + TextBox1.Text + "','" + TextBox2.Text + "','" + TextBox3.Text + "','" + TextBox4.Text + "','" + TextBox5.Text + "')";
            int i = objcls.Fn_nonquery(ins);
            if (i == 1)
            {
                string inslog = "insert into Log_tab values(" + reg_id + ",'" + TextBox6.Text + "','" + TextBox8.Text + "','admin')";
                int j = objcls.Fn_nonquery(inslog);
            }
        }
    }
}
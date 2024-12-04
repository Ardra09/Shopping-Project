using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Project
{
    public partial class Admin_reg : System.Web.UI.Page
    {
        ConClass objcls = new ConClass();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
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
            string ins = "insert into User_reg values(" + reg_id + ",'" + TextBox1.Text + "','" + TextBox2.Text + "','" + TextBox3.Text + "','" + TextBox4.Text + "','" + TextBox5.Text + "','" + TextBox6.Text + "','"+RadioButtonList1.SelectedItem.Text+"','active')";
            int i = objcls.Fn_nonquery(ins);
            if (i == 1)
            {
                string inslog = "insert into Log_tab values(" + reg_id + ",'" + TextBox7.Text + "','" + TextBox9.Text + "','user')";
                int j = objcls.Fn_nonquery(inslog);
            }
        }

       
    }
}
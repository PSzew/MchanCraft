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
using System;

namespace mechanCraft_WPF
{  
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void info(object sender, RoutedEventArgs e)
        {
            connectioSettings cs = new connectioSettings();
            cs.Show();
        }

        private void send(object sender, RoutedEventArgs e)
        {
            if (Serialport.serialPort == null || Serialport.serialPort.IsOpen == false)
            {
                MessageBox.Show("Błąd połączenia!", "MechanCraft", MessageBoxButton.OK,MessageBoxImage.Error);
                return;
            }

            int result;
            bool isValidResult = false;
            if(int.TryParse(wynik.Text,out result))
            {
                if (result == 3)
                    isValidResult = true;
                else
                {
                    MessageBox.Show("Nie poprawny wynik,spróbuj ponownie!", "MechanCraft", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                MessageBox.Show("Podaj liczbe!", "MechanCraft", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            

            try
            {
                if(isValidResult)
                Serialport.serialPort.Write("a");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

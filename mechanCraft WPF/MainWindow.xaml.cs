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
using System.Collections.Generic;

namespace mechanCraft_WPF
{  
    public partial class MainWindow : Window
    {
        Random r = new Random();
        List<Question> pytania = new List<Question>();
        int questioncounter = 0;
        string pin = "";
        string userpin = "";
        public MainWindow()
        {
            InitializeComponent();

            pytania.Add(new Question("2x+3=9", 3));
            pytania.Add(new Question("x+12-3=14", 5));
            pytania.Add(new Question("x-5=4", 9));
            pytania.Add(new Question("x+6=10", 4));
            pytania.Add(new Question("x-3=4", 7));
            pytania.Add(new Question("x+1=9", 8));
            pytania.Add(new Question("2+2*2", 6));


            nextQuestion();
        }

        private void info(object sender, RoutedEventArgs e)
        {
            connectioSettings cs = new connectioSettings();
            cs.Show();            
        }

        private void send(object sender, RoutedEventArgs e)
        {
            if(questioncounter==4)
            {
                try
                {
                    Serialport.serialPort.Write(wynik.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                if (Serialport.serialPort == null || Serialport.serialPort.IsOpen == false)
                {
                    MessageBox.Show("Błąd połączenia!", "MechanCraft", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                int result = 0;
                bool isValidResult = false;
                userpin += wynik.Text;
                if (int.TryParse(userpin, out result))
                {
                    if (result == int.Parse(pin))
                        isValidResult = true;
                    else
                    {
                        try
                        {
                            Serialport.serialPort.Write("e");
                        }
                        catch (Exception ex)
                        {

                        }
                        MessageBox.Show("Nie poprawny wynik,spróbuj ponownie!", "MechanCraft", MessageBoxButton.OK, MessageBoxImage.Warning);
                        reset();
                        
                    }
                }
                else
                {
                    MessageBox.Show("Podaj liczbe!", "MechanCraft", MessageBoxButton.OK, MessageBoxImage.Warning);
                }

                if(isValidResult)
                {
                    try
                    {          
                        Serialport.serialPort.Write("a");
                        MessageBox.Show("Gratulacje udało ci się rozwiązać 4 zagadki, odbierz twoją nagrodę z pudełka po lewej stronie!", "MechanCraft", MessageBoxButton.OK, MessageBoxImage.Information);
                        MessageBox.Show("Wcisnij Ok kiedy odbierzesz nagrodę! ", "MechanCraft", MessageBoxButton.OK, MessageBoxImage.Information);                 
                        BoxClose();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                
            }
            else
            {
                userpin += wynik.Text;
                try
                {
                    Serialport.serialPort.Write(wynik.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }     
                nextQuestion();
            }
        }
        private void nextQuestion()
        {
            wynik.Text = string.Empty;
            int index = r.Next(0, pytania.Count);
            question.Content = pytania[index].question;
            pin += pytania[index].anwser.ToString();
            pytania.RemoveAt(index);
            questioncounter++;
        }

        private void reset()
        {
            pytania.Clear();
            pytania.Add(new Question("2x+3=9", 3));
            pytania.Add(new Question("x+12-3=14", 5));
            pytania.Add(new Question("x-5=4", 9));
            pytania.Add(new Question("x+6=10", 4));
            pytania.Add(new Question("x-3=4", 7));
            pytania.Add(new Question("x+1=9", 8));
            pytania.Add(new Question("2+2*2", 6));

            wynik.Text = string.Empty;
            pin = "";
            userpin = "";
            questioncounter = 0;
            nextQuestion();
            try
            {
                Serialport.serialPort.Write("c");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void BoxClose()
        {
            try
            {                             
                Serialport.serialPort.Write("b");
                reset();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

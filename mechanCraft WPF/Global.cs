using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;

namespace mechanCraft_WPF
{
    internal class Global
    {
        public static class Serialport
        {
            public static System.IO.Ports.SerialPort serialPort { get; set; }
        }
        public class ComPort
        {
            public string? DeviceID { get; set; }
            public string? Description { get; set; }
        }
        public class Question
        {
            public string question { get; set; }
            public int anwser { get; set; }

            public Question(string question, int anwser)
            {
                this.question = question;
                this.anwser = anwser;
            }
        }
    }
}

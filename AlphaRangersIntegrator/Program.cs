using System;
using System.IO.Ports;
using System.Threading;

namespace AlphaRangersIntegrator
{
    class Program
    {  
        static SerialPort _serialPort;
        public static void Main()
        {
            _serialPort = new SerialPort
            {
                PortName = "COM4",
                BaudRate = 9600
            };

            if(!_serialPort.IsOpen)
                _serialPort.Open();

            while (true)
            {
                string a = _serialPort.ReadExisting();
                Console.WriteLine(a);
                Thread.Sleep(200);
            }
        }
    }
}

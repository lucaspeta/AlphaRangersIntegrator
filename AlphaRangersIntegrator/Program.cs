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

            Console.WriteLine("ALPHA RANGERS - SerialReader\n\n");

            Console.WriteLine("Running...\n\n");
            Console.WriteLine("Any exception will be printed here\n\n");

            while (true)
            {
                string data = _serialPort.ReadExisting();

                var dataParsed = data.Split(";");

                Baja baja = new Baja();

                try
                {
                    baja.Velocidade = Int32.Parse(dataParsed[0]);
                    baja.Temperatura = Int32.Parse(dataParsed[1]);
                    baja.FreioQTD = Int32.Parse(dataParsed[2]);
                    baja.VoltasQTD = Int32.Parse(dataParsed[3]);
                    baja.Tensao = Int32.Parse(dataParsed[4]);

                    if (baja.ValidateData())
                        baja.InsertData();
                    else
                        throw new Exception("Não foi possível inserir no banco, Algo de errado nao está certo!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }                

                Thread.Sleep(200);
            }
        }
    }
}

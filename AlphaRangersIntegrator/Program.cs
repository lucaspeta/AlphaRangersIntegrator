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
                BaudRate = 115200
            };

            if(!_serialPort.IsOpen)
                _serialPort.Open();

            Console.WriteLine("ALPHA RANGERS - SerialReader\n\n");

            Console.WriteLine("Running...\n\n");
            Console.WriteLine("Any exception will be printed here\n\n");

            while (true)
            {
                string data = _serialPort.ReadExisting();

                Console.WriteLine(data);

                var dataParsed = data.Split(";");

                string data_type = dataParsed[0];

                try
                {
                    if(data_type == "baja"){
                        Baja baja = new Baja();
                        
                        baja.Velocidade = Int32.Parse(dataParsed[1]);
                        baja.Temperatura = Int32.Parse(dataParsed[2]);
                        baja.FreioQTD = Int32.Parse(dataParsed[3]);
                        baja.VoltasQTD = Int32.Parse(dataParsed[4]);
                        baja.Tensao = Int32.Parse(dataParsed[5]);

                        baja.InsertData();
                        baja.print();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                } 

                Flags Flags = new Flags();

                Flags.ReadFlagsFromDB();

                if(Flags.isThereAnyFlag)
                {
                    Console.WriteLine("True");
                    _serialPort.Write("flags;"+Flags.Flag_Verde+";"+Flags.Flag_Amarela+";"+Flags.Flag_Vermelha+";"+Flags.Flag_Desligar);

                    Flags.Print();
                    Flags.CheckAsRead();
                }

                Thread.Sleep(200);
            }
        }
    }
}

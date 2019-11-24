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

                if(Flags.isThereAnyData)
                {
                    _serialPort.Write("{0};{1};{2};{3};{4}", "flags", Flags.Flag_Verde, Flags.Flag_Amarela, Flags.Flag_Vermelha, Flags.Flag_Desligar);

                    Flags.CheckAsRead();
                }

                Thread.Sleep(200);
            }
        }
    }
}

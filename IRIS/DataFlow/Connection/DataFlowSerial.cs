using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Threading;
using IRIS.DataFlow.Color;

namespace IRIS.DataFlow.Connection
{
    public class DataFlowSerial
    {
        const int GLOBAL_WAIT_TIME = 64;
        private SerialPort Port { get; set; }

        public DataFlowSerial(SerialPort port)
        {
            Port = port;
        }

        public static string[] GetPorts()
        {
            return SerialPort.GetPortNames();
        }

        private void PortDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            // Show all the incoming data in the port's buffer
            Console.WriteLine(Port.ReadExisting());
        }

        public void Connect()
        {
            Port.Open();
            Console.WriteLine("Connected to: " + Port.PortName + ".");
            Console.WriteLine("IRIS RGB Initializing...");

            // send color and wait for response
            Port.Write("0 0 0 ");

            while (true)
            {
                Port.Write("0 0 0 ");
                Thread.Sleep(GLOBAL_WAIT_TIME);
                if(Port.ReadLine() == "Color has been set."){
                    Console.WriteLine("IRIS Initialized.");
                    break;
                }
                
            }

        }

        public void SetColor(DataFlowRGB color)
        {
            Port.Write(color.ToString());
        }

        public void Transition(DataFlowRGB startColor, DataFlowRGB endColor, int delay, double acc)
        {
            for(double i = 0; i < 1; i+= acc)
            {
                DataFlowRGB color = startColor.Interpolate(endColor, i);
                Port.Write(color.ToString());
                Thread.Sleep(delay);
            }
        }

        public void Strobe(DataFlowRGB color, int delay)
        {
            while (true)
            {
                Port.Write(color.ToString());
                Thread.Sleep(delay);
                Port.Write("0 0 0 ");
                Thread.Sleep(delay);
            }
        }
        public void Pulse(DataFlowRGB color, int delay, double acc, int interval)
        {
            while (true) 
            {
                Transition(color, new DataFlowRGB(0, 0, 0), delay, acc);
                Thread.Sleep(interval);
                Transition(new DataFlowRGB(0, 0, 0), color, delay, acc);
                Thread.Sleep(interval);
            }
        }
        public void ColorWheel(int delay, double acc)
        {
            Transition(new DataFlowRGB(255, 0, 0), new DataFlowRGB(0, 255, 0), delay, acc);
            Transition(new DataFlowRGB(0, 255, 0), new DataFlowRGB(0, 0, 255), delay, acc);
            Transition(new DataFlowRGB(0, 0, 255), new DataFlowRGB(255, 0, 0), delay, acc);
            Thread.Sleep(delay);
        }
    }
        

}

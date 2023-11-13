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
        const int GLOBAL_WAIT_TIME = 512;
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
            System.Diagnostics.Debug.WriteLine("Connected to: " + Port.PortName + ".");
            System.Diagnostics.Debug.WriteLine("IRIS RGB Initializing...");
        }

        public void SetColor(DataFlowRGB color)
        {
            Port.Write(color.ToString());
        }

        public void Transition(DataFlowRGB startColor, DataFlowRGB endColor, int delay, double acc)
        {
            
            for (double i = 0; i <= 1; i+= acc)
            {
                DataFlowRGB color = startColor.Interpolate(endColor, i);
                Port.Write(color.ToString());
                Thread.Sleep(delay);
            }
            
        }

        public void SmoothRandom(int delay, double acc)
        {
            Random rand = new();
            DataFlowRGB start = new(rand.Next(0, 255), rand.Next(0, 255), rand.Next(0, 255));
            while (true)
            {
                DataFlowRGB end = new(rand.Next(0, 255), rand.Next(0, 255), rand.Next(0, 255));
                Transition(start, end, delay, acc);
                Thread.Sleep(delay);
                start = end;
            }
        }

        public void HarshRandom(int delay, double acc)
        {
            Random rand = new();
            while (true)
            {
                DataFlowRGB start = new(rand.Next(0, 255), rand.Next(0, 255), rand.Next(0, 255));
                DataFlowRGB end = new(rand.Next(0, 255), rand.Next(0, 255), rand.Next(0, 255));
                Transition(start, end, delay, acc);
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
            while (true) 
            { 
            Transition(new DataFlowRGB(255, 0, 0), new DataFlowRGB(0, 255, 0), delay, acc);
            Thread.Sleep(delay);
            Transition(new DataFlowRGB(0, 255, 0), new DataFlowRGB(0, 0, 255), delay, acc);
            Thread.Sleep(delay);
            Transition(new DataFlowRGB(0, 0, 255), new DataFlowRGB(255, 0, 0), delay, acc);
            Thread.Sleep(delay);
            }
        }
    }
        

}


using System.IO.Ports;
using IRIS.DataFlow.Connection;
namespace IRIS;

public class Program
{
    public static void Init()
    {
        System.Diagnostics.Debug.WriteLine("Started init.");
        DataFlowSerial connection = new(new SerialPort("COM 11", 115200));
        connection.Connect();
        //connection.ColorWheel(64, 0.01);
        //connection.Strobe(new DataFlow.Color.DataFlowRGB(255, 0, 0), 512);
        //connection.SetColor(new DataFlow.Color.DataFlowRGB(255, 0, 255));
        connection.HarshRandom(64, 0.1);
            
    }

    public static void Main()
    {
        System.Diagnostics.Debug.WriteLine("hi");
        Init();
    }
}
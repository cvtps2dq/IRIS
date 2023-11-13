using IRIS.DataFlow.Connection;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace IRIS
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public void init()
        {
            System.Diagnostics.Debug.WriteLine("Started init.");
            DataFlowSerial connection = new(new SerialPort("COM 11", 115200));
            connection.Connect();
            //connection.ColorWheel(64, 0.01);
            //connection.Strobe(new DataFlow.Color.DataFlowRGB(255, 0, 0), 512);
            //connection.SetColor(new DataFlow.Color.DataFlowRGB(255, 0, 255));
            connection.HarshRandom(64, 0.1);
            
        }
        public MainWindow()
        {
            Thread thread = new(new ThreadStart(init));
            thread.Start();
            InitializeComponent();
            
            
            
    }
    }
}

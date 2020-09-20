using System;
using System.Collections.Generic;
using System.IO.Ports;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ComProject
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {        
        private double coordCurrentX = 0.00000;
        private double coordCurrentY = 0;
        private double coordCurrentZ = 0;
        WinCOM COMPortWindow = new WinCOM();
        COMPort port;
        public MainWindow()
        {
            InitializeComponent();
        }
        private void WindowLoaded(object sender, RoutedEventArgs e)
        {           
            //WindowState = WindowState.Maximized;
            COMPortWindow.Owner = this;
            COMPortWindow.Show();
            txtBlockCoordX.Text = coordCurrentX + " мм";
            txtBlockCoordY.Text = coordCurrentY + " мм";
            txtBlockCoordZ.Text = coordCurrentZ + " мм";
        }
        private void BtnConnectClick(object sender, RoutedEventArgs e)
        {
            string portName = COMPortWindow.comboBoxCOMPorts.SelectedItem as string;
            port = new COMPort(portName);
            port.Connect();
        }
        private void BtnSendClick(object sender, RoutedEventArgs e)
        {            
            coordCurrentX = double.Parse(stepX.textBox.Text) * double.Parse(coeffX.textBox.Text);
            txtBlockCoordX.Text = coordCurrentX + " мм";
            coordCurrentY = double.Parse(stepY.textBox.Text) * double.Parse(coeffY.textBox.Text);
            txtBlockCoordY.Text = coordCurrentY + " мм";
            coordCurrentZ = double.Parse(stepZ.textBox.Text) * double.Parse(coeffZ.textBox.Text);
            txtBlockCoordZ.Text = coordCurrentZ + " мм";
            
            if (Int32.Parse(stepX.textBox.Text) < 10 && Int32.Parse(coeffX.textBox.Text) < 10)
            {
                port.Send();
            }
            else
                port.Send();
        }
        private void BtnRequestClick(object sender, RoutedEventArgs e)
        {
            //port.Receive(sender, e);
        }
        private void BtnDisConnectClick(object sender, RoutedEventArgs e)
        {
            port.DisConnect();
        }
    }
}
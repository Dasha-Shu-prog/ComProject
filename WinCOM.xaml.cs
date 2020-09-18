using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.IO.Ports;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ComProject
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class WinCOM : Window
    {
        //SerialPort serialPort = new SerialPort();
        public WinCOM()
        {
            InitializeComponent();
        }
        private void BtnOK_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
        private void ComboBoxCOMPorts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedComboBoxItem = sender as ComboBox;
           // string name = selectedComboBoxItem.SelectedItem as string;
            //serialPort.PortName = comboBoxCOMPorts.SelectedItem.ToString();
        }
    }
}

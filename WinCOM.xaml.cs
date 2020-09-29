using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
        public WinCOM()
        {
            InitializeComponent();

            comboBoxCOMPorts.Items.Add("МК не подключен");
            comboBoxCOMPorts.SelectedIndex = 0;
            btnOK.IsEnabled = false;
            var portsNames = SerialPort.GetPortNames();
            if (portsNames.Count() != 0)
            {
                btnOK.IsEnabled = true;
                comboBoxCOMPorts.Items.Clear();

                foreach (string name in portsNames)
                {
                    comboBoxCOMPorts.Items.Add(name);
                }
                comboBoxCOMPorts.SelectedIndex = 0;
            }
        }
        private void BtnOK_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}

﻿using System;
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

namespace ComProject
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<string> list = new List<string>();
        public decimal coordCurrentStepX = 0;
        public decimal coordCurrentStepY = 0;
        public decimal coordCurrentStepZ = 0;
        public decimal coordCurrentCoeffX = 0;
        public decimal coordCurrentCoeffY = 0;
        public decimal coordCurrentCoeffZ = 0;
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
            txtBlockCoordX.Text = coordCurrentStepX + " мм";
            txtBlockCoordY.Text = coordCurrentStepY + " мм";
            txtBlockCoordZ.Text = coordCurrentStepZ + " мм";
        }
        private void BtnConnectClick(object sender, RoutedEventArgs e)
        {
            string portName = COMPortWindow.comboBoxCOMPorts.SelectedItem as string;
            port = new COMPort(portName);
            port.Connect();
        }
        private void BtnSendClick(object sender, RoutedEventArgs e)
        {            
            coordCurrentStepX = decimal.Parse(stepX.textBox.Text);
            coordCurrentCoeffX = decimal.Parse(coeffX.textBox.Text);
            txtBlockCoordX.Text = coordCurrentStepX * coordCurrentCoeffX + " мм";
            coordCurrentStepY = decimal.Parse(stepY.textBox.Text);
            coordCurrentCoeffY = decimal.Parse(coeffY.textBox.Text);
            txtBlockCoordY.Text = coordCurrentStepY * coordCurrentCoeffY + " мм";
            coordCurrentStepZ = decimal.Parse(stepZ.textBox.Text);
            coordCurrentCoeffZ = decimal.Parse(coeffZ.textBox.Text);
            txtBlockCoordZ.Text = coordCurrentStepZ * coordCurrentCoeffZ + " мм";
            list.Add("SSX" + coordCurrentStepX);
            list.Add("SCX" + coordCurrentCoeffX);
            list.Add("SSY" + coordCurrentStepY);
            list.Add("SCY" + coordCurrentCoeffY);
            list.Add("SSZ" + coordCurrentStepZ);
            list.Add("SCZ" + coordCurrentCoeffZ);
            port.Send(list);
        }
        private void BtnRequestClick(object sender, RoutedEventArgs e)
        {            
            //port.Receive(sender, e);
        }
        private void BtnDisConnectClick(object sender, RoutedEventArgs e)
        {
            port.Disсonnect();
        }
        private void Window_Closed(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
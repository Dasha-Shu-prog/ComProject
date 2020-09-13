using System;
using System.Collections.Generic;
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
        private double coordCurrentY = 0.00000;
        private double coordCurrentZ = 0.00000;
        public MainWindow()
        {
            InitializeComponent();
        }
        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            txtBlockCoordX.Text = coordCurrentX.ToString() + " мм";
            txtBlockCoordY.Text = coordCurrentY.ToString() + " мм";
            txtBlockCoordZ.Text = coordCurrentZ.ToString() + " мм";
        }
        private void BtnConnectClick(object sender, RoutedEventArgs e)
        {
            //string coordinateX = coordCurrentX.ToString() + stepX.Text + coeffX.Text;
            //txtBlockCoordX.Text = coordinateX +" мм";
        }
        private void BtnSendClick(object sender, RoutedEventArgs e)
        {

        }
        private void BtnRequestClick(object sender, RoutedEventArgs e)
        {

        }
    }
}
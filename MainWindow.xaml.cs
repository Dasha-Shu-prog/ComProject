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
            //WindowState = WindowState.Maximized;
            txtBlockCoordX.Text = coordCurrentX + " мм";
            txtBlockCoordY.Text = coordCurrentY + " мм";
            txtBlockCoordZ.Text = coordCurrentZ + " мм";
        }
        private void BtnConnectClick(object sender, RoutedEventArgs e)
        {
            string msgText = "Ничего не происходит...";
            string caption = "...";
            MessageBox.Show(msgText, caption, MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }
        private void BtnSendClick(object sender, RoutedEventArgs e)
        {            
            coordCurrentX = double.Parse(stepX.textBox.Text) * double.Parse(coeffX.textBox.Text);
            txtBlockCoordX.Text = coordCurrentX + " мм";
            coordCurrentY = double.Parse(stepY.textBox.Text) * double.Parse(coeffY.textBox.Text);
            txtBlockCoordY.Text = coordCurrentY + " мм";
            coordCurrentZ = double.Parse(stepZ.textBox.Text) * double.Parse(coeffZ.textBox.Text);
            txtBlockCoordZ.Text = coordCurrentZ + " мм";
        }
        private void BtnRequestClick(object sender, RoutedEventArgs e)
        {
            string msgText = "Ничего не происходит...";
            string caption = "...";
            MessageBox.Show(msgText, caption, MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }
        private void BtnDisConnectClick(object sender, RoutedEventArgs e)
        {
            string msgText = "Ничего не происходит...";
            string caption = "...";
            MessageBox.Show(msgText, caption, MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }
    }
}
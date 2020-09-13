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
    /// Логика взаимодействия для CoeffControl.xaml
    /// </summary>
    public partial class CoeffControl : UserControl
    {
        private int value = 5;
        const int MIN_VALUE = 5;
        const int MAX_VALUE = 100;
        public string Text
        {
            get { return textBoxCoeff.Text; }
            set { textBoxCoeff.Text = value; }
        }
        public CoeffControl()
        {
            InitializeComponent();
        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Int32.TryParse(textBoxCoeff.Text, out int result))
            {
                ValidateData(ref result);
                value = result;
            }
            textBoxCoeff.Text = value.ToString();

            if (value == MIN_VALUE)
                decrement.IsEnabled = false;

            else if (value == MAX_VALUE)
                increment.IsEnabled = false;

            else
            {
                decrement.IsEnabled = true;
                increment.IsEnabled = true;
            }
        }
        private void ValidateData(ref int value)
        {
            if (value > MAX_VALUE)
            {
                value = MAX_VALUE;
            }
            if (value < MIN_VALUE)
            {
                value = MIN_VALUE;
            }
        }
        private void DecrementClick(object sender, RoutedEventArgs e)
        {
            int newValue = value - 5;
            ValidateData(ref newValue);
            textBoxCoeff.Text = newValue.ToString();
        }
        private void IncrementClick(object sender, RoutedEventArgs e)
        {
            int newValue = value + 5;
            ValidateData(ref newValue);
            textBoxCoeff.Text = newValue.ToString();
        }
        private void Textbox_MouseDown(object sender, MouseButtonEventArgs e)
        {
            textBoxCoeff.Focusable = true;
        }
        private void Textbox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Right || e.Key == Key.Up)
            {
                IncrementClick(sender, null);
                increment.Focusable = true;
            }
            else if (e.Key == Key.Left || e.Key == Key.Down)
            {
                DecrementClick(sender, null);
                decrement.Focusable = true;
            }
        }
        private void Textbox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Right:
                    {
                        IncrementClick(sender, null);
                        break;
                    }
                case Key.Left:
                    {
                        DecrementClick(sender, null);
                        break;
                    }
                case Key.Up:
                    {
                        IncrementClick(sender, null);
                        break;
                    }
                case Key.Down:
                    {
                        DecrementClick(sender, null);
                        break;
                    }
            }
        }
    }
}

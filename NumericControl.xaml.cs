using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using Key = System.Windows.Input.Key;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Controls.Primitives;

namespace ComProject
{
    /// <summary>
    /// Логика взаимодействия для NumericControl.xaml
    /// </summary>
    public partial class NumericControl : UserControl
    {
        private double stepValue = 0;
        const double MINStep = 0;
        const double MAXStep = 1000;
        private double coeffValue = 0;
        const double MINCoeff = 0;
        const double MAXCoeff = 200;
        public string Text
        {
            get { return textBox.Text; }
            set { textBox.Text = value; }
        }
        public NumericControl()
        {
            InitializeComponent();
        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Double.TryParse(textBox.Text, out double result))
            {
                ValidateData(ref result);
                stepValue = result;
            }
            textBox.Text = stepValue.ToString();

            if (stepValue == MINStep)
                decrement.IsEnabled = false;

            else if (stepValue == MAXStep)
                increment.IsEnabled = false;

            else
            {
                decrement.IsEnabled = true;
                increment.IsEnabled = true;
            }
        }
        private void ValidateData(ref double value)
        {
            if (value > MAXStep)
            {
                value = MAXStep;
            }
            if (value < MINStep)
            {
                value = MINStep;
            }
        }
        private void DecrementClick(object sender, RoutedEventArgs e)
        {
            double newValue = stepValue - 5;
            ValidateData(ref newValue);
            textBox.Text = newValue.ToString();
        }
        private void IncrementClick(object sender, RoutedEventArgs e)
        {
            double newValue = stepValue + 5;
            ValidateData(ref newValue);
            textBox.Text = newValue.ToString();
        }
        private void Textbox_MouseDown(object sender, MouseButtonEventArgs e)
        {
            textBox.Focusable = true;
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

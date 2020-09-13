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

namespace ComProject
{
    /// <summary>
    /// Логика взаимодействия для decIncBtn.xaml
    /// </summary>
    public partial class StepControl : UserControl
    {
        private double value = 0;
        const double MIN_VALUE = 0.00002;
        const double MAX_VALUE = 0.00010;
        public string Text
        {
            get { return textBoxStep.Text; }
            set { textBoxStep.Text = value; }
        }
        public StepControl()
        {
            InitializeComponent();
        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Double.TryParse(textBoxStep.Text, out double result))
            {
                ValidateData(ref result);
                value = result;
            }
            textBoxStep.Text = value.ToString();

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
        private void ValidateData(ref double value)
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
            double newValue = value - 0.00001;
            ValidateData(ref newValue);
            textBoxStep.Text = newValue.ToString();
        }
        private void IncrementClick(object sender, RoutedEventArgs e)
        {
            double newValue = value + 0.00001;
            ValidateData(ref newValue);
            textBoxStep.Text = newValue.ToString();
        }
        private void Textbox_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            textBoxStep.Focusable = true;
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

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

namespace Parbandhan.UserControls
{
    /// <summary>
    /// Interaction logic for SecureTextBoxUserControl.xaml
    /// </summary>
    public partial class SecureTextBoxUserControl : UserControl
    {
        private bool isTextVisible = false;
        public SecureTextBoxUserControl()
        {
            InitializeComponent();
        }

        private void TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                ToggleTextVisibility();
            }
        }

        private void ToggleTextVisibility()
        {
            isTextVisible = !isTextVisible;
            UpdateTextVisibility();
        }

        private void UpdateTextVisibility()
        {
            if (isTextVisible)
            {
                textBox.Text = SecureText;
            }
            else
            {
                SecureText = textBox.Text;
                textBox.Text = new string('*', SecureText.Length);
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            isTextVisible = true;
            UpdateTextVisibility();
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            isTextVisible = false;
            UpdateTextVisibility();
        }

        public static readonly DependencyProperty SecureTextProperty =
            DependencyProperty.Register("SecureText", typeof(string), typeof(SecureTextBoxUserControl));

        public string SecureText
        {
            get { return (string)GetValue(SecureTextProperty); }
            set { SetValue(SecureTextProperty, value); }
        }
    }
}

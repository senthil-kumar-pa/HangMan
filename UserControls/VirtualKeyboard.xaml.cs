using System.Windows;
using System.Windows.Controls;

namespace HangMan.UserControls
{
    public partial class VirtualKeyboard : UserControl
    {
        public event EventHandler<string> KeyPressed;

        public VirtualKeyboard()
        {
            InitializeComponent();
        }

        private void Key_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                string key = button.Content.ToString();
                KeyPressed?.Invoke(this, key); // Raise event with pressed key
            }
        }
    }
}

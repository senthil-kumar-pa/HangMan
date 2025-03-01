using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace HangMan.UserControls
{
    public partial class LetterDisplayControl : UserControl
    {
        public LetterDisplayControl()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty WordProperty =
            DependencyProperty.Register("Word", typeof(string), typeof(LetterDisplayControl),
                new PropertyMetadata(string.Empty, OnWordChanged));

        public string Word
        {
            get => (string)GetValue(WordProperty);
            set => SetValue(WordProperty, value);
        }

        private static void OnWordChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is LetterDisplayControl control && e.NewValue is string newWord)
            {
                control.UpdateWordDisplay(newWord);
            }
        }

        private void UpdateWordDisplay(string word)
        {
            WordPanel.Children.Clear();

            for (int i = 0; i < word.Length; i++)
            {
                char letter = word[i];
                Label lbl = new Label
                {
                    Content = letter.ToString(),
                    FontSize = 40, // BIGGER FONT
                    FontWeight = FontWeights.Bold,
                    Width = 50,
                    Height = 60,
                    HorizontalContentAlignment = HorizontalAlignment.Center,
                    VerticalContentAlignment = VerticalAlignment.Center,
                    BorderBrush = Brushes.Purple,
                    BorderThickness = new Thickness(2),
                    Margin = new Thickness(4),
                    Foreground = Brushes.Purple,
                    Background = Brushes.LightGray,
                    Effect = Create3DEffect(),
                    Tag = i.ToString()
                };

                WordPanel.Children.Add(lbl);
            }
        }

        private Effect Create3DEffect()
        {
            DropShadowEffect shadow = new DropShadowEffect
            {
                Color = Colors.Black,
                Direction = 320,
                ShadowDepth = 5,
                BlurRadius = 8,
                Opacity = 0.6
            };

            BevelBitmapEffect bevel = new BevelBitmapEffect
            {
                BevelWidth = 2,
                LightAngle = 45
            };

            return shadow; // Drop Shadow for realistic depth
        }
    }
}

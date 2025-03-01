using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using HangMan.GameLogic;

namespace HangMan
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly List<Image> images = new List<Image>();

        private readonly string columnImage = "pack://application:,,,/Images/column{0}.png";
        private readonly string baseImage = "pack://application:,,,/Images/base{0}.png";
        private readonly string extensionImage = "pack://application:,,,/Images/extension{0}.png";
        private readonly string supportImage = "pack://application:,,,/Images/support{0}.png";
        private readonly string ropeImage = "pack://application:,,,/Images/rope{0}.png";
        private readonly string headImage = "pack://application:,,,/Images/head{0}.png";
        private readonly string bodyImage = "pack://application:,,,/Images/body{0}.png";
        private readonly string leftHandImage = "pack://application:,,,/Images/leftHand{0}.png";
        private readonly string rightHandImage = "pack://application:,,,/Images/rightHand{0}.png";
        private readonly string leftLegImage = "pack://application:,,,/Images/leftLeg{0}.png";
        private readonly string rightLegImage = "pack://application:,,,/Images/rightLeg{0}.png";

        private const int maxErrors = 12;
        private int errors = 1;

        private string _originalWord = string.Empty;
        private string _maskedWord = string.Empty;

        private int _totalScore = 0;
        private int _currentScore = 0;
        private int _highScore = 0;
        private int _pointsPerLetter = 10;

        public MainWindow()
        {
            InitializeComponent();

            images.Add(ColumnImage);
            images.Add(BaseImage);
            images.Add(ExtensionImage);
            images.Add(SupportImage);
            images.Add(RopeImage);
            images.Add(HeadImage);
            images.Add(BodyImage);
            images.Add(LeftHandImage);
            images.Add(RightHandImage);
            images.Add(LeftLegImage);
            images.Add(RightLegImage);

            //SetHangmanSource(false);
        }

        private void IncreaseErrors()
        {
            ShowHangman(errors);
            errors++;
        }

        private void ShowHangman(int errorCount)
        {
            switch (errors)
            {
                case 0:
                    SetHangmanSource(false);
                    break;
                case 1:
                    SetImage(BaseImage, baseImage, true);
                    break;
                case 2:
                    SetImage(ColumnImage, columnImage, true);
                    break;
                case 3:
                    SetImage(SupportImage, supportImage, true);
                    break;
                case 4:
                    SetImage(ExtensionImage, extensionImage, true);
                    break;
                case 5:
                    SetImage(RopeImage, ropeImage, true);
                    break;
                case 6:
                    SetImage(HeadImage, headImage, true);
                    break;
                case 7:
                    SetImage(LeftHandImage, leftHandImage, true);
                    break;
                case 8:
                    SetImage(RightHandImage, rightHandImage, true);
                    break;
                case 9:
                    SetImage(BodyImage, bodyImage, true);
                    break;
                case 10:
                    SetImage(LeftLegImage, leftLegImage, true);
                    break;
                case 11:
                    SetImage(RightLegImage, rightLegImage, true);
                    break;
                default:
                    break;
            }
        }

        private void SetImage(Image image, string source, bool isEnabled)
        {
            try
            {
                image.Source = new BitmapImage(new Uri(isEnabled ? string.Format(source, "Enabled") : string.Format(columnImage, "Disabled")));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private void SetHangmanSource(bool isEnabled)
        {
            foreach (var image in images)
            {
                image.Visibility = Visibility.Visible;
            }

            ColumnImage.Source = new BitmapImage(new Uri(isEnabled ? string.Format(columnImage, "Enabled") : string.Format(columnImage, "Disabled")));
            BaseImage.Source = new BitmapImage(new Uri(isEnabled ? string.Format(baseImage, "Enabled") : string.Format(baseImage, "Disabled")));
            ExtensionImage.Source = new BitmapImage(new Uri(isEnabled ? string.Format(extensionImage, "Enabled") : string.Format(extensionImage, "Disabled")));
            SupportImage.Source = new BitmapImage(new Uri(isEnabled ? string.Format(supportImage, "Enabled") : string.Format(supportImage, "Disabled")));
            RopeImage.Source = new BitmapImage(new Uri(isEnabled ? string.Format(ropeImage, "Enabled") : string.Format(ropeImage, "Disabled")));
            HeadImage.Source = new BitmapImage(new Uri(isEnabled ? string.Format(headImage, "Enabled") : string.Format(headImage, "Disabled")));
            BodyImage.Source = new BitmapImage(new Uri(isEnabled ? string.Format(bodyImage, "Enabled") : string.Format(bodyImage, "Disabled")));
            LeftHandImage.Source = new BitmapImage(new Uri(isEnabled ? string.Format(leftHandImage, "Enabled") : string.Format(leftHandImage, "Disabled")));
            RightHandImage.Source = new BitmapImage(new Uri(isEnabled ? string.Format(rightHandImage, "Enabled") : string.Format(rightHandImage, "Disabled")));
            LeftLegImage.Source = new BitmapImage(new Uri(isEnabled ? string.Format(leftLegImage, "Enabled") : string.Format(leftLegImage, "Disabled")));
            RightLegImage.Source = new BitmapImage(new Uri(isEnabled ? string.Format(rightLegImage, "Enabled") : string.Format(rightLegImage, "Disabled")));

        }

        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            ResetGame();

            (_originalWord, _maskedWord) = HangmanWordDatabase.GetRandomWord();

            WordDisplay.Word = _maskedWord;

            var pos = Utilities.GetCharacterPositions(_maskedWord, '*');

            foreach (var c in _maskedWord.ToCharArray())
            {
                _originalWord = _originalWord.Replace(c, '*');
            }
        }

        private void ResetGame()
        {
            WordDisplay.WordPanel.Children.Clear();
            WordDisplay.IsEnabled = true;
            _originalWord = "";
            _maskedWord = "";
            errors = 1;
            _currentScore = 0;
            CurrentScoreLabel.Content = _currentScore;
            SetHangmanSource(false);
        }

        private void VirtualKeyboard_KeyPressed(object sender, string key)
        {
            //txtInput.Text += key; // Append the pressed key to the TextBox
            var pos = Utilities.GetCharacterPositions(_originalWord, char.Parse(key));

            if (!(pos.Count() > 0))
            {
                IncreaseErrors();
                if (errors >= maxErrors)
                {
                    MessageBox.Show("Lost the game");
                    WordDisplay.IsEnabled = false;
                }
            }
            else
            {
                _currentScore += pos.Count * _pointsPerLetter;
                _totalScore += _currentScore;

                if (_highScore < _totalScore)
                {
                    _highScore = _totalScore;
                }

                foreach (var lbl in WordDisplay.WordPanel.Children)
                {
                    Label childLabel = (Label)lbl;
                    if (pos.Any(x => x.ToString() == childLabel.Tag.ToString()))
                    {
                        childLabel.Content = key;
                    }
                }

                _originalWord = _originalWord.Replace(key, "*");

                DisplayScores();

                if (_originalWord.Replace("*", "").Length == 0)
                {
                    MessageBox.Show("Won the game");
                }
            }
        }

        private void DisplayScores()
        {
            CurrentScoreLabel.Content = _currentScore;
            TotalScoreLabel.Content = _totalScore;
            HighScoreLabel.Content = _highScore;
        }
    }

}
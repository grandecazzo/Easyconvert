using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace easyconverter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Color panelWhite;
        Color panelLigherGray;
        Color panelDarkerGray;
        Color panelGreen;

        short convert_type = 0;
        public static short convert_status = 0;
        public MainWindow()
        {
            convert_type = 0;
            panelWhite = (Color)ColorConverter.ConvertFromString("#eeeeee");
            panelLigherGray = (Color)ColorConverter.ConvertFromString("#393e46");
            panelDarkerGray = (Color)ColorConverter.ConvertFromString("#232931");
            panelGreen = (Color)ColorConverter.ConvertFromString("#4ecca3");

            InitializeComponent();

            this.parseString(TextBoxInput.Text);
        }

        new private void MouseEnter(object sender, MouseEventArgs e)
        {
            (sender as Button).Background = new SolidColorBrush(panelWhite);
            ((sender as Button).Parent as Border).BorderBrush = new SolidColorBrush(panelWhite);
            (sender as Button).Foreground =  new SolidColorBrush(panelDarkerGray);
        }

        new private void MouseLeave(object sender, MouseEventArgs e)
        {
            (sender as Button).Background = new SolidColorBrush(panelGreen);
            ((sender as Button).Parent as Border).BorderBrush = new SolidColorBrush(panelGreen);

            if((sender as Button).Tag==null)
            {
                (sender as Button).Foreground =  new SolidColorBrush(panelDarkerGray);
                return;
            }

            if(convert_type == Int32.Parse((sender as Button).Tag.ToString())){
                (sender as Button).Foreground =  new SolidColorBrush(panelWhite);}
            else{
                (sender as Button).Foreground =  new SolidColorBrush(panelDarkerGray);
            }

        }

        private void TypeButtonClick(object sender, RoutedEventArgs e)
        {
            string input = String.Copy(TextBoxInput.Text);
            string [] array_current = DataConverter.ConvertValues(input,convert_type);
            convert_type = (short)Int32.Parse((sender as Button).Tag.ToString());
            Console.WriteLine(convert_type);
            if(array_current!=null){
            string _current = array_current[convert_type];
            TextBoxInput.Text = _current;
            }

            (hex_button as Button).Foreground =  new SolidColorBrush(panelDarkerGray);
            (dec_button as Button).Foreground =  new SolidColorBrush(panelDarkerGray);
            (bin_button as Button).Foreground =  new SolidColorBrush(panelDarkerGray);
            (oct_button as Button).Foreground =  new SolidColorBrush(panelDarkerGray);
        }

        private void CleanButtonClick(object sender, RoutedEventArgs e)
        {
            TextBoxInput.Text = "";
        }

        private void LabelMouseEnter(object sender, MouseEventArgs e)
        {
            (sender as Label).Foreground = new SolidColorBrush(panelWhite);
        }

        private void LabelMouseLeave(object sender, MouseEventArgs e)
        {
            if((sender as Label).Tag != null)
            {
                (sender as Label).Foreground = new SolidColorBrush(panelGreen);
            }
            else
            {
                (sender as Label).Foreground = new SolidColorBrush(panelDarkerGray); 
            }
        }

        private void LabelClick(object sender, MouseEventArgs e)
        {
            if(convert_status==0)
            {
                var result = ((sender as Label).Content as String).Split(" ").Last();
                var head = ((sender as Label).Content as String).Split(" ").First();
                Clipboard.SetText(result);

                System.Windows.Controls.Primitives.Popup popup = new System.Windows.Controls.Primitives.Popup();

                Label popupText = new Label{Content = "COPIED!", Background= new SolidColorBrush(panelLigherGray),Foreground = new SolidColorBrush(panelGreen)};

                popup.Child = popupText;

                popup.PlacementTarget = (sender as Label);
                popup.IsOpen=true;

                this.MouseMove += (sender, e) => {if(popup!=null)popup.IsOpen=false;};
            }
        }

        private void parseString(string input)
        {
            var converted = DataConverter.ConvertValues(input,convert_type);
            try{
            if(converted==null)
            {
                HEXLabel.Content = "Problem appeared:";

                switch(convert_status)
                {
                    case -1:
                        DECLabel.Content = "Произошла некоторая";
                        BINLabel.Content = "неопознанная хуйня";
                        OCTLabel.Content = "( ＾◡＾)っ╰⋃╯";
                        break;
                    case -2:
                        DECLabel.Content = "Oversized input";
                        BINLabel.Content = "Max input: 9999999";
                        OCTLabel.Content = "__(┐「ε:)_";
                        break;
                    case -3:
                        DECLabel.Content = "Wrong input";
                        BINLabel.Content = "__(┐「ε:)_";
                        OCTLabel.Content = "";
                        break;
                }
            }
            else
            {
                HEXLabel.Content = "HEX: "+converted[0];
                DECLabel.Content = "DEC: "+converted[1];
                BINLabel.Content = "BIN: "+converted[2];
                OCTLabel.Content = "OCT: "+converted[3];
            }
            }
            catch(Exception e){};
        }

        private void ExitButtonMouseEnter(object sender, MouseEventArgs e)
        {
            (sender as Image).Source = new BitmapImage(new Uri("pack://application:,,,/easyconverter;component/_resources/close-circle-fill-white.png"));
        }

        private void ExitButtonMouseLeave(object sender, MouseEventArgs e)
        {
            (sender as Image).Source = new BitmapImage(new Uri("pack://application:,,,/easyconverter;component/_resources/close-circle-fill.png"));
        }

        private void ExitButtonMouseClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void PinButtonMouseEnter(object sender, MouseEventArgs e)
        {
            if(this.Topmost)
            {
                (sender as Image).Source = new BitmapImage(new Uri("pack://application:,,,/easyconverter;component/_resources/pushpin-fill-white.png"));
            }
            else
            {
                (sender as Image).Source = new BitmapImage(new Uri("pack://application:,,,/easyconverter;component/_resources/pushpin-line-white.png"));
            }
        }

        private void PinButtonMouseLeave(object sender, MouseEventArgs e)
        {
            if(this.Topmost)
            {
                (sender as Image).Source = new BitmapImage(new Uri("pack://application:,,,/easyconverter;component/_resources/pushpin-fill.png"));
            }
            else
            {
                (sender as Image).Source = new BitmapImage(new Uri("pack://application:,,,/easyconverter;component/_resources/pushpin-line.png"));
            }
        }

        private void PinButtonMouseClick(object sender, RoutedEventArgs e)
        {
            if(this.Topmost)
            {
                this.Topmost = false;
                pinImage.Source = new BitmapImage(new Uri("pack://application:,,,/easyconverter;component/_resources/pushpin-line-white.png"));
            }
            else
            {
                this.Topmost = true;
                pinImage.Source = new BitmapImage(new Uri("pack://application:,,,/easyconverter;component/_resources/pushpin-fill-white.png"));
            }

            (sender as Button).UpdateLayout();
        }

        private void WindowDrag(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void TextChangedHandler(object sender, TextChangedEventArgs args)
        {
            string input = (sender as TextBox).Text;

            parseString(input);
        }
    }
}

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Yahtzee
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly BitmapImage[] dice = new BitmapImage[6];

        private readonly Random random = new Random();

        public MainWindow()
        {
            InitializeComponent();

            for (int i = 0; i < dice.Length; i++)
            {
                dice[i] = new BitmapImage(new Uri("/Dice/" + (i + 1) + ".png", UriKind.Relative));
            }

            Image img = (Image)spDice.Children[0];
            img.Source = dice[2];
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            foreach (Image image in spDice.Children)
            {
                image.Source = dice[random.Next(0, dice.Length)];
            }
        }
    }
}

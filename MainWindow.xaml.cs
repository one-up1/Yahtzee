using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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

        private int throwsLeft = 3;

        private int[] ones = new int[2];
        private int[] twos = new int[2];
        private int[] threes = new int[2];
        private int[] fours = new int[2];
        private int[] fives = new int[2];
        private int[] sixes = new int[2];

        public MainWindow()
        {
            InitializeComponent();

            for (int i = 0; i < dice.Length; i++)
            {
                dice[i] = new BitmapImage(new Uri("/Dice/" + (i + 1) + ".png", UriKind.Relative));
            }
        }

        private void RollDice_Click(object sender, RoutedEventArgs e)
        {
            foreach (Image image in spDice.Children)
            {
                // Als we voor de eerste keer gooien mogen alle dobbelstenen weer gebruikt worden.
                if (throwsLeft == 3)
                {
                    image.Visibility = Visibility.Visible;
                }

                // Alleen dobbelstenen die nog niet zijn gebruikt krijgen een nieuw nummer.
                if (image.IsVisible)
                {
                    int i = random.Next(0, dice.Length);
                    image.Source = dice[i];
                    image.Tag = i + 1;
                }
            }

            if (throwsLeft > 1)
            {
                label.Content = "Klik de dobbelstenen aan die je wilt houden." +
                    Environment.NewLine + "Je mag nog " + (--throwsLeft) + " keer gooien";
            }
            else
            {
                label.Content = "Klik op het scorebord";
                bThrowDice.IsEnabled = false;
            }
        }

        private void Dice_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image image = (Image)sender;
            image.Visibility = Visibility.Hidden;

            int i = (int)image.Tag;
            Console.WriteLine("Dice clicked: " + i);

            switch (i)
            {
                case 1:
                    ones[1] += 1;
                    break;
                case 2:
                    twos[1] += 2;
                    break;
                case 3:
                    threes[1] += 3;
                    break;
                case 4:
                    fours[1] += 4;
                    break;
                case 5:
                    fives[1] += 5;
                    break;
                case 6:
                    sixes[1] += 6;
                    break;
            }
            SetLabels();
        }

        private void lOnes_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ones[0] += ones[1];
            Reset();
        }

        private void lTwos_MouseDown(object sender, MouseButtonEventArgs e)
        {
            twos[0] += twos[1];
            Reset();
        }

        private void lThrees_MouseDown(object sender, MouseButtonEventArgs e)
        {
            threes[0] += threes[1];
            Reset();
        }

        private void lFours_MouseDown(object sender, MouseButtonEventArgs e)
        {
            fours[0] += fours[1];
            Reset();
        }

        private void lFives_MouseDown(object sender, MouseButtonEventArgs e)
        {
            fives[0] += fives[1];
            Reset();
        }

        private void lSixes_MouseDown(object sender, MouseButtonEventArgs e)
        {
            sixes[0] += sixes[1];
            Reset();
        }

        private void SetLabels()
        {
            lOnes.Content = ones[0] + ones[1];
            lTwos.Content = twos[0] + twos[1];
            lThrees.Content = threes[0] + threes[1];
            lFours.Content = fours[0] + fours[1];
            lFives.Content = fives[0] + fives[1];
            lSixes.Content = sixes[0] + sixes[1];
        }

        private void Reset()
        {
            label.Content = "Druk op 'Dobbelstenen Gooien'";
            bThrowDice.IsEnabled = true;
            throwsLeft = 3;

            ones[1] = 0;
            twos[1] = 0;
            threes[1] = 0;
            fours[1] = 0;
            fives[1] = 0;
            sixes[1] = 0;
            SetLabels();
        }
    }
}

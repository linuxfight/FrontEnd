using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Threading;

namespace PlatformerApp
{

    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int speed = 10;
        int coins = 0;
        int dropSpeed = 10;
        bool goRight, goLeft;

        public MainWindow()
        {
            InitializeComponent();

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(20);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            Canvas.SetTop(hero, Canvas.GetTop(hero) + dropSpeed);

            lblInfo.Content = coins;

            var rectangles = MyCanvas.Children.OfType<Rectangle>();

            foreach (var rect in rectangles)
            {
                if ((string)rect.Tag == "platform")
                {
                    rect.Stroke = Brushes.Black;

                    Rect rect_hero = new Rect(Canvas.GetLeft(hero), Canvas.GetTop(hero),
                        hero.Width, hero.Height);

                    Rect rect_rectangle = new Rect(Canvas.GetLeft(rect), Canvas.GetTop(rect),
                        rect.Width, rect.Height);

                    if (rect_hero.IntersectsWith(rect_rectangle))
                    {
                        dropSpeed = 0;
                        Canvas.SetTop(hero, Canvas.GetTop(rect) - hero.Height); //get the pos of platform and put hero on it
                    }
                    else
                        dropSpeed = 10;
                }
                if ((string)rect.Tag == "coin")
                { 
                    Rect rect_hero = new Rect(Canvas.GetLeft(hero), Canvas.GetTop(hero),
                        hero.Width, hero.Height);

                    Rect rect_coin = new Rect(Canvas.GetLeft(rect), Canvas.GetTop(rect),
                        rect.Width, rect.Height);

                    if (rect_hero.IntersectsWith(rect_coin) && rect.Visibility != Visibility.Hidden)
                    {
                        coins++;
                        rect.Visibility = Visibility.Hidden;
                    }
                }
                if ((string)rect.Tag == "portal")
                {
                    Rect rect_hero = new Rect(Canvas.GetLeft(hero), Canvas.GetTop(hero),
                        hero.Width, hero.Height);

                    Rect rect_portal = new Rect(Canvas.GetLeft(rect), Canvas.GetTop(rect),
                        rect.Width, rect.Height);

                    if (rect_hero.IntersectsWith(rect_portal) && coins == 3)
                    {
                        MessageBox.Show("Вы прошли уровень!");
                        this.Close();
                    }
                }
            }

            if (goRight && Canvas.GetLeft(hero) + hero.Width <= 370)
                Canvas.SetLeft(hero, Canvas.GetLeft(hero) + speed);
            if (goLeft && Canvas.GetLeft(hero) > 0)
                Canvas.SetLeft(hero, Canvas.GetLeft(hero) - speed);
        }

        private void MyCanvas_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left)
                goLeft = true;
            else if (e.Key == Key.Right)
                goRight = true;
        }

        private void MyCanvas_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left)
                goLeft = false;
            else if (e.Key == Key.Right)
                goRight = false;
        }
    }
}

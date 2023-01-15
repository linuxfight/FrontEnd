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

namespace FlyApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool isClicked = false;
        private int clickCount = 0;
        private Random random = new Random();
        private double x, y = -50;

        private Stopwatch stopwatch = Stopwatch.StartNew();
        private TimeSpan prevTime;

        public MainWindow()
        {
            InitializeComponent();

            Move();

            prevTime = stopwatch.Elapsed;

            CompositionTarget.Rendering += CompositionTarget_Rendering;
        }

        private void CompositionTarget_Rendering(object sender, EventArgs e)
        {
            info.Content = Math.Round(stopwatch.Elapsed.TotalSeconds - prevTime.TotalSeconds);
            score.Content = $"Score: {clickCount}";

            if (stopwatch.Elapsed.TotalSeconds - prevTime.TotalSeconds > 15)
            {
                MessageBox.Show($"You Lose! Score: {clickCount}");
                this.Close();
            }

            if (isClicked)
            {
                Move();
                isClicked = false;
                prevTime = stopwatch.Elapsed;
            }
        }

        private void fly_Click(object sender, RoutedEventArgs e)
        {
            isClicked = true;
            clickCount++;
        }

        private void Move()
        {
            x = random.Next(0, Convert.ToInt32(750 - fly.ActualHeight));
            y = random.Next(0, Convert.ToInt32(400 - fly.ActualWidth));

            Canvas.SetTop(fly, y);
            Canvas.SetLeft(fly, x);
        }
    }
}

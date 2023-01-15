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

namespace RenderingApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private double speedX = 200; //сколько пикселей за 1 секунду
        private double speedY = 100;
        private double x = 0;
        private double y = 0;

        private TimeSpan prevTime;
        private Stopwatch stopwatch = Stopwatch.StartNew();

        public MainWindow()
        {
            InitializeComponent();

            prevTime = stopwatch.Elapsed;

            CompositionTarget.Rendering += CompositionTarget_Rendering;
        }

        private void CompositionTarget_Rendering(object sender, EventArgs e)
        {
            var currentTime = stopwatch.Elapsed;
            var delta = (currentTime - prevTime).TotalSeconds;
            prevTime = currentTime;

            lblInfo.Content = delta;

            UpdateBall(delta);
        }

        private void UpdateBall(double delta)
        {
            x = x + speedX * delta;
            y = y + speedY * delta;

            if (x < 0)
            {
                speedX = -speedX;
                x = 0;
            }
            else if (x > MyCanvas.ActualWidth - ball.ActualWidth)
            {
                speedX = -speedX;
                x = MyCanvas.ActualWidth - ball.ActualWidth;
            }
            if (y < 0)
            {
                speedY = -speedY;
                y = 0;
            }
            else if (y > MyCanvas.ActualHeight - ball.ActualHeight)
            {
                speedY = -speedY;
                y = MyCanvas.ActualHeight - ball.ActualHeight;
            }

            Canvas.SetLeft(ball, x);
            Canvas.SetTop(ball, y);
        }
    }
}

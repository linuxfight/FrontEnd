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

using System.Windows.Threading; // timer

namespace MoveObjectTimer
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static int left = 0;
        static int speed = 5;

        public MainWindow()
        {
            InitializeComponent();

            left = (int)ball.Margin.Left;

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(10);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            ball.Margin = new Thickness(left, 0, 0, 20);
            left += speed;

            if (left + ball.Width > 800 - 20)
            {
                speed = -5;
            }

            if (left < 20)
            {
                speed = 5;
            }
        }
    }
}

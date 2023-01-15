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
using System.Threading;

namespace MultiThreading
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public delegate void MyAction(int num);

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Thread thread = new Thread(x =>
            {
                Dispatcher.Invoke((Action)(() =>
                {
                    lblResult.Content = (int)x * (int)x;
                }));
            });

            /*Dispatcher.BeginInvoke((MyAction)(x =>
            {
                lblResult.Content = (int)x * (int)x;
            }), 4);*/

            thread.Start(4);
        }
    }
}

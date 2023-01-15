using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace CalcAppAsyncMultithread
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public delegate void MyAction();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void AsyncButton_Click(object sender, RoutedEventArgs e)
        {
            Dispatcher.BeginInvoke((MyAction)(() =>
            {
                int a, b = 0;

                if (int.TryParse(TextBox1.Text, out a) && int.TryParse(TextBox2.Text, out b))
                {
                    int result = a + b;
                    LabelResult.Content = $"Асинхронный результат: {result}";
                }
                else
                {
                    LabelResult.Content = "Асинхронный результат: Ошибка";
                }
            }));
        }

        private void ThreadButton_Click(object sender, RoutedEventArgs e)
        {
            Thread thread = new Thread(() =>
            {
                Dispatcher.Invoke((MyAction)(() =>
                {
                    int a, b = 0;

                    if (int.TryParse(TextBox1.Text, out a) && int.TryParse(TextBox2.Text, out b))
                    {
                        int result = a + b;
                        LabelResult.Content = $"Многопоточный результат: {result}";
                    }
                    else
                    {
                        LabelResult.Content = "Многопоточный результат: Ошибка";
                    }
                }));
            });

            thread.Start();
        }
    }
}

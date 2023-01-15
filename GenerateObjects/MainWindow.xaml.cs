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

namespace GenerateObjects
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool IsDragging = false;
        private Rectangle DragObject = null;
        private Point PreviousLocation = new Point();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void MyCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.Source is Rectangle)
            {
                Rectangle sourceRectangle = e.Source as Rectangle;
                MyCanvas.Children.Remove(sourceRectangle);
            }
            else
            {
                Rectangle rectangle = new Rectangle()
                {
                    Width = 50,
                    Height = 50,
                    Fill = Brushes.RoyalBlue
                };
                Canvas.SetLeft(rectangle, Mouse.GetPosition(MyCanvas).X - rectangle.Width / 2);
                Canvas.SetTop(rectangle, Mouse.GetPosition(MyCanvas).Y - rectangle.Height / 2);
                MyCanvas.Children.Add(rectangle);
            }
        }

        private void MyCanvas_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.Source is Rectangle)
            {
                IsDragging = true;
                DragObject = e.Source as Rectangle;
                PreviousLocation = Mouse.GetPosition(MyCanvas);
            }
        }

        private void MyCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (!IsDragging)
                return;
            Canvas.SetLeft(DragObject, Mouse.GetPosition(MyCanvas).X - DragObject.Width / 2);
            Canvas.SetTop(DragObject, Mouse.GetPosition(MyCanvas).Y - DragObject.Height / 2);
            System.Diagnostics.Debug.WriteLine(Mouse.GetPosition(MyCanvas).X);
        }

        private void MyCanvas_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (!IsDragging)
                return;
            if (Mouse.GetPosition(MyCanvas).X - DragObject.Width / 2 < 400)
            {
                Canvas.SetLeft(DragObject, PreviousLocation.X);
                Canvas.SetTop(DragObject, PreviousLocation.Y);
            }
            IsDragging = false;
            DragObject = null;
            PreviousLocation = new Point();
        }
    }
}

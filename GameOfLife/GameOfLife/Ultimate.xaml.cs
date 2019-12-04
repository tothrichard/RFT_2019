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
using System.Windows.Shapes;
using System.Windows.Threading;

namespace GameOfLife
{
    /// <summary>
    /// Interaction logic for Ultimate.xaml
    /// </summary>
    public partial class Ultimate : Window
    {
        private int time = 240;
        private DispatcherTimer Timer2;
        public Ultimate()
        {

            InitializeComponent();
            Timer2 = new DispatcherTimer();                     //ÚJ ELEM 
            Timer2.Interval = new TimeSpan(0, 0, 1);            //ÚJ ELEM 
            Timer2.Tick += Timer_Tick2;                         //ÚJ ELEM 
            Timer2.Start();                                     //ÚJ ELEM 
            Random cube = new Random();

            Board.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
            Board.Arrange(new Rect(0.0, 0.0, Board.DesiredSize.Width, Board.DesiredSize.Height));

            for (int i = 0; i < cellwide; i++) //a ciklus feltölti a gridet
            {
                for (int j = 0; j < cellhigh; j++)
                {
                    Rectangle r = new Rectangle();
                    r.Width = Board.ActualWidth / cellwide - 2.0; //aktuális szélesség
                    r.Height = Board.ActualHeight / cellhigh - 2.0; //aktuális magasság
                    r.Fill = (cube.Next(0, 2) == 1) ? Brushes.MediumBlue : Brushes.Yellow; //kitöltés
                    Board.Children.Add(r);
                    Canvas.SetLeft(r, j * Board.ActualWidth / cellwide);
                    Canvas.SetTop(r, i * Board.ActualHeight / cellhigh);
                    r.MouseDown += R_MouseDown;

                    area[i, j] = r;
                }
            }

            timer.Interval = TimeSpan.FromSeconds(0.1);
            timer.Tick += Timer_Tick;
        }
        void Timer_Tick2(object sender, EventArgs e)            //ÚJ ELEM 
        {
            if (time > 0)
            {
                if (time <= 10)
                {
                    if (time % 2 == 0)
                    {
                        TBCountDown.Foreground = Brushes.MediumBlue;
                    }
                    else
                    {
                        TBCountDown.Foreground = Brushes.Yellow;

                    }
                    time--;
                    TBCountDown.Text = string.Format("00:0{0}:0{1}", time / 60, time % 60);
                }
                else
                {
                    time--;
                    TBCountDown.Text = string.Format("00:0{0}:{1}", time / 60, time % 60);
                }
            }
            else
            {
                Timer2.Stop();
                Win game = new Win();
                game.Show();
                this.Close();
            }
        }

        const int cellwide = 45;
        const int cellhigh = 45;
        Rectangle[,] area = new Rectangle[cellwide, cellhigh];
        DispatcherTimer timer = new DispatcherTimer();

        private void R_MouseDown(object sender, MouseButtonEventArgs e) //gomblenyomásra színt változtat
        {
            ((Rectangle)sender).Fill =
                (((Rectangle)sender).Fill == Brushes.MediumBlue) ? Brushes.Yellow : Brushes.MediumBlue;
        }

        private void Timer_Tick(object sender, EventArgs e) //vizsgálja a táblát
        {
            int[,] neighbors = new int[cellhigh, cellwide];
            for (int i = 0; i < cellhigh; i++)
            {
                for (int j = 0; j < cellwide; j++)
                {
                    int top = i - 1;
                    if (top < 0)
                    { top = cellhigh - 1; }
                    int iRight = i + 1;
                    if (iRight >= cellhigh)
                    { iRight = 0; }
                    int jLeft = j - 1;
                    if (jLeft < 0)
                    { jLeft = cellwide - 1; }
                    int jRight = j + 1;
                    if (jRight >= cellwide)
                    { jRight = 0; }

                    int a = 0;

                    //ez vizsgálja hány szomszédja van 
                    if (area[top, jLeft].Fill == Brushes.Yellow)
                    { a++; }
                    if (area[top, j].Fill == Brushes.Yellow)
                    { a++; }
                    if (area[top, jRight].Fill == Brushes.Yellow)
                    { a++; }
                    if (area[i, jLeft].Fill == Brushes.Yellow)
                    { a++; }
                    if (area[i, jRight].Fill == Brushes.Yellow)
                    { a++; }
                    if (area[iRight, jLeft].Fill == Brushes.Yellow)
                    { a++; }
                    if (area[iRight, j].Fill == Brushes.Yellow)
                    { a++; }
                    if (area[iRight, jRight].Fill == Brushes.Yellow)
                    { a++; }

                    neighbors[i, j] = a;
                }
            }

            //ez vizsgálja a játék szabálya szerint a játékot  
            for (int i = 0; i < cellhigh; i++)
            {
                for (int j = 0; j < cellwide; j++)
                {
                    if (neighbors[i, j] < 2 || neighbors[i, j] > 3)
                    {
                        area[i, j].Fill = Brushes.MediumBlue;
                    }
                    else if (neighbors[i, j] == 3)
                    {
                        area[i, j].Fill = Brushes.Yellow;
                    }
                }
            }
        }

        private void buttonStartStop_Click(object sender, RoutedEventArgs e)
        {
            if (timer.IsEnabled)
            {
                timer.Stop();
                buttonStartStop.Content = "Start!";
            }
            else
            {
                timer.Start();
                buttonStartStop.Content = "Stop!";

            }
        }

        private void check_Click(object sender, RoutedEventArgs e)
        {
            Levels level = new Levels();
            level.Show();
            Hide();
        }

        private void menu_Click(object sender, RoutedEventArgs e)
        {
            MainWindow menu = new MainWindow();
            menu.Show();
            Hide();
        }
    }
}

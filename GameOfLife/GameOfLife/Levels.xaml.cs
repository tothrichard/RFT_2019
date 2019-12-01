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

namespace GameOfLife
{
    /// <summary>
    /// Interaction logic for Levels.xaml
    /// </summary>
    public partial class Levels : Window
    {
        public Levels()
        {
            InitializeComponent();
        }

        private void Easy_Click(object sender, RoutedEventArgs e)
        {
            Easy game = new Easy();
            game.Show();
            Hide();
        }

        private void Medium_Click(object sender, RoutedEventArgs e)
        {
            Medium game = new Medium();
            game.Show();
            Hide();

        }

        private void Hard_Click(object sender, RoutedEventArgs e)
        {
            Hard game = new Hard();
            game.Show();
            Hide();

        }

        private void Expert_Click(object sender, RoutedEventArgs e)
        {
            Expert game = new Expert();
            game.Show();
            Hide();

        }

        private void Legend_Click(object sender, RoutedEventArgs e)
        {
            Legend game = new Legend();
            game.Show();
            Hide();

        }

        private void Ultimate_Click(object sender, RoutedEventArgs e)
        {
            Ultimate game = new Ultimate();
            game.Show();
            Hide();

        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow menu = new MainWindow();
            menu.Show();
            Hide();
        }
    }
}

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
using Microsoft.VisualBasic;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Boolean isGameRunning = false;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void GameStart (object sender, RoutedEventArgs e)
        {
            if(!isGameRunning)
            {
                GameState gameState = new GameState(this);
                isGameRunning = true;
            }
        }

        private void clickDraw (object sender, RoutedEventArgs e)
        {
            if (isGameRunning)
            {

            }
            else
            {
                return;
            }
        }

        private void GameLoad (object sender, RoutedEventArgs e)
        {

        }

        private void GameSave (object sender, RoutedEventArgs e)
        {

        }
    }
}

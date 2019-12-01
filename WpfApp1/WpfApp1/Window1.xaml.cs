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

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1(int numberType, String playerName, int value)
        {
            InitializeComponent();
            if(numberType == 0)
            {
                Instructions.Text = playerName + "! Pick which pawn to move " + value + " spaces!";
            }
        }

        /*Ok so here is all the stuff to submit from the form*/
        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

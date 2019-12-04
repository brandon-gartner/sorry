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
        public Pawn[] allPawns;
        public Pawn[] otherPawns;
        public Pawn gotPawn;
        public Pawn otherPlayerPawn;
        int numberType;
        String card11Choice;
        public String card7Choice;
        public String card10Choice;
        public int move7;
        public MainWindow main;
        public int value;
        public String playerName;
        public Window1(int numberType, String playerName, int value, Pawn[] allPawns, Pawn[] otherPawns, MainWindow main)
        {
            this.playerName = playerName;
            this.value = value;
            this.main = main;
            this.move7 = value;
            this.allPawns = allPawns;
            this.otherPawns = otherPawns;
            this.numberType = numberType;
            InitializeComponent();
            //for any normal movement
            if(numberType == 0)
            {
                Choice_enem.Visibility = Visibility.Hidden;
                Instructions.Text = playerName + "! Pick which pawn to move " + value + " spaces (or move out of home if not 7)!";
                if(value == 11)
                {
                    Instructions.Text = playerName + "! Sorry no pawns can be exchanged! Pick which pawn to move.";
                }
                for(int i = 0; i < allPawns.Length; i++)
                {
                    Choice.Items.Add(allPawns[i].pawnToString());
                    
                }

            }
            //for card sorry
            else if(numberType == 2)
            {
                this.otherPawns = otherPawns;
                Instructions.Text = playerName + "! Pick which pawn you want to switch with what other players' pawn! (Sorry Card)";
                for (int i = 0; i < allPawns.Length; i++)
                {
                    Choice.Items.Add(allPawns[i].pawnToString());
                }

                for (int i = 0; i < otherPawns.Length; i++)
                {
                    Choice_enem.Items.Add(otherPawns[i].pawnToString());
                }

                
            }
            //for card 11 (part 1)
            else if(numberType == 11)
            {
                Choice_enem.Visibility = Visibility.Hidden;
                Instructions.Text = playerName + ", do you want to switch with another player or do you want to advance 11 spaces or forfeit turn?";
                Choice.Items.Add("Switch");
                Choice.Items.Add("Advance 11 spaces");
                Choice.Items.Add("Forfeit");
            }
            //for card 11(switch)
            else if(numberType == 4)
            {
                Instructions.Text = playerName + ", please pick an available pawn to switch with another available pawn. (11 Switch)";
                for (int i = 0; i < allPawns.Length; i++)
                {
                    Choice.Items.Add(allPawns[i].pawnToString());
                }

                for (int i = 0; i < otherPawns.Length; i++)
                {
                    Choice_enem.Items.Add(otherPawns[i].pawnToString());
                }
            }
            /*
            //for card 1 and 2
            else if(numberType == 3)
            {
                Instructions.Text = "Pick a pawn to get out of home or to move forward " + value + " spaces!";
                Choice_enem.Visibility = Visibility.Hidden;
                //Instructions.Text = playerName + " Do you want to switch whith another player or do you want to advance 11 spaces";
                for(int i = 0; i < allPawns.Length; i++)
                {
                    Choice.Items.Add(allPawns[i].pawnToString());
                }
                this.numberType = 0;
            }
            */
            //for card 7
            else if(numberType == 6)
            {
                Choice.Visibility = Visibility.Hidden;
                Instructions.Text = playerName + ", do you want to move one pawn by 7 spaces, or split the movement across 2 pawns? (Card 7)";
                Choice_enem.Items.Add("Put all 7 on one pawn");
                Choice_enem.Items.Add("Separate between 2 pawns");
            }
            //for card 10
            else if(numberType == 10)
            {
                Choice_enem.Visibility = Visibility.Hidden;
                Instructions.Text = playerName + ", do you want to move one pawn by 10 spaces, or go back 1 space? (Card 10)";
                Choice.Items.Add("Move a pawn forward 10 spaces");
                Choice.Items.Add("Move a pawn backward 1 space");
            }
            //For card 7 part 2
            else
            {

                //This if is to see if it's the first or second choice (you have to get rid of the first pawn)
                if(this.move7 == 7)
                {
                    Instructions.Text = playerName + "! Pick the first pawn you want to move and by how many spaces";
                    for (int i = 0; i < allPawns.Length; i++)
                    {
                        Choice.Items.Add(allPawns[i].pawnToString());
                    }
                    for (int i = 1; i < this.move7; i++)
                    {
                        Choice_enem.Items.Add(i);
                    }
                }
                else
                {
                    Instructions.Text = playerName + "! Pick the pawn you want to move for the rest of the spaces";

                    for (int i = 0; i < allPawns.Length; i++)
                    {
                        Choice.Items.Add(allPawns[i].pawnToString());
                    }
                    Choice_enem.Visibility = Visibility.Hidden;
                }

                
            }

        }

        /*Ok so here is all the stuff to submit from the form*/
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Generic
            if(this.numberType == 0)
            {
                String inputtedText = Choice.Text;
                for (int i = 0; i < allPawns.Length; i++)
                {
                    if (allPawns[i].pawnToString().Equals(inputtedText))
                    {
                        this.gotPawn = allPawns[i];
                    }
                }
                this.main.genericHelper(this, this.value);
                this.Close();
            }
            //Sorry
            else if(this.numberType == 2 || this.numberType == 4)
            {
                String inputtedPlayerPawn = Choice.Text;
                String inputtedEnemPawn = Choice_enem.Text;
                for (int i = 0; i < allPawns.Length; i++)
                {
                    if (allPawns[i].pawnToString().Equals(inputtedPlayerPawn))
                    {
                        this.gotPawn = allPawns[i];
                    }
                }
                for (int i = 0; i < this.otherPawns.Length; i++)
                {
                    if(otherPawns[i].pawnToString().Equals(inputtedEnemPawn))
                    {
                        this.otherPlayerPawn = otherPawns[i];
                    }
                }

                this.main.sorryAnd11Helper(this, this.value);
                this.Close();
            }
            //Card 11 intial choice
            else if (this.numberType == 11)
            {
                card11Choice = Choice.Text;
                this.main.only11Helper(this, this.value);
                this.Close();

            }
            //Card 7
            else if(this.numberType == 6)
            {
                card7Choice = Choice_enem.Text;
                this.main._7Helper(this);
                this.Close();
            }
            //This is for when splitting the pawn into 2
            else if(this.numberType == 7)
            {
                if(this.value == 7)
                {
                    this.move7 = Convert.ToInt32(Choice_enem.Text);
                }

                String inputtedText = Choice.Text;
                for (int i = 0; i < allPawns.Length; i++)
                {
                    if (allPawns[i].pawnToString().Equals(inputtedText))
                    {
                        this.gotPawn = allPawns[i];
                    }
                }
                this.main.__7HelperPart2(this);
                this.Close();
            }
            else if(this.numberType == 10)
            {
                this.card10Choice = Choice.Text;
                this.main._10Helper(this);
                this.Close();
            }
            this.Close();

        }
        public Pawn getSelectedPawn()
        {
            return gotPawn;
        }
        public String getChoice11()
        {
            return card11Choice;
        }

        public String getChoice7()
        {
            return card7Choice;
        }
    }
}

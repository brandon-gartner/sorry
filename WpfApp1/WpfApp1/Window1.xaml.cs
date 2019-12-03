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
        Pawn[] allPawns;
        Pawn[] otherPawns;
        public Pawn gotPawn;
        public Pawn otherPlayerPawn;
        int numberType;
        String card11Choice;
        String card1Choice;
        String card2Choice;
        public String card7Choice;
        public int move7;
        MainWindow main;
        public int value;
        public Window1(int numberType, String playerName, int value, Pawn[] allPawns, Pawn[] otherPawns, MainWindow main)
        {
            this.value = value;
            this.main = main;
            this.move7 = value;
            this.allPawns = allPawns;
            this.numberType = numberType;
            InitializeComponent();
            //for any normal mouvement
            if(numberType == 0)
            {
                Choice_enem.Visibility = Visibility.Hidden;
                Instructions.Text = playerName + "! Pick which pawn to move " + value + " spaces!";
                for(int i = 0; i < allPawns.Length; i++)
                {
                    Choice.Items.Add(allPawns[i].pawnToString());
                    
                }

            }
            //for card sorry
            else if(numberType == 2)
            {
                this.otherPawns = otherPawns;
                Instructions.Text = playerName + "! Pick which pawn you want to switch with what other players' pawn";
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
                Instructions.Text = playerName + " Do you want to switch whith another player or do you want to advance 11 spaces or forfeit turn?";
                Choice_enem.Items.Add("switch");
                Choice_enem.Items.Add("advance 11 spaces");
                Choice_enem.Items.Add("Forfeit");
            }
            //for card 11(switch)
            else if(numberType == 4)
            {
                Instructions.Text = playerName + ", please pick an available pawn to switch with another available pawn.";
                for (int i = 0; i < allPawns.Length; i++)
                {
                    Choice.Items.Add(allPawns[i].pawnToString());
                }

                for (int i = 0; i < otherPawns.Length; i++)
                {
                    Choice_enem.Items.Add(otherPawns[i].pawnToString());
                }
            }
            //for card 1
            else if(numberType == 3)
            {
                Choice_enem.Visibility = Visibility.Hidden;
                //Instructions.Text = playerName + " Do you want to switch whith another player or do you want to advance 11 spaces";
                Choice_enem.Items.Add("Get a pawn out of the start zone");
                Choice_enem.Items.Add("Move 1 space");
            }
            //for card 2
            else if (numberType == 5)
            {
                Choice_enem.Visibility = Visibility.Hidden;
                //Instructions.Text = playerName + " Do you want to switch whith another player or do you want to advance 11 spaces";
                Choice_enem.Items.Add("Get a pawn out of the start zone");
                Choice_enem.Items.Add("Move 2 space");
            }
            //for card 7
            else if(numberType == 6)
            {
                Choice.Visibility = Visibility.Hidden;
                //Instructions.Text = playerName + " Do you want to switch whith another player or do you want to advance 11 spaces";
                Choice_enem.Items.Add("Put all 7 on one pawn");
                Choice_enem.Items.Add("seperate between 2 pawns");
            }
            else
            {

                //This if is to see if it's the first or second choice (you have to get rid of the first pawn)
                if(this.move7 != 0)
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
                card11Choice = Choice_enem.Text;

            }
            //Card 7
            else if(this.numberType == 6)
            {
                card7Choice = Choice_enem.Text;
            }
            //This is for when splitting the pawn into 2
            else if(this.numberType == 7)
            {
                this.move7 = Convert.ToInt32(Choice_enem.Text);
                String inputtedText = Choice.Text;
                for (int i = 0; i < allPawns.Length; i++)
                {
                    if (allPawns[i].pawnToString().Equals(inputtedText))
                    {
                        this.gotPawn = allPawns[i];
                    }
                }
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

        public String getChoice1()
        {
            return card1Choice;
        }

        public String getChoice2()
        {
            return card2Choice;
        }

        public String getChoice7()
        {
            return card7Choice;
        }
    }
}

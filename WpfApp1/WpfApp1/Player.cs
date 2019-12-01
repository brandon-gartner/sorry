using System;
using WpfApp1;
using System.Windows.Controls;
using System.Threading.Tasks;

namespace WpfApp1
{
    [Serializable]
    public class Player
    {
        public String PlayerName { get; set; }
        public ISpace[] SafetySpaceAndHome = new ISpace[6];
        public Pawn[] pawns;
        public Boolean realPlayer;
        public MainWindow main;
        public String color;
        public int playerNumber;



        /*So the constructor sets the player's name as well as which color they chose */
        public Player(string playerName, String color, MainWindow main, int playerNumber)
        {
            this.color = color;
            this.playerNumber = playerNumber;
            this.main = main;
            this.pawns = new Pawn[3];
            this.PlayerName = playerName;
            for (int i = 0; i < 3; i++)
            {
                this.pawns[i] = new Pawn(i, color, this.playerNumber);
            }
            InitialisePlayersBoard();
            realPlayer = true;
        }

        //if it is an AI player
        public Player(String color)
        {
            this.pawns = new Pawn[3];
            for (int i = 0; i <= 3; i++)
            {
                this.pawns[i] = new Pawn(i+1, color, this.playerNumber);
            }
            this.PlayerName = "CPU";
            InitialisePlayersBoard();
        }

        public void InitialisePlayersBoard()
        {
            for (int i = 0; i < 5; i++)
            {
                this.SafetySpaceAndHome[i] = new SafetySpace(this);
            }
            this.SafetySpaceAndHome[5] = new HomeSpace();
        }


        public Boolean PlayTurn()
        {
            Boolean hasWon = true;

            for (int i = 0; i < 3; i++)
            {
                if (this.pawns[i].decommissioned)
                {
                    hasWon = false;
                }
            }
            main.DrawCard.IsEnabled = true;

            while(!endedTurn)
            {
                
            }
            return hasWon;
        }

        /*This method is for actually drawing everything relating to the player on the xaml page*/
        public void DrawPlayer()
        {
            /*So we'll have to add a couple of things (like displaying the names and such for the board) for now the pawns are ok*/

            /*Drawing the pawns*/
            for (int i = 0; i < this.pawns.Length; i++)
            {
                if (this.color.Equals("Red"))
                {
                    Grid.SetRow(this.pawns[i].image, 2);
                    Grid.SetColumn(this.pawns[i].image, 4);
                    this.main.MainGrid.Children.Add(this.pawns[i].image);
                }
                else if (this.color.Equals("Blue"))
                {
                    Grid.SetRow(this.pawns[i].image, 4);
                    Grid.SetColumn(this.pawns[i].image, 13);
                    this.main.MainGrid.Children.Add(this.pawns[i].image);
                }
                else if (this.color.Equals("Yellow"))
                {
                    Grid.SetRow(this.pawns[i].image, 11);
                    Grid.SetColumn(this.pawns[i].image, 2);
                    this.main.MainGrid.Children.Add(this.pawns[i].image);
                }
                else
                {
                    Grid.SetRow(this.pawns[i].image, 13);
                    Grid.SetColumn(this.pawns[i].image, 11);
                    this.main.MainGrid.Children.Add(this.pawns[i].image);
                }
            }
        }

        public void drawAtNextPosition(int pawnNumber)
        {
            //Setting the row and column numbers by checking which position it's at
            int nextPosition = this.pawns[pawnNumber].spaceNumber;
            int rowNum;
            int colNum;

            //Checking absolute positions of all pawns
            if(nextPosition <= 15)
            {
                rowNum = 0;
                colNum = nextPosition;
            }
            else if(nextPosition > 15 && nextPosition <= 30)
            {
                colNum = 15;
                rowNum = nextPosition - 15;
            }
            else if(nextPosition > 30 && nextPosition <= 45)
            {
                rowNum = 15;
                colNum = nextPosition - 30;

            }
        }
    }
}
using System;
using WpfApp1;



    public class Player
    {
        public String PlayerName { get; set; }
        public ISpace[] SafetySpaceAndHome;
        public Pawn[] pawns;



        /*So the constructor sets the player's name as well as which color they chose */
        public Player(string playerName, String color)
        {
            this.pawns = new Pawn[3];
            this.PlayerName = playerName;
            for (int i = 0; i < 3; i++)
            {
                this.pawns[i] = new Pawn(i, color);
            }



        }

        public void InitialisePlayersBoard()
        {
            for (int i = 0; i < 5; i++)
            {
                this.SafetySpaceAndHome[i] = new SafetySpace(this);
            }
            this.SafetySpaceAndHome[5] = new HomeSpace();
        }

        public void playTurn()
        {
            MainWindow.DrawCard.isEnabled = true;
        }
    }
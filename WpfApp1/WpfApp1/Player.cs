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
        public Space[] safetySpaces = new Space[5];
        public Pawn[] pawns;
        public Boolean realPlayer;
        public String color;
        public int playerNumber;
        //public Boolean endedTurn;



        /*So the constructor sets the player's name as well as which color they chose */
        public Player(String playerName, String color, int playerNumber)
        {
            this.color = color;
            this.playerNumber = playerNumber;
            this.pawns = new Pawn[3];
            this.PlayerName = playerName;
            for (int i = 0; i < 3; i++)
            {
                this.pawns[i] = new Pawn(i+1, color, this.playerNumber, this.PlayerName);
            }
            this.realPlayer = true;
            InitialisePlayersBoard();
        }

        //if it is a null player
        public Player(String color)
        {
            this.pawns = new Pawn[3];
            for (int i = 0; i < 3; i++)
            {
                this.pawns[i] = new Pawn(i+1, color, this.playerNumber, this.PlayerName);
            }
            this.PlayerName = "nullPlayer";
            this.realPlayer = false;
            InitialisePlayersBoard();
        }

        //if it is a CPU player
        public Player(String color, Boolean AI)
        {
            if (AI)
            {
                this.pawns = new Pawn[3];
                for (int i = 0; i < 3; i++)
                {
                    this.pawns[i] = new Pawn(i + 1, color, this.playerNumber, this.PlayerName);
                }
                this.PlayerName = "AI";
                InitialisePlayersBoard();
            }
            else
            {
                throw new ArgumentException("generate the AI with this true");
            }
            
        }

        public void InitialisePlayersBoard()
        {
            for (int i = 1; i < safetySpaces.Length - 1; i++)
            {
                this.safetySpaces[i] = new Space(4, this);
            }
            this.safetySpaces[4] = new Space(2);
        }



        //This is actually useless
        /*
        public Boolean PlayTurn()
        {
            Boolean hasWon = true;

            main.DrawCard.IsEnabled = true;
            this.endedTurn = false;

            while(!this.endedTurn)
            {
                
            }
            for (int i = 0; i < 3; i++)
            {
                if (!(this.pawns[i].decommissioned))
                {
                    hasWon = false;
                }
            }
            return hasWon;
        }
        */


    }
}
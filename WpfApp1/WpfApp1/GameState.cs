using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Microsoft.VisualBasic;
using WpfApp1;

namespace WpfApp1
{
    public class GameState
    {

        public int playerCount = 0;
        public Player[] players;
        public MainWindow main;
        public Dictionary<int, Border> numberToSpace = new Dictionary<int, Border>();
        public Dictionary<Border, int> spaceToNumber = new Dictionary<Border, int>();
        public Deck deck;
        public int currentPlayer;
        public Board mainBoard;
        public GameState(MainWindow main)
        {
            this.main = main;
            GenerateDictionaries();
            /*Creating the three players and getting their names and such*/
            String playerCountInput;
            do
            {
                playerCountInput = Interaction.InputBox("How many players are there?", "Player Count", "Please enter a number between 2 and 4");
                playerCount = Convert.ToInt32(playerCountInput);
            } while (playerCount <= 1 || playerCount >= 5);

            players = new Player[playerCount];
            /*Asking their names and what colour they want (probably the easiest way to do this is to use the same idea 
              as for the player names but correspond an integer with a color(here i just hardcoded it for ease of use*/
            String[] colors = { "Blue", "Red", "Green", "Yellow" };

            for (int i = 0; i < playerCount; i++)
            {
                String playerName = Interaction.InputBox("What is player " + i + "'s name?", "Name?");
                players[i] = new Player(playerName, colors[i], main, i+1);
            }

            /*Creating board (locations)*/
            this.mainBoard = new Board(players, this.main);

            /*Drawing players*/
            for (int i = 0; i < this.players.Length; i++)
            {
                this.players[i].DrawPlayer();
            }

            //Creating deck
            this.deck = new Deck();
            this.deck.shuffle();


        }

        //runs the game, until a player wins.  playTurn will constantly set ended to false, until a player wins
        public void Run()
        {
            this.currentPlayer = -1;
            Boolean ended = false;
            for (int playerTurn = 0; ended; playerTurn++)
            {
                this.currentPlayer = playerTurn % players.Length;
                ended = players[this.currentPlayer].PlayTurn();

            }

            MessageBox.Show("Player " + this.currentPlayer + " wins!");
        }


        //creates the dictionaries we will be using to easily get between space position and actualy border location on the XAML file
        public void GenerateDictionaries()
        {
            numberToSpace.Add(0, main._0);
            numberToSpace.Add(1, main._1);
            numberToSpace.Add(2, main._2);
            numberToSpace.Add(3, main._3);
            numberToSpace.Add(4, main._4);
            numberToSpace.Add(5, main._5);
            numberToSpace.Add(6, main._6);
            numberToSpace.Add(7, main._7);
            numberToSpace.Add(8, main._8);
            numberToSpace.Add(9, main._9);
            numberToSpace.Add(10, main._10);
            numberToSpace.Add(11, main._11);
            numberToSpace.Add(12, main._12);
            numberToSpace.Add(13, main._13);
            numberToSpace.Add(14, main._14);
            numberToSpace.Add(15, main._15);
            numberToSpace.Add(16, main._16);
            numberToSpace.Add(17, main._17);
            numberToSpace.Add(18, main._18);
            numberToSpace.Add(19, main._19);
            numberToSpace.Add(20, main._20);
            numberToSpace.Add(21, main._21);
            numberToSpace.Add(22, main._22);
            numberToSpace.Add(23, main._23);
            numberToSpace.Add(24, main._24);
            numberToSpace.Add(25, main._25);
            numberToSpace.Add(26, main._26);
            numberToSpace.Add(27, main._27);
            numberToSpace.Add(28, main._28);
            numberToSpace.Add(29, main._29);
            numberToSpace.Add(30, main._30);
            numberToSpace.Add(31, main._31);
            numberToSpace.Add(32, main._32);
            numberToSpace.Add(33, main._33);
            numberToSpace.Add(34, main._34);
            numberToSpace.Add(35, main._35);
            numberToSpace.Add(36, main._36);
            spaceToNumber.Add(main._0, 0);
            spaceToNumber.Add(main._1, 1);
            spaceToNumber.Add(main._2, 2);
            spaceToNumber.Add(main._3, 3);
            spaceToNumber.Add(main._4, 4);
            spaceToNumber.Add(main._5, 5);
            spaceToNumber.Add(main._6, 6);
            spaceToNumber.Add(main._7, 7);
            spaceToNumber.Add(main._8, 8);
            spaceToNumber.Add(main._9, 9);
            spaceToNumber.Add(main._10, 10);
            spaceToNumber.Add(main._11, 11);
            spaceToNumber.Add(main._12, 12);
            spaceToNumber.Add(main._13, 13);
            spaceToNumber.Add(main._14, 14);
            spaceToNumber.Add(main._15, 15);
            spaceToNumber.Add(main._16, 16);
            spaceToNumber.Add(main._17, 17);
            spaceToNumber.Add(main._18, 18);
            spaceToNumber.Add(main._19, 19);
            spaceToNumber.Add(main._20, 20);
            spaceToNumber.Add(main._21, 21);
            spaceToNumber.Add(main._22, 22);
            spaceToNumber.Add(main._23, 23);
            spaceToNumber.Add(main._24, 24);
            spaceToNumber.Add(main._25, 25);
            spaceToNumber.Add(main._26, 26);
            spaceToNumber.Add(main._27, 27);
            spaceToNumber.Add(main._28, 28);
            spaceToNumber.Add(main._29, 29);
            spaceToNumber.Add(main._30, 30);
            spaceToNumber.Add(main._31, 31);
            spaceToNumber.Add(main._32, 32);
            spaceToNumber.Add(main._33, 33);
            spaceToNumber.Add(main._34, 34);
            spaceToNumber.Add(main._35, 35);
            spaceToNumber.Add(main._36, 36);
            numberToSpace.Add(37, main._37);
            spaceToNumber.Add(main._37, 37);
            numberToSpace.Add(38, main._38);
            spaceToNumber.Add(main._38, 38);
            numberToSpace.Add(39, main._39);
            spaceToNumber.Add(main._39, 39);
            numberToSpace.Add(40, main._40);
            spaceToNumber.Add(main._40, 40);
            numberToSpace.Add(41, main._41);
            spaceToNumber.Add(main._41, 41);
            numberToSpace.Add(42, main._42);
            spaceToNumber.Add(main._42, 42);
            numberToSpace.Add(43, main._43);
            spaceToNumber.Add(main._43, 43);
            numberToSpace.Add(44, main._44);
            spaceToNumber.Add(main._44, 44);
            numberToSpace.Add(45, main._45);
            spaceToNumber.Add(main._45, 45);
            numberToSpace.Add(46, main._46);
            spaceToNumber.Add(main._46, 46);
            numberToSpace.Add(47, main._47);
            spaceToNumber.Add(main._47, 47);
            numberToSpace.Add(48, main._48);
            spaceToNumber.Add(main._48, 48);
            numberToSpace.Add(49, main._49);
            spaceToNumber.Add(main._49, 49);
            numberToSpace.Add(50, main._50);
            spaceToNumber.Add(main._50, 50);
            numberToSpace.Add(51, main._51);
            spaceToNumber.Add(main._51, 51);
            numberToSpace.Add(52, main._52);
            spaceToNumber.Add(main._52, 52);
            numberToSpace.Add(53, main._53);
            spaceToNumber.Add(main._53, 53);
            numberToSpace.Add(54, main._54);
            spaceToNumber.Add(main._54, 54);
            numberToSpace.Add(55, main._55);
            spaceToNumber.Add(main._55, 55);
            numberToSpace.Add(56, main._56);
            spaceToNumber.Add(main._56, 56);
            numberToSpace.Add(57, main._57);
            spaceToNumber.Add(main._57, 57);
            numberToSpace.Add(58, main._58);
            spaceToNumber.Add(main._58, 58);
            numberToSpace.Add(59, main._59);
            spaceToNumber.Add(main._59, 59);

        }
        public Player[] GetPlayers()
        {
            return this.players;
        }

        public MainWindow GetMainWindow()
        {
            return this.main;
        }
        public int GetPlayerCount()
        {
            return this.playerCount;
        }

        //THIS ONLY WORKS FOR DRAWING THINGS AROUND ON THE ACTUAL BOARD
        public void drawAtNextPosition(Pawn pawn)
        {
            //Setting the row and column numbers by checking which position it's at
            int nextPosition = pawn.spaceNumber;
            int rowNum;
            int colNum;
            this.main.MainGrid.Children.Remove(pawn.image);

            //Checking absolute positions of all pawns
            if (nextPosition <= 15)
            {
                rowNum = 0;
                colNum = nextPosition;
            }
            else if (nextPosition > 15 && nextPosition <= 30)
            {
                colNum = 15;
                rowNum = nextPosition - 15;
            }
            else if (nextPosition > 30 && nextPosition <= 45)
            {
                rowNum = 15;
                colNum = (nextPosition - 30);
                colNum = (colNum + 15) - colNum;
            }
            else
            {
                colNum = 0;
                rowNum = (nextPosition - 30);
                rowNum = (rowNum + 15) - rowNum;

            }
            Grid.SetRow(pawn.image, rowNum);
            Grid.SetColumn(pawn.image, colNum);
            this.main.MainGrid.Children.Add(pawn.image);
        }

        public void drawAtStart(Pawn pawn)
        {
            Grid.SetRow(pawn.image, pawn.startPositionRow);
            Grid.SetColumn(pawn.image, pawn.startPositionCol);
            this.main.MainGrid.Children.Add(pawn.image);
        }
    }
}

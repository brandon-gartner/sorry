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
        public Deck deck;
        public int currentPlayer = -1;
        public Board mainBoard;
        public GameState(MainWindow main)
        {
            this.main = main;
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
            String[] colors = { "Red", "Blue", "Green", "Yellow" };

            for (int i = 0; i < playerCount; i++)
            {
                String playerName = Interaction.InputBox("What is player " + i + "'s name?  Please don't repeat names.", "Name?");
                Boolean repeatedName = false;
                for (int j = 0; j < i; j++)
                {
                    if (players[j].PlayerName.Equals(playerName))
                    {
                        repeatedName = true;
                    }
                }
                if (repeatedName)
                {
                    i--;
                    continue;
                }
                players[i] = new Player(playerName, colors[i], main, i+1);
            }

            /*Creating board (locations)*/
            this.mainBoard = new Board(players, this.main);

            /*Drawing players*/
            for (int i = 0; i < this.players.Length; i++)
            {
                for (int j = 0; j < this.players[i].pawns.Length; j++)
                {
                    Player tempPlayer = this.players[i];
                    Pawn currentPawn = tempPlayer.pawns[j];
                    if (tempPlayer.color.Equals("Red"))
                    {
                        Grid.SetRow(currentPawn.image, 2);
                        Grid.SetColumn(currentPawn.image, 4);
                        this.main.MainGrid.Children.Add(currentPawn.image);
                    }
                    else if (tempPlayer.color.Equals("Blue"))
                    {
                        Grid.SetRow(currentPawn.image, 4);
                        Grid.SetColumn(currentPawn.image, 13);
                        this.main.MainGrid.Children.Add(currentPawn.image);
                    }
                    else if (tempPlayer.color.Equals("Yellow"))
                    {
                        Grid.SetRow(currentPawn.image, 11);
                        Grid.SetColumn(currentPawn.image, 2);
                        this.main.MainGrid.Children.Add(currentPawn.image);
                    }
                    else
                    {
                        Grid.SetRow(currentPawn.image, 13);
                        Grid.SetColumn(currentPawn.image, 11);
                        this.main.MainGrid.Children.Add(currentPawn.image);
                    }
                }
            }

            //Creating deck
            this.deck = new Deck();
            this.deck.shuffle();


        }

        //Updates player number
        public void updatePlayer()
        {
            
            //for (int playerTurn = 0; !ended; playerTurn++)
            //{
            if (this.currentPlayer < this.playerCount)
            {
                this.currentPlayer++;
            }
            else
            {
                this.currentPlayer = 0;
            }


            //}

            //MessageBox.Show("Player " + this.currentPlayer + " wins!");
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
            pawn.inStart = true;
            pawn.spaceNumber = 99;
            Grid.SetRow(pawn.image, pawn.startPositionRow);
            Grid.SetColumn(pawn.image, pawn.startPositionCol);
            this.main.MainGrid.Children.Add(pawn.image);
        }

        //DRAWING RIGHT OUTSIDE THE START
        public void drawOutsideStart(Pawn pawn)
        {
            pawn.inStart = false;
            if(pawn.color.Equals("Red"))
            {
                pawn.spaceNumber = 4;
            }
            else if(pawn.color.Equals("Blue"))
            {
                pawn.spaceNumber = 19;
            }
            else if(pawn.color.Equals("Green"))
            {
                pawn.spaceNumber = 34;
            }
            else
            {
                pawn.spaceNumber = 49;
            }
            drawAtNextPosition(pawn);
        }
    }
}

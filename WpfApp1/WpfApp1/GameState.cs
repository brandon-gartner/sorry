using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Microsoft.VisualBasic;
using WpfApp1;

namespace WpfApp1
{
    [Serializable]
    public class GameState
    {

        public int playerCount = 0;
        public Player[] players;
        public Deck deck;
        public int currentPlayer = -1;
        public GameState(MainWindow main)
        {
            /*Creating the three players and getting their names and such*/
            String playerCountInput;
            do
            {
                playerCountInput = Interaction.InputBox("How many human players are there?  Please enter a number between 1 and 4.  If you selected 1, you must have an AI.  If you selected 4, you cannot.", "Human Player Count", "");
                playerCount = Convert.ToInt32(playerCountInput);
            } while (playerCount <= 0 || playerCount >= 5);

            String AIPlayer;
            int CountAI = 0;
            Boolean failed = false;
            do
            {
                failed = false;
                AIPlayer = Interaction.InputBox("How many AI players would you like?", "AI Player Count", "Please enter a number between 0 and 1.  If you selected 1, before, please select 1 now.  If you selected 4 before, select 0.");
                CountAI = Convert.ToInt32(AIPlayer);
                if (playerCount == 1 && CountAI != 1)
                {
                    failed = true;
                }
                else if (playerCount == 4 && CountAI != 0)
                {
                    failed = true;
                }
            } while (CountAI <= -1 || CountAI >= 2 || failed);

            players = new Player[playerCount + CountAI];
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
                players[i] = new Player(playerName, colors[i], i+1);
                
            }
            if (playerCount != players.Length)
            {
                players[players.Length - 1] = new Player(colors[players.Length - 1], true, players.Length - 1);
                MessageBox.Show(colors[3]);
                MessageBox.Show(Convert.ToString(players.Length));
            }

            //OK SO THE FOLLOWING HAS TO BE MOVED TO MAIN WINDOW.cs

            /*Creating board (locations)*/
            /*
            this.mainBoard = new Board(players, this.main);
            */
            /*Drawing players*/
            /*
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
            */
            //Creating deck
            this.deck = new Deck();
            this.deck.shuffle();


        }

        //Updates player number
        public void updatePlayer()
        {
            
            if (this.currentPlayer < this.playerCount - 1)
            {
                this.currentPlayer++;
            }
            else
            {
                this.currentPlayer = 0;
            }

        }

        public Player[] GetPlayers()
        {
            return this.players;
        }
        public int GetPlayerCount()
        {
            return this.playerCount;
        }

 

        /*THIS GOT MOVED TO MAINWINDOW*/
        //THIS ONLY WORKS FOR DRAWING THINGS AROUND ON THE ACTUAL BOARD
        /*
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
                colNum = 15 - colNum;
            }
            else
            {
                colNum = 0;
                rowNum = (nextPosition - 45);
                rowNum = 15 - rowNum;

            }
            Grid.SetRow(pawn.image, rowNum);
            Grid.SetColumn(pawn.image, colNum);
            this.main.MainGrid.Children.Add(pawn.image);
        }
        */
        /*
        public void drawAtStart(Pawn pawn)
        {
            this.main.MainGrid.Children.Remove(pawn.image);
            pawn.inStart = true;
            pawn.spaceNumber = 99;
            Grid.SetRow(pawn.image, pawn.startPositionRow);
            Grid.SetColumn(pawn.image, pawn.startPositionCol);
            this.main.MainGrid.Children.Add(pawn.image);
        }
        */
        /*
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
        */
    }
}

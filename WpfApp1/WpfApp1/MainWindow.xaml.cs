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
using WpfApp1;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Collections;

namespace WpfApp1
{
    
    
    [Serializable]
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //to be able to save the game state later
        SaveableGameState stateToSave;

        Boolean isGameRunning = false;

        //this is for the load
        Stream pubStream;
        SaveableGameState stateToLoad;

        //these next variable declarations are for when we load the data from the load method
        Player[] loadedPlayers;
        Dictionary<int, Border> loadedNumberToSpace;
        Dictionary<Border, int> loadedSpaceToNumber;
        int loadedPlayerCount;
        GameState gameState;

        //Rando

        public MainWindow()
        {
            InitializeComponent();
        }
        //this will start the game and initiate the gamestate
        private void GameStart (object sender, RoutedEventArgs e)
        {
            if(!isGameRunning)
            {
                gameState = new GameState(this);

                isGameRunning = true;
                gameState.Run();
            }
        }

        private void ClickDraw (object sender, RoutedEventArgs e)
        {
            //if the gams is not running then it will exit the method
            if (isGameRunning)
            {
                Card card = this.gameState.deck.getNextCard();
                activateCard(card.getCard_Id(), gameState.currentPlayer);

            }
            else
            {
                return;
            }
        }
        //this will load the game
        private void GameLoad (object sender, RoutedEventArgs e)
        {
            //check if the game is runnig and if not display a message
            if (isGameRunning)
            {
                BinaryFormatter load = new BinaryFormatter();
                this.pubStream = new FileStream(@".\save.txt", FileMode.Open, FileAccess.Read);
                this.stateToLoad = (SaveableGameState)load.Deserialize(this.pubStream);
                this.pubStream.Close();

                loadedPlayers = stateToLoad.GetPlayers();
                loadedPlayerCount = stateToLoad.GetPlayerCount();
            }
            else
            {
                MessageBox.Show("The game has not started");
            }
        }
        //this method will save the game
        private void GameSave (object sender, RoutedEventArgs e)
        {
            if (isGameRunning)
            {
                stateToSave = new SaveableGameState(gameState.GetPlayers(), gameState.GetPlayerCount());
                BinaryFormatter write = new BinaryFormatter();
                Stream stream = new FileStream(@".\saveTheGameState.txt", FileMode.Create, FileAccess.Write);
                write.Serialize(stream, this.stateToSave);
                stream.Close();
                MessageBox.Show("Game Saved!");
            }
            else
            {
                MessageBox.Show("The game has not started");
                
            }

        }
        //this method will manage the card that has been drawn
        private void activateCard(int cardId, int playerId)
        {
            //this switch case will manage everycard differently
            switch (cardId)
            {
                
                //For fire and ice cards
                case 1:

                break;

                case 2:

                break;

                case 3:
                case 4:
                case 5:
                case 8:
                case 12:

                    moveCard(cardId, playerId);

                break;

                case 7:

                break;

                case 10:

                break;

                case 11:

                break;

                case -1:
                    Pawn[] allSwitchablePawn = findWhichPawnsCanSwitch();
                    allSwitchablePawn = removeAllOfOwnPlayerCard(allSwitchablePawn, playerId);
                break;
            }
        }

        private void moveCard(int value, int playerId)
        {
            Pawn[] availablePawns = getWhichPawnsCanMove();
            if(availablePawns == null)
            {
                ContentLog.Text = "Unfortunately you have no pawns that can move that distance!";
            }
            ContentLog.Text = "Picked up a card of value " + value + "!";
            //Wait a bit
            String player = this.gameState.players[this.gameState.currentPlayer].PlayerName;
            Window1 options = new Window1(0, player, value);

        }

        /*Make it so it returns the pawns the player himself can move (for the generic cards)*/
        private Pawn[] getWhichPawnsCanMove()
        {
            Player currentPlayer = this.gameState.players[this.gameState.currentPlayer];
            Pawn[] allPawns = currentPlayer.pawns;
            ArrayList availablePawns = new ArrayList();

            for(int i = 0; i < allPawns.Length; i++)
            {
                Pawn currentPawn = allPawns[i];
                if(currentPawn.decommissioned || currentPawn.inStart)
                {
                    availablePawns.Add(currentPawn);
                }
            }

            allPawns = (Pawn[])availablePawns.ToArray(typeof(Pawn));
            return allPawns;
        }


        //this will return an array of pawns that can have their place switched
        private Pawn[] findWhichPawnsCanSwitch()
        {
            Pawn[] allPossiblePawns = new Pawn[numberOfAvailablePawns()];
            int arraycount = 0;
            for (int i = 0; i < gameState.GetPlayers().Length; i++)
            {
                for (int j = 0; i < 3;i++)
                {
                    if (gameState.GetPlayers()[i].pawns[i].canYouSwitchWithPawn())
                    {
                        allPossiblePawns[arraycount] = gameState.GetPlayers()[i].pawns[i];
                        arraycount++;
                    }
                }
            }
            return allPossiblePawns;
        }
        //this method will return the number of pawns that can be used to switch
        private int numberOfAvailablePawns()
        {
            int count = 0;
            for (int i = 0; i<gameState.GetPlayers().Length; i++)
            {
                for (int j = 0; j < 3;j++)
                {
                    if ( gameState.GetPlayers()[i].pawns[i].canYouSwitchWithPawn() )
                    {
                        count++;
                    }
                }
            }
            return count;
        }
        private Pawn[] removeAllOfOwnPlayerCard(Pawn[] listOfPawn,int playerNum)
        {
            //gets the number of available pawn
            int count = 0;
            for (int i = 0; i < listOfPawn.Length; i++)
            {
                if(!(listOfPawn[i].playerNumber == playerNum))
                {
                    count++;
                }
            }
            Pawn[] finalPawnArray = new Pawn[count];
            //creates the array of the valid length and pawns
            int arraycount = 0;
            for (int i = 0; i < count; i++)
            {
                if (!(listOfPawn[i].playerNumber == playerNum))
                {
                    finalPawnArray[arraycount] = listOfPawn[i];
                    arraycount++;
                }
            }
            return finalPawnArray;
        }
    }
}

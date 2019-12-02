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
        private void GameStart(object sender, RoutedEventArgs e)
        {
            if (!isGameRunning)
            {
                gameState = new GameState(this);

                isGameRunning = true;
                gameState.Run();
            }
        }

        private void ClickDraw(object sender, RoutedEventArgs e)
        {
            //if the gams is not running then it will exit the method
            if (isGameRunning)
            {
                Card card = this.gameState.deck.getNextCard();
                activateCard(card.getCard_Id(), gameState.currentPlayer);
                this.gameState.players[this.gameState.currentPlayer].endedTurn = true;
            }
            else
            {
                return;
            }
        }
        //this will load the game
        private void GameLoad(object sender, RoutedEventArgs e)
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
        private void GameSave(object sender, RoutedEventArgs e)
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
                    handleCard1();
                    break;

                case 2:
                    handleCard2();
                    break;

                case 3:
                case 4:
                case 5:
                case 8:
                case 12:
                    handleGenericCard(cardId, playerId);
                    break;

                case 7:
                    handleCard7();
                    break;

                case 10:
                    handleGenericCard(cardId, playerId);
                    break;

                case 11:
                    handleCard11(playerId);
                    break;

                case -1:
                    handleSorryCard(playerId);

                    break;
            }
        }

        //This is for the generic moving of cards (no special event)
        private void handleGenericCard(int value, int playerId)
        {
            Pawn[] availablePawns = getWhichPawnsCanMove();
            if (availablePawns == null)
            {
                if (value == 10)
                {
                    String player = this.gameState.players[this.gameState.currentPlayer].PlayerName;
                    Window1 options = new Window1(0, player, -1, availablePawns, null);
                    Pawn selectedPawn = options.getSelectedPawn();
                    this.gameState.mainBoard.MovePawn(selectedPawn, value);
                }
                ContentLog.Text = "Unfortunately you have no pawns that can move that distance! Turn skipped.";
            }
            else
            {
                ContentLog.Text = "Picked up a card of value " + value + "!";
                //Wait a bit
                String player = this.gameState.players[this.gameState.currentPlayer].PlayerName;
                Window1 options = new Window1(0, player, value, availablePawns, null);
                Pawn selectedPawn = options.getSelectedPawn();
                this.gameState.mainBoard.MovePawn(selectedPawn, value);
            }
        }
        //create card 11
        private void handleCard11(int playerId)
        {
            Pawn[] availablePawns = getWhichPawnsCanMoveOnCard11();
            Pawn[] switchablePawn = findWhichPawnsCanSwitch(playerId);
            String player = this.gameState.players[this.gameState.currentPlayer].PlayerName;
            if (availablePawns != null && switchablePawn != null)
            {
                Window1 options = new Window1(11, player, 11, availablePawns, null);
                //if the player wants to switch then....
                if (options.getChoice11().Equals("switch"))
                {
                    Window1 optionsSwitch = new Window1(4, player, 11, availablePawns, switchablePawn);
                    Pawn currentPlayerPawn = optionsSwitch.gotPawn;
                    Pawn pawnToSwitch = optionsSwitch.otherPlayerPawn;
                    switchPawns11(currentPlayerPawn, pawnToSwitch);
                }
                else if(options.getChoice11().Equals("Forfeit"))
                {
                    ContentLog.Text = "Turn Forfeit!";
                }
                //if the player wants to advance then....
                else
                {

                    handleGenericCard(11, playerId);

                }
            }
            else
            {
                ContentLog.Text = "Unfortunately you have no pawns that can move 11 moves nor are there any pawns that you can switch! Turn skipped.";
            }

        }
        //create card 1
        private void handleCard1()
        {
            Pawn[] availablePawns = getWhichPawnsCanMove();
            Pawn[] switchablePawn = getWhichPawnsCanMoveOnCard11();
            String player = this.gameState.players[this.gameState.currentPlayer].PlayerName;
            Window1 options = new Window1(3, player, 1, availablePawns, null);

            if (options.getChoice11().Equals("Get a pawn out of the start zone"))
            {
                //*********************************************             GET PAWN OUT OF START                  *******************************************
            }
            //this means advance 1 space
            else
            {
                Window1 optionsAdvance = new Window1(0, player, 1, availablePawns, null);
            }
        }
        //create card 2
        private void handleCard2()
        {
            Pawn[] availablePawns = getWhichPawnsCanMove();
            Pawn[] switchablePawn = getWhichPawnsCanMoveOnCard11();
            String player = this.gameState.players[this.gameState.currentPlayer].PlayerName;
            Window1 options = new Window1(4, player, 1, availablePawns, null);

            if (options.getChoice11().Equals("Get a pawn out of the start zone"))
            {
                //*********************************************             GET PAWN OUT OF START                  *******************************************
            }
            //this means advance 2 space
            else
            {
                Window1 optionsAdvance = new Window1(0, player, 2, availablePawns, null);
            }
        }
        //create card 7
        private void handleCard7()
        {
            Pawn[] availablePawns = getWhichPawnsCanMove();
            Pawn[] switchablePawn = getWhichPawnsCanMoveOnCard11();
            String player = this.gameState.players[this.gameState.currentPlayer].PlayerName;
            Window1 options = new Window1(5, player, 7, availablePawns, null);

            if (options.getChoice11().Equals("Put all 7 on one pawn"))
            {
                Window1 optionsAdvance7 = new Window1(0, player, 7, availablePawns, null);
            }
            //this means separate 7 into 2 pawns
            else
            {
                //the next lines are made so that the sum of the two number are equal to 7
                int firstMove = 0;
                int secondMove = 0;
                int flag = 0;
                while (flag == 0)
                {
                    String firstMouvement = Interaction.InputBox("how many spaces for the first pawn?", "movement separation", "Please enter of number of spaces");
                    firstMove = Convert.ToInt32(firstMouvement);
                    String secondMouvement = Interaction.InputBox("how many spaces for the second pawn?", "movement separation", "Please enter a number of spaces");
                    secondMove = Convert.ToInt32(secondMouvement);
                    if (((firstMove + secondMove) == 7) && (firstMove > 0) && (secondMove > 0))
                    {
                        flag = 1;
                    }

                }
                //will give the choice to add the number of each pawn to a pawn of their choosing
                Window1 optionsAdvancefirstMove = new Window1(0, player, firstMove, availablePawns, null);
                Window1 optionsAdvancesecondMove = new Window1(0, player, secondMove, availablePawns, null);
            }
        }



        //This is for the sorry card(replacing pawn from start with another pawn)
        private void handleSorryCard(int playerId)
        {
            
            String player = this.gameState.players[this.gameState.currentPlayer].PlayerName;
            Pawn[] allSwitchablePawn = findWhichPawnsCanSwitch(playerId);

            //Checks what pawns are at start
            Player currentPlayer = this.gameState.players[this.gameState.currentPlayer];
            Pawn[] allPawns = currentPlayer.pawns;
            ArrayList availablePawns = new ArrayList();

            for (int i = 0; i < allPawns.Length; i++)
            {
                Pawn currentPawn = allPawns[i];
                if (currentPawn.inStart)
                {
                    availablePawns.Add(currentPawn);
                }
            }

            allPawns = (Pawn[])availablePawns.ToArray(typeof(Pawn));

            if (allPawns == null)
            {
                ContentLog.Text = "You don't have any pawns at Start :(";
            }
            else
            {
                Window1 options = new Window1(2, player, 0, allPawns, allSwitchablePawn);
                Pawn pawnAtStart = options.gotPawn;
                Pawn pawnToSwitch = options.otherPlayerPawn;
                switchPawns(pawnAtStart, pawnToSwitch);
            }
        }

        //For switching pawns (for sorry card)
        private void switchPawns(Pawn pawnAtStart, Pawn pawnToSwitch)
        {
            pawnAtStart.spaceNumber = pawnToSwitch.spaceNumber;
            this.gameState.drawAtNextPosition(pawnAtStart);
            this.gameState.drawAtStart(pawnToSwitch);

        }

        //For switching pawns (card 11)
        private void switchPawns11(Pawn currentPlayerPawn, Pawn pawnToSwitch)
        {
            int temp = currentPlayerPawn.spaceNumber;
            currentPlayerPawn.spaceNumber = pawnToSwitch.spaceNumber;
            pawnToSwitch.spaceNumber = temp;

            this.gameState.drawAtNextPosition(currentPlayerPawn);
            this.gameState.drawAtNextPosition(pawnToSwitch);
        }

        /*Make it so it returns the pawns the player himself can move (for the generic cards)*/
        private Pawn[] getWhichPawnsCanMove()
        {
            //get current player
            Player currentPlayer = this.gameState.players[this.gameState.currentPlayer];
            //get all current player's pawns
            Pawn[] allPawns = currentPlayer.pawns;
            //create an arraylist to store the pawns
            ArrayList availablePawns = new ArrayList();

            for (int i = 0; i < allPawns.Length; i++)
            {
                Pawn currentPawn = allPawns[i];
                if (!currentPawn.decommissioned || !currentPawn.inStart)
                {
                    availablePawns.Add(currentPawn);
                }
            }

            allPawns = (Pawn[])availablePawns.ToArray(typeof(Pawn));
            return allPawns;
        }

        //This only works for the current players pawns (card 11)
        private Pawn[] getWhichPawnsCanMoveOnCard11()
        {
            //gte current player
            Player currentPlayer = this.gameState.players[this.gameState.currentPlayer];
            //get all current player's pawns
            Pawn[] allPawns = currentPlayer.pawns;
            //create an arraylist to store the pawns
            ArrayList availablePawns = new ArrayList();

            for (int i = 0; i < allPawns.Length; i++)
            {
                Pawn currentPawn = allPawns[i];
                if (!currentPawn.decommissioned || !currentPawn.inStart || !currentPawn.safe)
                {
                    availablePawns.Add(currentPawn);
                }
            }

            allPawns = (Pawn[])availablePawns.ToArray(typeof(Pawn));
            return allPawns;
        }

        //this will return an array of pawns that can have their place switched(sorry and 11 cards)
        private Pawn[] findWhichPawnsCanSwitch(int playerId)
        {
            //Goes through all the players and adds the pawns that are able to be replaced
            Player[] allPlayers = this.gameState.players;
            ArrayList availablePawns = new ArrayList();
            for (int i = 0; i < allPlayers.Length; i++)
            {
                if (i != playerId)
                {
                    Player tempPlayer = allPlayers[i];
                    Pawn[] allPawnsOnePlayer = tempPlayer.pawns;
                    for (int j = 0; j < allPawnsOnePlayer.Length; j++)
                    {
                        if (!allPawnsOnePlayer[j].decommissioned || !allPawnsOnePlayer[j].inStart || !allPawnsOnePlayer[j].safe)
                        {
                            availablePawns.Add(allPawnsOnePlayer[j]);
                        }
                    }
                }
            }
            Pawn[] allSwitchablePawn = (Pawn[])availablePawns.ToArray(typeof(Pawn));
            return allSwitchablePawn;
        }
        /*
        //this method will return the number of pawns that can be used to switch
        private int numberOfAvailablePawns()
        {
            int count = 0;
            for (int i = 0; i < gameState.GetPlayers().Length; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (gameState.GetPlayers()[i].pawns[i].canYouSwitchWithPawn())
                    {
                        count++;
                    }
                }
            }
            return count;
        }

        private Pawn[] removeAllOfOwnPlayerCard(Pawn[] listOfPawn, int playerNum)
        {
            //gets the number of available pawn
            int count = 0;
            for (int i = 0; i < listOfPawn.Length; i++)
            {
                if (!(listOfPawn[i].playerNumber == playerNum))
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
        */
    }
    
}

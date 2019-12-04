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

        int loadedPlayerCount;
        public GameState gameState;
        public Board mainBoard;

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
                this.mainBoard = new Board(this.gameState.players, this);
                drawInitialPawns();
                DrawCard.IsEnabled = true;
                Start.IsEnabled = false;

                isGameRunning = true;
                gameState.updatePlayer();
                this.Player_Display.Text = this.gameState.players[this.gameState.currentPlayer].PlayerName +" it is your turn!";
            }
        }

        private void ClickDraw(object sender, RoutedEventArgs e)
        {
            //if the gams is not running then it will exit the method
            if (isGameRunning)
            {

                //CURRENTLY TESTING CARDS
                //Card card = this.gameState.deck.getNextCard();
                //activateCard(card.getCard_Id(), gameState.currentPlayer);

                Card temp = new Card(1);
                drawOutsideStart(this.gameState.players[0].pawns[0]);
                this.gameState.players[1].pawns[0].spaceNumber = 5;
                drawAtNextPosition(this.gameState.players[1].pawns[0]);
                this.mainBoard.landingSpaces[4].localPawn = this.gameState.players[0].pawns[0];
                this.mainBoard.landingSpaces[5].localPawn = this.gameState.players[1].pawns[0];
                activateCard(temp.getCard_Id(), gameState.currentPlayer);

                Next_Turn.IsEnabled = true;
                DrawCard.IsEnabled = false;

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
                this.gameState = (GameState)load.Deserialize(this.pubStream);
                this.pubStream.Close();
                /*scrapped idea
                loadedPlayers = stateToLoad.GetPlayers();
                loadedPlayerCount = stateToLoad.GetPlayerCount();
                */
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
                BinaryFormatter write = new BinaryFormatter();
                Stream stream = new FileStream(@".\save.txt", FileMode.Create, FileAccess.Write);
                write.Serialize(stream, gameState);
                stream.Close();
                MessageBox.Show("Game Saved!");


                    /* was a scraped idea
                stateToSave = new SaveableGameState(gameState.GetPlayers(), gameState.GetPlayerCount());
                BinaryFormatter write = new BinaryFormatter();
                Stream stream = new FileStream(@".\saveTheGameState.txt", FileMode.Create, FileAccess.Write);
                write.Serialize(stream, this.stateToSave);
                stream.Close();
                MessageBox.Show("Game Saved!");
                */
            }
            else
            {
                MessageBox.Show("The game has not started");

            }

        }

        //(IMPLEMENT ENDGAME METHOD)
        private void Next_Turn_Click(object sender, RoutedEventArgs e)
        {
            int counter = 0;
            Pawn[] playersPawns = this.gameState.players[this.gameState.currentPlayer].pawns;
            for (int i = 0; i < playersPawns.Length; i++)
            {
                if(playersPawns[i].decommissioned)
                {
                    counter++;
                }
            }
            if(counter == 3)
            {
                MessageBox.Show(this.gameState.players[this.gameState.currentPlayer].PlayerName + "has won! Congratulations!");
                //Implement a endGame method here
            }
            else
            {
                gameState.updatePlayer();
                this.Player_Display.Text = this.gameState.players[this.gameState.currentPlayer].PlayerName + " it is your turn!";
                Next_Turn.IsEnabled = false;
                DrawCard.IsEnabled = true;
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
                case 2:
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


        /*HANDLING ALL THE CARDS*/

        //CERTIFIED WORKS
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
                Window1 options = new Window1(2, player, 0, allPawns, allSwitchablePawn, this);
                options.Show();
            }
        }
        //This is for the generic moving of cards (no special event) (fix card 10 thing)
        //WORKS FOR MOVING OUT OF INITIAL SPACE AND MOVING(*havent tested collision yet)
        private void handleGenericCard(int value, int playerId)
        {
            Pawn[] availablePawns = getWhichPawnsCanMove();
            if (availablePawns == null)
            {
                //This is actually not quite the correct implementation, but we need another method to check if a pawn can move 10 spots or not
                if (value == 10)
                {
                    String player = this.gameState.players[this.gameState.currentPlayer].PlayerName;
                    Window1 options = new Window1(0, player, -1, availablePawns, null, this);
                    options.Show();
                }
                ContentLog.Text = "Unfortunately you have no pawns that can move that distance! Turn skipped.";
            }
            else
            {
                ContentLog.Text = "Picked up a card of value " + value + "!";
                //Wait a bit
                String player = this.gameState.players[this.gameState.currentPlayer].PlayerName;
                Window1 options = new Window1(0, player, value, availablePawns, null, this);
                options.Show();
            }
        }
        //create card 11
        private void handleCard11(int playerId)
        {
            Pawn[] availablePawns = getWhichPawnsCanMoveOnCard11();
            Pawn[] switchablePawn = findWhichPawnsCanSwitch(playerId);
            String player = this.gameState.players[this.gameState.currentPlayer].PlayerName;
            if (availablePawns.Length != 0 && switchablePawn.Length != 0)
            {
                Window1 options = new Window1(11, player, 11, availablePawns, switchablePawn, this);
                options.Show();
                //if the player wants to switch then....
                
            }
            else if(availablePawns.Length != 0)
            {
                handleGenericCard(11, playerId);
            }
            else
            {
                ContentLog.Text = "Unfortunately you have no pawns that can move 11 moves nor are there any pawns that you can switch! Turn skipped.";
            }

        }
        //create card 1 (OBSOLETE)
        /*
        private void handleCard1And2(int value)
        {
            Pawn[] availablePawns = getPawnsOnCards1And2();
            String player = this.gameState.players[this.gameState.currentPlayer].PlayerName;
            Window1 options = new Window1(3, player, 1, availablePawns, null, this);
            options.Show();
        }
        */
        //create card 7 (also have to add if the players can actually move 2 pawns or not, otherwise just call normal thing)
        private void handleCard7()
        {
            Pawn[] availablePawns = getWhichPawnsCanMove();
            String player = this.gameState.players[this.gameState.currentPlayer].PlayerName;
            Window1 options = new Window1(6, player, 7, availablePawns, null, this);
            options.Show();
        }
        //This is for the sorry card(replacing pawn from start with another pawn)
        


        /*HANDLERS FOR WINDOW OPTIONS*/
        public void genericHelper(Window1 input, int value)
        {
            Pawn selectedPawn = input.getSelectedPawn();
            this.mainBoard.MovePawn(selectedPawn, value, true);
        }

        //This is used by the sorry card as well as the 11 card(if they want to switch)
        public void sorryAnd11Helper(Window1 input, int value)
        { 
            if(value == 0)
            {
                Pawn pawnAtStart = input.gotPawn;
                Pawn pawnToSwitch = input.otherPlayerPawn;
                switchPawns(pawnAtStart, pawnToSwitch);
            }
            else if(value == 11)
            {
                Pawn currentPlayerPawn = input.gotPawn;
                Pawn pawnToSwitch = input.otherPlayerPawn;
                switchPawns11(currentPlayerPawn, pawnToSwitch);
            }

        }

        //This helper isuse
        public void only11Helper(Window1 input, int value)
        {
            if (input.getChoice11().Equals("Switch"))
            {
                Pawn[] newPawns = currentPawnsCanSwitch(input.allPawns);
                Window1 optionsSwitch = new Window1(4, input.playerName, 11, newPawns, input.otherPawns, this);
                optionsSwitch.Show();
            }
            else if (input.getChoice11().Equals("Forfeit"))
            {
                ContentLog.Text = "Turn Forfeited!";
            }
            //if the player wants to advance then....
            else
            {

                handleGenericCard(11, this.gameState.currentPlayer);

            }
        }
        //Ok so this is the first helper that decides whether to call the generic cards or to split
        public void _7Helper(Window1 input)
        {
            if (input.getChoice11().Equals("Put all 7 on one pawn"))
            {
                handleGenericCard(7, this.gameState.currentPlayer);
            }
            //this means separate 7 into 2 pawns
            else
            {
                //the next lines are made so that the sum of the two number are equal to 7

                Window1 optionsSplit = new Window1(7, input.playerName, 7, input.allPawns, null, this);
                optionsSplit.Show();
            }
        }
        //This one is for the first pawn in the split
        public void __7HelperPart2(Window1 input)
        {
            //Ok so since i don't feel like writing another method essentially if the value is 7 it means that only the first pawn was chosen, otherwise the second one is moved
            int firstMove = input.move7;
            Pawn selectedPawn = input.gotPawn;
            if(input.value == 7)
            {
                Pawn[] gotPawns = input.allPawns;
                ArrayList availablePawns = new ArrayList();

                for (int i = 0; i < gotPawns.Length; i++)
                {
                    if (gotPawns[i] != selectedPawn)
                    {
                        availablePawns.Add(gotPawns[i]);
                    }
                }

                Pawn[] newPawns = (Pawn[])availablePawns.ToArray(typeof(Pawn));
                this.mainBoard.MovePawn(selectedPawn, firstMove, true);
                input = new Window1(7, input.playerName, (7 - input.move7), newPawns, null, this);
                input.Show();
            }
            else
            {
                this.mainBoard.MovePawn(selectedPawn, input.value, true);
            }
           
        }

        /*HANDLING SWITCHING THE PAWNS*/
        //For switching pawns (for sorry card)
        private void switchPawns(Pawn pawnAtStart, Pawn pawnToSwitch)
        {
            pawnAtStart.spaceNumber = pawnToSwitch.spaceNumber;
            drawAtNextPosition(pawnAtStart);
            drawAtStart(pawnToSwitch);

        }

        //For switching pawns (card 11)
        private void switchPawns11(Pawn currentPlayerPawn, Pawn pawnToSwitch)
        {
            int temp = currentPlayerPawn.spaceNumber;
            currentPlayerPawn.spaceNumber = pawnToSwitch.spaceNumber;
            pawnToSwitch.spaceNumber = temp;

            drawAtNextPosition(currentPlayerPawn);
            drawAtNextPosition(pawnToSwitch);
        }



        /*GETTING VALID PAWNS*/
        /*Make it so it returns the pawns the player himself can move (for the generic cards and 7)*/
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
                if (!currentPawn.decommissioned)
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
                        if (!allPawnsOnePlayer[j].decommissioned && !allPawnsOnePlayer[j].inStart && !allPawnsOnePlayer[j].safe)
                        {
                            availablePawns.Add(allPawnsOnePlayer[j]);
                        }
                    }
                }
            }
            Pawn[] allSwitchablePawn = (Pawn[])availablePawns.ToArray(typeof(Pawn));
            return allSwitchablePawn;
        }

        //used for the 11 card part 2
        private Pawn[] currentPawnsCanSwitch(Pawn[] inputs)
        {
            ArrayList availablePawns = new ArrayList();
            for(int i = 0; i < inputs.Length; i++)
            {
                if (!inputs[i].decommissioned && !inputs[i].inStart)
                {
                    availablePawns.Add(inputs[i]);
                }
            }
            Pawn[] allSwitchablePawn = (Pawn[])availablePawns.ToArray(typeof(Pawn));
            return allSwitchablePawn;
        }

        /*DRAWING PAWNS + PLAYERS*/
        private void drawInitialPawns()
        {
            for (int i = 0; i < this.gameState.players.Length; i++)
            {
                for (int j = 0; j < this.gameState.players[i].pawns.Length; j++)
                {
                    Player tempPlayer = this.gameState.players[i];
                    Pawn currentPawn = tempPlayer.pawns[j];
                    if (tempPlayer.color.Equals("Red"))
                    {
                        Grid.SetRow(currentPawn.image, 2);
                        Grid.SetColumn(currentPawn.image, 4);
                        MainGrid.Children.Add(currentPawn.image);
                    }
                    else if (tempPlayer.color.Equals("Blue"))
                    {
                        Grid.SetRow(currentPawn.image, 4);
                        Grid.SetColumn(currentPawn.image, 13);
                        MainGrid.Children.Add(currentPawn.image);
                    }
                    else if (tempPlayer.color.Equals("Yellow"))
                    {
                        Grid.SetRow(currentPawn.image, 11);
                        Grid.SetColumn(currentPawn.image, 2);
                        MainGrid.Children.Add(currentPawn.image);
                    }
                    else
                    {
                        Grid.SetRow(currentPawn.image, 13);
                        Grid.SetColumn(currentPawn.image, 11);
                        MainGrid.Children.Add(currentPawn.image);
                    }
                }
            }
        }

        public void drawAtNextPosition(Pawn pawn)
        {
            //Setting the row and column numbers by checking which position it's at
            int nextPosition = pawn.spaceNumber;
            int rowNum;
            int colNum;
            MainGrid.Children.Remove(pawn.image);

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
            MainGrid.Children.Add(pawn.image);
        }

        public void drawAtStart(Pawn pawn)
        {
            MainGrid.Children.Remove(pawn.image);
            pawn.inStart = true;
            pawn.spaceNumber = 99;
            Grid.SetRow(pawn.image, pawn.startPositionRow);
            Grid.SetColumn(pawn.image, pawn.startPositionCol);
            MainGrid.Children.Add(pawn.image);
        }
        public void drawOutsideStart(Pawn pawn)
        {
            pawn.inStart = false;
            if (pawn.color.Equals("Red"))
            {
                pawn.spaceNumber = 4;
                this.mainBoard.landingSpaces[4].localPawn = pawn;
            }
            else if (pawn.color.Equals("Blue"))
            {
                pawn.spaceNumber = 19;
                this.mainBoard.landingSpaces[19].localPawn = pawn;
            }
            else if (pawn.color.Equals("Green"))
            {
                pawn.spaceNumber = 34;
                this.mainBoard.landingSpaces[34].localPawn = pawn;
            }
            else
            {
                pawn.spaceNumber = 49;
                this.mainBoard.landingSpaces[49].localPawn = pawn;
            }
            drawAtNextPosition(pawn);
        }
        /*
        private Pawn[] getPawnsOnCards1And2()
        {
            Player currentPlayer = this.gameState.players[this.gameState.currentPlayer];
            //get all current player's pawns
            Pawn[] allPawns = currentPlayer.pawns;
            //create an arraylist to store the pawns
            ArrayList availablePawns = new ArrayList();

            for (int i = 0; i < allPawns.Length; i++)
            {
                Pawn currentPawn = allPawns[i];
                if (!currentPawn.decommissioned || currentPawn.inStart || currentPawn.safe)
                {
                    availablePawns.Add(currentPawn);
                }
            }

            allPawns = (Pawn[])availablePawns.ToArray(typeof(Pawn));
            return allPawns;
        }
        */
    }
    
}

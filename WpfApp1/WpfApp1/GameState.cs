using System;
using Microsoft.VisualBasic;
using WpfApp1;

public class GameState
{

    int playerCount = 0;
    Player[] players;
    MainWindow main;
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
        String[] colors = { "Blue", "Red", "Green", "Yellow" };

        for (int i = 0; i < playerCount; i++)
        {
            String playerName = Interaction.InputBox("What is player " + i + "'s name?", "Name?");
            players[i] = new Player(playerName, colors[i], main);
        }

        /*Creating board (locations)*/
        Board mainBoard = new Board(players, this.main);

        /*Drawing players*/
        for(int i = 0; i < this.players.Length; i++)
        {
            this.players[i].drawPlayer();
        }


    }

    public void run()
    {
        Boolean ended = false;
        for (int playerTurn = 0; ended; playerTurn++)
        {
            players[playerTurn % players.Length].playTurn();
        }
    }
}

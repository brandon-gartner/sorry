using System;
using Microsoft.VisualBasic;

public class GameState
{

    int playerCount = 0;
    Player[] players;
	public GameState()
	{
        int playercount;
        String playerCountInput;
        Boolean numberAllowed = false;
        do
        {
            playerCountInput = Interaction.InputBox("How many players are there?", "Player Count", "Please enter a number between 2 and 4");
            playerCount = Convert.ToInt32(playerCountInput);
        } while (playerCount <= 1 || playerCount >= 5);

        players = new Player[playerCount];

        for (int i = 0; i < playerCount; i++)
        {
            String playerName = Interaction.InputBox("What is player " + i + "'s name?", "Name?");
            players[i] = new Player(playerName);
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

using System;
using Microsoft.VisualBasic;
public class GameState
{

    int playerCount = 0;
    Player[] players;
	public GameState()
	{
        
	}

    public void run()
    {
        do
        {
            int playerCount = Microsoft.VisualBasic.Interaction.InputBox("How many players are there?", "Player Count", "Please choose 2-4.");
        } while (playerCount < 2 && playerCount > 5);

        players = new Player[playerCount];

        for(int i = 0; i < playerCount; i++) { 
            players[i].playerName = (Microsoft.VisualBasic.Interaction.InputBox("What is player" + i + "'s name?", "Name?", "Please enter your name."));
        }
    }
}

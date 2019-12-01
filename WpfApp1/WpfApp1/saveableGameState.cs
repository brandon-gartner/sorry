using System;
using System.Collections.Generic;
using System.Windows.Controls;

[Serializable]
public class saveableGameState
{
    private int playerCount;
    private Player[] players;

    public saveableGameState(Player[] players, int playerCount)
	{
        this.players = players;
        this.playerCount= playerCount;

    }

    public Player[] GetPlayers()
    {
        return this.players;
    }

    public int GetPlayerCount()
    {
        return this.playerCount;
    }

}

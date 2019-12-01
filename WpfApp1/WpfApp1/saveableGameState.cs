using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace WpfApp1 
{
    [Serializable]
    public class SaveableGameState
    {
        private int playerCount;
        private Player[] players;

        public SaveableGameState(Player[] players, int playerCount)
        {
            this.players = players;
            this.playerCount = playerCount;

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
}
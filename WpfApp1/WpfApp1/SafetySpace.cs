/*using System;
using WpfApp1;

namespace WpfApp1
{

    [Serializable]
    internal class SafetySpace
    {
        private Player ThisPlayer { get; }

        public SafetySpace(Player player)
        {
            this.ThisPlayer = player;
        }

        //this is a space inside of a player's safety zone.  they can't move past the end, must move in exact numbers, and 
        public void LandedOn(Pawn p)
        {
            p.safe = true;
        }

        public void SteppedOn(Pawn p)
        {

        }
    }
}*/
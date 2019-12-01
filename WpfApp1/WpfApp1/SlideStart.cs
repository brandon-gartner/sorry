//the class for the beginning of a slide
using System;
using WpfApp1;

namespace WpfApp1
{

    [Serializable]
    internal class SlideStart
    {
        public Player player;
        public int connectedSpace;

        //takes a player from the program and reads it as the owner player
        public SlideStart(Player player, int connectedPiece)
        {
            this.player = player;
            this.connectedSpace = connectedPiece;
        }

        //when this gets landed on, should check whether you slide or not.  if you do, steps to the connectedPiece, which steps to the connectedPiece.
        //this will use a modified step method, which returns pieces to their home bases if you step on them.  they continue to move forward until a connectedpiece sends you to one which is a slideend.
        public void LandedOn(Pawn p)
        {
            if (p.playerNumber != this.player.playerNumber)
            {

            }
        }

        public void SteppedOn(Pawn p)
        {

        }

        
    }
}
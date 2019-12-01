//this is both the end of a slide, and the place where people will leave start
using System;

[Serializable]
internal class SlideEndStartExit : ISpace
{
    public Player player;

    public SlideEndStartExit(Player player)
    {
        this.player = player;
    }


    //should both count as a slide end (meaning anywhere that looks for a slide end should also look for this.  however, when a pawn leaves their home space, they should also appear here
    public void landedOn(Pawn p)
    {
        throw new System.NotImplementedException();
    }

    public void steppedOn(Pawn p)
    {
        throw new NotImplementedException();
    }
}
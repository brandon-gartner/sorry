using System;

public class Pawn
{
    public int spaceNumber { get; set; }
    public Boolean safe;
    public Boolean inStart;

    public Pawn(Player p, int numbering)
    {
    }

    //is run in the boards movePawn method
    public int validateFutureLocation(int movement)
    {
        int potentialFutureLocation = (this.spaceNumber + movement);
        //if the pawn has passed the starting position, move it there
        if (potentialFutureLocation >= 60)
        {
            return potentialFutureLocation - 60;
        }
        //if the pawn has wrapped around backwards past the starting position, move it here
        else if (potentialFutureLocation <= 0)
        {
            return potentialFutureLocation + 60;
        }
        //otherwise return the position it should be at
        else
        {
            return potentialFutureLocation;
        }

    }
}

using System;
using System.Windows.Controls;

public class Pawn
{
    int spaceNumber;
    Boolean safe;
    Boolean inStart;
    int numberOfPawn;
    public Label image;

    /*So basically the color is going to be the color of the pawn*/
    public Pawn(int numberOfPawn, String color)
    {
        this.numberOfPawn = numberOfPawn;
        safe = false;
        inStart = true;
        image.Background =;
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

    public void setSpaceNumber(int newSpaceNumber)
    {
        this.spaceNumber = newSpaceNumber;
    }
}

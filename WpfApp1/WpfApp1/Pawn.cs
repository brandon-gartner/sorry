using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

public class Pawn
{
    public int spaceNumber;
    public Boolean safe;
    public Boolean inStart;
    public Boolean decommisioned;
    public int numberOfPawn;
    public Border image;

    /*So basically the color is going to be the color of the pawn*/
    public Pawn(int numberOfPawn, String color)
    {
        this.numberOfPawn = numberOfPawn;
        safe = false;
        inStart = true;
        this.image = new Border();
        if (color.Equals("Red"))
        {
            image.Background = Brushes.Red;
        }
        else if (color.Equals("Green"))
        {
            image.Background = Brushes.Green;
        }
        else if (color.Equals("Blue"))
        {
            image.Background = Brushes.Blue;
        }
        else
        {
            image.Background = Brushes.Yellow;
        }

        /*Setting the design properties*/
        TextBlock text = new TextBlock();
        text.Text = Convert.ToString(numberOfPawn);
        image.Child = text;
        image.CornerRadius = new CornerRadius(25);
        image.Margin = new Thickness(8);
        image.BorderThickness = new Thickness(2);
        image.BorderBrush = Brushes.Black;
        text.VerticalAlignment = VerticalAlignment.Center;
        text.HorizontalAlignment = HorizontalAlignment.Center;
        text.FontWeight = FontWeights.Bold;


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
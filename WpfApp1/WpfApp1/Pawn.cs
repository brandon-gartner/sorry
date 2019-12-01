﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using WpfApp1;

namespace WpfApp1
{
    [Serializable]
    public class Pawn
    {
        public int spaceNumber;
        public Boolean safe;
        public Boolean inStart;
        public Boolean decommissioned;
        public int numberOfPawn;
        public Border image;
        public int playerNumber;
        public String playerName;
        public Boolean isFire;

        /*So basically the color is going to be the color of the pawn*/
        public Pawn(int numberOfPawn, String color, int playerNumber, String playerName)
        {
            this.isFire = false;
            this.playerName = playerName;
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
        public int movingPawn(int movement)
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

        public void SetSpaceNumber(int newSpaceNumber)
        {
            this.spaceNumber = newSpaceNumber;
        }

        public void ReturnHome()
        {
            this.inStart = true;
            this.spaceNumber = 99;
        }
        //this method will check if a pawn can switch place 
        public Boolean canYouSwitchWithPawn()
        {
            if (this.safe)
            {
                return false;
            }
            if (this.inStart)
            {
                return false;
            }
            if (this.decommissioned)
            {
                return false;
            }
            return true;
        }
        public String pawnToString()
        {
            return (this.playerName + "'s Pawn #" + this.numberOfPawn);
        }

    }
}
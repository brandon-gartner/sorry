using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using WpfApp1;

namespace WpfApp1
{
    [Serializable]
    public class Pawn
    {
        public String color;
        public int spaceNumber;
        public Boolean safe;
        public Boolean inStart;
        public Boolean decommissioned;
        public int numberOfPawn;
        [NonSerialized]
        public Border image;
        public int playerNumber;
        public String playerName;
        //public Boolean isFire;
        //Following four are for start position and right outside it
        public int startPositionCol;
        public int startPositionRow;

        //For safety spaces
        public int safetyColumn;
        public int safetyRow;
        public Boolean goingIntoSafety = false;
        


        /*So basically the color is going to be the color of the pawn*/
        public Pawn(int numberOfPawn, String color, int playerNumber, String playerName)
        {
            this.color = color;
            //this.isFire = false;
            this.playerName = playerName;
            this.numberOfPawn = numberOfPawn;
            safe = false;
            inStart = true;
            this.image = new Border();
            if (color.Equals("Red"))
            {
                image.Background = Brushes.Red;
                this.startPositionRow = 2;
                this.startPositionCol = 4;

            }
            else if (color.Equals("Green"))
            {
                image.Background = Brushes.Green;
                this.startPositionRow = 13;
                this.startPositionCol = 11;

            }
            else if (color.Equals("Blue"))
            {
                image.Background = Brushes.Blue;
                this.startPositionRow = 4;
                this.startPositionCol = 13;


            }
            else
            {
                image.Background = Brushes.Yellow;
                this.startPositionRow = 11;
                this.startPositionCol = 2;

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
        public int validateNextLocation(Boolean forward)
        {
            if (forward)
            {
                int potentialFutureLocation = (this.spaceNumber + 1);
                //if the pawn has passed the starting position, move it there
                if (potentialFutureLocation > 59)
                {
                    return potentialFutureLocation - 60;
                }
                else
                {
                    return potentialFutureLocation;
                }
            }
            else
            {
                int potentialFutureLocation = (this.spaceNumber - 1);
                //if the pawn has wrapped around backwards past the starting position, move it here
                if (potentialFutureLocation < 0)
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

        public void sendToSafety()
        {
            this.safe = true;
            this.goingIntoSafety = false;
            this.spaceNumber = 99;
            if (color.Equals("Red"))
            {
                this.safetyRow = 1;
                this.safetyColumn = 2;
            }
            else if (color.Equals("Green"))
            {
                this.safetyRow = 14;
                this.safetyColumn = 13;
            }
            else if (color.Equals("Blue"))
            {
                this.safetyRow = 2;
                this.safetyColumn = 14;
            }
            else
            {
                this.safetyRow = 13;
                this.safetyColumn = 1;
            }
        }

        public void updateSafety()
        {
            if (color.Equals("Red"))
            {
                this.safetyRow++;
            }
            else if (color.Equals("Green"))
            {
                this.safetyRow--;
            }
            else if (color.Equals("Blue"))
            {
                this.safetyColumn--;
            }
            else
            {
                this.safetyColumn++;
            }
        }
        

        public void SetSpaceNumber(int newSpaceNumber)
        {
            this.spaceNumber = newSpaceNumber;
        }

        public void ReturnHome(Pawn p)
        {
            p.inStart = true;
            p.spaceNumber = 99;
            
        }

        /*
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
        */
        public String pawnToString()
        {
            return (this.playerName + "'s Pawn #" + this.numberOfPawn);
        }




        public void colorCorrection()
        {
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

    }
}
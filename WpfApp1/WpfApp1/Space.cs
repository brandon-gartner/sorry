using System;
using System.Windows;
using WpfApp1;

namespace WpfApp1{
    [Serializable]
    //interface which all spaces implement
    public class Space
    {
        public int type;
        public Player player;
        public int connectedSpace;

        //the contructor for the spaces which don't have an associated player or connected space
        //this means NormalSpaces (0), SlideEnds (1), HomeSpaces (2)
        public Space(int type)
        {
            if (type > 2 || type < 0)
            {
                throw new ArgumentException("wrong input for space constructor with only a type");
            }
            this.type = type;
        }

        //the constructor for the spaces which are associated with a certain player
        //this means SlideEndStartExit (3), SafetySpace (4), SafetyEntry (5), 
        public Space(int type, Player player)
        {
            if (type > 5 || type < 3)
            {
                throw new ArgumentException("wrong input for space constructor with a type and a player");
            }
            this.type = type;
            this.player = player;
        }

        //the constructor for the spaces which are associated with a certain connected space
        //this means ConnectingSpace (6)
        public Space(int type, int connectedSpace)
        {
            if (type != 6)
            {
                throw new ArgumentException("wrong input for space constructor with a type and a connectedSpace");
            }
            this.type = type;
            this.connectedSpace = connectedSpace;
        }

        //the constructor for the spaces which have a player and a connected space
        //this means SlideStart (7)
        public Space(int type, int connectedSpace, Player player)
        {
            if (type != 7)
            {
                throw new ArgumentException("wrong input for space constructor with a type, a connectedSpace, and a player");
            }
            this.type = type;
            this.player = player;
            this.connectedSpace = connectedSpace;
        }

        public void LandedOn(Pawn p)
        {
            switch (this.type)
            {
                //if you land on a NormalSpace, nothing special happens
                case 0:
                    break;
                //if you land on a SlideEnd, nothing special happens
                case 1:
                    break;
                //if you land on a HomeSpace, the pawn is decommissioned and no longer is active
                case 2:
                    p.decommissioned = true;
                    MessageBox.Show("Congratulations!  " + p.playerName + "'s pawn number " + p.numberOfPawn + " has reached its Home space!");
                    break;
                //if you land on a SlideEndStartExit
                case 3:
                    break;
                //if you land on a SafetySpace
                case 4:
                    break;
                //if you land on a SafetyEntry
                case 5:
                    break;
                //if you land on a ConnectingSpace
                case 6:
                    break;
                //if you land on a SlideStart
                case 7:
                    break;
            }
        }

        public void SteppedOn(Pawn p)
        {
            switch (this.type)
            {
                //if you step on a NormalSpace, nothing special happens
                case 0:
                    break;
                //if you step on a SlideEnd, nothing special happens
                case 1:
                    break;
                //if you step on a HomeSpace
                case 2:
                    break;
                //if you step on a SlideEndStartExit
                case 3:
                    break;
                //if you step on a SafetySpace
                case 4:
                    break;
                //if you step on a SafetyEntry
                case 5:
                    break;
                //if you step on a ConnectingSpace
                case 6:
                    break;
                //if you step on a SlideStart
                case 7:
                    break;
            }
        }
    }
}
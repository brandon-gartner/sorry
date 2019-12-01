﻿using System;
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
        //this means SlideEndStartExit (3), SafetySpace (4),  
        public Space(int type, Player player)
        {
            if (type > 4 || type < 3)
            {
                throw new ArgumentException("wrong input for space constructor with a type and a player");
            }
            this.type = type;
            this.player = player;
        }

        //the constructor for the spaces which are associated with a certain connected space
        //this means ConnectingSpace (5)
        public Space(int type, int connectedSpace)
        {
            if (type != 5)
            {
                throw new ArgumentException("wrong input for space constructor with a type and a connectedSpace");
            }
            this.type = type;
            this.connectedSpace = connectedSpace;
        }

        //the constructor for the spaces which have a player and a connected space
        //this means SlideStart (6), SafetyEntry
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
                //if you land on a non-safe space, your safety should be set to false
                //if you land on a NormalSpace, nothing special happens
                case 0:
                    return;
                //if you land on a SlideEnd, nothing special happens
                case 1:
                    return;
                //if you land on a HomeSpace, the pawn is decommissioned and no longer is active
                case 2:
                    p.safe = true;
                    p.decommissioned = true;
                    MessageBox.Show("Congratulations!  " + p.playerName + "'s pawn number " + p.numberOfPawn + " has reached its Home space!");
                    return;
                //if you land on a SlideEndStartExit, nothing special happens
                case 3:
                    return;
                //if you land on a SafetySpace, you should become safe
                case 4:
                    p.safe = true;
                    p.setSpaceNumber(0);
                    return;
                //if you land on a SafetyEntry, if your next movement is forward, it should move you onto the safety array of the player.  if not, nothing
                case 5:
                    break;
                //if you land on a ConnectingSpace, nothing special should happen
                case 6:
                    return;
                //if you land on a SlideStart, you will start to slide
                case 7:
                    p.slide();
                    break;

                case 8:
                    for (int i = 0; i < players.length; i++)
                    {
                        if (players[i].playerName.Equals(p.playerName))
                        {
                            p.SetSpaceNumber((i * 15) + 2);
                            p.safe = false;
                            break;
                        }
                    }
                    break;

            }
        }

        public void SteppedOn(Pawn p, int startPosition)
        {
            switch (this.type)
            {
                //if you step on a NormalSpace, nothing special happens
                case 0:
                    return;
                //if you step on a SlideEnd, nothing special happens
                case 1:
                    return;
                //if you step on a HomeSpace, you should return to wherever you started, as you did not get into home with the exact amount of steps.  DO YOU GET ANOTHER MOVEMENT?
                case 2:
                    p.SetSpaceNumber(startPosition);
                    MessageBox.Show("You moved too far!  That pawn has been returned to the position it started at this turn.");
                    return;
                //if you step on a SlideEndStartExit, nothing special should happen
                case 3:
                    return;
                //if you step on a SafetySpace, it shouldn't do anything, as you may be getting sent back anyway
                case 4:
                    return;
                //if you step on a SafetyEntry, your next step should be onto the safety array
                case 5:
                    p.safe = true;
                    p.SetSpaceNumber(0);
                    break;
                //if you step on a ConnectingSpace, nothing special should happen
                case 6:
                    return;
                //if you step on a SlideStart, nothing should happen, because you need to land on it
                case 7:
                    return;
                //if you step on a SafetyExit, safe becomes false, and your position becomes 
                case 8:
                    for (int i = 0; i < players.length; i++)
                    {
                        if (players[i].playerName.Equals(p.playerName))
                        {
                            p.SetSpaceNumber((i * 15) + 2);
                            p.safe = false;
                            break;
                        }
                    }
                    break;
            }
        }
    }
}
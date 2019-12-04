﻿using System;
using System.Windows;
using WpfApp1;
using System.Threading;

namespace WpfApp1
{
    //the board class, holds our entire internal implementation of the board class
    [Serializable]
    public class Board
    {
        //stores a mainwindow (our mainwindow)
        public MainWindow main;
        public Player[] players;
        public Space[] landingSpaces;

        //takes an array of players, and a mainWindow
        public Board(Player[] players, MainWindow main)
        {

            //saves the mainwindow as the mainwindow
            this.main = main;
            this.players = players;
            //each corner is a multiple of 15
            landingSpaces = new Space[60];

            //creates the board based on the number of players
            //still need to properly implement creating the slide connecting pieces
            for (int i = 0; i < players.Length; i++)
            {
                //creates a slideStart
                landingSpaces[(i * 15) + 1] = new Space(6, 2, players[i]);
                //creates a connecting space/safety entry
                landingSpaces[(i * 15) + 2] = new Space(7, 3, players[i]);
                //creates a connecting space
                landingSpaces[(i * 15) + 3] = new Space(5, 4);
                //creates a slideEndStartExit
                landingSpaces[(i * 15) + 4] = new Space(3, players[i]);
                //creates a slideStart
                landingSpaces[(i * 15) + 9] = new Space(6, 10, players[i]);
                //creates a connecting space
                landingSpaces[(i * 15) + 10] = new Space(5, 11);
                //creates a connecting space
                landingSpaces[(i * 15) + 11] = new Space(5, 12);
                //creates a connecting space
                landingSpaces[(i * 15) + 12] = new Space(5, 13);
                //creates a slideEnd
                landingSpaces[(i * 15) + 13] = new Space(1);
            }

            //if there are only 3 players, make some slides that don't belong to anyone.
            Player nullPlayer = new Player("null"/*, this.main*/);
            if (players.Length < 4)
            {
                //slide start
                landingSpaces[46] = new Space(6, 47, nullPlayer);
                //connecting spaces
                landingSpaces[47] = new Space(5, 48);
                landingSpaces[48] = new Space(5, 49);
                //slide end 
                landingSpaces[49] = new Space(1);

                //slide start
                landingSpaces[54] = new Space(6, 55, nullPlayer);
                //connected spaces
                landingSpaces[55] = new Space(5, 56);
                landingSpaces[56] = new Space(5, 57);
                landingSpaces[57] = new Space(5, 58);
                //slide end
                landingSpaces[58] = new Space(1);
            }

            //if there are only 2 players, make more slides that don't belong to anyone
            if (players.Length < 3)
            {
                landingSpaces[31] = new Space(6, 32, nullPlayer);
                landingSpaces[32] = new Space(5, 33);
                landingSpaces[33] = new Space(5, 34);
                landingSpaces[34] = new Space(1);
                landingSpaces[39] = new Space(6, 40, nullPlayer);
                landingSpaces[40] = new Space(5, 41);
                landingSpaces[41] = new Space(5, 42);
                landingSpaces[42] = new Space(5, 43);
                landingSpaces[43] = new Space(1);
            }

            //fill the rest of the board with empty spaces
            for (int i = 0; i < landingSpaces.Length; i++)
            {
                if (landingSpaces[i] == null)
                {
                    landingSpaces[i] = new Space(0);
                }
            }
        }

        //
        public void MovePawn(Pawn p, int movementDistance, Boolean forward)
        {
            //So first it needs to check whether or not the pawn is in start(if it's not in start it does the other stuff)
            //YOU GOTTA ADD WHATEVER HAPPENS TO THE BOARD CUZ I HAVE NO CLUE (in the initial if)
            if (p.inStart)
            {
                this.main.drawOutsideStart(p);
            }
            else if (validateFutureLocation(p, movementDistance, forward))
            {
                if (forward)
                {

                    //Does pawnStep right until the before last move
                    for (int i = 0; i < movementDistance - 1; i++)
                    {
                        if(!p.goingIntoSafety)
                        {
                            PawnStep(p, false, true);
                        }
                        else if(p.goingIntoSafety && p.safe == false)
                        {
                            PawnStepFirstSafe(p, false, true);
                        }
                        //PawnStep in safety
                        else
                        {

                        }
                    }
                    //Does pawnStep for checking if there is a pawn there already
                    Boolean collision = checkCollisions(p, true);
                    //If there is, it calls handle collision
                    if (collision)
                    {
                        HandleCollision(p, p.spaceNumber);
                    }
                    //Draws
                    PawnStep(p, true, true);
                }
                else
                {

                    for (int i = 0; i < movementDistance - 1; i++)
                    {
                        PawnStep(p, false, false);
                    }
                    Boolean collision = checkCollisions(p, false);
                    if (collision)
                    {
                        HandleCollision(p, p.spaceNumber);
                    }
                    PawnStep(p, true, false);
                }
            }
            else
            {
                main.ContentLog.Text = "Unfortunately, you already have a pawn in that location.";
            }
        }

        public void HandleCollision(Pawn p, int location)
        {
            if (landingSpaces[p.spaceNumber+ 1].localPawn != null && landingSpaces[p.spaceNumber + 1].localPawn != p)
            {

                ReturnHome(landingSpaces[p.spaceNumber + 1].localPawn);
                //landingSpaces[p.spaceNumber].localPawn = p;
            }
        }

        //moves a pawn by 1 space
        //IF YOU WANT IT TO ACTUALLY DISPLAY EVERY LITTLE WHILE ADD A DISPATCHER
        public void PawnStep(Pawn p, Boolean last, Boolean forward)
        {
            int startingLocation = p.spaceNumber;
            if (!last)
            {

                landingSpaces[p.spaceNumber].localPawn = null;
                p.spaceNumber = p.validateNextLocation(forward);
                SteppedOn(p, landingSpaces[p.spaceNumber], startingLocation);
                this.main.drawAtNextPosition(p);
                return;
            }
            else
            {

                landingSpaces[p.spaceNumber].localPawn = null;
                p.spaceNumber = p.validateNextLocation(forward);
                LandedOn(p, landingSpaces[p.spaceNumber]);
                this.main.drawAtNextPosition(p);
                return;
            }
        }

        public void PawnStepFirstSafe(Pawn p, Boolean last, Boolean forward)
        {
            if(!last)
            {
                this.main.drawInSafety(p);
            }
        }

        public Boolean checkCollisions(Pawn p, Boolean forward)
        {
            if (forward)
            {
                if (landingSpaces[p.spaceNumber + 1].localPawn != null)
                {
                    return true;
                }
            }
            else
            {
                if (landingSpaces[p.spaceNumber - 1].localPawn != null)
                {
                    return true;
                }
            }
            return false;
        }
        //FIX SAFETY ENTRY STUFF
        public void LandedOn(Pawn p, Space s)
        {
            switch (s.type)
            {
                //if you land on a non-safe space, your safety should be set to false
                //if you land on a NormalSpace, nothing special happens
                case 0:
                    s.localPawn = p;
                    return;
                //if you land on a SlideEnd, nothing special happens 
                case 1:
                    s.localPawn = p;
                    return;
                //if you land on a HomeSpace, the pawn is decommissioned and no longer is active
                case 2:
                    p.safe = true;
                    p.decommissioned = true;
                    MessageBox.Show("Congratulations!  " + p.playerName + "'s pawn number " + p.numberOfPawn + " has reached its Home space!");
                    return;
                //if you land on a SlideEndStartExit, nothing special happens
                case 3:
                    s.localPawn = p;
                    return;
                //if you land on a SafetySpace, you should become safe
                case 4:
                    s.localPawn = p;
                    p.safe = true;
                    return;
                //if you land on a ConnectingSpace, nothing special should happen
                case 5:
                    s.localPawn = p;
                    break;
                //if you land on a SlideStart, you will start to slide
                case 6:
                    slide(p, s);
                    s.localPawn = p;
                    break;
                //if you land on a safetyEntry
                case 7:
                    if(s.player.PlayerName.Equals(p.playerName))
                    {
                        s.localPawn = p;
                        p.safe = true;
                        p.SetSpaceNumber(0);
                    }

                    return;
                //if you land on a safetyExit
                case 8:
                    for (int i = 0; i < players.Length; i++)
                    {
                        if (players[i].PlayerName.Equals(p.playerName))
                        {
                            p.SetSpaceNumber((i * 15) + 2);
                            main.drawAtNextPosition(p);
                            p.safe = false;
                            main.drawAtNextPosition(p);
                            break;
                        }
                    }
                    break;

            }
        }

        public void SteppedOn(Pawn p, Space s, int startPosition)
        {
            //if you land on a SafetyEntry, if your next movement is forward, it should move you onto the safety array of the player.  if not, nothing
            switch (s.type)
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
                //if you step on a ConnectingSpace, nothing special should happen
                case 5:
                    return;
                //if you step on a SlideStart, nothing should happen, because you need to land on it
                case 6:
                    return;
                //if you step on a SafetyEntry, your next step should be onto the safety array
                case 7:
                    if (s.player.PlayerName.Equals(p.playerName))
                    {
                        p.goingIntoSafety = true;
                        //p.SetSpaceNumber(0);
                    }

                    break;
                //if you step on a SafetyExit, safe becomes false, and your position becomes 
                case 8:
                    for (int i = 0; i < players.Length; i++)
                    {
                        if (players[i].PlayerName.Equals(p.playerName))
                        {
                            p.SetSpaceNumber((i * 15) + 2);
                            p.safe = false;
                            break;
                        }
                    }
                    break;
            }
        }

        public void slide(Pawn p, Space s)
        {
            if (p.playerName.Equals(s.player.PlayerName))
            {
                s.localPawn = p;
                return;
            }
            else
            {
                for (; !(landingSpaces[p.spaceNumber].type == 1) && !(landingSpaces[p.spaceNumber].type == 3);)
                {
                    if (landingSpaces[p.spaceNumber].localPawn != p)
                    {
                        HandleCollision(p, p.spaceNumber);
                    }
                    MovePawn(p, 1, true);
                }
            }
        }

        //returns false if you can't move there, true if you can.
        public Boolean validateFutureLocation(Pawn p, int distance, Boolean forward)
        {
            //if the pawn is moving forward
            if (forward)
            {
                //makes sure that the location you are moving to is on the board
                int potentialLocation;
                if ((p.spaceNumber + distance) > 59)
                {
                    potentialLocation = (p.spaceNumber + distance) - 60;
                }
                else
                {
                    potentialLocation = p.spaceNumber + distance;
                }

                if(landingSpaces[potentialLocation].localPawn == null)
                {
                    return true;
                }
                //if there is a pawn at that location, and its the current players
                for (int i = 0; i < players.Length; i++)
                {
                    for (int j = 0; j < players[i].pawns.Length; j++)
                    {
                        if (landingSpaces[potentialLocation].localPawn != p && landingSpaces[potentialLocation].localPawn.playerName.Equals(p.playerName))
                        {
                            return false;
                        }
                    }
                }
                return true;
            }
            else
            {
                int potentialLocation;
                if ((p.spaceNumber - distance) < 0)
                {
                    potentialLocation = (p.spaceNumber - distance) + 60;
                }
                else
                {
                    potentialLocation = p.spaceNumber - distance;
                }
                for (int i = 0; i < players.Length; i++)
                {
                    for (int j = 0; i < players[i].pawns.Length; j++)
                    {
                        if (landingSpaces[potentialLocation].localPawn != p && landingSpaces[potentialLocation].localPawn.playerName.Equals(p.playerName))
                        {
                            return false;
                        }
                    }
                }
                return true;
            }
        }

        //sets a pawn to be inStart, puts their position to an impossible value, and draws them in their start location
        public void ReturnHome(Pawn p)
        {
            p.inStart = true;
            p.spaceNumber = 99;
            main.drawAtStart(p);
        }
    }
}
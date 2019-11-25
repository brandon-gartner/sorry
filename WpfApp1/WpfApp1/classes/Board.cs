﻿using System;

public class Board
{

    public Board(Player[] players) {

        //each corner is a multiple of 15
        Space[] landingSpaces = new Space[60];

        //creates the board based on the number of players
        for (int i = 0; i < players.Length; i++)
        {
            landingSpaces[(i * 15) + 4] = new SlideEndStartExit(players[i]);
            landingSpaces[(i * 15) + 1] = new SlideStart(players[i]);
            landingSpaces[(i * 15) + 9] = new SlideStart(players[i]);
            landingSpaces[(i * 15) + 13] = new SlideEnd(players[i]);
            landingSpaces[(i * 15) + 2] = new SafetyEntry(players[i]);
        }

        //if there are only 3 players, make some slides that don't belong to anyone.
        Player nullPlayer = new Player();
        if (players.Length < 4)
        {
            landingSpaces[46] = new SlideStart(nullPlayer);
            landingSpaces[54] = new SlideStart(nullPlayer);
            landingSpaces[58] = new SlideEnd(nullPlayer);
            landingSpaces[49] = new SlideEnd(nullPlayer);
        }

        //if there are only 2 players, make more slides that don't belong to anyone
        if (players.Length < 3)
        {
            landingSpaces[31] = new SlideStart(nullPlayer);
            landingSpaces[31] = new SlideStart(nullPlayer);
            landingSpaces[43] = new SlideEnd(nullPlayer);
            landingSpaces[34] = new SlideEnd(nullPlayer);
        }


    }

    public void movePawn(Pawn p, int movementDistance)
    {
        possibleLocation = p.validateFutureLocation();
        p.spaceNumber = possibleLocation;

    }
        /*//loop to create startExits
        for (int i = 4; i < 60; i += 15)
        {
            landingSpaces[i] = new SlideEndStartExit();
        }

        //creates one of each player's slide starts
        for (int i = 1; i < 60; i += 15)
        {
            landingSpaces[i] = new SlideStart(Player p);
        }

        //creates the locations where players enter the safety zones
        for (int i = 2; i < 60; i += 15)
        {
            landingSpaces[i] = new SafetyEntry(Player p);
        }
        */
}

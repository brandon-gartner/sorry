using System;

public class Board
{

    public Board(Player[] players) {

        //each corner is a multiple of 15
        ISpace[] landingSpaces = new ISpace[60];

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
        Player nullPlayer = new Player("false", null);
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
        int possibleLocation = p.validateFutureLocation(movementDistance);
        p.setSpaceNumber(possibleLocation);

    }
}

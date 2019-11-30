using System;
using WpfApp1;

//the board class, holds our entire internal implementation of the board class
public class Board
{
    //stores a mainwindow (our mainwindow)
    MainWindow main;
    Player[] players;

    //takes an array of players, and a mainWindow
    public Board(Player[] players, MainWindow main) {

        //saves the mainwindow as the mainwindow
        this.main = main;
        this.players = players;
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
        Player nullPlayer = new Player("false", "null", this.main);
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

        //fill the rest of the board with empty spaces
        for (int i = 0; i < landingSpaces.Length; i++)
        {
            if (landingSpaces[i] == null) {
                landingSpaces[i] = new NormalSpace();
            }
        }
    }


    public void movePawn(Pawn p, int movementDistance)
    {
        for (int i = 0; i < movementDistance - 1; i++)
        {
            pawnStep(p, false);
        }
        Boolean collision = pawnStep(p, true);
        if (collision)
        {
            handleCollision(p, (p.spaceNumber + movementDistance);
        }
    }

    public void handleCollision(Pawn p, int location)
    {

    }

    //moves a paw
    public Boolean pawnStep(Pawn p, Boolean last)
    {
        if (!last)
        {
            p.setSpaceNumber(p.spaceNumber + 1);
            return false;
        }
        else
        {
            for (int i = 0; i < players.Length; i++)
            {
                for (int j = 0; i < players[i].pawns.Length; i++)
                {
                    if (players[i].pawns[j].spaceNumber == p.spaceNumber + 1)
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }
}

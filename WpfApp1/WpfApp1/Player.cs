using System;
using WpfApp1;


[Serializable]
public class Player
{
    public String PlayerName { get; set; }
    public ISpace[] SafetySpaceAndHome = new ISpace[6];
    public Pawn[] pawns;
    public Boolean realPlayer;



    /*So the constructor sets the player's name as well as which color they chose */
    public Player(string playerName, String color/*, MainWindow main*/)
    {
        //this.main = main;
        this.pawns = new Pawn[3];
    this.PlayerName = playerName;
    for (int i = 0; i < 3; i++)
        {
            this.pawns[i] = new Pawn(i, color);
        }
        InitialisePlayersBoard();

    }

    //if it is an AI player
    public Player(String color)
    {
        //MainWindow main = new WpfApp1.MainWindow();
        //MainWindow main = main;
        this.pawns = new Pawn[3];
        for (int i = 0; i < 3; i++)
        {
            this.pawns[i] = new Pawn(i, color);
        }
        this.PlayerName = "CPU";
        InitialisePlayersBoard();
    }

    public void InitialisePlayersBoard()
    {
        for (int i = 0; i < 5; i++)
        {
            this.SafetySpaceAndHome[i] = new SafetySpace(this);
        }
        this.SafetySpaceAndHome[5] = new HomeSpace();
    }


    public Boolean PlayTurn()
    {
        Boolean hasWon = true;

        for (int i = 0; i < 3; i++)
        {
            if (this.pawns[i].decommissioned)
            {
                hasWon = false;
            }
        }
        MainWindow main = new WpfApp1.MainWindow();
        main.DrawCard.IsEnabled = true;
        return hasWon;
    }
    
    /*This method is for actually drawing everything relating to the player on the xaml page*/
    public void DrawPlayer()
    {
        /*So we'll have to add a couple of things (like displaying the names and such for the board) for now the pawns are ok*/


        /*Drawing the pawns*/
        for (int i = 0; i < this.pawns.Length; i++)
        {
            //MainWindow main = new WpfApp1.MainWindow();
            //main._0.Child = this.pawns[i].image;
        }
    }
}
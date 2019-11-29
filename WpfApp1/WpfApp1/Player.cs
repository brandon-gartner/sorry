using System;
using WpfApp1;



public class Player
{
    public String PlayerName { get; set; }
    public ISpace[] SafetySpaceAndHome;
    public Pawn[] pawns;
    public MainWindow main;



    /*So the constructor sets the player's name as well as which color they chose */
    public Player(string playerName, String color, MainWindow main)
    {
    this.main = main;
    this.pawns = new Pawn[3];
        this.PlayerName = playerName;
        for (int i = 0; i < 3; i++)
        {
            this.pawns[i] = new Pawn(i, color);
        }
    }

    public void InitialisePlayersBoard()
    {
        for (int i = 0; i < 5; i++)
        {
            this.SafetySpaceAndHome[i] = new SafetySpace(this);
        }
        this.SafetySpaceAndHome[5] = new HomeSpace();
    }

    public void playTurn()
    {
        main.DrawCard.IsEnabled = true;
    }
    
    /*This method is for actually drawing everything relating to the player on the xaml page*/
    public void drawPlayer()
    {
        /*So we'll have to add a couple of things (like displaying the names and such for the board) for now the pawns are ok*/


        /*Drawing the pawns*/
        for (int i = 0; i < this.pawns.Length; i++)
        {
            main._0.Child = this.pawns[i].image;
        }
    }
}
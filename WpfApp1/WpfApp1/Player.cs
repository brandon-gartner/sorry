using System;

public class Player
{
    public String PlayerName { get; set; }
    public ISpace[] SafetySpaceAndHome;
    public Pawn[] pawns;

	public Player(string playerName)
	{
        for (int i = 0; i < 3; i++)
        {

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

    
}

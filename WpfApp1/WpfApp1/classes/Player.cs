using System;

public class Player
{
    public String playerName { get; set; }
    public ISpace[] safetySpaceAndHome;
    public Pawn[] pawns;

	public Player(String playerName)
	{
        this.playerName = playerName;
        for (int i = 0; i < 3; i++)
        {
            pawns[i] = new Pawn(this, i);
        }

	}

    public void initialisePlayersBoard()
    {
        for (int i = 0; i < 5; i++)
        {
            safetySpaceAndHome[i] = new SafetySpace(this);
        }
        safetySpaceAndHome[5] = new HomeSpace();
    }

    public void runTurn()
    {
        //stuff involving picking a card, running the card's whenPicked (or whatever its called) method
        //doing what the card entails
        //end of turn?
    }
    
}

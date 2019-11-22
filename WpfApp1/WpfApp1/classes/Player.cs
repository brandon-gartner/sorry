using System;

public class Player
{
    public String playerName { get; set; }
    public Space[] safetySpaceAndHome;

	public Player()
	{
        for (int i = 0; i < 5; i++)
        {
            safetySpaceAndHome[i] = new SafetySpace(Player p);
        }
        safetySpaceAndHome[5] = new HomeSpace();
	}

    
}

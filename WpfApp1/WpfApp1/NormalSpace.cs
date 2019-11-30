using System;


// the normal spaces that exist on most locations
public class NormalSpace : ISpace
{
    Boolean occupied { get; set; }
	public NormalSpace()
	{
	}

    public void landedOn(Pawn p)
    {
        return;
    }
}

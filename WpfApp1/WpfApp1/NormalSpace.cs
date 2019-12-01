using System;


// the normal spaces that exist on most locations
public class NormalSpace : ISpace
{
	public NormalSpace()
	{
	}



    //should check for collisions, and if there is one, return the piece that was there first to its start.
    public void LandedOn(Pawn p)
    {
        return;
    }

    //literally does nothing
    public void SteppedOn(Pawn p)
    {
        return;
    }
}

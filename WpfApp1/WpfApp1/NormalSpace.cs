using System;

public class NormalSpace : ISpace
{
    Boolean occupied { get; set; }
	public NormalSpace()
	{
	}

    public void landedOn(Pawn p)
    {
        throw new NotImplementedException();
    }
}

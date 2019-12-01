using System;
//place where pawns start. cannot be landed on, so nothing should happen if it is landed on.
//space at the end of the safety space array
[Serializable]
public class HomeSpace : ISpace
{

    //when you land on it, that pawn is out of the game
    public void LandedOn(Pawn p)
    {
        p.decommissioned = true;
        return;
    }

    public void SteppedOn(Pawn p)
    {
        return;
    }
}
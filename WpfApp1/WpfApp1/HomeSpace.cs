//place where pawns start. cannot be landed on, so nothing should happen if it is landed on.
public class HomeSpace : ISpace
{

    public void landedOn(Pawn p)
    {
        return;
    }
}
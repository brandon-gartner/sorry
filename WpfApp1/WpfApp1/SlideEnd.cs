internal class SlideEnd : ISpace
{
    private Player nullPlayer;

    public SlideEnd(Player nullPlayer)
    {
        this.nullPlayer = nullPlayer;
    }

    public void landedOn(Pawn p)
    {
        throw new System.NotImplementedException();
    }
}
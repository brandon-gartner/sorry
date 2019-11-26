internal class SlideStart : ISpace
{
    private Player nullPlayer;

    public SlideStart(Player nullPlayer)
    {
        this.nullPlayer = nullPlayer;
    }

    public void landedOn(Pawn p)
    {
        throw new System.NotImplementedException();
    }
}
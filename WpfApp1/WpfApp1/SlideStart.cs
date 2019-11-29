//the class for the beginning of a slide
internal class SlideStart : ISpace
{
    private Player player;

    //takes a player from the program and reads it as the owner player
    public SlideStart(Player player)
    {
        this.player = player;
    }

    public void landedOn(Pawn p)
    {
        throw new System.NotImplementedException();
    }
}
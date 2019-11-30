//the class for the beginning of a slide
internal class SlideStart : ISpace
{
    private Player player;

    //takes a player from the program and reads it as the owner player
    public SlideStart(Player player, int connectedPiece)
    {
        this.player = player;
    }

    //when this gets landed on, should check whether you slide or not.  if you do, steps to the connectedPiece, which steps to the connectedPiece.
    //this will use a modified step method, which returns pieces to their home bases if you step on them.  they continue to move forward until a connectedpiece sends you to one which is a slideend.
    public void landedOn(Pawn p)
    {

    }
}
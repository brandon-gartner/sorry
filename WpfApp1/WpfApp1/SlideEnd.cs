using System;

[Serializable]
internal class SlideEnd : ISpace
{
    private Player player;

    public SlideEnd(Player player)
    {
        this.player = player;
    }

    //when the special slide step touches this or slideendstartexit, they stop moving here.
    public void landedOn(Pawn p)
    {
        throw new System.NotImplementedException();
    }

    public void steppedOn(Pawn p)
    {

    }
}
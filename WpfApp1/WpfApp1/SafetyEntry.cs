internal class SafetyEntry : ISpace
{
    private Player player;

    public SafetyEntry(Player player)
    {
        this.player = player;
    }

    public void landedOn(Pawn p)
    {
        throw new System.NotImplementedException();
    }
}
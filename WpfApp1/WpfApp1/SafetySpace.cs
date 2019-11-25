internal class SafetySpace : ISpace
{
    private Player player { get; }

    public SafetySpace(Player player)
    {
        this.player = player;
    }

    public void landedOn(Pawn p)
    {
        throw new System.NotImplementedException();
    }
}
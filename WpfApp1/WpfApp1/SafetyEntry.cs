using System;
using WpfApp1;

[Serializable]
internal class SafetyEntry : ISpace
{
    //stores the player which this space belongs to
    private Player player;


    //takes a player as input, which it stores as the owner player
    public SafetyEntry(Player player)
    {
        this.player = player;
    }

    //if a pawn steps over it, which belongs to the owner player, they will move onto the safety space next;
    public void LandedOn(Pawn p)
    {
        throw new System.NotImplementedException();
    }

    public void SteppedOn(Pawn p)
    {

    }
}
using System;
using WpfApp1;

namespace WpfApp1
{

    [Serializable]
    internal class SlideEnd : ISpace
    {
        private Player player;

        public SlideEnd(Player player)
        {
            this.player = player;
        }

        //when the special slide step touches this or slideendstartexit, they stop moving here.
        public void LandedOn(Pawn p)
        {
            throw new System.NotImplementedException();
        }

        public void SteppedOn(Pawn p)
        {

        }
    }
}
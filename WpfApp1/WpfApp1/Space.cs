using System;
using WpfApp1;

namespace WpfApp1{

    //interface which all spaces implement
    public interface ISpace
    {
        //occurs when a space is landed on
        void LandedOn(Pawn p);

        //occurs when a space is being stepped over (aka you step on it, but your pawn continues after that move)
        void SteppedOn(Pawn p);


    }
}
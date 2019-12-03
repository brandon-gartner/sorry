using System;
using System.Windows;
using WpfApp1;

namespace WpfApp1{
    [Serializable]
    //interface which all spaces implement
    public class Space
    {
        public int type;
        public Player player;
        public int connectedSpace;
        public Pawn localPawn;

        //the contructor for the spaces which don't have an associated player or connected space
        //this means NormalSpaces (0), SlideEnds (1), HomeSpaces (2)
        public Space(int type)
        {
            if (type > 2 || type < 0)
            {
                throw new ArgumentException("wrong input for space constructor with only a type");
            }
            this.type = type;
        }

        //the constructor for the spaces which are associated with a certain player
        //this means SlideEndStartExit (3), SafetySpace (4),  
        public Space(int type, Player player)
        {
            if (type > 4 || type < 3)
            {
                throw new ArgumentException("wrong input for space constructor with a type and a player");
            }
            this.type = type;
            this.player = player;
        }

        //the constructor for the spaces which are associated with a certain connected space
        //this means ConnectingSpace (5)
        public Space(int type, int connectedSpace)
        {
            if (type != 5)
            {
                throw new ArgumentException("wrong input for space constructor with a type and a connectedSpace");
            }
            this.type = type;
            this.connectedSpace = connectedSpace;
        }

        //the constructor for the spaces which have a player and a connected space
        //this means SlideStart (6), SafetyEntry (7), SafetyExit (8)
        public Space(int type, int connectedSpace, Player player)
        {
            if (type > 8 || type < 6)
            {
                throw new ArgumentException("wrong input for space constructor with a type, a connectedSpace, and a player");
            }
            this.type = type;
            this.player = player;
            this.connectedSpace = connectedSpace;
        }



        
    }
}
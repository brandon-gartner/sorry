using System;
using WpfApp1;

namespace WpfApp1
{
    [Serializable]
    public class Card
    {
        //fire and ice rule video explanation https://www.youtube.com/watch?v=IKudEqXbTUY
        //name of the card
        private int cardId;

        public Card(int cardId)
        {
            this.cardId = cardId;
        }
        public int getCard_Id()
        {
            return this.cardId;
        }
    }
}
using System;

public class Deck
{
    //a deck needs to have a minimum of 45 cards 
    Card[] deck = new Card[45];
	public Deck()
	{
        deck = createDeck(deck);

    }
    private Card[] createDeck(Card[] inputDeck)
    {

    }
    private void createCard1()
    {
        Card tempCard = new Card(1);
        for (int i = 0;i < 5;i++)
        {
            deck[i] = tempCard;
        }
    }

    private void createCard2()
    {
        Card tempCard1 = new Card(1);
        Card tempCard2 = new Card(2);
        Card tempCard3 = new Card(3);
        Card tempCard4 = new Card(4);
        Card tempCard5 = new Card(5);
        Card tempCard7 = new Card(7);
        Card tempCard8 = new Card(8);
        Card tempCard9 = new Card(10);
        Card tempCard10 = new Card(11);
        Card tempCard12 = new Card(12);
        Card tempCard-1 = new Card(-1);
        for (int i = 0; i < 45; i++)
        {
            if (i < 5)
            {
                deck[i] = tempCard1;
            }
        }
    }
}

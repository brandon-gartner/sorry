using System;

public class Deck
{
    //a deck needs to have a minimum of 45 cards 
    Card[] deck = new Card[45];
    //this variable is used to be able to get the next card in the deck
    int deckCount;


	public Deck()
	{
        createDeck();
        deckCount=0;
    }

    private void createDeck()
    {
        //create each different possible card
        Card tempCard1 = new Card(1);
        Card tempCard2 = new Card(2);
        Card tempCard3 = new Card(3);
        Card tempCard4 = new Card(4);
        Card tempCard5 = new Card(5);
        Card tempCard7 = new Card(7);
        Card tempCard8 = new Card(8);
        Card tempCard10 = new Card(10);
        Card tempCard11 = new Card(11);
        Card tempCard12 = new Card(12);
        Card tempCardMinus1 = new Card(-1);

        //this loop wil create all of the cards with the correct proportions
        for (int i = 0; i < 45; i++)
        {
            //card 1
            if (i < 5)
            {
                this.deck[i] = tempCard1;
            }
            //card 2
            if (i<5 && i<9)
            {
                this.deck[i] = tempCard2;
            }
            //card 3
            if (i<9 && i<13)
            {
                this.deck[i] = tempCard3;
            }
            //card 4
            if (i<13 && i<17)
            {
                this.deck[i] = tempCard4;
            }
            //card 5
            if (i<17 && i<21)
            {
                this.deck[i] = tempCard5;
            }
            //card 7
            if (i<21 && i<25)
            {
                this.deck[i] = tempCard7;
            }
            //card 8
            if (i<25 && i<29)
            {
                this.deck[i] = tempCard8;
            }
            //card 10
            if (i<29 && i<33)
            {
                this.deck[i] = tempCard10;
            }
            //card 11
            if (i<33 && i<37)
            {
                this.deck[i] = tempCard11;
            }
            //card 12
            if (i<37 && i<41)
            {
                this.deck[i] = tempCard12;
            }
            //card -1
            if (i<41 && i<45)
            {
                this.deck[i] = tempCardMinus1;
            }
        }
    }

    //this method is used to shuffle the position of the cards randomly
    public void shuffle (){
        Card swapCard;
        Card currentCard;
        int randPosition;
        Random randNum = new Random();
        for (int i = 0; i < this.deck.Length;i++ ){
            randPosition = randNum.Next(0, 44);
            currentCard=this.deck[i];
            swapCard=this.deck[randPosition];
            this.deck[i]=swapCard;
            this.deck[randPosition]=currentCard;
        }
        deckCount=0;
    }
    //this method will get the next card in the neck and if it reaches the end of the deck and it will reshuffle the deck reset the deckCount
    public Card getNextCard(){
        if(deckCount == this.deck.Length){
            shuffle();
        }
        deckCount++;
        return this.deck[deckCount-1];
    }
    public Card[] getDeck(){
        return this.deck;
    }
}
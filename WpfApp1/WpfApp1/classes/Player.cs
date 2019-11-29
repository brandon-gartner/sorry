using System;

public class Player
{
    public String playerName { get; set; }
    public ISpace[] safetySpaceAndHome;
    public Pawn[] pawns;

	public Player(String playerName)
	{
        this.playerName = playerName;
        for (int i = 0; i < 3; i++)
        {
            pawns[i] = new Pawn(this, i);
        }

	}

    public void initialisePlayersBoard()
    {
        for (int i = 0; i < 5; i++)
        {
            safetySpaceAndHome[i] = new SafetySpace(this);
        }
        safetySpaceAndHome[5] = new HomeSpace();
    }

    public void playTurn()
    {
        //stuff involving picking a card, running the card's whenPicked (or whatever its called) method
        //doing what the card entails
        Pawn selectedPawn = this.selectPawn();

    }

    public Pawn selectPawn()
    {
        String options = "";
        for (int i = 0; i < pawns.Length; i++)
        {
            if (pawns[i].safe)
            {
                options += Convert.ToString(i) + ", ";
            }
            do
            {
                int optionSelected = (Microsoft.VisualBasic.Interaction.InputBox("Which pawn would you like to select?  Your options are " + options, "Pawn to select?", "Please enter one of the options."));
            } while ((optionSelected < 1 || optionSelected > 3) || pawns[optionSelected].safe);

            return pawns[optionSelected];
        }
    }
    
}

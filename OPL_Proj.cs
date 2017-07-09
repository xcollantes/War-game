using System;
using System.Collections.Generic;
using System.Linq;

public class WarGame
{
	delegate void winnerOutput(string s);
	public static void Main()
	{
		winnerOutput winOutput = n => { string s = n + " wins the game!  Woot Woot!"; Console.WriteLine(s); };	// Functional Aspect 1 - Lambda Expression

		List<Tuple<int, string>> Deck = new List<Tuple<int, string>>();	// Functional Aspect 2 - Tuple
		int x = 0;

		int[] numbers = { 3,2,0,1 };	// Functional Aspect 3 - Extension Method
		var suits = numbers.OrderBy(g => g);

		foreach (int b in suits){
			for (int a = 2; a < 15; a++){
				if (b == 0)
					Deck.Add(Tuple.Create (a, " Clubs"));
				else if (b == 1)
					Deck.Add(Tuple.Create (a, " Spades"));
				else if (b == 2)
					Deck.Add(Tuple.Create (a, " Diamonds"));
				else
					Deck.Add(Tuple.Create (a, " Hearts"));
				x++;
			}
		}

		List<int> numRange = new List<int>();	// Create unique random numbers 0-51
		for (int i = 0; i < Deck.Count; i++)
			numRange.Add(i);
		List<int> randList = new List<int>();
		Random r = new Random();
		for (int i = 0; i < Deck.Count; i++){
			int randNum = r.Next(1,numRange.Count) - 1;
			randList.Add(numRange[randNum]);
			numRange.RemoveAt(randNum);
		}

		List<Tuple<int, string>> randDeck = new List<Tuple<int, string>>();	// Randomize the sorted deck
		for(int g = 0; g < Deck.Count; g++){
			randDeck.Add(Deck[randList[g]]);
		}
			
		List<Tuple<int, string>> p1Deck = new List<Tuple<int, string>>();	//Divide randomized deck into 2 decks
		List<Tuple<int, string>> p2Deck = new List<Tuple<int, string>>();
		for (int c = 0; c < 26; c++){
			p1Deck.Add(Tuple.Create (randDeck [c].Item1, randDeck [c].Item2));
			p2Deck.Add(Tuple.Create (randDeck [c+26].Item1, randDeck [c+26].Item2));
		}

		int z = 0;
		int count = 1;
		bool playGame = true;
		while (playGame) {
			if (p1Deck.Count == 52) {
				//Console.WriteLine ("Player 1 wins the game!  Woot Woot!");
				winOutput ("Player 1");
				playGame = false;
			}else if(p2Deck.Count == 52){
				//Console.WriteLine ("Player 2 wins the game!  Woot Woot!");
				winOutput ("Player 2");
				playGame = false;
			}else{
				try{
					Console.WriteLine ("Round " + count + "... ");
					if(p2Deck[z].Item1 < 100){}
					if(p1Deck[z].Item1 < 11)
						Console.Write ("Player 1 drew " + p1Deck [z].Item1 + " of " + p1Deck [z].Item2);
					else if (p1Deck [z].Item1 == 11)
						Console.Write ("Player 1 drew Jack of " + p1Deck[z].Item2);
					else if (p1Deck [z].Item1 == 12)
						Console.Write ("Player 1 drew Queen of " + p1Deck[z].Item2);
					else if(p1Deck [z].Item1 == 13)
						Console.Write("Player 1 drew King of "  + p1Deck[z].Item2);
					else if(p1Deck [z].Item1 == 14)
						Console.Write("Player 1 drew Ace of " + p1Deck[z].Item2);
					if(p2Deck[z].Item1 < 11)
						Console.WriteLine(" and Player 2 drew " + p1Deck [z].Item1 + " of " + p2Deck [z].Item2);
					else if (p2Deck [z].Item1 == 11)
						Console.WriteLine(" and Player 2 drew Jack of " + p2Deck[z].Item2);
					else if (p2Deck [z].Item1 == 12)
						Console.WriteLine(" and Player 2 drew Queen of " + p2Deck[z].Item2);
					else if(p2Deck [z].Item1 == 13)
						Console.WriteLine(" and Player 2 drew King of "  + p2Deck[z].Item2);
					else if(p2Deck [z].Item1 == 14)
						Console.WriteLine(" and Player 2 drew Ace of " + p2Deck[z].Item2);
					if (p1Deck [z].Item1 > p2Deck [z].Item1) {
						Console.WriteLine ("Player 1 won " + (z+1) + " card(s) this round.");	
						while (z > 0) {
							p1Deck.Add (p2Deck [0]);	
							p2Deck.RemoveAt (0);
							p1Deck.Add (p1Deck [0]);
							p1Deck.RemoveAt (0);
							z--;
						}
						if (z == 0) {
							p1Deck.Add (p2Deck [0]);	
							p2Deck.RemoveAt (0);
							p1Deck.Add (p1Deck [0]);
							p1Deck.RemoveAt (0);
						}
					}else if (p2Deck [z].Item1 > p1Deck [z].Item1) {
						Console.WriteLine ("Player 2 won " + (z+1) + " card(s) this round.");
						while (z > 0) {
							p2Deck.Add (p1Deck [0]); 
							p1Deck.RemoveAt (0);
							p2Deck.Add (p2Deck [0]);
							p2Deck.RemoveAt (0);
							z--;
						}
						if (z == 0) {
							p2Deck.Add (p1Deck [0]); 
							p1Deck.RemoveAt (0);
							p2Deck.Add (p2Deck [0]);
							p2Deck.RemoveAt (0);
						}
					}else {
						Console.WriteLine ("War Case " + (z/4+1) +"...!  WARRRRR HRRRRR!!!!!");
						z += 4; 
					}
				}catch(ArgumentOutOfRangeException e){
					Console.WriteLine ("Unique War Case... Neither player wins.  ");
					playGame = false;
				}
			}
			count++;
		}
	}
}

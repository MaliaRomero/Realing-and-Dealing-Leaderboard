using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    public string deckName;
    public List<Card> cards = new List<Card>();
    // The discard pile for the deck
    public List<Card> discardPile = new List<Card>();

     public void ResetDeck()
    {
        // Move all cards from discard pile back to the main deck
        cards.AddRange(discardPile);
        discardPile.Clear();

        //This connects to the !hasbeenplayed check in OnMouseDown in Card class
        //Basically prevents double discard/only one discard per turn
        //Lost myself in the logic a bit and flag has stupid name- need to revisit this later
        //Because it is called at the end of the deck, the has been played flag will be true
        //for all cards and will need to be reset so player can continue to discard them.
        foreach (Card card in cards)
        {
            card.hasBeenPlayed = false; // Reset the hasBeenPlayed flag
        }
    }
}

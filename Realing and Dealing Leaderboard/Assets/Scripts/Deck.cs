using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    public string deckName;
    public List<Card> cards = new List<Card>();
    // The discard pile for the deck
    public List<Card> discardPile = new List<Card>();
}

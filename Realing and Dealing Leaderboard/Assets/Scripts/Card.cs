using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public bool hasBeenPlayed;
    public int handIndex;
    private GameManager gm;
    private Deck currentDeck;
    public Deck originDeck;  // Add a reference to the deck this card belongs to for discard
//---------------------------------

    public int pointValue;  // The point value of this card
    public int baitValue;   // The bait value of this card when discarded

    private void Start()
    {
        gm = GameManager.instance;  // Access the GameManager instance

        // Add points when the card enters the hand
        GameManager.playerController.AddPoints(pointValue);

    }
    private void OnMouseDown()
    {
        // Check if the card has not been played yet
        if ((!hasBeenPlayed) && gm.skipDiscardButton.activeSelf)
        {   
            gm.HideSkipDiscardButton();

            // Mark it as played and free up the card slot in hand
            hasBeenPlayed = true;
            gm.availableCardSlots[handIndex] = true;

           // Adjust points and bait when the card is discarded
            GameManager.playerController.RemovePoints(pointValue);  // Subtract points when playing the card
            GameManager.playerController.AddBait(baitValue);        // Increase bait value as the card is discarded
            GameManager.instance.cardCounter--;
            Debug.Log("Hand-" + GameManager.instance.cardCounter);
            MoveToDiscardPile();

            // Trigger an event in GameManager and end the turn
            //ADD SKIP DISCARD
            GameManager.instance.TriggerEvent();
            GameManager.instance.EndTurn();
            
        }
    }

    void MoveToDiscardPile()
    {
        // Ensure the card has an origin deck to move to its discard pile
        if (originDeck != null)
        {
            Debug.Log("Move to discard pile");

            // Add the card to the discard pile of its origin deck
            originDeck.discardPile.Add(this);

            // Deactivate the card so it is no longer visible or interactive
            gameObject.SetActive(false);
        }

        // Disable the MeshRenderer to hide the card visually if not deactivated
        MeshRenderer renderer = GetComponent<MeshRenderer>();
        if (renderer != null)
        {
            renderer.enabled = false; // Turn off visibility
        }
    }
}
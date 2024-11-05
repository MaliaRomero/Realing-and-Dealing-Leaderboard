using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //--------THIS SCRIPT HANDLES UI AND BAIT/TROPHY COUNTERS--------
    public static PlayerController me;

    public int baitCount = 3;

    public int points;

    public void AddPoints(int amount)
    {
        points += amount;
        Debug.Log("Points added: " + amount + ". Total points: " + points);
        UpdateTrophyPointsUI(points);

    }

    public void RemovePoints(int amount)
    {
        points -= amount;
        Debug.Log("Points removed: " + amount + ". Total points: " + points);
        UpdateTrophyPointsUI(points);

    }

    public void AddBait(int amount)
    {
        baitCount += amount;
        UpdateBaitUI();
    }

    public void RemoveBait(int amount)
    {
        baitCount -= amount;
        UpdateBaitUI();
    }

    public void UpdateBaitUI()
    {
        GameManager.instance.tackleBoxText.text = "Bait: " + baitCount.ToString();
    }

    public void UpdateTrophyPointsUI(int points)
    {
        GameManager.instance.trophyPointsText.text = "Trophy Points: " + points.ToString();
    }
    
     public int GetBaitCost(int deckIndex)
    {
        if (deckIndex == 0) return 1; // Deck 1 costs 1 bait
        if (deckIndex == 1) return 2; // Deck 2 costs 2 bait
        if (deckIndex == 2) return 3;
        return 0; // Default cost
    }
}
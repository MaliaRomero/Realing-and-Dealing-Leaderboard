using UnityEngine;

public class NextButton : MonoBehaviour
{
    public GameObject[] panels; // Array to hold the tutorial panels

    public GameObject nextButton;
    private int currentPanelIndex = 0; // Index of the current active panel

    // Call this method on button press
    public void NextPanel()
    {
        if (currentPanelIndex < panels.Length) //if not end, turn off then move counter up
        //then turn on next
        {
            panels[currentPanelIndex].SetActive(false);
            currentPanelIndex++;
            if (currentPanelIndex < panels.Length)
            {
                panels[currentPanelIndex].SetActive(true);
            }
            else
            {
                nextButton.SetActive(false);
            }
        }
    }

    private void Start()
    {
        // Ensure only the first panel is active at the start
        for (int i = 0; i < panels.Length; i++)
        {
            panels[i].SetActive(i == 0);
        }
    }
}

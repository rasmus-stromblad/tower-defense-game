using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    public SceneFader sceneFader;

    public Button[] levelButtons;

    void Start()
    {
        // Enables to save game progress in the PlayerPrefs. LevelReached is the value we get and it is set as 1 the first time the game is started
        int levelReached = PlayerPrefs.GetInt("levelReached", 1);

        for (int i = 0; i < levelButtons.Length; i++)
        {
            // If the current value of the button is greater than number of levels reached
            if(i + 1 > levelReached)
            {
                // Disable each button
                levelButtons[i].interactable = false;
            }
            
        }
    }

    public void Select(string levelName)
    {
        sceneFader.FadeTo(levelName);
    }
}

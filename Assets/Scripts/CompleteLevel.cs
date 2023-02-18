using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompleteLevel : MonoBehaviour
{
    public string menuSceneName = "MainMenu";

    public string nextLevel = "Level02";
    public int levelToUnlock = 2;

    public SceneFader sceneFader;

    public void Continue()
    {
        // If the level to unlock is greater than the highest saved level
        if(levelToUnlock > PlayerPrefs.GetInt("levelReached"))
        {
            // Set a new value to the levelReached key. This value is set in the inspector and is the next level after the current
            PlayerPrefs.SetInt("levelReached", levelToUnlock);
            sceneFader.FadeTo(nextLevel);
        }
        else
        {
            sceneFader.FadeTo("LevelSelector");
        }
        
    }

    public void Menu()
    {
        sceneFader.FadeTo(menuSceneName);
    }

}

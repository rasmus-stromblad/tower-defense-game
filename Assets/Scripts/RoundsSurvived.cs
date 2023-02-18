using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RoundsSurvived : MonoBehaviour
{
    public TMP_Text roundsText;

    // Called every time this gameObject is enabled (every time the panel is enabled)
    void OnEnable()
    {
        StartCoroutine(AnimateText());
    }

    IEnumerator AnimateText()
    {
        roundsText.text = "0";
        int round = 0;

        // Wait .7 seconds before beginning to count
        yield return new WaitForSeconds(.7f);

        while(round < PlayerStats.rounds)
        {
            round++;
            roundsText.text = round.ToString();

            // Wait .05 seconds before running the code again. This makes the rounds text visually count up
            yield return new WaitForSeconds(.05f);
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{

    public ScoreSystem scoreSystem;

    public TMPro.TextMeshProUGUI uiLabel;

    private void Update()
    {
        //scoreSystem.score += 10;

        uiLabel.text = "Score: " + scoreSystem.score;
    }


}

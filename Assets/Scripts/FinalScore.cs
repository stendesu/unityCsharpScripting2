using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalScore : MonoBehaviour
{
    public ScoreSystem scoreSystem;
    public GameObject scoreSystemObject;

    public TMPro.TextMeshProUGUI uiLabel;

    // Start is called before the first frame update
    void Start()
    {

        scoreSystemObject = GameObject.Find("ScoreSystemObject");
        scoreSystem = scoreSystemObject.GetComponent<ScoreSystem>();

        uiLabel.text = "Total Score: " + scoreSystem.score;
    }


}

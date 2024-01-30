using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ScoreSystem : MonoBehaviour
{
    public int score;
    public Button AddScoreButton;

    public void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;
    }

    // Start is called before the first frame update
    void Start()
    {
        Button btn = AddScoreButton.GetComponent<Button>();
        
        AddScoreButton.onClick.AddListener(handleOnClick);
    }

    public void handleOnClick()
    {
        Debug.Log("clicked it");
        AddScore(5);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}

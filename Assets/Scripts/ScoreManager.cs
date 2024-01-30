using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{

    public TextMeshProUGUI scoreLabel;

    public int score = 0;

    public void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreLabel.text = "Score: " + score;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

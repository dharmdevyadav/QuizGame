using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class EndScreen : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI EndText;
    ScoreKeeper endScore;

    void Awake()
    {
        endScore = FindObjectOfType<ScoreKeeper>();
    }

    public void showFinalScore()
    {
        EndText.text = "Congratulation !\n" + "You Scored: " +endScore.CalculateScore() + "%";
    }

   
}

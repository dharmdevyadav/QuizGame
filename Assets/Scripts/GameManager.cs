using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
   
    Quiz quiz;
    EndScreen endScreen;
    private void Awake()
    {
        quiz = FindObjectOfType<Quiz>();
        endScreen = FindObjectOfType<EndScreen>();

    }
    void Start()
    {
        
        quiz.gameObject.SetActive(true);
        endScreen.gameObject.SetActive(false);
    }

    void Update()
    {
        if (quiz.isComplete)
        {
            quiz.gameObject.SetActive(false);
            endScreen.gameObject.SetActive(true);
            endScreen.showFinalScore();
        }
    }
    public void LoadnextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex) ;
    }

   
}

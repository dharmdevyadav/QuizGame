using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Image=UnityEngine.UI.Image;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;
using Button = UnityEngine.UI.Button;
using Slider = UnityEngine.UI.Slider;

public class Quiz : MonoBehaviour
{
    [Header("Questions")]
    [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] List<QuestionSO> questions = new List<QuestionSO>();
    QuestionSO currentQuestion;


    [Header("Answers:")]
    [SerializeField] GameObject[] AnswerButton;
    int CorrectAnswerIndex;
    bool hasAnsweredEarly = true;

    [Header("buttonImage")]
    [SerializeField] Sprite DefaultAnswerSprite;
    [SerializeField] Sprite CorrectAnswerSprite;

    [Header("Timer")]
    [SerializeField] Image timerImage;
    Timer timer;

    [Header("Scoring")]
    [SerializeField] TextMeshProUGUI scoreText;
    ScoreKeeper scoreKeeper;

    [Header("ProgressBar")]
    [SerializeField] Slider progressBar;
    public bool isComplete;


    void Awake()
    {
        timer = FindObjectOfType<Timer>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        progressBar.maxValue = questions.Count;
        progressBar.value = 0;

      

    }
    void Update()
    {
        timerImage.fillAmount = timer.fillFraction;
       
            if (timer.loadNextQuestion)
            {
                if (progressBar.value == progressBar.maxValue)
                   {
                      isComplete = true;
                      return;
                   }
                hasAnsweredEarly = false;
                GetNextQuestion();
                timer.loadNextQuestion = false;

            }
          
            else if (!hasAnsweredEarly && !timer.isAnsweringQuestion)
                    {
                        DisplayAnswer(-1);
                        SetButtonState(false);
                        scoreText.text = "Score: " + scoreKeeper.CalculateScore() + "%";

        }
    }

    public void OnAnswerSelected(int index)
    {
        hasAnsweredEarly = true;
        DisplayAnswer(index);
        SetButtonState(false);
        timer.CancelTimer();
        scoreText.text = "Score: " + scoreKeeper.CalculateScore() + "%";

    }

    void DisplayAnswer(int index)
        {
        Image buttonImage;
        if (index == currentQuestion.GetCorrectAnswerIndex())
            {
                questionText.text = "Correct!";
                buttonImage = AnswerButton[index].GetComponent<Image>();
                buttonImage.sprite = CorrectAnswerSprite;
                scoreKeeper.IncrementCorrectAnswers();
            }
        else
            {
                CorrectAnswerIndex = currentQuestion.GetCorrectAnswerIndex();
                string correctAnswer = currentQuestion.GetAnswer(CorrectAnswerIndex);
                questionText.text = "Sorry, the correct answer was;\n" + correctAnswer;
                buttonImage = AnswerButton[CorrectAnswerIndex].GetComponent<Image>();
                buttonImage.sprite = CorrectAnswerSprite;
            }

         }

    void GetNextQuestion()
        {

        if (questions.Count > 0)

           {
            SetButtonState(true);
            SetDefaultButtonSprites();
            GetRandomQuestion();
            DisplayQuestion();
            progressBar.value++;
            scoreKeeper.IncrementQuestionsSeen();
           }
        }

    void GetRandomQuestion()
    {
        int index = Random.Range(0, questions.Count);
        currentQuestion = questions[index];

        if (questions.Contains(currentQuestion))
           {
            questions.Remove(currentQuestion);
           }

    }

    void DisplayQuestion()
        {
           questionText.text = currentQuestion.GetQuestion();

           for (int i = 0; i < AnswerButton.Length; i++)
             {
            TextMeshProUGUI buttonText = AnswerButton[i].GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = currentQuestion.GetAnswer(i);
          

          //  buttonText.text = currentQuestion.GetAnswer(i);
              }

        }

    void SetButtonState(bool state)
        {
            for (int i = 0; i < AnswerButton.Length; i++)
            {
                Button button = AnswerButton[i].GetComponent<Button>();
                button.interactable = state;
            }
        }

        void SetDefaultButtonSprites()
        {
            for (int i = 0; i < AnswerButton.Length; i++)
            {
                Image buttonImage = AnswerButton[i].GetComponent<Image>();
                buttonImage.sprite = DefaultAnswerSprite;
            }
        }
    }


    


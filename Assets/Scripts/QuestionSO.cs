using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Quiz Question", fileName="New Question")]
public class QuestionSO : ScriptableObject
{
    [TextArea(2,3)]
    [SerializeField]string question = "Enter a New Question here ";
    [SerializeField] string[] Answers = new string[4];
    [SerializeField] int CorrectAnswerIndex;
   // [SerializeField] string CorrectAnswer;
   
    public string GetQuestion()
    {
        return question;
    }

    public string GetAnswer(int index)
    {
        return Answers[index];
    }
    public int GetCorrectAnswerIndex()
    {
        return CorrectAnswerIndex;
    }


}


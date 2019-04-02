using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionManager : MonoBehaviour
{
    //a class that contains a question a it's answer
    [System.Serializable]
    public class QuestionAnswer
    {
        public string question;
        public string answer;
    }

    //refere in the inspector the list of questions, there is no need to fill the answer field
    public List<QuestionAnswer> QuestionList;

    //take the reference of the canvas to update the text
    public GameObject questionDisplayer;
    //a reference of the current question
    [HideInInspector]
    public QuestionAnswer currentQuestionAnswer;
    //list of the cubes used to answer the question to change their colors
    public List<GameObject> responseCube;

    //set a question for the begining of the game
    private void Start()
    {
        NextQuestion();
    }

    //pick a random question in the list
    public QuestionAnswer ChooseRandomQuestion()
    {
        int questionIndex = Random.Range(0, QuestionList.Count);
        return QuestionList[questionIndex];
    }

    //update the UI with the answer chosen randomly
    public void UpdateUI(string question)
    {
        questionDisplayer.transform.GetChild(0).GetComponent<Text>().text = "Question : " + question;
    }

    //take a question randomly and set the current question to this randomly chosen question
    public void NextQuestion()
    {
        currentQuestionAnswer  = ChooseRandomQuestion();
        SetCubeColor();
        UpdateUI(currentQuestionAnswer.question);
    }
    //set the colors of the cubes randomly
    void SetCubeColor()
    {
        for (int i = 0; i < responseCube.Count; i++)
        {
            float probability = Random.Range(0f, 1f);
            if (probability < 0.33)
            {
                responseCube[i].GetComponent<Renderer>().material.color = Color.blue;
                responseCube[i].GetComponent<ResponseCube>().currentColor = "blue";
            }
            else if (probability < 0.66)
            {
                responseCube[i].GetComponent<Renderer>().material.color = Color.red;
                responseCube[i].GetComponent<ResponseCube>().currentColor = "red";
            }
            else
            {
                responseCube[i].GetComponent<Renderer>().material.color = Color.green;
                responseCube[i].GetComponent<ResponseCube>().currentColor = "green";
            }
            
        }
    }

    //functions that returns a boolean if there is a response cube available to answer the question
    public bool CheckQuestionAvailability()
    {
        for (int i = 0; i < responseCube.Count; i++)
        {
            if (responseCube[i].GetComponent<ResponseCube>().currentColor == currentQuestionAnswer.answer)
            {
                return true;
            }
        }
        return false;
    }

}

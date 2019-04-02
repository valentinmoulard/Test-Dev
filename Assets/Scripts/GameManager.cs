using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //game manager that handles the stats of correct answer, time, etc
    //also manage the state of the game (main menu, in game, final score state)
    public static GameManager gameManager;

    //reference of the question manager so we can activate it when the game starts
    public GameObject questionManager;

    //the different canvas to activate and deactivate according to the state
    public GameObject startCanvas;
    public GameObject questionCanvas;
    public GameObject finalScoreCanvas;
    public GameObject goodAnswerCanvas;
    public GameObject wrongAnswerCanvas;
    
    //number of questions wanted to be done to finish the test
    public int questionNumber;

    //the index of the current question to keep track of the progression of the quizz
    [HideInInspector]
    public int currentQuestion;

    //count the number of correct answer
    [HideInInspector]
    public int correctAnswer;

    //time passed to finish the quizz
    [HideInInspector]
    public float time;

    //current state of the game (main menu, in game, final score state)
    [HideInInspector]
    public int state;

    private bool questionCanvasActivated;


    void Awake()
    {
        if (gameManager == null)
        {
            gameManager = this;
        }
        else if (gameManager != this)
        {
            Destroy(this.gameObject);
        }
    }
    

    void Update()
    {
        //manage the states
        if (state == 0)
        {
            if (Input.anyKey)
            {
                startCanvas.SetActive(false);
                state++;
            }
        }
        else if (state == 1)
        {
            if (!questionCanvasActivated)
            {
                questionManager.SetActive(true);
                questionCanvas.SetActive(true);
                questionCanvasActivated = true;
            }
            time += Time.deltaTime;
        }
        else if (state == 2)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                //Restart();
            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                //UnityEditor.EditorApplication.isPlaying = false;
                Application.Quit();
            }
        }
        
    }

    //function that displays the final score
    public void DisplayFinalScore()
    {
        state++;
        questionCanvas.SetActive(false);
        finalScoreCanvas.transform.GetChild(1).transform.GetChild(0).GetComponent<Text>().text = "Correct answers : " + correctAnswer.ToString();
        finalScoreCanvas.transform.GetChild(1).transform.GetChild(1).GetComponent<Text>().text = "Time : " + time.ToString() + " s";
        finalScoreCanvas.SetActive(true);
    }

    //function that restarts the game
    void Restart()
    {
        state = 0;
        time = 0;
        correctAnswer = 0;
        startCanvas.SetActive(true);
        finalScoreCanvas.SetActive(false);
    }

}

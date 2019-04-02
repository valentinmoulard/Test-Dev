using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickDestroy : MonoBehaviour
{
    Ray ray;
    RaycastHit hit;

    void Update()
    {
        //if we are in the right state, we allow the player to do a left click on the cube
        if (GameManager.gameManager.state == 1)
        {
            if (Input.GetMouseButtonDown(0))
            {
                //ray cast from the camera to check what we collide with our ray
                ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.CompareTag("Answer"))
                    {
                        //if there is an answer cube available and the player clicks on the good cube, increase score
                        if (hit.collider.gameObject.GetComponent<ResponseCube>().currentColor == GameManager.gameManager.questionManager.GetComponent<QuestionManager>().currentQuestionAnswer.answer && GameManager.gameManager.questionManager.GetComponent<QuestionManager>().CheckQuestionAvailability())
                        {
                            StartCoroutine(DisplayGoodAnswerCanvas());
                            GameManager.gameManager.correctAnswer++;
                        }
                        //if there are no answer cube available to answer the question and if we click the bootom cube, increse the score
                        else if (!GameManager.gameManager.questionManager.GetComponent<QuestionManager>().CheckQuestionAvailability() && hit.collider.name == "None")
                        {
                            StartCoroutine(DisplayGoodAnswerCanvas());
                            GameManager.gameManager.correctAnswer++;
                        }
                        //else it's a wrong answer...
                        else
                        {
                            StartCoroutine(DisplayWrongAnswerCanvas());
                        }

                        //go to the next question
                        GameManager.gameManager.currentQuestion++;

                        //if there are questions to answer, take the next question
                        if (GameManager.gameManager.currentQuestion < GameManager.gameManager.questionNumber)
                        {
                            GameManager.gameManager.questionManager.GetComponent<QuestionManager>().NextQuestion();
                        }
                        //else we display the final score
                        else
                        {
                            GameManager.gameManager.DisplayFinalScore();
                        }
                    }
                }
            }
        }
    }

    IEnumerator DisplayGoodAnswerCanvas()
    {
        GameManager.gameManager.goodAnswerCanvas.SetActive(true);
        yield return new WaitForSeconds(2f);
        GameManager.gameManager.goodAnswerCanvas.SetActive(false);
    }

    IEnumerator DisplayWrongAnswerCanvas()
    {
        GameManager.gameManager.wrongAnswerCanvas.SetActive(true);
        yield return new WaitForSeconds(2f);
        GameManager.gameManager.wrongAnswerCanvas.SetActive(false);
    }
}

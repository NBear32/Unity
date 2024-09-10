using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ObjectGenManager : MonoBehaviour
{
    public ObjectGenPoint[] objGens;
    HashSet<int> genNumbers = new HashSet<int>();
    string[][] qNumberSort;
    public float howManyEmptyPoint = 2.0f;

    GameObject[] objectGenPoints;
    public bool isEnterOn = false;
    public bool isExitOn = false;
    public bool isFinishOn = false;
    public bool isShuffled = false;
    UIController uiController;

    public string question;
    public string answer1;
    public string answer2;
    public string answer3;
    public string answer4;
    public int correctAnswer;

    public int qnum = 0;

    // Start is called before the first frame update
    void Start()
    {
        uiController = GameObject.FindGameObjectWithTag("UITextBar").GetComponent<UIController>();
        objGens = GetComponentsInChildren<ObjectGenPoint>();
        ObjectGenPoint[] objGensSort = objGens.OrderBy(objGens => objGens.name).ToArray();
        objGens = objGensSort;

        for (int i = 0; i < objGens.Length - howManyEmptyPoint;)
        {
            int index = Random.Range(0, objGens.Length);
            genNumbers.Add(index);

            if (genNumbers.Count == (i + 1))
            {
                ObjectGenPoint objgen = objGens[index];
                objgen.ObjectCreate(qnum);
                i = i + 1;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isFinishOn)
        {
            return;
        }

        if (isEnterOn)
        {
            uiController.Image.SetActive(true);

            if (!isShuffled)
            {
                qnum = qnum - 1;
                UIController.MyData[] questions = uiController.Questions;

                question = questions[qnum].question;
                answer1 = questions[qnum].answer1;
                answer2 = questions[qnum].answer2;
                answer3 = questions[qnum].answer3;
                answer4 = questions[qnum].answer4;
                correctAnswer = questions[qnum].correctAnswer;

                uiController.UIText.text = question;
                string[] q1 = { answer1, "1" };
                string[] q2 = { answer2, "2" };
                string[] q3 = { answer3, "3" };
                string[] q4 = { answer4, "4" };

                qNumberSort = new string[][] { q1, q2, q3, q4 };

                Shuffle(qNumberSort);

                for (int i = 0; i < objGens.Length; i++)
                {
                    if (int.Parse(qNumberSort[i][1]) == correctAnswer)
                    {
                        Debug.Log("qNumberSort: " + int.Parse(qNumberSort[i][1]));
                        Debug.Log("qNumberQuiz: " + qNumberSort[i][0]);
                        Debug.Log("Correct: " + correctAnswer);
                        objGens[i].isCorrect = true;
                    }
                }

                foreach (string[] number in qNumberSort)
                {
                    Debug.Log(number);
                }

                uiController.QuizItem1.text = "1. " + qNumberSort[0][0];
                uiController.QuizItem2.text = "2. " + qNumberSort[1][0];
                uiController.QuizItem3.text = "3. " + qNumberSort[2][0];
                uiController.QuizItem4.text = "4. " + qNumberSort[3][0];

                isShuffled = true;
            }

            Debug.Log("ÀÔÀå");
            isEnterOn = false;
        }
        if (isExitOn)
        {
            uiController.QuizItem1.text = "";
            uiController.QuizItem2.text = "";
            uiController.QuizItem3.text = "";
            uiController.Image.SetActive(false);
            uiController.answerIcon.SetActive(false);
            uiController.UIText.text = "";
            Debug.Log("ÅðÀå");
       /*     if (qnum <= 2)
            {
                qnum = qnum + 1;
            }*/
            isExitOn = false;
        }

    }

    void Shuffle<T>(T[] array)
    {
        for (int i = array.Length - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            T temp = array[i];
            array[i] = array[randomIndex];
            array[randomIndex] = temp;
        }
    }
}

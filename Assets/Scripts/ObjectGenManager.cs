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
    public bool isShuffled = false;
    UIController uiController;

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
                objgen.ObjectCreate();
                i = i + 1;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isEnterOn)
        {
            uiController.Image.SetActive(true);

            if (!isShuffled)
            {
                uiController.UIText.text = "웹에 공개된 정보를 탐색하기 위한 프로그램은?";
                string[] q1 = { "웹브라우저\n(Web Browser)", "1" };
                string[] q2 = { "웹에디터\n(Web Editor)", "2" };
                string[] q3 = { "데이터베이스\n(database)", "3" };
                string[] q4 = { "하이브리드앱\n(Hybrid App)", "4" };

                int qCorrect = 1;

                qNumberSort = new string[][] { q1, q2, q3, q4 };

                Shuffle(qNumberSort);

                for (int i = 0; i < objGens.Length; i++)
                {
                    if (int.Parse(qNumberSort[i][1]) == qCorrect)
                    {
                        Debug.Log("qNumberSort: " + int.Parse(qNumberSort[i][1]));
                        Debug.Log("qNumberQuiz: " + qNumberSort[i][0]);
                        Debug.Log("qCorrect: " + qCorrect);
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

            Debug.Log("입장");
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

            Debug.Log("퇴장");
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

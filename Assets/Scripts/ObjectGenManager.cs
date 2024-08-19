using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGenManager : MonoBehaviour
{
    public ObjectGenPoint[] objGens;
    HashSet<int> genNumbers = new HashSet<int>();
    public float howManyEmptyPoint = 2.0f;

    GameObject[] objectGenPoints;
    public bool isEnterOn = false;
    public bool isExitOn = false;
    UIController uiController;

    // Start is called before the first frame update
    void Start()
    {
        uiController = GameObject.FindGameObjectWithTag("UITextBar").GetComponent<UIController>();
        objGens = GetComponentsInChildren<ObjectGenPoint>();
        
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
            uiController.UIText.text = "테스트문제: 1번이 정답입니다";
            uiController.QuizItem1.text = "1. 정답";
            uiController.QuizItem2.text = "2. 오답";
            uiController.QuizItem3.text = "3. 오답";

            for (int i = 0; i < objGens.Length; i++)
            {
                if (objGens[i].GenPointNum == 1)
                {
                    objGens[i].isCorrect = true;
                    break;
                }
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
            uiController.answerIcon.SetActive(true);
            uiController.UIText.text = "";
            Debug.Log("퇴장");
            isExitOn = false;
        }

    }
}

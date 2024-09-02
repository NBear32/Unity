using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizManager : MonoBehaviour
{
    GameObject[] objectGenPoints;
    public bool isEnterOn = false;
    public bool isExitOn = false;
    UIController uiController;

    // Start is called before the first frame update
    void Start()
    {
        uiController = GameObject.FindGameObjectWithTag("UITextBar").GetComponent<UIController>();
    }

    // Update is called once per frame
    void Update()
    {
/*        if (isEnterOn)
        {
            uiController.Image.SetActive(true);
            GameObject.FindGameObjectWithTag("QuizItem").SetActive(true);
            uiController.UIText.text = "테스트문제: 1번이 정답입니다";
            uiController.QuizItem1.text = "정답";
            uiController.QuizItem2.text = "오답";
            uiController.QuizItem3.text = "오답";
            Debug.Log("입장");
            isEnterOn = false;
        }
        if (isExitOn)
        {
            GameObject.FindGameObjectWithTag("QuizItem").SetActive(false);
            uiController.QuizItem1.text = "";
            uiController.QuizItem2.text = "";
            uiController.QuizItem3.text = "";
            uiController.Image.SetActive(false);
            uiController.UIText.text = "";
            Debug.Log("퇴장");
            isExitOn = false;
        }*/
    }
}

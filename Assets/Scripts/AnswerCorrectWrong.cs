using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnswerCorrectWrong : MonoBehaviour
{

    public Material[]  mat = new Material[3];
    public Sprite[] spr = new Sprite[2];
    int switchIcon = 0;
    bool isCorrect;
    public int getNumber = 0;
    UIController uiController;
    int count = 0;

    CameraShake Camera;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<MeshRenderer>().material = mat[switchIcon];
        uiController = GameObject.FindGameObjectWithTag("UITextBar").GetComponent<UIController>();
        Camera = GameObject.FindWithTag("MainCamera").GetComponent<CameraShake>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider collider)
    {
        isCorrect = GetComponentInParent<ObjectGenPoint>().isCorrect;

        if (collider.gameObject.tag == "Player")
        {
            if (isCorrect == false)
            {
                switchIcon = switchIcon = 1;
            }
            else if (isCorrect == true)
            {
                switchIcon = 2;
            }

            gameObject.GetComponent<MeshRenderer>().material = mat[switchIcon];
            uiController.answerIcon.SetActive(true);
            uiController.answerIcon.GetComponent<Image>().sprite = spr[switchIcon - 1];
            if (switchIcon == 1)
            {
                Camera.VibrateForTime(0.2f);
                uiController.RunningResult[count] = false;
            }
            else if (switchIcon == 2)
            {
                uiController.RunningResult[count] = true;
            }
            count++;

            Debug.Log(switchIcon);
        }
    }
}

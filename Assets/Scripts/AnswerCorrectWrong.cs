using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnswerCorrectWrong : MonoBehaviour
{

    public Material[]  mat = new Material[3];
    public Sprite[] spr = new Sprite[2];
    int isAnyIcon = 0;
    bool isCorrect;
    public int getNumber = 0;
    UIController uiController;

    CameraShake Camera;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<MeshRenderer>().material = mat[isAnyIcon];
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
                isAnyIcon = isAnyIcon = 1;
            }
            else if (isCorrect == true)
            {
                isAnyIcon = isAnyIcon = 2;
            }

            gameObject.GetComponent<MeshRenderer>().material = mat[isAnyIcon];
            uiController.answerIcon.SetActive(true);
            uiController.answerIcon.GetComponent<Image>().sprite = spr[isAnyIcon - 1];
            if (isAnyIcon == 1)
            {
                Camera.VibrateForTime(0.2f);
            }

            Debug.Log(isAnyIcon);
        }
    }
}

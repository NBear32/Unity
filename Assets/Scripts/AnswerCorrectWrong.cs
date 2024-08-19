using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerCorrectWrong : MonoBehaviour
{

    public Material[]  mat = new Material[3];
    int isAnyIcon = 0;
    bool isCorrect;
    public int getNumber = 0;
    UIController uiController;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<MeshRenderer>().material = mat[isAnyIcon];
        uiController = GameObject.FindGameObjectWithTag("UITextBar").GetComponent<UIController>();
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
            /*uiController.answerIcon.GetComponent<Material>().ma = mat[isAnyIcon];*/

            Debug.Log(isAnyIcon);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizEnter : MonoBehaviour
{
    public bool isExit = false;
    public bool isFinish = false;
    public GameObject QuizGenPoint;
    ObjectGenManager objectGenManager;
    AnswerCorrectWrong answerCorrectWrong;

    // Start is called before the first frame update
    void Start()
    {
        objectGenManager = QuizGenPoint.GetComponent<ObjectGenManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (isFinish)
        {
            objectGenManager.isFinishOn = true;
            GameObject.FindGameObjectWithTag("SceneStatus").GetComponent<SceneStatusManager>().sceneState = SceneStatusManager.SceneStatus.Finish;
            Debug.Log("isFinish");
        }
        else if (!isExit)
        {
            objectGenManager.isEnterOn = true;
        }
        else if (isExit)
        {
            objectGenManager.isExitOn = true;
        }
    }
}

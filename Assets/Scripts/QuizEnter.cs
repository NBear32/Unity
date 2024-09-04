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
        if (!isExit)
        {
            objectGenManager.isEnterOn = true;
        }
        else if (isExit)
        {
            objectGenManager.isExitOn = true;
        }

        if (!isFinish)
        {
            GetComponent<SceneStatusManager>().sceneState = SceneStatusManager.SceneStatus.Finish;
        }
    }
}

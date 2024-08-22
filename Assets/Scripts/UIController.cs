using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class UIController : MonoBehaviour
{

    GameObject sceneStatusManager;
    public GameObject Image;
    public TextMeshProUGUI UIText;
    public TextMeshProUGUI QuizItem1;
    public TextMeshProUGUI QuizItem2;
    public TextMeshProUGUI QuizItem3;
    public TextMeshProUGUI QuizItem4;
    public GameObject ready;
    public GameObject three;
    public GameObject two;
    public GameObject one;
    public GameObject start;
    public GameObject answerIcon;

    int statusCount = 5;
    float timer = 0.0f;
    float waitingTime = 1.0f;


    // Start is called before the first frame update
    void Start()
    {
        Image.SetActive(false);
        UIText.text = "";
        sceneStatusManager = GameObject.FindGameObjectWithTag("SceneStatus");
        if (sceneStatusManager.GetComponent<SceneStatusManager>().sceneState == SceneStatusManager.SceneStatus.Ready)
        {
            ready.SetActive(true);
        }
        answerIcon.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (sceneStatusManager.GetComponent<SceneStatusManager>().sceneState != SceneStatusManager.SceneStatus.Start)
        {
            timer += Time.deltaTime;
            if (timer > waitingTime)
            {
                if (statusCount == 4)
                {
                    ready.SetActive(false);
                    three.SetActive(true);
                }
                else if (statusCount == 3)
                {
                    three.SetActive(false);
                    two.SetActive(true);
                }
                else if (statusCount == 2)
                {
                    two.SetActive(false);
                    one.SetActive(true);
                }
                else if (statusCount == 1)
                {
                    sceneStatusManager.GetComponent<SceneStatusManager>().sceneState = SceneStatusManager.SceneStatus.Set;
                    one.SetActive(false);
                    start.SetActive(true);
                    waitingTime = 1.2f;
                }
                else if (statusCount == 0)
                {
                    start.SetActive(false);
                    sceneStatusManager.GetComponent<SceneStatusManager>().sceneState = SceneStatusManager.SceneStatus.Start;
                }
                statusCount = statusCount - 1;
                timer = 0.0f;
            }
        }
    }
}

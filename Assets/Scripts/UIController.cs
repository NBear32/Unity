using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Runtime.InteropServices;


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
    public GameObject RunningMapQuizPanel;
    public GameObject EnterKeyInfo;

    int statusCount = 5;
    float timer = 0.0f;
    float waitingTime = 1.0f;

    [DllImport("__Internal")]
    private static extern void MyJSFunction(string message);


    ///////////////////////////////////////////////////////////////////////////////////////////////

    private bool isPaused = false;
    public void TogglePause()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0 : 1; // 게임 시간의 흐름을 멈추거나 다시 시작
    }

    public void PauseGame()
    {
        /* 다른 React Window 에서 Keyboard Input => 적용후 timeScale 0 으로 세팅 */
        WebGLInput.captureAllKeyboardInput = false;

        isPaused = true;
        Time.timeScale = 0;
    }

    public void ContinueGame()
    {
        isPaused = false;
        Time.timeScale = 1;

        /* Player 이동 등 Keyboard Input => timeScale 1로 세팅후 적용 */
        WebGLInput.captureAllKeyboardInput = true;
    }

    [System.Serializable]
    public class MyData
    {
        public string lectureId;
        public long lectureContentSeq;
        public int questionSeq;
        public string question;
        public string answer1;
        public string answer2;
        public string answer3;
        public string answer4;
        public int correctAnswer;

        //        "lectureId": "L00000000052",
        //        "lectureContentSeq": 3,
        //        "questionSeq": 1,
        //        "question": "다음 중 HTML5의 시맨틱 태크 (Semantic Tag)가 아닌 것은?",
        //        "answer1": "head",
        //        "answer2": "nav",
        //        "answer3": "aside",
        //        "answer4": "footer",
        //        "correctAnswer": 1

    }

    [System.Serializable]
    public class MyDataArrayWrapper
    {
        public MyData[] items;
    }

    public MyData[] Questions;

    public void ReceiveJsonData(string jsonData)
    {

        Debug.Log("Unity Received JSON: " + jsonData);

        // JSON 문자열을 MyDataArrayWrapper 객체로 변환
        var wrapper = JsonUtility.FromJson<MyDataArrayWrapper>("{\"items\":" + jsonData + "}");

        // 래퍼에서 MyData 배열을 추출
        MyData[] dataArray = wrapper.items;

        // 데이터 처리 로직
        if (dataArray != null && dataArray.Length > 0)
        {
            foreach (var data in dataArray)
            {
                Debug.Log($"Received data: lectureId = {data.lectureId}, lectureContentSeq = {data.lectureContentSeq}, questionSeq = {data.questionSeq}," +
                    $" Question = {data.question}, Answer1 = {data.answer1}, Answer2 = {data.answer2}, Answer3 = {data.answer3}, Answer4 = {data.answer4}, correctAnswer = {data.correctAnswer}");
            }

            Questions = dataArray;
            Debug.Log("문제 2: " + dataArray[1].question);
        }
        else
        {
            Debug.Log("No data received or data is null.");
        }
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////


    private void Awake()
    {
        sceneStatusManager = GameObject.FindGameObjectWithTag("SceneStatus");
    }
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);

        Debug.Log(SceneManager.GetActiveScene().name);
        Debug.Log(SceneManager.GetActiveScene().name == "RunningMap");

        if (SceneManager.GetActiveScene().name == "RunningMap")
        {
            RunningMapQuizPanel.SetActive(true);
            EnterKeyInfo.SetActive(false);
        }
        else if (SceneManager.GetActiveScene().name != "RunningMap")
        {
            RunningMapQuizPanel.SetActive(false);
            EnterKeyInfo.SetActive(true);
        }

        Image.SetActive(false);
        UIText.text = "";
        if (sceneStatusManager.GetComponent<SceneStatusManager>().sceneState == SceneStatusManager.SceneStatus.Ready)
        {
            ready.SetActive(true);
        }
        answerIcon.SetActive(false);

        SceneManager.sceneLoaded += UIChangeStatus;
    }   

    private void UIChangeStatus(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("UIChangeStatus");

        if (SceneManager.GetActiveScene().name == "RunningMap")
        {
            RunningMapQuizPanel.SetActive(true);
            EnterKeyInfo.SetActive(false);
        }
        else if (SceneManager.GetActiveScene().name != "RunningMap")
        {
            RunningMapQuizPanel.SetActive(false);
            EnterKeyInfo.SetActive(true);
        }
        answerIcon.SetActive(false);

        Image.SetActive(false);
        UIText.text = "";
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

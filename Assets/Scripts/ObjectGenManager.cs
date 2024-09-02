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

    public string question;
    public string answer1;
    public string answer2;
    public string answer3;
    public string answer4;
    public int correctAnswer;

    public int qnum = 0;







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

    MyData[] Questions;

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
                question = Questions[qnum].question;
                answer1 = Questions[qnum].answer1;
                answer2 = Questions[qnum].answer2;
                answer3 = Questions[qnum].answer3;
                answer4 = Questions[qnum].answer4;
                correctAnswer = Questions[qnum].correctAnswer;

                uiController.UIText.text = question;
                string[] q1 = { answer1, "1" };
                string[] q2 = { answer2, "2" };
                string[] q3 = { answer3, "3" };
                string[] q4 = { answer4, "4" };

                qNumberSort = new string[][] { q1, q2, q3, q4 };

                Shuffle(qNumberSort);

                for (int i = 0; i < objGens.Length; i++)
                {
                    if (int.Parse(qNumberSort[i][1]) == correctAnswer)
                    {
                        Debug.Log("qNumberSort: " + int.Parse(qNumberSort[i][1]));
                        Debug.Log("qNumberQuiz: " + qNumberSort[i][0]);
                        Debug.Log("Correct: " + correctAnswer);
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
            if (qnum <= 2)
            {
                qnum = qnum + 1;
            }
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

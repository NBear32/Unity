using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class QuizDataBase : MonoBehaviour
{
    [System.Serializable]
    public class QuizArray
    {
        public string quiz;
        public string[] right, wrong1, wrong2;
        public QuizArray(string quiz_, string right_, string wrong1_, string wrong2_)
        {
            right = new string[] { right_, "true" };
            wrong1 = new string[] { wrong1_, "false" };
            wrong2 = new string[] { wrong2_, "false" };
            quiz = quiz_;
        }

        QuizArray quiz1 = new QuizArray("문제",
                                        "정답",
                                        "오답",
                                        "오답");

        QuizArray quiz2 = new QuizArray("가장 무거운 동물을 고르시오",
                                        "코끼리",
                                        "카피바라",
                                        "햄스터");

        QuizArray quiz3 = new QuizArray("다음 중 가장 큰 행성은?",
                                        "태양",
                                        "수성",
                                        "목성");
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

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

        QuizArray quiz1 = new QuizArray("����",
                                        "����",
                                        "����",
                                        "����");

        QuizArray quiz2 = new QuizArray("���� ���ſ� ������ ���ÿ�",
                                        "�ڳ���",
                                        "ī�ǹٶ�",
                                        "�ܽ���");

        QuizArray quiz3 = new QuizArray("���� �� ���� ū �༺��?",
                                        "�¾�",
                                        "����",
                                        "��");
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RunningResultUIController : MonoBehaviour
{
    public TextMeshProUGUI ResultScore;
    public GameObject[] Result = new GameObject[3];
    public Sprite[] spr = new Sprite[2];
    int score = 0;

    GameObject gameUI;

    private void Awake()
    {
        //this.gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        GameObject gameUI = GameObject.FindGameObjectWithTag("UITextBar");
        gameUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResultScreenOpen()
    {
        UIController uiController = GameObject.FindGameObjectWithTag("UITextBar").GetComponent<UIController>();
        for (int i = 0; i < Result.Length; i++)
        {
            if (uiController.RunningResult[i + 1] == true)
            {
                Result[i].GetComponent<Image>().sprite = spr[0];
                score = score + 1;
                Debug.Log("Result" + i + ": true");
            }
            else if (uiController.RunningResult[i + 1] == false)
            {
                Result[i].GetComponent<Image>().sprite = spr[1];
                Debug.Log("Result" + i + ": false");
            }
        }

        ResultScore.text = "SCORE: " + score;
    }
}

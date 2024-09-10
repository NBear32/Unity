using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RunningResultUIController : MonoBehaviour
{
    public TextMeshProUGUI ResultScore;
    UIController uiController;
    public GameObject[] Result;
    public Sprite[] spr = new Sprite[2];
    int score = 0;

    GameObject gameUI;

    private void Awake()
    {
        this.gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        GameObject gameUI = GameObject.FindGameObjectWithTag("UITextBar");
        uiController = GameObject.FindGameObjectWithTag("UITextBar").GetComponent<UIController>();
        gameUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResultScreenOpen()
    {
        for (int i = 0; i < Result.Length; i++)
        {
            if (uiController.RunningResult[i] == true)
            {
                Result[i].GetComponent<Image>().sprite = spr[0];
                score = score + 1;
            }
            else if (uiController.RunningResult[i] == false)
            {
                Result[i].GetComponent<Image>().sprite = spr[1];
            }
        }

        ResultScore.text = "SCORE: " + score;
    }
}

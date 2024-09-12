using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public bool isExitDisplay = false;
    public string displayTag;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void GoToLobby()
    {
        Debug.Log("Button");
        if (isExitDisplay)
        {
            GameObject.FindGameObjectWithTag(displayTag).SetActive(false);
        }
        else if (!isExitDisplay)
        {
            SceneManager.LoadScene("Lobby");
        }
    }
}

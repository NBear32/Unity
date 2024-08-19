using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalManager : MonoBehaviour
{
    public int portalNum = 0;
    public int movePortalNum = 0;
    public string sceneName = "Lobby";
    public bool isPortal = false;
    public bool isPortalLock = false;
    public bool isPortalEnable = true;
    GameObject[] portals;
    float timer = 0.0f;
    float waitingTime = 1.0f;
    UIController uiController;
    public string Roomname;


    // Start is called before the first frame update
    void Start()
    {
        portals = GameObject.FindGameObjectsWithTag("Exit");
        uiController = GameObject.FindGameObjectWithTag("UITextBar").GetComponent<UIController>();
        uiController.Image.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKey(KeyCode.F) || Input.GetKey(KeyCode.Space)) && isPortal == true && isPortalEnable)
        {
            for (int i = 0; i < portals.Length; i++)
            {
                GameObject portalObj = portals[i];
                PortalManager move = portalObj.GetComponent<PortalManager>();

                if (movePortalNum == move.portalNum)
                {
                    string scene = SceneManager.GetActiveScene().name;

                    if (scene != sceneName)
                    {
                        SceneManager.LoadScene(sceneName);
                    }

                    float x = portalObj.transform.position.x;
                    float y = portalObj.transform.position.y;
                    float z = portalObj.transform.position.z;

                    GameObject player = GameObject.FindGameObjectWithTag("Player");
                    player.transform.position = new Vector3(x, y, z);
                    move.isPortal = false;
                    move.isPortalLock = true;
                    break;
                }
            }
        }
    }

    private void FixedUpdate()
    {
        if (isPortalLock)
        {
            timer += Time.deltaTime;
            if (timer > waitingTime)
            {
                isPortalLock = !isPortalLock;
                timer = 0.0f;
            }
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (90 < portalNum) return;

        if (col.gameObject.tag == "Player" && !isPortalLock)
        {
            isPortal = true;
            uiController.Image.SetActive(true);
            if (Roomname != null) uiController.UIText.text = Roomname;
        }
    }

    private void OnTriggerStay(Collider col)
    {
        if (90 < portalNum) return;

        if (col.gameObject.tag == "Player" && !isPortalLock)
        {
            isPortal = true;
            uiController.Image.SetActive(true);
            if (Roomname != null) uiController.UIText.text = Roomname;

        }
    }

    public void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            isPortal = false;
            uiController.Image.SetActive(false);
        }
    }
}

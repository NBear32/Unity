using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Runtime.InteropServices;

public class PortalManager : MonoBehaviour
{
    // GameOver�ÿ� �̸��� ���� ����
    // [DllImport("__Internal")]
    // private static extern void GameOverExtern(string userName, int score);

    // JavaScript���� ����� ���� P/Invoke ����
    [DllImport("__Internal")]
    private static extern void OpenReactWindow(string Roomname);

    [DllImport("__Internal")]
    private static extern void NoticesStart();

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

    private bool isPaused = false;
    public void TogglePause()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0 : 1; // ���� �ð��� �帧�� ���߰ų� �ٽ� ����
    }

    public void PauseGame()
    {
        /* �ٸ� React Window ���� Keyboard Input => ������ timeScale 0 ���� ���� */
        WebGLInput.captureAllKeyboardInput = false;

        isPaused = true;
        Time.timeScale = 0;
    }

    public void ContinueGame()
    {
        isPaused = false;
        Time.timeScale = 1;

        /* Player �̵� �� Keyboard Input => timeScale 1�� ������ ���� */
        WebGLInput.captureAllKeyboardInput = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        portals = GameObject.FindGameObjectsWithTag("Exit");
        uiController = GameObject.FindGameObjectWithTag("UITextBar").GetComponent<UIController>();
        //uiController.Image.SetActive(false);

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
        if (Roomname == "��������" ||
            Roomname == "ȸ������" ||
            Roomname == "ȸ��Ż��" ||
            Roomname == "�����н�" ||
            Roomname == "��ٱ���" ||
            Roomname == "�̺�Ʈ" ||
            Roomname == "��������")
        {

            Debug.Log("OnTriggerEnter PortalManager!!! => React Load!!!: " + Roomname);

            //�ٸ� React Window ���� Keyboard Input �� PauseGame
                PauseGame();
            // extern �Լ� ȣ��, WEBGL �� Message ����
#if UNITY_WEBGL == true && UNITY_EDITOR == false
                    OpenReactWindow(Roomname); // ������ ����(��������)
                    // NoticesStart(); // �������� ������ ���� alert
#endif
        }
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
            // Debug.Log("OnTriggerStay PortalManager!!!");
        }
    }

    public void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            isPortal = false;
            uiController.Image.SetActive(false);
            // Debug.Log("OnTriggerExit PortalManager!!!");
        }
    }
}

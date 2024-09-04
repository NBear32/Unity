using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Runtime.InteropServices;

public class PortalManager : MonoBehaviour
{
    // GameOver시에 이름과 점수 전달
    // [DllImport("__Internal")]
    // private static extern void GameOverExtern(string userName, int score);

    // JavaScript와의 통신을 위한 P/Invoke 선언
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
        if (Roomname == "공지사항" ||
            Roomname == "회원정보" ||
            Roomname == "회원탈퇴" ||
            Roomname == "나의학습" ||
            Roomname == "장바구니" ||
            Roomname == "이벤트" ||
            Roomname == "질의응답")
        {

            Debug.Log("OnTriggerEnter PortalManager!!! => React Load!!!: " + Roomname);

            //다른 React Window 에서 Keyboard Input 및 PauseGame
                PauseGame();
            // extern 함수 호출, WEBGL 로 Message 전달
#if UNITY_WEBGL == true && UNITY_EDITOR == false
                    OpenReactWindow(Roomname); // 윈도우 오픈(공지사항)
                    // NoticesStart(); // 공지사항 위도우 오픈 alert
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Component;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

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

    public float moveSpeedSet = 4f;
    float moveSpeed;
    float SpeedKeep;
    public bool LockRotation = false;
    float timer = 0.0f;
    float Statetimer = 0.0f;
    float waitingTime = 0.1f;
    private Vector3 Move;
    private Rigidbody Rigid;

    string sceneName;

    Animator animator;
    GameObject sceneStatusManager;
    public string stopAnime = "Idle";
    public string moveAnime = "Walk";
    public string runAnime = "Run";
    string nowAnime = "";
    string oldAnime = "";

    bool isObstacle = false;
    bool isBoost = false;

    public GameObject runningEffect;
    public GameObject boostEffect;
    GameObject boostEft;
    public Material playerMaterial;
    CameraShake Camera;

    // Start is called before the first frame update
    void Start()
    {
        Rigid = this.GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        sceneStatusManager = GameObject.FindGameObjectWithTag("SceneStatus");
        nowAnime = stopAnime;
        oldAnime = stopAnime;
        moveSpeed = moveSpeedSet;
        SpeedKeep = moveSpeedSet;
        sceneName = SceneManager.GetActiveScene().name;
        Camera = GameObject.FindWithTag("MainCamera").GetComponent<CameraShake>();
    }

    // Update is called once per frame
    void Update()
    {
        if (sceneStatusManager.GetComponent<SceneStatusManager>().sceneState == SceneStatusManager.SceneStatus.Ready ||
            sceneStatusManager.GetComponent<SceneStatusManager>().sceneState == SceneStatusManager.SceneStatus.Finish)
        {
            return;
        }

        if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) || sceneName == "RunningMap")
        {
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                transform.rotation = Quaternion.Euler(0, 315, 0);
                LockRotation = true;
            }
            else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                transform.rotation = Quaternion.Euler(0, 45, 0);
                LockRotation = true;
            }
            else if (!LockRotation)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }
        else if ((Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) && sceneName != "RunningMap")
        {
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                transform.rotation = Quaternion.Euler(0, 225, 0);
                LockRotation = true;
            }
            else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                transform.rotation = Quaternion.Euler(0, 135, 0);
                LockRotation = true;
            }
            else if (!LockRotation)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
        }
        else if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) && !LockRotation)
        {
            transform.rotation = Quaternion.Euler(0, 270, 0);
        }
        else if ((Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) && !LockRotation)
        {
            transform.rotation = Quaternion.Euler(0, 90, 0);
        }
    }

    private void FixedUpdate()
    {
        if (nowAnime == runAnime && !isBoost)
        {
            Vector3 pos = new Vector3(transform.position.x, transform.position.y + 0.4f, transform.position.z);
            Instantiate(runningEffect, pos, Quaternion.Euler(0, transform.position.y, 0));
        } 

        if (isObstacle)
        {
            moveSpeedSet = 2;
            Color color;
            ColorUtility.TryParseHtmlString("#FF7272", out color);
            playerMaterial.color = color;
            Statetimer += Time.deltaTime;
            if (Statetimer > 0.5f)
            {

                playerMaterial.color = Color.white;
                moveSpeedSet = SpeedKeep;
                isObstacle = false;
                Statetimer = 0.0f;
            }
        }
        if (isBoost)
        {
            moveSpeedSet = 8;
           /* Color color;
            ColorUtility.TryParseHtmlString("#48D3FF", out color);
            playerMaterial.color = color;*/
            Statetimer += Time.deltaTime;
            if (Statetimer > 3f)
            {

               /* playerMaterial.color = Color.white;*/
                moveSpeedSet = SpeedKeep;
                animator.SetFloat("RunningSpeed", 1);
                isBoost = false;
                Statetimer = 0.0f;
            }
        }

        float moveDistanceZ = Input.GetAxisRaw("Vertical");
        float moveDistanceX = Input.GetAxisRaw("Horizontal");

        if (sceneStatusManager.GetComponent<SceneStatusManager>().sceneState == SceneStatusManager.SceneStatus.Ready ||
            sceneStatusManager.GetComponent<SceneStatusManager>().sceneState == SceneStatusManager.SceneStatus.Finish)
        {
            moveDistanceX = 0.0f;
            moveDistanceZ = 0.0f;
            nowAnime = stopAnime;
            animator.Play(nowAnime);
            return;
        }

        if (Input.GetKey(KeyCode.LeftShift) || sceneName == "RunningMap")
        {
            moveSpeed = (moveSpeedSet * 2);
        }
        else if (!Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed = moveSpeedSet;
        }

       
        if (sceneName == "RunningMap")
        {
            moveDistanceZ = 1.0f;
        }

        Move = new Vector3(moveDistanceX, 0, moveDistanceZ);

        Rigid.velocity = Move.normalized * moveSpeed;

        if (LockRotation)
        {
            timer += Time.deltaTime;
            if (timer > waitingTime)
            {
                LockRotation = !LockRotation;
                timer = 0.0f;
            }
        }

        if (moveDistanceX == 0.0f && moveDistanceZ == 0.0f)
        {
            nowAnime = stopAnime;
        }
        else if (moveSpeed == (moveSpeedSet * 2))
        {
            nowAnime = runAnime;
        }
        else if (moveSpeed == moveSpeedSet)
        {
            nowAnime = moveAnime;
        }
        if (nowAnime != oldAnime)
        {
            oldAnime = nowAnime;
            animator.Play(nowAnime);
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Obstacle")
        {
            animator.SetFloat("RunningSpeed", 1);
            Camera.VibrateForTime(0.2f);
            Statetimer = 0.0f;
            isBoost = false;
            if (boostEft) Destroy(boostEft);
            isObstacle = true;
        }
        if (collision.gameObject.tag == "Wall")
        {
            isObstacle = true;
            Rigid.AddForce(new Vector3(transform.position.x * 3, 0, transform.position.z * -3), ForceMode.VelocityChange);
        }
        if (collision.gameObject.tag == "Boost")
        {
            if (boostEft) Destroy(boostEft);
            Statetimer = 0.0f;
            isBoost = true;
            animator.SetFloat("RunningSpeed", 2);

            Vector3 pos = new Vector3(transform.position.x, transform.position.y + 0.1f, transform.position.z);
            boostEft = Instantiate(boostEffect, pos, Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.rotation.z));
            boostEft.transform.parent = this.transform;
            Destroy(boostEft, 3.0f);
        }
    }
}

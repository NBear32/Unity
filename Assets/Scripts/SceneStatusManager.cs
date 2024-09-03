using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneStatusManager : MonoBehaviour
{
    public enum SceneStatus
    {
        Ready,
        Set,
        Start
    }

    public SceneStatus sceneState;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);

        if (SceneManager.GetActiveScene().name == "RunningMap")
        {
            sceneState = SceneStatus.Ready;
        }
        else
        {
            sceneState = SceneStatus.Start;
        }

        SceneManager.sceneLoaded += SceneChangeStatus;
    }

    private void SceneChangeStatus(Scene scene, LoadSceneMode mode)
    {
        if (SceneManager.GetActiveScene().name == "RunningMap")
        {
            sceneState = SceneStatus.Ready;
        }
        else
        {
            sceneState = SceneStatus.Start;
        }
    }

   // Update is called once per frame
   void Update()
    {
        
    }
}

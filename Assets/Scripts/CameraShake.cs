using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public float ShakeAmount;
    //public Canvas canvas;
    public float ShakeTime;
    Vector3 initialPosition;

    GameObject player;

    public void VibrateForTime(float time)
    {
        ShakeTime = time;
        //canvas.renderMode = RenderMode.ScreenSpaceCamera;
        //canvas.renderMode = RenderMode.WorldSpace;
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        initialPosition = new Vector3(player.transform.position.x, player.transform.position.y + GetComponent<CameraManager>().cameraY, player.transform.position.z - GetComponent<CameraManager>().cameraZ);

        if (ShakeTime > 0)
        {
            transform.position = Random.insideUnitSphere * ShakeAmount + initialPosition;
            ShakeTime -= Time.deltaTime;
        }
        else
        {
            ShakeTime = 0.0f;
            transform.position = initialPosition;
            //canvas.renderMode = RenderMode.ScreenSpaceCamera;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public int cameraY = 3;
    public int cameraZ = 3;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
        if (player != null && camera.GetComponent<CameraShake>().ShakeTime <= 0)
        {
            // 플레이어의 위치와 연동
            transform.position = new Vector3(player.transform.position.x, player.transform.position.y + cameraY, player.transform.position.z - cameraZ);
        }
    }
}

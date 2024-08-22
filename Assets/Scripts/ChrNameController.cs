using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChrNameController : MonoBehaviour
{
    Transform lookAtCamera;

    // Start is called before the first frame update
    void Start()
    {
        lookAtCamera = GameObject.FindWithTag("MainCamera").transform;
    }

    // Update is called once per frame
    void Update()
    {
 
    }

    void LateUpdate()
    {
        transform.rotation = Quaternion.Euler(lookAtCamera.eulerAngles.x, 0, 0);
    }
}

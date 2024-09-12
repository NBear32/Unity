using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChrNameController : MonoBehaviour
{
    Transform lookAtCamera;
    public bool isIcon = false;

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
        if (isIcon)
        {
            transform.rotation = Quaternion.Euler(lookAtCamera.eulerAngles.x, 180, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(lookAtCamera.eulerAngles.x, 0, 0);
        }
    }
}

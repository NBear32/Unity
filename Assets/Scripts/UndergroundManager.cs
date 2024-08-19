using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UndergroundManager : MonoBehaviour
{
    public int movePortalNum = 99;
    GameObject[] portals;

    // Start is called before the first frame update
    void Start()
    {
        portals = GameObject.FindGameObjectsWithTag("Exit");
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void FixedUpdate()
    {
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
            {
                for (int i = 0; i < portals.Length; i++)
                {
                    GameObject portalObj = portals[i];
                    PortalManager move = portalObj.GetComponent<PortalManager>();

                    if (movePortalNum == move.portalNum)
                    {
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
}

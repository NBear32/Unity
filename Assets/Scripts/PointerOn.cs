using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerOn : MonoBehaviour
{
    public GameObject Portal;
    public GameObject Pointer;
    public Material down;
    public GameObject PortalCircle;
    public Material PortalCircleMaterial;
    public GameObject portalManager;

    // Start is called before the first frame update
    void Start()
    {
        Pointer.SetActive(false);
        PortalCircle.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player" && portalManager.GetComponent<PortalManager>().isPortalLock == false)
        {
            Pointer.SetActive(true);
            PortalCircle.SetActive(true);
        }
    }

    private void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "Player" && portalManager.GetComponent<PortalManager>().isPortalLock == false)
        {
            Pointer.SetActive(true);
            PortalCircle.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            Pointer.SetActive(false);
            PortalCircle.SetActive(false);
        }
    }
}

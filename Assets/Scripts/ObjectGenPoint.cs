using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGenPoint : MonoBehaviour
{
    public GameObject genObject;
    public float getY = 0f;
    public float RotationX = 0.0f;
    public float RotationY = 0.0f;
    public float RotationZ = 0.0f;
    public int GenPointNum = 0;
    public bool isCorrect = false;
    GameObject obj;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ObjectCreate(int qnum)
    {
        Vector3 pos = new Vector3(transform.position.x, transform.position.y + getY, transform.position.z);
        obj = Instantiate(genObject, pos, Quaternion.Euler(RotationX, RotationY, RotationZ));
        obj.transform.parent = this.transform;

        if (qnum != 0)
        {
            obj.GetComponent<AnswerCorrectWrong>().count = qnum;
        }
    }
}

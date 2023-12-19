using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateDice : MonoBehaviour
{
    public float rotationSpeed = 10000f;
    public bool checkRoll = true;
    public bool checkRollCurrent = false;
    public int index;
    private Rigidbody rigid;

    private void Awake()
    {
        
        rigid = gameObject.GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (checkRoll){ RollRotation(); }
    }

    public void ClickRoll()
    {
        rigid.useGravity = true;
        StartCoroutine(ShowIndex());
    }

    public void RollRotation()
    {
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        transform.Rotate(Vector3.right, rotationSpeed * Time.deltaTime);
    }

    IEnumerator ShowIndex()
    {
        yield return new WaitForSeconds(1f);
        checkRoll = false;
        yield return new WaitForSeconds(5f);
        DetermineDiceFace();
    }

    void DetermineDiceFace()
    {
        Vector3 currentVT = Vector3.zero;
        currentVT.x = (int)transform.localEulerAngles.x;
        currentVT.y = (int)transform.localEulerAngles.y;
        currentVT.z = (int)transform.localEulerAngles.z;
        index = indexRoll(currentVT);
        checkRollCurrent = true;
    }


    private int indexRoll(Vector3 vector)
    {
        if(((vector.x >= 350 && vector.x <= 360) || (vector.x >= 0 && vector.x <= 5)) && (vector.z >= 265 && vector.z <= 280))
        {
            return 1;
        }else if((vector.x >= 260 && vector.x <= 280) && vector.z == 0)
        {
            return 2;
        }else if(vector.x >= 0 && vector.x <= 10 && vector.z >= 0 && vector.z <= 20)
        {
            return 3;
        }else if(((vector.x >= 350 && vector.x <= 360) || (vector.x >= 0 && vector.x <= 5)) && (vector.z >= 170 && vector.z <= 190))
        {
            return 4;
        }else if(vector.x >= 80 && vector.x <= 100 && vector.z >= 0 && vector.z <= 10)
        {
            return 5;
        }
        else
        {
            return 6;
        }
    }

    public void ResetDice()
    {
        rigid.useGravity = false;
        Vector3 vec = Vector3.zero;
        vec.y = 20f;
        transform.position = vec;
        checkRollCurrent = false;
        checkRoll = true;
        index = 0;
    }
}

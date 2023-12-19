using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class test : MonoBehaviour
{
    private GameObject cubeB;
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray raycast = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(raycast, out hit) && hit.collider.CompareTag("CubeB"))
            {
                cubeB = hit.collider.gameObject;
            }
            //if (cubeB != null){
            //    transform.DOMove(cubeB.transform.position, 5).SetEase(Ease.Linear).OnKill(() =>{
            //        Debug.Log("Move In");
            //    });
            //}
            StartCoroutine(move(cubeB.transform));
        }
    }

    IEnumerator move(Transform cubeB)
    {
        float distance = Vector3.Distance(transform.position, cubeB.position);

        while (distance > 0.1f)
        {
            Vector3 target = cubeB.position - transform.position;
            transform.rotation = Quaternion.LookRotation(target);
            transform.Translate(Vector3.forward * Time.deltaTime * 5f);
            yield return null;
            distance = Vector3.Distance(transform.position, cubeB.position);
        }
    }
}

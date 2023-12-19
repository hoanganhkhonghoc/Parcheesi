using UnityEngine;
using System.Collections;

public class NewMonoBehaviour : MonoBehaviour
{
    [SerializeField] private Transform positon;
    private void Start()
    {
        StartCoroutine(move());
    }

    IEnumerator move()
    {
        yield return new WaitForSeconds(5f);
        Vector3 vec = positon.position - transform.position;
        transform.rotation = Quaternion.LookRotation(vec);
        transform.Translate(Vector3.forward * Time.deltaTime * 5f);
        
    }
}


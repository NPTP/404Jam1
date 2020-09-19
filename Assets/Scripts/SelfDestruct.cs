using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DelayedSD());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator DelayedSD()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }
}

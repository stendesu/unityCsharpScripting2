using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPartical : MonoBehaviour
{

    private IEnumerator destroyParticle()
    {
        yield return new WaitForSeconds(0.5f);

        Destroy(gameObject);
    }


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(destroyParticle());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

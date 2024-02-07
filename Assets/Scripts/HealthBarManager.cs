using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarManager : MonoBehaviour
{
    private SimpleNavMeshFollow enemyScript;
    Vector3 localScale;
    public GameObject enemySelf;

    // Start is called before the first frame update
    void Start()
    {
        enemySelf = transform.parent.gameObject;
        enemyScript = enemySelf.GetComponent<SimpleNavMeshFollow>();
        localScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        float hpPercent = (enemyScript.currentHp / enemyScript.maxHp);
        localScale.x = hpPercent;
        transform.localScale = localScale;


    }
}

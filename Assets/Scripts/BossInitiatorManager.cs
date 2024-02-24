using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllowSpawner : MonoBehaviour
{
    public GameObject Spawner;
    public BoxCollider2D boxCollider;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.TryGetComponent<BoxCollider2D>(out BoxCollider2D playerHitBox))
        {
            if (collider == playerHitBox)
            {
                if (collider.gameObject.TryGetComponent<TopDownCharacterController>(out TopDownCharacterController playerScript))
                {
                    if (collider.gameObject.tag == "Player")
                    {
                        initiateSpawner();
                    }
                }
            }
        }
    }
    
    public void initiateSpawner()
    {
        EnemySpawnerManager spawnerScript = Spawner.GetComponent<EnemySpawnerManager>();
        spawnerScript.allow = true;
        GameObject bossWall = GameObject.Find("tempWall");
        bossWall.transform.position = new Vector3(5, 200, 0);
    }
    
    // Start is called before the first frame update
    void Start()
    {
        Spawner = GameObject.Find("EnemySpawner");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public bool allow = false;
    bool canSpawn = true;
    bool calledCooldown = false;
    public Sprite[] enemySprites;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void spawnEnemy()
    {
        Debug.Log(canSpawn);
        if (canSpawn)
        {
            
            Vector3 randomPos = transform.position + new Vector3(Random.Range(20, -20), Random.Range(20, -20), 0);
            float chance = Random.Range(0, 1);
            Debug.Log(chance);
            GameObject player = GameObject.Find("character");

            if (chance <= 0.5)  //  spawn ranged enemies
            {
                GameObject enemy = Instantiate(enemyPrefab, randomPos, Quaternion.identity);
                SimpleNavMeshFollow enemyScript = enemy.GetComponent<SimpleNavMeshFollow>();

                enemyScript.isMelee = false;
                enemyScript.maxHp = Random.Range(300, 500);
                enemyScript.targetSprite = enemySprites[Random.Range(0, 6)];
                enemyScript.m_target = player.transform;
                canSpawn = false;
                calledCooldown = true;
                StartCoroutine(spawnCooldown(Random.Range(3, 9)));
            }
            else   //   spawn close ranged enemies
            {
                GameObject enemy = Instantiate(enemyPrefab, randomPos, Quaternion.identity);
                SimpleNavMeshFollow enemyScript = enemy.GetComponent<SimpleNavMeshFollow>();

                enemyScript.isMelee = true;
                enemyScript.maxHp = Random.Range(300, 500);
                enemyScript.targetSprite = enemySprites[Random.Range(0, 6)];
                enemyScript.m_target = player.transform;
                canSpawn = false;
                calledCooldown = true;
                StartCoroutine(spawnCooldown(Random.Range(3 , 9)));
            }

        }
    }

    IEnumerator spawnCooldown(float duration)
    {
        if (calledCooldown)
        {
            yield return new WaitForSeconds(duration);

            canSpawn = true;
            calledCooldown = false;
        }

    }



    // Update is called once per frame
    void Update()
    {
        if (allow)
        {
            spawnEnemy();
        }
    }
}

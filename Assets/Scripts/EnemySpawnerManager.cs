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
        if (canSpawn)
        {
            Vector3 randomPos = transform.position + new Vector3(Random.Range(30, -30), Random.Range(30, -30), 0);
            float chance = Random.Range(0, 1);
            GameObject player = GameObject.Find("character");

            if (chance <= 0.5)  //  spawn ranged enemies
            {
                GameObject enemy = Instantiate(enemyPrefab, randomPos, Quaternion.identity);
                SimpleNavMeshFollow enemyScript = enemy.GetComponent<SimpleNavMeshFollow>();

                enemyScript.isMelee = false;
                enemyScript.maxHp = Random.Range(300, 500);
                enemyScript.targetSprite = enemySprites[Random.Range(0, 6)];
                enemyScript.m_target = player.transform;
                StartCoroutine(spawnCooldown(Random.Range(5, 9)));
            }
            else   //   spawn close ranged enemies
            {
                GameObject enemy = Instantiate(enemyPrefab, randomPos, Quaternion.identity);
                SimpleNavMeshFollow enemyScript = enemy.GetComponent<SimpleNavMeshFollow>();

                enemyScript.isMelee = true;
                enemyScript.maxHp = Random.Range(300, 500);
                enemyScript.targetSprite = enemySprites[Random.Range(0, 6)];
                enemyScript.m_target = player.transform;
                StartCoroutine(spawnCooldown(Random.Range(5, 9)));
            }

            canSpawn = false;
            calledCooldown = true;
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

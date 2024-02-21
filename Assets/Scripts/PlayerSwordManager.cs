using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SocialPlatforms;

public class PlayerSwordManager : MonoBehaviour
{
    int swordDamage = 175;
    public BoxCollider2D boxCollider;
    public GameObject player;


    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.TryGetComponent<BoxCollider2D>(out BoxCollider2D enemyHitBox)) // get enemy hit box (box collider)
        {
            if (collider == enemyHitBox)    // check for enemy hit box
            {
                if (collider.gameObject.TryGetComponent<SimpleNavMeshFollow>(out SimpleNavMeshFollow enemyComponent))   // get enemy script to deal damage
                {
                    if (collider.gameObject.tag == "Enemy") //  ensures that it is an enemy
                    {
                        enemyComponent.takeDamage(swordDamage);
                    }
                }
            }
        }
    }

    void Start()
    {
        StartCoroutine(destroySword(0.8f));
    }

    private IEnumerator destroySword(float lifeSpan)
    {
        yield return new WaitForSeconds(lifeSpan);
        Destroy(gameObject);
    }

    void Update()
    {
        //transform.position = player.transform.position;
        //Debug.Log(player.transform.position);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SocialPlatforms;

public class SwordManager : MonoBehaviour
{
    int swordDamage = 35;
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
                        playerScript.takeDamage(swordDamage);
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

    }
}

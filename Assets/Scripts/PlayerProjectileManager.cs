using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectileManager : MonoBehaviour
{
    private float projectileDmg = 50.0f;
    public CircleCollider2D Collider2D;
    public GameObject damagedEnemy;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
        }
        else if (collider.gameObject.TryGetComponent<BoxCollider2D>(out BoxCollider2D enemyHitBox))
        {
            if (collider == enemyHitBox)
            {
                if (collider.gameObject.TryGetComponent<SimpleNavMeshFollow>(out SimpleNavMeshFollow enemyComponent))
                {
                    enemyComponent.takeDamage(projectileDmg);

                    Destroy(gameObject);
                }
            }


        }
        else
        {
            Debug.Log("not wall, not enemy");
        }

        
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

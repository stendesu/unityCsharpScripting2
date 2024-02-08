using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SimpleNavMeshFollow : MonoBehaviour
{
    public Transform m_target;
    NavMeshAgent m_agent;
    bool calledDelay = false;
    public float currentHp, maxHp = 300.0f;
    [SerializeField] float stopDistance;
    public Transform sprite;
    public float projectileSpeed = 20.0f;
    public GameObject bulletPrefab;

    public enum EnemyState
    {
        Idle,
        MoveToPlayer,
        Attack,
        AtkOnCooldown,
        Death
    };

    public EnemyState m_enemyStates;

    private void facePlayer()
    {
        //  if enemy is facing left by default
        if (m_target != null)
        {
            if (m_target.transform.position.x > transform.position.x)
            {
                sprite.transform.localEulerAngles = new Vector3(0, 180, 0);
            }
            else
            {
                sprite.transform.localEulerAngles = new Vector3(0, 0, 0);
            }
        }

    }

    public void takeDamage(float damage)
    {
        currentHp -= damage;
        if (currentHp <= 0)
        {
            m_enemyStates = EnemyState.Death;
        }
    }

    #region enemy states
    private void death()
    {
        Destroy(gameObject);
    }

    private void idle()
    {
        gameObject.GetComponent<NavMeshAgent>().isStopped = true;
        //Debug.Log("idling");
    }

    private void moveToPlayer()
    {
        if (m_target != null)
        {
            if (Vector2.Distance(transform.position, m_target.position) >= m_agent.stoppingDistance)
            {
                m_agent.SetDestination(m_target.position);
                // enables movement
                gameObject.GetComponent<NavMeshAgent>().isStopped = false;
                //Debug.Log("moving to player");
            }
            else
            {
                //Debug.Log("calling attack");
                m_enemyStates = EnemyState.Attack;
            }
        }
        
    }

    private void attack()
    {
        bool canAttack = true;

        if (Vector2.Distance(transform.position, m_target.position) <= m_agent.stoppingDistance && canAttack)
        {
            // attack once
            bool afterAttack = false;
            if (!afterAttack)   //  actual attack event
            {
                GameObject spawnProjectile = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
                Debug.Log("attacked");
                canAttack = false;
                Vector2 shootDir = (m_target.position - transform.position).normalized;
                spawnProjectile.GetComponent<Rigidbody2D>().AddForce(shootDir * projectileSpeed, ForceMode2D.Impulse);


                afterAttack = true;
                calledDelay = false;
                m_enemyStates = EnemyState.AtkOnCooldown;
            }
        }
    }
        
    private void AtkCooldown()
    {
        if (!calledDelay)
        {
            calledDelay = true;
            GetComponent<NavMeshAgent>().isStopped = true;
            //Debug.Log("calling delay");
            StartCoroutine (SetAtkCooldown(3));
            //Debug.Log("delay called");
        }

    }
    #endregion

    private IEnumerator SetAtkCooldown(float atkDelay)
    {
        //Debug.Log("yielding");
        yield return new WaitForSeconds(atkDelay);
        //Debug.Log("yielded");
        m_enemyStates = EnemyState.MoveToPlayer;
    }

    // Start is called before the first frame update
    void Start()
    {
        m_agent = GetComponent<NavMeshAgent>();
        m_agent.stoppingDistance = stopDistance;
        currentHp = maxHp;

    }



    // Update is called once per frame
    void Update()
    {
        facePlayer();

        switch (m_enemyStates)
        {
            case EnemyState.Idle:
                idle();
                break;
            case EnemyState.MoveToPlayer:
                moveToPlayer();
                break;
            case EnemyState.Attack:
                attack();
                break;
            case EnemyState.AtkOnCooldown: 
                AtkCooldown();
                break;
            case EnemyState.Death:
                death();
                break;
            default:
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            m_enemyStates = EnemyState.MoveToPlayer;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            m_enemyStates = EnemyState.Idle;
        }

    }
    
    
    
    
    //roam attempts
    /*
    private IEnumerator randomRoamCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(3);

            
            Vector2 roamPos = transform.position;
            m_agent.SetDestination(roamPos);
            roamPos += (Vector2)Random.insideUnitCircle.normalized * 5;


            Debug.Log("raoming");
        }
    }

    public Vector3 RandomNavmeshLocation(float radius)
    {
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection += transform.position;
        NavMeshHit hit;
        Vector3 finalPosition = Vector3.zero;
        if (NavMesh.SamplePosition(randomDirection, out hit, radius, 1))
        {
            finalPosition = hit.position;
        }
        return finalPosition;
    }

    public Vector3 RandomNavMeshLocation2()
    {
        Vector3 finalPosition = Vector3.zero;
        Vector3 randomPosition = Random.insideUnitSphere * 10;
        randomPosition += transform.position;
        Debug.Log("roaming");

        return finalPosition;
    }
    */
}

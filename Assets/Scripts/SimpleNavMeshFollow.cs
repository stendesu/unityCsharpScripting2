using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SimpleNavMeshFollow : MonoBehaviour
{
    public Transform m_target;
    NavMeshAgent m_agent;

    public enum EnemyState
    {
        Idle,
        MoveToPlayer,
        Attack
    };

    public EnemyState m_enemyStates;

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

    private void idle()
    {
        gameObject.GetComponent<NavMeshAgent>().isStopped = true;
    }

    private void moveToPlayer()
    {
        if (Vector2.Distance(transform.position, m_target.position) >= m_agent.stoppingDistance)
        {
            m_agent.SetDestination(m_target.position);
            gameObject.GetComponent<NavMeshAgent>().isStopped = false;
        }
        else
        {
            m_enemyStates = EnemyState.Attack;
        }
    }

    private void attack()
    {
        if (Vector2.Distance(transform.position, m_target.position) <= m_agent.stoppingDistance)
        {
            Debug.Log("attack");
        }
        else
        {
            m_enemyStates = EnemyState.MoveToPlayer;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        m_agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {


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
}

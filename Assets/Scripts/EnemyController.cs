using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform m_Player;
    public float m_speed;
    public float m_stoppingDistance;
    public bool m_PlayerInSight;

    public enum EnemyState
    {
        Idle,
        MoveToPlayer,
        Attack
    };

    public EnemyState m_enemyStates;


    // Start is called before the first frame update
    void Start()
    {
        m_Player = FindObjectOfType<TopDownCharacterController>().transform;
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

    private void idle()
    {
        m_PlayerInSight = false;
    }

    private void moveToPlayer()
    {
        if (Vector2.Distance(transform.position, m_Player.position) >= m_stoppingDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, m_Player.position, m_speed * Time.deltaTime);
        }
        else
        {
            m_enemyStates = EnemyState.Attack;
        }
    }

    private void attack()
    {
        if (Vector2.Distance(transform.position, m_Player.position) <= m_stoppingDistance)
        {
            Debug.Log("attack");
        }
        else
        {
            m_enemyStates = EnemyState.MoveToPlayer;
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

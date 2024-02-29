using UnityEngine;

public class MovingObstacle : MonoBehaviour
{
    [SerializeField] Transform m_startPoint;
    [SerializeField] Transform m_endPoint;
    [SerializeField] int m_moveSpeed;
    
    Transform target;
    float rotationSpeed = 270f;

    // Start is called before the first frame update
    void Start()
    {
        target = m_startPoint;

    }

    //Rigidbody2D rb;
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target.position, m_moveSpeed * Time.deltaTime);

        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);

        //transform.Translate(Vector2.up * m_moveSpeed * Time.deltaTime);

        //rb.velocity = Vector2.up * m_moveSpeed;

        //  directly moving
        /*
        Vector2 position = transform.position;
        position.y = transform.position.y + 0.01f * Time.deltaTime;
        //  position.x = transform.position.x + 0.1f;
        //  for moving sideways
        transform.position = position;
        */
    }

    void ChangeTarget()
    {
        if (target == m_startPoint)
        {
            target = m_endPoint;
        }
        else
        {
            target = m_startPoint;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("MovingObstacleWayPoint"))
        {
            ChangeTarget();
        }
    }

}

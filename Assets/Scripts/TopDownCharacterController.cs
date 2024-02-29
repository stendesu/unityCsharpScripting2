using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TopDownCharacterController : MonoBehaviour
{
    #region Framework Stuff
    //Reference to attached animator
    private Animator animator;

    //Reference to attached rigidbody 2D
    private Rigidbody2D rb;

    //The direction the player is moving in
    private Vector2 playerDirection;

    //The speed at which they're moving
    private float playerSpeed = 1f;

    [Header("Movement parameters")]
    //The maximum speed the player can move
    [SerializeField] private float playerMaxSpeed = 100f;
    #endregion
    
    [SerializeField] GameObject m_bulletPrefab;
    [SerializeField] Transform m_firePoint;
    [SerializeField] float m_projectileSpeed;
    public float currentHp, maxHp = 100.0f;
    bool canShoot = true;
    public bool canSwing = false;
    public bool canAxe = false;
    public string equippedWeapon = "gun";

    Vector3 mousePointOnScreen;

    /// <summary>
    /// When the script first initialises this gets called, use this for grabbing componenets
    /// </summary>
    private void Awake()
    {
        //Get the attached components so we can use them later
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    /// <summary>
    /// Called after Awake(), and is used to initialize variables e.g. set values on the player
    /// </summary>
    private void Start()
    {
        currentHp = maxHp;
    }

    /// <summary>
    /// When a fixed update loop is called, it runs at a constant rate, regardless of pc perfornamce so physics can be calculated properly
    /// </summary>
    private void FixedUpdate()
    {
        //Set the velocity to the direction they're moving in, multiplied
        //by the speed they're moving
        rb.velocity = playerDirection * (playerSpeed * playerMaxSpeed) * Time.fixedDeltaTime;
    }

    void Fire()
    {
        if (canShoot)
        {
            GameObject bulletToSpawn = Instantiate(m_bulletPrefab, transform.position, Quaternion.identity);

            if (bulletToSpawn.GetComponent<Rigidbody2D>() != null)
            {

                canShoot = false;
                mousePointOnScreen.z = 0;
                Vector2 shootDir = (mousePointOnScreen - transform.position).normalized;
                bulletToSpawn.GetComponent<Rigidbody2D>().AddForce(shootDir * m_projectileSpeed, ForceMode2D.Impulse);
                StartCoroutine(setAtkCooldown(0.1f));
            }
        }
    }

    bool canDagger = true;
    public GameObject daggerPrefab;
    float DaggerSpeed = 60.0f;

    void ThrowDagger()
    {
        if (canDagger)
        {
            GameObject spawnDagger = Instantiate(daggerPrefab, transform.position, Quaternion.identity);

            if (spawnDagger.GetComponent<Rigidbody2D>() != null)
            {
                canDagger = false;
                mousePointOnScreen.z = 0;
                Vector2 shootDir = (mousePointOnScreen - transform.position).normalized;
                spawnDagger.GetComponent<Rigidbody2D>().AddForce(shootDir * DaggerSpeed, ForceMode2D.Impulse);
                StartCoroutine(setDaggerCooldown(0.4f));
            }
        }
    }
    
    public GameObject swordPrefab;
    private Animator swordAnim;
    bool calledSwingDelay = false;

    void swingSword()
    {
        if (canSwing)
        {
            if (playerDirection.x < 0)    //  swing left
            {
                Vector3 spawnPos = transform.position + new Vector3 (-1, 0, 0);
                GameObject spawnSword = Instantiate(swordPrefab, spawnPos, Quaternion.identity);
                spawnSword.GetComponent<PlayerSwordManager>();

                canSwing = false;
                swordAnim = spawnSword.GetComponent<Animator>();
                swordAnim.Play("swordSwingAnim");
                calledSwingDelay = false;
                StartCoroutine(swingCooldown(0.6f));
            }
            else if (playerDirection.x > 0)    //  swing right
            {
                Vector3 spawnPos = transform.position + new Vector3(1, 0, 0);
                GameObject spawnSword = Instantiate(swordPrefab, spawnPos, Quaternion.identity);
                canSwing = false;
                swordAnim = spawnSword.GetComponent<Animator>();
                swordAnim.Play("swordSwingAnimRight");
                calledSwingDelay = false;
                StartCoroutine(swingCooldown(0.6f));
            }
            else    //  swing left if X = 0
            {
                Vector3 spawnPos = transform.position + new Vector3(-1, 0, 0);
                GameObject spawnSword = Instantiate(swordPrefab, spawnPos, Quaternion.identity);
                canSwing = false;
                swordAnim = spawnSword.GetComponent<Animator>();
                swordAnim.Play("swordSwingAnim");
                calledSwingDelay = false;
                StartCoroutine(swingCooldown(0.6f));
            }

        }
    }

    public GameObject axePrefab;
    private Animator axeAnim;
    bool calledAxeDelay = false;

    void throwAxe()
    {
        if (canAxe)
        {
            canAxe = false;
            //Vector3 spawnPos = transform.position + new Vector3 (0, 0, 0);
            GameObject SpawnAxe = Instantiate(axePrefab, transform.position, Quaternion.identity);
            ThrowAxeScript axeScript = SpawnAxe.GetComponent<ThrowAxeScript>();

            // lerp axe to mouse pos
            mousePointOnScreen.z = 0;
            axeScript.targetLocation = mousePointOnScreen;


            //axeScript.returnLocation = transform.position;

            //Debug.Log("PlayerLocation = " + transform.position);
            //Debug.Log("AxeReturnLocation = " + axeScript.returnLocation);

            // anim control
            axeAnim = SpawnAxe.GetComponent<Animator>();
            axeAnim.Play("AxeThrow");

            // cooldown
            calledAxeDelay = false;
            StartCoroutine(axeCooldown(0.9f));
        }
    }

    private IEnumerator axeCooldown(float duration)
    {
        if (!calledAxeDelay)
        {
            yield return new WaitForSeconds(duration);
            calledAxeDelay = true;
            canAxe = true;
        }
    }

    private IEnumerator swingCooldown(float duration)
    {
        if (!calledSwingDelay) 
        { 
            yield return new WaitForSeconds(duration);
            calledSwingDelay = true;
            canSwing = true;
        }

    }

    private IEnumerator setAtkCooldown (float duration)
    {
        yield return new WaitForSeconds(duration);
        canShoot = true;
    }

    private IEnumerator setDaggerCooldown(float duration)
    {
        yield return new WaitForSeconds(duration);
        canDagger = true;
    }

    void getMousePos()
    {
        mousePointOnScreen = Camera.main.ScreenToWorldPoint(Input.mousePosition);   
    }

    private void checkIfDead()
    {
        if (currentHp <= 0)
        {
            SceneManager.LoadScene("UI_Death", LoadSceneMode.Additive);
            Destroy(gameObject); return;
        }
    }

    public void takeDamage(float damage)
    {
        currentHp -= damage;
        checkIfDead();
    }

    public Vector3 mousePos;

    /// <summary>
    /// When the update loop is called, it runs every frame, ca run more or less frequently depending on performance. Used to catch changes in variables or input.
    /// </summary>
    private void Update()
    {
        getMousePos();

        // read input from WASD keys
        playerDirection.x = Input.GetAxis("Horizontal");
        playerDirection.y = Input.GetAxis("Vertical");

        // check if there is some movement direction, if there is something, then set animator flags and make speed = 1
        if (playerDirection.magnitude != 0)
        {
            animator.SetFloat("Horizontal", playerDirection.x);
            animator.SetFloat("Vertical", playerDirection.y);
            animator.SetFloat("Speed", playerDirection.magnitude);

            //And set the speed to 1, so they move!
            playerSpeed = 1f;

            if (Input.GetKeyDown(KeyCode.LeftShift))
                animator.SetTrigger("Rolling");

            if (animator.GetCurrentAnimatorStateInfo(0).IsName("rollTree"))
                playerSpeed = 1.5f;
        }
        else
        {
            //Was the input just cancelled (released)? If so, set
            //speed to 0
            playerSpeed = 0f;

            //Update the animator too, and return
            animator.SetFloat("Speed", 0);
        }

        // Was the fire button pressed (mapped to Left mouse button or gamepad trigger)
        if (Input.GetButton("Fire1"))
        {
            if (equippedWeapon == "gun")
            {
                if (Input.GetButton("Fire1"))
                {
                    Fire();
                }
            }
            else if (equippedWeapon == "VampireDagger")
            {
                if (Input.GetButton("Fire1"))
                {
                    ThrowDagger();

                }
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            swingSword();
        }

        if (Input.GetKeyDown (KeyCode.Q)) 
        {
            throwAxe();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            equippedWeapon = "gun";
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            equippedWeapon = "VampireDagger";
        }

        mousePos = mousePointOnScreen;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameObject pauseManager = GameObject.Find("pauseMenuManager");
            pauseMenuManager pauseMgrScript = pauseManager.GetComponent<pauseMenuManager>();

            if (!pauseMgrScript.paused)
            {
                pauseMgrScript.activateCanvas();
            }

        }

    }


}

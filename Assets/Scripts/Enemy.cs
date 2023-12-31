using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private BoxCollider2D boxCollider;
    ItemsSpawner boost;
    [SerializeField] private float enemySpeedX = 0.4f;
    [SerializeField] private float enemySpeedY = 0f;
    private Transform enemyDeadPos;
    private PlayerControl playerControl;
    SFXcontrol audioManager;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        boost = FindObjectOfType<ItemsSpawner>();
        playerControl = FindObjectOfType<PlayerControl>();
        audioManager = GameObject.FindGameObjectWithTag("SFX").GetComponent<SFXcontrol>();

    }

    void Update()
    {
        if (!playerControl.pauseOn)
        {
            rb.velocity = new Vector2(enemySpeedX, enemySpeedY);
        }
        else
        {
            rb.velocity = new Vector2(0, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "EnemyWall")
        {
            Vector3 localScale = transform.localScale;
            localScale.x *= -1;
            //localScale.y *= -1; //for wall movement
            transform.localScale = localScale;
            enemySpeedX *= -1;
            enemySpeedY *= -1;  

        }
        if (collision.tag == "Dagger")
        {
            enemySpeedX = 0;
            enemySpeedY = 0;
            anim.SetBool("Dead", true);
            audioManager.PlaySFX(audioManager.enemyDeath);//////////////////////////////////SFX//////////
                                                
            Debug.Log("boost spawned");
            enemyDeadPos = transform;
            

        }
        if (collision.tag == "Player")
        {
            enemySpeedX = 0;
            enemySpeedY = 0;
            anim.SetTrigger("Hit");
   
        }


    }


    public void DestroyEnemy()
    {
        Destroy(this.gameObject);
        Debug.Log("enemys dead");

    }

    private void DisableCollider()
    {
        boxCollider.enabled = false;
    }

    public void Boost()
    {
        boost.SpawnBoost(enemyDeadPos);
    }

    public void Item()
    {
        boost.SpawnItem(enemyDeadPos);
    }

}

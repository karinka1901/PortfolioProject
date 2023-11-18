using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private BoxCollider2D boxCollider;
    ItemsSpawner boost;
    [SerializeField] private float enemySpeed = 0.4f;
    private Transform enemyDeadPos;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        boost = FindObjectOfType<ItemsSpawner>();
        //boost = GetComponent<ItemsSpawner>();

    }

    void Update()
    {
        rb.velocity = new Vector2(enemySpeed, rb.velocity.y);
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "EnemyWall")
        {
            Vector3 localScale = transform.localScale;
            localScale.x *= -1;
            transform.localScale = localScale;
            enemySpeed *= -1;
        }
        if (collision.tag == "Dagger")
        {
            enemySpeed = 0;
            anim.SetBool("Dead", true);
           // Debug.Log("enemys dead");
            Debug.Log("boost spawned");
            enemyDeadPos = transform;
            

        }
        if (collision.tag == "Player")
        {
            enemySpeed = 0;
            anim.SetTrigger("Hit");
   
        }


    }
    private IEnumerator EnemyDeath(float delay)
    {
        
        anim.SetBool("Dead", true);
        yield return new WaitForSeconds(delay);

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

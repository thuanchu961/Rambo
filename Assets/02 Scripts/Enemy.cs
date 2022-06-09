using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private int heath;
    [SerializeField] private int enemyScore;
    [SerializeField] private GameObject effectEnemyDeath;

    Transform playerTransform;
    bool moveLeftOnStart;
    protected bool canMove;
    Vector2 scale;
    Animator anim;
    BoxCollider2D box;
    Rigidbody2D rigi;
    private void Awake()
    {
        box = this.GetComponent<BoxCollider2D>();
        rigi = this.GetComponent<Rigidbody2D>();
        anim = this.GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        playerTransform = PlayerController.player_instance.transform;
        speed = Random.Range(2,5);
        scale = this.transform.localScale;
        CheckMoveDirection();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.isPaused)
            return;
        if (Vector2.Distance(playerTransform.position, transform.position) < 5)
        {
            canMove = false;
            //anim.SetTrigger("idle");
        }
        else
        {
            canMove = true;
        }

        if (canMove)
        {
            Move();
        }
    }

    protected void Move()
    {
        if (moveLeftOnStart) 
        {
            if(transform.position.x < playerTransform.position.x)
            {
                transform.Translate(new Vector2(speed * Time.deltaTime, 0)); // di chuyen sang phai
                this.transform.localScale = new Vector2(scale.x * -1, scale.y);
            }
            else
            {
                transform.Translate(new Vector2(-speed * Time.deltaTime, 0)); // di chuyen sang trai
                this.transform.localScale = scale;
            }
            
        }
        else
        {
            if(transform.position.x > playerTransform.position.x)
            {
                transform.Translate(new Vector2(-speed * Time.deltaTime, 0)); // di chuyen sang trai
                this.transform.localScale = scale;
            }
            else
            {
                transform.Translate(new Vector2(speed * Time.deltaTime, 0)); // di chuyen sang phai
                this.transform.localScale = new Vector2(scale.x * -1, scale.y);
            }
            
        }
        anim.SetTrigger("walking");
    }

    void CheckMoveDirection()
    {
        moveLeftOnStart = transform.position.x > 0 ? true : false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            rigi.bodyType = RigidbodyType2D.Kinematic;
            box.isTrigger = true;
            canMove = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player_bullet")
        {
            canMove = false;
            anim.SetTrigger("die");
            StartCoroutine(deActive());
        }
        else if (collision.gameObject.tag == "Grenade")
        {
            canMove = false;
            anim.SetTrigger("grenadeDie");
            StartCoroutine(deActive());
        }
    }

    IEnumerator deActive()
    {
        //yield return new WaitForSeconds(0.5f);
        effectEnemyDeath.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        WaveSpawner.instance.currentEnemiesKilled++;
        PlayerController.player_instance.score += enemyScore;
        PlayerPrefs.SetInt("Score", PlayerController.player_instance.score);
        this.gameObject.SetActive(false);
    }
}

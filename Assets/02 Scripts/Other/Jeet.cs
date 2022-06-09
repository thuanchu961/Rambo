using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jeet : MonoBehaviour
{
    [SerializeField]private float speed;
    [SerializeField]private float heath;
    [SerializeField]private Transform BombPoint;

    Transform playerPos;
    Rigidbody2D rigi;
    PolygonCollider2D poly;
    Animator anim;

    float maxDir;
    float minDir;
    bool canMove;
    float delayTime = 0.3f;
    float timer;
    int waitTime;
    Vector2 scale;
    private float _way;
    //public float way
    //{
    //    get { return _way; }

    //    set
    //    {
    //        if (value > 0)
    //        {
    //            _way = 1;
    //        }
    //        else
    //        {
    //            _way = -1;
    //        }
    //    }
    //}
    private void Awake()
    {
        rigi = this.GetComponent<Rigidbody2D>();
        poly = this.GetComponent<PolygonCollider2D>();
        anim = this.GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        canMove = true;
        
        scale = this.transform.localScale;
        _way = -1;
        timer = delayTime;
        waitTime = Random.Range(5, 10);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.isPaused)
            return;
        Debug.Log("canMove : " + canMove);
        if (canMove)
        {
            Movement();
        }
        playerPos = PlayerController.player_instance.transform;
        maxDir = playerPos.position.x + 2;
        minDir = playerPos.position.x - 2;
        if (transform.position.x >= minDir && transform.position.x <= maxDir)
        {
            DropTheBomb();
        }
    }

    void Movement()
    {
        rigi.velocity = new Vector2(_way * speed * Time.deltaTime, 0);
        this.transform.localScale = new Vector2((-1)*_way * scale.x, scale.y);
    }
    

    void SpawnBomb()
    {
        Bomb b = ObjectPoolings.instance.GetBomb().GetComponent<Bomb>();
        b.transform.position = BombPoint.position;
        b.gameObject.SetActive(true);
        AudioManager.instance.PlaySound(3);
    }

    void DropTheBomb()
    {
        if(timer > 0)
        {
            timer -= Time.deltaTime;
            return;
        }
        SpawnBomb();
        timer = delayTime;
    }
    
   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player_bullet")
        {
            heath--;
            if(heath == 0)
            {
                StartCoroutine(deActive());
            }
        }
        if(collision.gameObject.tag == "End")
        {
            canMove = false;
            rigi.velocity = Vector2.zero;
            StartCoroutine(jetTurn());
            Debug.Log("1");
        }
        if (collision.gameObject.tag == "End1")
        {
            canMove = false;
            rigi.velocity = Vector2.zero;
            StartCoroutine(jetTurn1());
            Debug.Log("1");
        }
    }
    IEnumerator deActive()
    {
        rigi.gravityScale = 1;
        yield return new WaitForSeconds(0.5f);
        anim.SetBool("isDead", true);
        yield return new WaitForSeconds(1);
        this.gameObject.SetActive(false);
    }
    IEnumerator jetTurn()
    {
        yield return new WaitForSeconds(waitTime);
        _way = 1;
        canMove = true;
    }
    IEnumerator jetTurn1()
    {
        yield return new WaitForSeconds(waitTime);
        _way = -1;
        canMove = true;
    }



}

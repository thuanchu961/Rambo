using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float health;
    
   
    public GameObject effectExplosion;
    public GameObject PartOfCar;
    public GameObject Gift;

    float timer = 0;
    float spawnTime = 3;

    Rigidbody2D rigi;
    BoxCollider2D boxColli;
    Animator anim;
    SpriteRenderer sprite;
    Transform playerTransform;
    private void Awake()
    {
        rigi = this.GetComponent<Rigidbody2D>();
        boxColli = this.GetComponent<BoxCollider2D>();
        anim = this.GetComponent<Animator>();
        sprite = this.GetComponent<SpriteRenderer>();
    }
    // Start is called before the first frame update
    void Start()
    {
        playerTransform = PlayerController.player_instance.transform;
        rigi.bodyType = RigidbodyType2D.Kinematic;
        boxColli.isTrigger = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(this.transform.position.x > (playerTransform.position.x + 10))
        {
            speed = 0;
            //boxColli.isTrigger = false;
            
        }
        Movement();
        
        
    }

    void Movement()
    {
        rigi.velocity = Vector2.right * speed * Time.deltaTime;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player_bullet")
        {
            health--;
            Debug.Log("Car health" +health);
            if(health <= 0)
            {
                //Car explosion
                sprite.enabled = false;
                boxColli.enabled = false;
                PartOfCar.SetActive(false);
                anim.SetBool("explosion", true);
                effectExplosion.SetActive(true);
                
                Gift.SetActive(true);
                StartCoroutine(waitForDestroy());

            }

        }
    }
    IEnumerator waitForDestroy()
    {
        
        yield return new WaitForSeconds(30f);
        Destroy(gameObject);
        
    }
}

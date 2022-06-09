using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour
{
    [SerializeField] private GameObject effectExplosion;
    int health = 1;
    Animator anim;
    CapsuleCollider2D capColli;
    SpriteRenderer sprite;
    private void Awake()
    {
        anim = this.GetComponent<Animator>();
        capColli = this.GetComponent<CapsuleCollider2D>();
        sprite = this.GetComponent<SpriteRenderer>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Grenade")
        {
            sprite.enabled = false;
            effectExplosion.SetActive(true);
            StartCoroutine(waitForDestroy());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player_bullet")
        {
            health++;
            anim.SetTrigger(health.ToString());
            if(health > 3)
            {
                sprite.enabled = false;
                effectExplosion.SetActive(true);
                StartCoroutine(waitForDestroy());
            }

        }
    }

    IEnumerator waitForDestroy()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}

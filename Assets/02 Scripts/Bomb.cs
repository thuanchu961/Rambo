using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{

    Rigidbody2D rigi;
    Animator anim;
    CapsuleCollider2D capColli;
    private void Awake()
    {
        rigi = this.GetComponent<Rigidbody2D>();
        anim = this.GetComponent<Animator>();
        capColli = this.GetComponent<CapsuleCollider2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnEnable()
    {
        rigi.bodyType = RigidbodyType2D.Dynamic;
        capColli.isTrigger = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Player")
        {
            StartCoroutine(deActive());
        }
    }

    IEnumerator deActive()
    {
        rigi.bodyType = RigidbodyType2D.Kinematic;
        capColli.isTrigger = true;
        anim.SetTrigger("Explosion");
        yield return new WaitForSeconds(0.5f);
        this.gameObject.SetActive(false);
    }
}

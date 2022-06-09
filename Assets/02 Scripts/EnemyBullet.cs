using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float dmg = 1;
    [SerializeField] float timeDestroy = 1f;

    Vector2 scale;
    private float _way = 1;
    public float way
    {
        get { return _way; }

        set
        {
            if (value > 0)
            {
                _way = -1;
            }
            else
            {
                _way = 1;
            }
        }
    }

   
    Rigidbody2D rigi;
    BoxCollider2D colli;
    private void Awake()
    {
        rigi = this.GetComponent<Rigidbody2D>();
        colli = this.GetComponent<BoxCollider2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        scale = this.transform.localScale;
        Destroy(gameObject, 2);
    }
   

    // Update is called once per frame
    void Update()
    {
        rigi.velocity = new Vector2(Time.deltaTime * speed * _way, 0);
        this.transform.localScale = new Vector2(scale.x * _way, scale.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
        
    }
  
}

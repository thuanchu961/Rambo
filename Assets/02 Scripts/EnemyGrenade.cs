using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGrenade : MonoBehaviour
{
    public Vector3 launchOffSet;
    public float speed;
    public float damage = 10;

    Rigidbody2D rigi;
    BoxCollider2D box;
    Animator anim;
    Vector2 scale;
    private float _way = 1;
    public float way
    {
        get { return _way; }

        set
        {
            if (value > 0)
            {
                _way = 1;
            }
            else
            {
                _way = -1;
            }
        }
    }
    private void Awake()
    {
        rigi = this.GetComponent<Rigidbody2D>();
        box = this.GetComponent<BoxCollider2D>();
        anim = this.GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        ThrowGrenade();
    }
    void ThrowGrenade()
    {
        var dir = -transform.right + Vector3.up;
        rigi.AddForce(new Vector3(dir.x * _way, dir.y, dir.z) * speed, ForceMode2D.Impulse);

        transform.Translate(launchOffSet);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Player")
        {
            anim.SetTrigger("explosion");
            StartCoroutine(waitFordestroy());
        }
    }
    IEnumerator waitFordestroy()
    {
        yield return new WaitForSeconds(0.2f);
        Destroy(gameObject);
    }

}

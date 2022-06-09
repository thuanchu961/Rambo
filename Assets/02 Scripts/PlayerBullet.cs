using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
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
            if(value > 0)
            {
                _way = -1;
            }
            else
            {
                _way = 1;
            }
        }
    }

    Coroutine WaitDestroy = null;
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
    }
    private void OnEnable()
    {
        WaitDestroy = StartCoroutine(DeActive());
    }

    // Update is called once per frame
    void Update()
    {
        rigi.velocity = new Vector2(Time.deltaTime * speed * _way, 0);
        this.transform.localScale = new Vector2(scale.x * _way, scale.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        StopCoroutine(WaitDestroy);
        this.gameObject.SetActive(false);
    }
    IEnumerator DeActive()
    {
        yield return new WaitForSeconds(timeDestroy);
        this.gameObject.SetActive(false);
    }
}

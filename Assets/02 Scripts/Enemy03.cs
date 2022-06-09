using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy03 : MonoBehaviour
{
    public GameObject EnemyBullet_prefabs;
    public Transform FirePoint;
    private Transform playerPos;
    float timer = 0;
    float timeShoot= 2f;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = this.GetComponent<Animator>();
        timer = timeShoot;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.isPaused)
            return;
        playerPos = PlayerController.player_instance.transform;
        if (Vector2.Distance(playerPos.position, transform.position) < 5)
        {
     
            timer += Time.deltaTime;
            if (timer > timeShoot)
            {
                Debug.Log("Fire");
                anim.SetTrigger("attack");
                EnemyBullet b = Instantiate(EnemyBullet_prefabs, FirePoint.position, Quaternion.identity).GetComponent<EnemyBullet>();
                b.way = this.transform.localScale.x;
                timer = 0;
            }
        }
    }
   

}

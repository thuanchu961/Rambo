using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy02 : MonoBehaviour
{
    public GameObject EnemyGrenade_prefabs;
    public Transform LaunchPoint;
    Transform playerTransform;
    Animator anim;
    float timer = 0;
    float timeThrow = 5f;
    // Start is called before the first frame update
    void Start()
    {
        anim = this.GetComponent<Animator>();
        playerTransform = PlayerController.player_instance.transform;
        timer = timeThrow;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.isPaused)
            return;
        if (Vector2.Distance(playerTransform.position, transform.position) < 5)
        {
            timer += Time.deltaTime;
            if(timer > timeThrow)
            {
                anim.SetTrigger("attack");
                EnemyGrenade g = Instantiate(EnemyGrenade_prefabs, LaunchPoint.position, Quaternion.identity).GetComponent<EnemyGrenade>();
                g.way = this.transform.localScale.x;
                timer = 0;
            }
            
        }
       
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gift : MonoBehaviour
{
    Animator anim;
    private void Awake()
    {
        anim = this.GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            StartCoroutine(collected());
        }
    }

    IEnumerator collected()
    {
        anim.SetTrigger("Collected");
        yield return new WaitForSeconds(0.2f);
        Destroy(gameObject);
        PlayerController.player_instance.effectBoostAttack.SetActive(true);
       
    }
}

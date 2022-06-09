using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hostage : MonoBehaviour
{
    public static Hostage instance;
    public GameObject Cage;
    public GameObject efffectRescue;
    bool isRelease = false;
    Animator anim;
    private void Awake()
    {
        instance = this;
        anim = this.GetComponent<Animator>();
    }
    private void Update()
    {
        if (!isRelease)
        {
            MissionSystem.instance.StatusOfMissionText.text = "0/1";
        }
        else
        {
            MissionSystem.instance.StatusOfMissionText.text = "1/1";
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            anim.SetTrigger("release");

            StartCoroutine(waitForRescue());
        }
    }

    IEnumerator waitForRescue()
    {
        yield return new WaitForSeconds(2);
        isRelease = true;
        efffectRescue.SetActive(true);
        yield return new WaitForSeconds(1);
        MissionSystem.instance.MissionCompleted = true;
    }
}

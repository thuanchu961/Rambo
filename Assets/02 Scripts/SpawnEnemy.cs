using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public int maxTime;
    float timer;
    // Start is called before the first frame update
    void Start()
    {
        maxTime = 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.isPaused)
            return;

        timer += Time.deltaTime;
        if (timer > maxTime)
        {
            Enemy e = ObjectPoolings.instance.GetEnemy().GetComponent<Enemy>();
            e.transform.position = this.transform.position;
            e.gameObject.SetActive(true);
            maxTime = Random.Range(2, 8);
            timer = 0;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObjectPooling : MonoBehaviour
{
    public static PlayerObjectPooling instance;
    [SerializeField] public GameObject BulletPrefab;
    [SerializeField] public GameObject GrenadePrefab;

    private List<GameObject> bullets = new List<GameObject>();
    private List<GameObject> grenades = new List<GameObject>();

    private void Awake()
    {
        instance = this;
    }
    public GameObject GetBullet()
    {
        foreach (GameObject b in bullets)
        {
            if (b.activeSelf)
            {
                continue;
            }
            return b;
        }
        GameObject b1 = Instantiate(BulletPrefab, this.transform.position, Quaternion.identity);
        bullets.Add(b1);
        return b1;
    }
    public GameObject GetGrenade()
    {
        foreach (GameObject g in grenades)
        {
            if (g.activeSelf)
            {
                continue;
            }
            return g;
        }
        GameObject g1 = Instantiate(GrenadePrefab, this.transform.position, Quaternion.identity);
        bullets.Add(g1);
        return g1;
    }
}

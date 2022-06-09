using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolings : MonoBehaviour
{
    public static ObjectPoolings instance;

    [SerializeField] public GameObject[] Enemy_prefabs;
    [SerializeField] public GameObject Bomb_prefabs;
    
   
    private List<GameObject> Bombs = new List<GameObject>();
    private List<GameObject> Enemies = new List<GameObject>();
    
    
    private void Awake()
    {
        instance = this;
    }
    public GameObject GetEnemy()
    {
        foreach(GameObject b in Enemies)
        {
            if (b.activeSelf)
            {
                continue;
            }
            return b;
        }
        int index = Random.Range(0, Enemy_prefabs.Length);
        GameObject b2 = Instantiate(Enemy_prefabs[index],this.transform.position,Quaternion.identity);
        Enemies.Add(b2);
        return b2;
    }

    public GameObject GetBomb()
    {
        foreach (GameObject b in Bombs)
        {
            if (b.activeSelf)
            {
                continue;
            }
            return b;
        }
        GameObject bomb = Instantiate(Bomb_prefabs, this.transform.position, Quaternion.identity);
        Bombs.Add(bomb);
        return bomb;
    }
    
}

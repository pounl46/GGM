using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMonsters : MonoBehaviour
{
    public List<GameObject> monster = new List<GameObject>();
    public bool StopSpawn = false;
    void Start()
    {
        Spawn();
        StartCoroutine(Monster2Wait());
        StartCoroutine(WaitMonster1());
    }

    IEnumerator enumerator()
    {
        yield return new WaitForSeconds(Random.Range(10,20)/7);
        if(StopSpawn)
        {
            for(int i = monsters.Count - 1;i >=0;i--)
            {
                Destroy(monsters[i]);
            }
        }
        Spawn();
    }
    IEnumerator WaitMonster1()
    {
        yield return new WaitForSeconds(75);
        StartCoroutine(Monster1DubleSpawn());
    }

    private bool spawn = true;

    IEnumerator Monster1DubleSpawn()
    {
        if(spawn)
        {
            spawn = false;
            yield return new WaitForSeconds(Random.Range(10,20)/5);
            Spawn();
            StartCoroutine(Monster1DubleSpawn());
        }
        else
        {
            yield return new WaitForSeconds(Random.Range(10,20)/5);
            StartCoroutine(Monster1DubleSpawn());
            spawn = true;
        }
    }

    IEnumerator Monster2Wait()
    {
        yield return new WaitForSeconds(120);
        Spawn2();
    }

    private int countMonster = 0;

    IEnumerator Monster2()
    {
        yield return new WaitForSeconds(10);
        Spawn2();
        countMonster++;
    }
    private List<GameObject> monsters = new List<GameObject>();
    void Spawn()
    {
        if(!StopSpawn)
        {
            GameObject monsterrrr = Instantiate(monster[0],new Vector2(Random.Range(-17,17),Random.Range(-7,47)),this.transform.rotation);
            StartCoroutine(enumerator());
            monsters.Add(monsterrrr);
        }
    }

    void Spawn2()
    {
        if(countMonster <= 10 && !StopSpawn)
        {
            GameObject monsterrrrr = Instantiate(monster[1],new Vector2(Random.Range(-17,17),Random.Range(-7,47)),this.transform.rotation);
            StartCoroutine(Monster2()); 
            monsters.Add(monsterrrrr);
        }  
    }
}

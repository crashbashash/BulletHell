using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Tuple<string, float>[] enemyList =
    {
        Tuple.Create("Striker", 3f),
        Tuple.Create("Spreet", 1f),
        Tuple.Create("S.Spirit", 4f),
        Tuple.Create("Spreet", 1f),
        Tuple.Create("Spreet", 2f),
        Tuple.Create("Strker", 4f),
        Tuple.Create("S.Spirit", 1f)
    };

    private bool running = true;
    private GlobalVars globalVars;
    private bool spawningEnemies = false;
    private Transform enemies;

    private GameObject strikerGO;
    private GameObject spreetGO;
    private GameObject sSpiritGO;

    public Transform[] striker_spawns = new Transform[4];
    public Transform[] spreet_spawns = new Transform[3];
    public Transform[] sSpirit_spawns = new Transform[3];

    public bool Running { get => running; set => running = value; }

    private void Start()
    {
        globalVars = GameObject.Find("Game Control").GetComponent<GlobalVars>();
        strikerGO = globalVars.enemies[0];
        spreetGO = globalVars.enemies[1];
        sSpiritGO = globalVars.enemies[2];
    }

    // Update is called once per frame
    void Update()
    {
        if(enemies == null)
        {
            enemies = GameObject.Find("Main Camera").transform.Find("Enemies");
        }
        if (running && !spawningEnemies)
        {
            StartCoroutine(SpawnEnemies());
            spawningEnemies = true;
        }
    }

    IEnumerator SpawnEnemies()
    {
        for (int i = 0; i < enemyList.Length; i++)
        {
            yield return new WaitForSeconds(enemyList[i].Item2);
            //Check if spawning striker
            if (enemyList[i].Item1 == "Striker")
            {
                for (int j = 0; j < striker_spawns.Length; j++)
                {
                    GameObject striker = Instantiate(strikerGO, striker_spawns[j].position, strikerGO.transform.rotation); //Spawn first striker
                    striker.transform.parent = enemies;
                    EnemyAI ai = striker.GetComponent<EnemyAI>();
                    ai.ID = j;

                    if (j == 0)
                        yield return new WaitForSeconds(0.5f);
                    else if (j == 1 || j == 2)
                        yield return new WaitForSeconds(0.25f);
                }
            }
            //Check if spawning spreet
            else if (enemyList[i].Item1 == "Spreet")
            {
                for (int j = 0; j < spreet_spawns.Length; j++)
                {
                    GameObject spreet = Instantiate(spreetGO, spreet_spawns[j].position, spreetGO.transform.rotation); //Spawn first striker
                    spreet.transform.parent = enemies;
                    EnemyAI ai = spreet.GetComponent<EnemyAI>();
                    ai.ID = j;
                    yield return new WaitForSeconds(0.25f);
                }
            }
            //Check if spawning S.Spirit
            else if (enemyList[i].Item1 == "S.Spirit")
            {
                for (int j = 0; j < sSpirit_spawns.Length; j++)
                {
                    GameObject spirit = Instantiate(sSpiritGO, sSpirit_spawns[j].position, sSpiritGO.transform.rotation);
                    spirit.transform.parent = enemies;
                    EnemyAI ai = spirit.GetComponent<EnemyAI>();
                    ai.ID = j;
                    yield return new WaitForSeconds(0f);
                }
            }
        }
        yield return new WaitForSeconds(5f);
        //running = false;
        spawningEnemies = false;
    }
}

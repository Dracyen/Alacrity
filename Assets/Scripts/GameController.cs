using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject[] Enemies;

    public Transform[] SpawnPoints;

    public Text levelCount;
    public int Level { get; private set; }

    private int enemyCount;

    void Start()
    {
        Level = 1;

        StartLevel();
    }

    private void Update()
    {
        if(enemyCount <= 0)
        {
            NextLevel();
        }
    }

    void StartLevel()
    {
        enemyCount = Level * 2;

        levelCount.text = ("Level: " + Level);

        if (enemyCount > Enemies.Length)
        {
            enemyCount = Enemies.Length;
        }

        for (int i = 0; i < enemyCount; i++)
        {
            Vector3 Spawn = SpawnPoints[Random.Range(0, SpawnPoints.Length)].position;

            Enemies[i].GetComponent<EnemyScript>().UpdatePos(Spawn);
        }
    }

    public void UpdateEnemies()
    {
        enemyCount -= 1;
    }

    void NextLevel()
    {
        Level += 1;
        StartLevel();
    }
}

using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System;

public class EnemiesManager : MonoBehaviour
{
    [Header("Tramsform settings")]
    [SerializeField] private Transform transformEnemiesParent;
    [SerializeField] private Transform parentBulletsEnemy;
    [SerializeField] private Transform targetPatrol;
    [SerializeField] private Transform targetAsteroid;

    [Header("Enemy settings")]
    [SerializeField] private List<EnemyInfo> enemyInfo;

    [Header("Other settings")]
    [SerializeField] private float startSpawnTime;
    [SerializeField] private float spawnDelay;
    [SerializeField] private int enemyCount;

    private List<EnemyByType> enemyByTypes;
    private PlayerController playerController;

    public event Action<int> OnEnemyDie;
    public event Action Victory;

    public int EnemyCount
    {
        get
        {
           return enemyCount;
        }
    }

    public void Init(PlayerController playerController)
    {
        this.playerController = playerController;
        CreatePool();
        StartCoroutine(SpawnEnemy());
    }

    public void Refresh()
    {
        for (int i = 0; i < enemyByTypes.Count; i++)
        {
            for (int j = 0; j < enemyByTypes[i].enemyControllers.Count; j++)
            {
                if (enemyByTypes[i].enemyControllers[j].gameObject.activeSelf)
                {
                    enemyByTypes[i].enemyControllers[j].Refresh();
                }
            }
        }
    }

    public void CreatePool()
    {
        enemyByTypes = new List<EnemyByType>();
        for (int i = 0; i < enemyInfo.Count; i++)
        {
            EnemyByType enemyByType = new EnemyByType(enemyInfo[i].typeEnemy);

            for (int j = 0; j < enemyInfo[i].poolCount; j++)
            {
                EnemyController enemyController = Instantiate(enemyInfo[i].enemyPrefab, enemyInfo[i].spawnPoint.position, enemyInfo[i].enemyPrefab.transform.rotation, transformEnemiesParent);
                enemyController.gameObject.SetActive(false);
                enemyController.Init(playerController, OnEnemyDieHandler, parentBulletsEnemy, targetPatrol, targetAsteroid);
                enemyByType.enemyControllers.Add(enemyController);
            }
            enemyByTypes.Add(enemyByType);
        }
    }

    public EnemyController GetPooledEnemy(TypeEnemy typeEnemy)
    {
        for (int i = 0; i < enemyByTypes.Count; i++)
        {
            if(enemyByTypes[i].typeEnemy == typeEnemy)
            {
                for (int j = 0; j < enemyByTypes[i].enemyControllers.Count; j++)
                {
                    if (!enemyByTypes[i].enemyControllers[j].gameObject.activeSelf)
                    {
                        return enemyByTypes[i].enemyControllers[j];
                    }
                }
            }

            EnemyController enemyController = Instantiate(enemyInfo[i].enemyPrefab, enemyInfo[i].spawnPoint.position, enemyInfo[i].enemyPrefab.transform.rotation, transformEnemiesParent);
            enemyController.gameObject.SetActive(false);
            enemyController.Init(playerController, OnEnemyDieHandler, parentBulletsEnemy, targetPatrol, targetAsteroid);
            enemyByTypes[i].enemyControllers.Add(enemyController);
            return enemyController;
        }
        return null;
    }

    public IEnumerator SpawnEnemy()
    {
        yield return new WaitForSeconds(startSpawnTime);

        while (playerController.IsAlive && enemyCount < 2)
        {
            for (int i = 0; i < enemyInfo.Count; i++)
            {
                EnemyController retrievedEnemy = GetPooledEnemy(enemyInfo[i].typeEnemy);
                if (retrievedEnemy != null)
                {
                    retrievedEnemy.transform.position = enemyInfo[i].spawnPoint.position;
                    retrievedEnemy.gameObject.SetActive(true);
                    enemyCount++;
                }
                yield return new WaitForSeconds(spawnDelay);
            }
        }
    }

    public void OnEnemyDieHandler(int score, int count)
    {
        OnEnemyDie?.Invoke(score);
        enemyCount -= count;
        CheckEnemyCount();
    }

    public void CheckEnemyCount()
    {
        if (EnemyCount <= 0)
        {
            Victory?.Invoke();
        }
    }
}

[Serializable]
public class EnemyInfo
{
    public TypeEnemy typeEnemy;
    public EnemyController enemyPrefab;
    public Transform spawnPoint;
    public int poolCount;
}

public enum TypeEnemy
{
    Dron,
    AttackingDron,
    EnemyShip,
    AttackingEnemyShip
}

public class EnemyByType
{
    public TypeEnemy typeEnemy;
    public List<EnemyController> enemyControllers;

    public EnemyByType(TypeEnemy typeEnemy)
    {
        this.typeEnemy = typeEnemy;
        enemyControllers = new List<EnemyController>();
    }
}

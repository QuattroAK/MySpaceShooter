﻿using System.Collections.Generic;
using UnityEngine;

public class BulletsController : MonoBehaviour
{
    [Header("Bullet parameters")]
    [SerializeField] private BulletController shotPrefab;
    [SerializeField] private Transform[] shotSpawn;
    [SerializeField] private string tagDamagedObject;
    [SerializeField] private float deactivationTime;
    [SerializeField] private int countPoolObjects;

    private List<BulletController> activePoolShots;
    private Stack<BulletController> diactivePoolShots;

    public void Init(float baseDamage)
    {
        activePoolShots = new List<BulletController>();
        diactivePoolShots = new Stack<BulletController>();

        for (int i = 0; i < countPoolObjects; i++)
        {
            var bulletObject = Instantiate(shotPrefab, shotSpawn[0]);
            bulletObject.Init(tagDamagedObject, baseDamage, deactivationTime);
            bulletObject.OnDisabled += BulletDisableHandler;
            diactivePoolShots.Push(bulletObject);
        }
    }

    public void SpawnBullet(Transform parent)
    {
        for (int i = 0; i < shotSpawn.Length; i++)
        {
            var bullet = GetBullet();
            bullet.StartToMove(shotSpawn[i], parent);
        }
    }

    public void RefreshBullets()
    {
        for (int i = 0; i < activePoolShots.Count; i++)
        {
            activePoolShots[i].CheckLifetime();
        }
    }

    private BulletController GetBullet()
    {
        BulletController bullet;
        
        if (diactivePoolShots.Count != 0)
        {
            bullet = diactivePoolShots.Pop();
            activePoolShots.Add(bullet);
        }
        else
        {
            bullet = Instantiate(shotPrefab, shotSpawn[0]);
            activePoolShots.Add(bullet);
        }
        return bullet;
    }

    private void BulletDisableHandler(BulletController bullet)
    {
        activePoolShots.Remove(bullet);
        diactivePoolShots.Push(bullet);
    }
}
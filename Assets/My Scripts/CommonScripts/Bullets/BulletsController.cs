using System.Collections.Generic;
using UnityEngine;

public class BulletsController : MonoBehaviour
{
    [SerializeField] private BulletController shotPrefab;
    [SerializeField] private Transform[] shotSpawn;
    [SerializeField] private int countPoolObjects;
    [SerializeField] private float zBoundary;
    [SerializeField] private string tagDamagedObject;

    private List<BulletController> activePoolShots;
    private Stack<BulletController> diactivePoolShots;

    public void Init(float baseDamage)
    {
        activePoolShots = new List<BulletController>();
        diactivePoolShots = new Stack<BulletController>();

        for (int i = 0; i < countPoolObjects; i++)
        {
            var bulletObject = Instantiate(shotPrefab, shotSpawn[0]);
            bulletObject.Init(tagDamagedObject, baseDamage);
            bulletObject.OnDisabled += BulletDisableHandler;
            diactivePoolShots.Push(bulletObject);
        }
    }

    public void SpawnBullet()
    {
        for (int i = 0; i < shotSpawn.Length; i++)
        {
            var bullet = GetBullet();
            bullet.StartToMove(shotSpawn[i]);
        }
    }

    public void RefreshBullets()
    {
        for (int i = 0; i < activePoolShots.Count; i++)
        {
            activePoolShots[i].CheckPosition(zBoundary);
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
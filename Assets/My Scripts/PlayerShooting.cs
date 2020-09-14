using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private float nextFire;
    [SerializeField] private float fireRate;
    [SerializeField] private GameObject shot;
    [SerializeField] private List<GameObject> poolShots;
    [SerializeField] private Transform shotSpawn;
    [SerializeField] private int countPool;
    [SerializeField] private float zBoundary;

    public void Init()
    {
        CreatePoolShots();
    }

    public void Refresh()
    {
        Fire();
        //ReturnToPool();
    }

    private void Fire()
    {
        if (Input.GetButton("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            SoundController.Instance.PlayAudio(TypeAudio.GunShot);
            GameObject retrivedShot = GetObjectInPool();

            if(retrivedShot != null)
            {
                retrivedShot.gameObject.SetActive(true);
            }

            if(retrivedShot.transform.position.z > zBoundary)
            {
                retrivedShot.gameObject.SetActive(false);
            }
        }
    }

    public void CreatePoolShots()
    {
        poolShots = new List<GameObject>();

        for (int i = 0; i < countPool; i++)
        {
            GameObject objectShot = Instantiate(shot, shotSpawn.transform.position, Quaternion.identity, shotSpawn);
            objectShot.gameObject.SetActive(false);
            poolShots.Add(objectShot);
        }
    }

    public GameObject GetObjectInPool()
    {
        for (int i = 0; i < poolShots.Count; i++)
        {
            if(!poolShots[i].activeInHierarchy)
            {
                return poolShots[i];
            }
        }

        GameObject objectShot = Instantiate(shot, shotSpawn.transform.position, Quaternion.identity, shotSpawn);
        objectShot.gameObject.SetActive(false);
        poolShots.Add(objectShot);
        return objectShot; 
    }

    //public void ReturnToPool()
    //{
    //    if(retrivedShotPref != null)
    //    {
    //        if (retrivedShotPref.transform.position.z > zBoundary)
    //        {
    //            retrivedShotPref.SetActive(false);
    //        }
    //    }
    //}
}

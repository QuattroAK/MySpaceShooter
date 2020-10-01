using UnityEngine;

namespace MyGame
{
    public class WeaponController : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private GameObject shot;
        [SerializeField] private Transform shotSpawn;
        [SerializeField] private float fireRate;
        [SerializeField] private float delay;

        void Start()
        {
            InvokeRepeating("Fire", delay, fireRate);
        }

        void Update()
        {

        }

        private void Fire()
        {
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
            audioSource.Play();
        }
    }
}

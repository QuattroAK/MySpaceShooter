using System;
using UnityEngine;

namespace MyGame
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Rigidbody rb;
        [SerializeField] private float speed;
        [SerializeField] private float tilt;
        [SerializeField] Boundary boundary;
        [SerializeField] private GameObject shot;
        [SerializeField] private Transform shotSpawn;
        [SerializeField] private float fireRate;
        [SerializeField] private float nextFire;
        [SerializeField] private AudioSource audioShoot;

        private void Update()
        {
            if(Input.GetButton("Fire1") && Time.time > nextFire)
            {
                nextFire = Time.time + fireRate;
                Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
                audioShoot.Play();
                
            }
        }

        private void FixedUpdate()
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");

            Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
            rb.velocity = movement * speed;

            rb.position = new Vector3(Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax), 0.0f, Mathf.Clamp(rb.transform.position.z, boundary.zMin, boundary.zMax));
            rb.rotation = Quaternion.Euler(0.0f, 0.0f, rb.velocity.x * -tilt);
        }
    }
    [Serializable]
    public class Boundary
    {
        public float xMin, xMax, zMin, zMax;
    }
}

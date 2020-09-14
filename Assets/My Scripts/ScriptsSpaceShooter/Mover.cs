using UnityEngine;

namespace MyGame
{
    public class Mover : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private Rigidbody rb;

        void Start()
        {
            rb.velocity = transform.forward * speed;
        }

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log(other.name);
        }
    }
}

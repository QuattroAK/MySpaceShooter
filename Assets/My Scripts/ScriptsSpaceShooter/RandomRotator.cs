using UnityEngine;

namespace MyGame
{
    public class RandomRotator : MonoBehaviour
    {
        [SerializeField] private Rigidbody rb;
        
        public float tumble;
        void Start()
        {
            rb.angularVelocity = Random.insideUnitSphere * tumble;    
        }
    }
}

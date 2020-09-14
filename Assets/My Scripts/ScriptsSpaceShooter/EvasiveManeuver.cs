using System.Collections;
using UnityEngine;

namespace MyGame
{
    public class EvasiveManeuver : MonoBehaviour
    {
        [SerializeField] private float dodge; // уклонение
        [SerializeField] private float smoothing;
        [SerializeField] private float currentSpeed;
        [SerializeField] private float tilt; // наклон
        [SerializeField] private Vector2 startWait;
        [SerializeField] private Vector2 maneuverWait;
        [SerializeField] private Vector2 maneuverTime;
        [SerializeField] private Rigidbody rb;
        [SerializeField] private Boundary boundary; // границы

        private float targetManeuver;
        
        private void Start()
        {
            StartCoroutine(Evade());
            currentSpeed = rb.velocity.z;
        }

        private void FixedUpdate()
        {
            float newManeuver = Mathf.MoveTowards(rb.velocity.x, targetManeuver, Time.fixedDeltaTime * smoothing);
            rb.velocity = new Vector3(newManeuver, 0.0f, currentSpeed);
            rb.position = new Vector3(Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax), 0.0f, Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax));
            rb.rotation = Quaternion.Euler(0.0f, 0.0f, rb.velocity.x * -tilt);
        }

        private IEnumerator Evade()
        {
            yield return new WaitForSeconds(Random.Range(startWait.x, startWait.y));

            while (true)
            {
                targetManeuver = Random.Range(1, dodge) * -Mathf.Sign(transform.position.x);
                yield return new WaitForSeconds(Random.Range(maneuverWait.x, maneuverWait.y));
                targetManeuver = 0;
                yield return new WaitForSeconds(Random.Range(maneuverWait.x, maneuverWait.y));
            }
        }
    }
}

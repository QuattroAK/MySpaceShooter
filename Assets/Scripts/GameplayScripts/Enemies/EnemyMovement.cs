using System.Collections;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Rigidbody rb;

    private Transform playerTarget;

    public void Init(Transform playerTarget)
    {
        this.playerTarget = playerTarget;
    }

    public void Refresh()
    {
        Vector3 lookDirection = (playerTarget.position - transform.position).normalized;
        rb.AddForce(lookDirection * speed);
        transform.rotation = Quaternion.LookRotation(lookDirection);
    }

}

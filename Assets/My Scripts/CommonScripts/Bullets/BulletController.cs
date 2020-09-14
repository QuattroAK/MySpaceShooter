using UnityEngine;
using System;

public class BulletController : MonoBehaviour
{
    public event Action<BulletController> OnDisabled;
    
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float speed;

    private string tagDamagedObject;

    public void Init(string tagDamagedObject)
    {
        this.tagDamagedObject = tagDamagedObject;
    }

    public void StartToMove(Transform spawnTransform)
    {
        transform.parent = spawnTransform;
        transform.position = spawnTransform.position;
        transform.rotation = spawnTransform.rotation;

        rb.velocity = transform.forward * speed;

        gameObject.SetActive(true);
    }

    public void CheckPosition(float zBoundary)
    {
        if (transform.position.z > zBoundary)
        {
            Disable();
        }
    }

    private void Disable()
    {
        rb.velocity = Vector3.zero;
        gameObject.SetActive(false);

        OnDisabled?.Invoke(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(tagDamagedObject))
        {
            Disable();
            // TODO Add damage player (in future add abstact class)
            // TODO Add some VFX (additional)
        }
    }
}
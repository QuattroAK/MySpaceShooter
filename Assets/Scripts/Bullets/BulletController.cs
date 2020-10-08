using UnityEngine;
using System;

public class BulletController : MonoBehaviour
{
    public event Action<BulletController> OnDisabled;
    
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float speed;

    private string tagDamagedObject;
    private float damage;
    private float deactivationTime;
    private float localTime;

    public void Init(string tagDamagedObject, float baseDamage, float deactivationTime)
    {
        this.tagDamagedObject = tagDamagedObject;
        damage = baseDamage;
        this.deactivationTime = deactivationTime;
    }

    public void StartToMove(Transform spawnTransform, Transform parentObject)
    {
        localTime = 0;

        transform.parent = parentObject;
        transform.position = spawnTransform.position;
        transform.rotation = spawnTransform.rotation;

        rb.velocity = transform.forward * speed;
        gameObject.SetActive(true);
    }

    public void CheckLifetime()
    {
        localTime += Time.deltaTime;

        if (localTime >= deactivationTime)
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
            var healthComponent = other.GetComponent<ObjectHealth>();
            healthComponent.TakeDamage(damage);

            // TODO Add damage player (in future add abstact class)
            // TODO Add some VFX (additional)
        }
    }
}
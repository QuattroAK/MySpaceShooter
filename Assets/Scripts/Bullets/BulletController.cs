using UnityEngine;
using System;
using System.Collections;

public class BulletController : MonoBehaviour
{
    public event Action<BulletController> OnDisabled;
    
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float speed;

    private string tagDamagedObject;
    private float damage;

    public void Init(string tagDamagedObject, float baseDamage)
    {
        this.tagDamagedObject = tagDamagedObject;
        damage = baseDamage;
    }

    public void StartToMove(Transform spawnTransform, Transform parentObject)
    {
        transform.parent = parentObject;
        transform.position = spawnTransform.position;
        transform.rotation = spawnTransform.rotation;

        rb.velocity = transform.forward * speed;

        gameObject.SetActive(true);
    }

    public float LifeCheck(float timeLife, float deactivationTime)
    {
        if (timeLife > deactivationTime)
        {
            Disable();
            timeLife = 0;
            return timeLife;
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
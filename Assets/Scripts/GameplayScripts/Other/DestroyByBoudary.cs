using System.Collections;
using UnityEngine;

public class DestroyByBoudary : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(DestroyTimer());

            IEnumerator DestroyTimer()
            {
                for (int i = 10; i > 0; i--)
                {
                    Debug.Log($"You will be destroyed in {i} seconds");
                    yield return new WaitForSeconds(1);
                }
                other.gameObject.SetActive(false);
            }
        }
    }

}

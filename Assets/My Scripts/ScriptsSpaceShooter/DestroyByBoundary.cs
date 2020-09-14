using UnityEngine;

    public class DestroyByBoundary : MonoBehaviour
    {
        private void OnTriggerExit(Collider other)
        {
        other.gameObject.SetActive(false);
        }
    }


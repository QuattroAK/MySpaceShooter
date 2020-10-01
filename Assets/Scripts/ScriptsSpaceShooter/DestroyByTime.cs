using UnityEngine;

namespace MyGame
{
    public class DestroyByTime : MonoBehaviour
    {
        [SerializeField] private float timeDestroy;

        void Start()
        {
            Destroy(gameObject, timeDestroy);
        }

    }
}

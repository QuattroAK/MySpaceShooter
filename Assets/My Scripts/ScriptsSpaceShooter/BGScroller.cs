﻿using UnityEngine;

namespace MyGame
{
    public class BGScroller : MonoBehaviour
    {
        [SerializeField] private float scrollSpeed;
        [SerializeField] private float tileSizeZ;

        private Vector3 startPosition;

        void Start()
        {
            startPosition = transform.position;
        }

        void Update()
        {
            float newPosition = Mathf.Repeat(Time.time * scrollSpeed, tileSizeZ);
            transform.position = startPosition + Vector3.forward * newPosition;
        }
    }
}

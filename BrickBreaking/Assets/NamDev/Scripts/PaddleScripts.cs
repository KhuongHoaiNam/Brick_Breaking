using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NamDev.BrickBreak
{
    public class PaddleScripts : MonoBehaviour
    {
        [SerializeField] float speed = 10f;
        [SerializeField] float screenEdge = 7f;

        void Update()
        {
            float moveInput = Input.GetAxis("Horizontal");
            Vector2 newPosition = transform.position + Vector3.right * moveInput * speed * Time.deltaTime;
            newPosition.x = Mathf.Clamp(newPosition.x, -screenEdge, screenEdge);
            transform.position = newPosition;
        }
    }
}
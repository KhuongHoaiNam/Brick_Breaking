using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NamDev.BrickBreak
{
    public class PaddleScripts : MonoBehaviour
    {
        [SerializeField] float m_speed = 10f;
        [SerializeField] float m_screenEdge = 2f; //man hinh 
        [SerializeField] private BallScripts m_ball;

        void Update()
        {
            float moveInput = Input.GetAxis("Horizontal");
            Vector2 newPosition = transform.position + Vector3.right * moveInput * m_speed * Time.deltaTime;
            newPosition.x = Mathf.Clamp(newPosition.x, -m_screenEdge, m_screenEdge);// vi tri duoc phep di chuyen trong khoang (-2f, 2f)
            transform.position = newPosition;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag(GameTag.Ball.ToString()))
            {
                BallScripts ball = collision.gameObject.GetComponent<BallScripts>();
                if (ball != null)
                {
                    if (ball.CurrentSpeed > 3f && ball.IsMoving == true)
                    {
                        ball.CurrentSpeed = 12f;

                    }
                    else
                    {
                        ball.transform.SetParent(this.transform);
                    }
                }

            }
        }
    
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag(GameTag.Ball.ToString()))
            {
                BallScripts ball = collision.gameObject.GetComponent<BallScripts>();
                if (ball != null)
                {
                    ball.transform.SetParent(null);
                }

            }
        }
    }
}
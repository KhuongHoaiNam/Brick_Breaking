using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NamDev.BrickBreak
{
    public class BallScripts : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private float initialSpeed = 5f;
        [SerializeField] private float minVerticalSpeed = 0.5f;
        [SerializeField] private LayerMask wallLayer;

        private Rigidbody2D rb;
        private Vector2 lastVelocity;

        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            LaunchBall();
        }

        void Update()
        {
            lastVelocity = rb.velocity; // Lưu lại vận tốc trước khi va chạm
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            // Kiểm tra va chạm với tường bằng Layer thay vì Tag
            if (collision.gameObject.CompareTag(GameTag.Paddle.ToString()))
            {
                HandleWallCollision(collision);
            }
        }

        private void LaunchBall()
        {
            // Khởi tạo hướng bay ngẫu nhiên
            Vector2 randomDirection = new Vector2(Random.Range(-1f, 1f), 1f).normalized;
            rb.velocity = randomDirection * initialSpeed;
        }

        private void HandleWallCollision(Collision2D collision)
        {
            // Lấy điểm va chạm đầu tiên và pháp tuyến
            ContactPoint2D contact = collision.GetContact(0);
            Vector2 normal = contact.normal;

            // Tính hướng mới với Reflect và giữ nguyên tốc độ
            Vector2 reflectedDirection = Vector2.Reflect(lastVelocity.normalized, normal);
            rb.velocity = reflectedDirection * initialSpeed;

            // Đảm bảo không di chuyển hoàn toàn ngang
            PreventHorizontalTrajectory();
        }

        private void PreventHorizontalTrajectory()
        {
            Vector2 currentVelocity = rb.velocity;

            if (Mathf.Abs(currentVelocity.y) < minVerticalSpeed)
            {
                // Thêm thành phần dọc ngẫu nhiên
                float newY = Mathf.Sign(currentVelocity.y) * minVerticalSpeed;
                float newX = currentVelocity.x + Random.Range(-0.2f, 0.2f);

                rb.velocity = new Vector2(newX, newY).normalized * initialSpeed;
            }
        }

        // Hàm debug vận tốc
        void OnDrawGizmos()
        {
            if (Application.isPlaying && rb != null)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawRay(transform.position, rb.velocity.normalized * 2f);
            }
        }
    }
}
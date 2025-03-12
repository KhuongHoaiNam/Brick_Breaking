using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace NamDev.BrickBreak
{
    public class BallScripts : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private float m_maxspeed = 5f;
        [SerializeField] private float m_minVerticalSpeed = 0.5f;
        [SerializeField] private float m_currentSpeed;
        [SerializeField] private bool m_isMoving;
        [SerializeField] private float m_speedRate;

        [SerializeField] private Transform m_spawnerPosition;
        [SerializeField] private LayerMask m_wallLayer;

        [SerializeField] private Rigidbody2D m_rb;
        [SerializeField] private Vector2 m_lastVelocity;
        private Vector3 m_dir;
        [SerializeField] private GameObject m_arrowDir;

        public float CurrentSpeed { get => m_currentSpeed; set => m_currentSpeed = value; }
        public bool IsMoving { get => m_isMoving; set => m_isMoving = value; }

        void Start()
        {
            m_rb = GetComponent<Rigidbody2D>();
        }

        void Update()
        {
            m_lastVelocity = m_rb.velocity; // Lưu lại vận tốc trước khi va chạm

            if (Input.GetMouseButton(0))
            {
                m_arrowDir.gameObject.SetActive(true);

                Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousePosition.z = 0f;

                Vector3 direction = mousePosition - this.transform.position;
                direction = Vector3.Normalize(direction);

                m_dir = direction;

                // Tính góc ban đầu (từ -180° đến 180°)
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

                // Chuẩn hóa góc về khoảng [-180, 180]
                angle = Mathf.Repeat(angle + 180f, 360f) - 180f;

                // Giới hạn góc trong khoảng [-95°, 95°]
                angle = Mathf.Clamp(angle, 5f, 180f);

                // Áp dụng góc xoay
                this.transform.rotation = Quaternion.Euler(0, 0, angle - 90f);
            }

            if (Input.GetMouseButtonUp(0) && m_isMoving == false)
            {
                m_arrowDir.gameObject.SetActive(false);
                LaunchBall(m_dir);


            }
            if (m_isMoving)
            {
                m_currentSpeed -= m_speedRate * Time.deltaTime;

                if (m_isMoving == true && m_currentSpeed < 3f)
                {
                    ResetBall();
                }
            }
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            // Kiểm tra va chạm với tường bằng Layer thay vì Tag
            if (collision.gameObject.CompareTag(GameTag.Paddle.ToString()) && m_isMoving == true)
            {
                HandleWallCollision(collision);
            }
            else if (collision.gameObject.CompareTag(GameTag.Death.ToString()) && m_isMoving == true)
            {
                ResetBall();
            }
        }

        public void ResetBall()
        {
            this.transform.position = m_spawnerPosition.position;
            m_isMoving = false;
            m_rb.velocity = Vector2.zero;
        }

        private void LaunchBall(Vector3 dir)
        {
            //  Vector2 randomDirection = new Vector2(Random.Range(-1f, 1f), 1f).normalized;
            m_currentSpeed = m_maxspeed;
            m_rb.velocity = dir.normalized * m_currentSpeed;
            m_isMoving = true;
        }

        private void HandleWallCollision(Collision2D collision)
        {
            // Lấy điểm va chạm đầu tiên và pháp tuyến
            ContactPoint2D contact = collision.GetContact(0);
            Vector2 normal = contact.normal;

            // Tính hướng mới với Reflect và giữ nguyên tốc độ
            Vector2 reflectedDirection = Vector2.Reflect(m_lastVelocity.normalized, normal);// lấy ra hướng phản xạ cho quả bong
            m_rb.velocity = reflectedDirection * m_currentSpeed;

            // Đảm bảo không di chuyển hoàn toàn ngang
            PreventHorizontalTrajectory();
        }

        private void PreventHorizontalTrajectory()
        {
            Vector2 currentVelocity = m_rb.velocity;

            if (Mathf.Abs(currentVelocity.y) < m_minVerticalSpeed)
            {
                // Thêm thành phần dọc ngẫu nhiên

                float newY = Mathf.Sign(currentVelocity.y) * m_minVerticalSpeed;
                float newX = currentVelocity.x + Random.Range(-0.2f, 0.2f);

                m_rb.velocity = new Vector2(newX, newY).normalized * m_currentSpeed;
            }
        }



        // Hàm debug vận tốc
        void OnDrawGizmos()
        {
            if (Application.isPlaying && m_rb != null)
            {
                Gizmos.color = Color.red;
                Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector3 direction = mousePosition - this.transform.position;
                //    direction = Vector3.Normalize(direction);
                //LaunchBall(direction);
                Gizmos.DrawRay(transform.position, direction.normalized * 2f);
            }
        }

    }
}
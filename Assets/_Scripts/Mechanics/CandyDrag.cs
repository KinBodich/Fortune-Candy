using Common;
using Common.Managers;
using UnityEngine;

namespace Mechanics
{
    public class CandyDrag : MonoBehaviour
    {
        [field: SerializeField] public CandyType CandyType { get; private set; }

        public bool IsDragging { get; private set; } = false;
        private Vector2 _offset;
        private Rigidbody2D _rb;
        private GameManager _gameManager;

        void Start()
        {
            _gameManager = GameManager.Instance;
            _rb = GetComponent<Rigidbody2D>();
        }

        void FixedUpdate()
        {
            if (_gameManager.GameState != GameState.Playing) return;
            if (IsDragging && (Input.touchCount > 0 || Input.GetMouseButton(0)))
            {
                Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 newPosition = mousePosition + _offset;

                // Check if the new position collides with any obstacles
                Collider2D[] colliders = Physics2D.OverlapCircleAll(newPosition, 0.1f);
                bool collidedWithObstacle = false;
                foreach (Collider2D collider in colliders)
                {
                    if (collider.CompareTag("Obstacle"))
                    {
                        collidedWithObstacle = true;
                        break;
                    }
                }

                // Move the candy only if it doesn't collide with any obstacle
                if (!collidedWithObstacle)
                {
                    _rb.MovePosition(newPosition);
                }
                else
                {
                    IsDragging = false; // Set isDragging to false if collided with obstacle while dragging
                }
            }
        }

        private void Update()
        {
            if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) || Input.GetMouseButtonDown(0))
            {
                Vector2 inputPosition = (Input.touchCount > 0) ? Input.GetTouch(0).position : (Vector2)Input.mousePosition;
                Vector2 touchPosition = Camera.main.ScreenToWorldPoint(inputPosition);

                RaycastHit2D hit = Physics2D.Raycast(touchPosition, Vector2.zero);
                if (hit.collider != null && hit.collider.gameObject == gameObject)
                {
                    IsDragging = true;
                    _offset = (Vector2)transform.position - touchPosition;
                    _rb.velocity = Vector2.zero;
                    _rb.angularVelocity = 0f;
                }
            }

            if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended) || Input.GetMouseButtonUp(0))
            {
                IsDragging = false;
            }
        }
    }
}

using UnityEngine;
namespace DonkeyKong
{
    public class Player : MonoBehaviour
    {
        private SpriteRenderer spriteRenderer;
        [SerializeField] Sprite[] runSprites;
        [SerializeField] Sprite climbSprite;
        private int spriteIndex;

        private new Rigidbody2D rigidbody;
        private new Collider2D collider;

        private Collider2D[] overlaps = new Collider2D[4];
        private Vector2 direction;

        private bool grounded;
        private bool climbing;

        [SerializeField] float moveSpeed = 3f;
        [SerializeField] float jumpStrength = 4f;
        private void Awake()
        {
            AwakeRef();
        }
        private void OnEnable()
        {
            InvokeRepeating(nameof(AnimateSprite), 1f / 12f, 1f / 12f);
        }
        private void OnDisable()
        {
            CancelInvoke();
        }
        private void Update()
        {
            CheckCollision();
            SetDirection();
        }
        private void CheckCollision()
        {
            grounded = false;
            climbing = false;

            Vector3 size = collider.bounds.size;
            size.y += 0.1f;
            size.x /= 2f;

            int amount = Physics2D.OverlapBoxNonAlloc(transform.position, size, 0, overlaps);

            for (int i = 0; i < amount; i++)
            {
                GameObject hit = overlaps[i].gameObject;

                if (hit.layer == LayerMask.NameToLayer("Ground"))
                {
                    grounded = hit.transform.position.y < (transform.position.y - 0.5f);

                    Physics2D.IgnoreCollision(overlaps[i], collider, !grounded);
                }
                else if (hit.layer == LayerMask.NameToLayer("Ladder"))
                    climbing = true;
            }
        }
        private void SetDirection()
        {
            if (climbing)
                direction.y = Input.GetAxis("Vertical") * moveSpeed;
            else if (grounded && Input.GetButtonDown("Jump"))
                direction = Vector2.up * jumpStrength;
            else
                direction += Physics2D.gravity * Time.deltaTime;

            direction.x = Input.GetAxis("Horizontal") * moveSpeed;

            // Prevent gravity from building up infinitely
            if (grounded)
                direction.y = Mathf.Max(direction.y, -1f);

            if (direction.x > 0f)
                transform.eulerAngles = Vector3.zero;
            else if (direction.x < 0f)
                transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }

        private void FixedUpdate()
        {
            rigidbody.MovePosition(rigidbody.position + direction * Time.fixedDeltaTime);
        }

        private void AnimateSprite()
        {
            if (climbing)
                spriteRenderer.sprite = climbSprite;
            else if (direction.x != 0f)
            {
                spriteIndex++;
                if (spriteIndex >= runSprites.Length)
                {
                    spriteIndex = 0;
                }
                spriteRenderer.sprite = runSprites[spriteIndex];
            }
            else
                spriteRenderer.sprite = runSprites[0];
        }
        void AwakeRef()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            rigidbody = GetComponent<Rigidbody2D>();
            collider = GetComponent<Collider2D>();
        }
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Objective"))
            {
                enabled = false;
                FindObjectOfType<GameManager>().LevelComplete();
            }
            else if (collision.gameObject.CompareTag("Obstacle"))
            {
                enabled = false;
                FindObjectOfType<GameManager>().LevelFailed();
            }
        }
    }
}
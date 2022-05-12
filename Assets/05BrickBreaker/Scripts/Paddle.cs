using UnityEngine;

namespace BrickBreaker
{
    public class Paddle : MonoBehaviour
    {
        public Rigidbody2D _rb { get; private set; }
        public Vector2 dir { get; private set; }
        [SerializeField] float speed = 30f;
        [SerializeField] float maxBounceAngle = 75;
        private void Awake()
        {
            this._rb = GetComponent<Rigidbody2D>();
        }
        private void Update()
        {
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
                this.dir = Vector2.left;
            else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
                this.dir = Vector2.right;
            else
                this.dir = Vector2.zero;
        }
        private void FixedUpdate()
        {
            if (dir != Vector2.zero)
            {
                _rb.AddForce(this.dir * speed);
            }
        }
        private void OnCollisionEnter2D(Collision2D other)
        {
            Ball ball = other.gameObject.GetComponent<Ball>();

            if (ball != null)
            {
                //paddle'in yerini aliyoruz.
                Vector2 paddlePos = this.transform.position;
                //top ile temas ettigi yeri aliyoruz.
                Vector2 contactPoint = other.GetContact(0).point;
                float offset = paddlePos.x - contactPoint.x;
                //paddle'in toplam uzunlugunun yarisi bize yetiyor 
                //cunku sola dogru ve saga dogru olmak uzere 2 kisma ayiriyoruz
                float width = other.otherCollider.bounds.size.x / 2;
                //topun gelis acisini aliyoruz
                float currentAngle = Vector2.SignedAngle(Vector2.up, ball.GetComponent<Rigidbody>().velocity);
                //topun temas ettigi yere gore aci eklemesi yapiyoruz.
                float bounceAngle = (offset / width) * maxBounceAngle;
                //topun gidecegiz yonun acisini tayin ediyoruz.
                float newAngle = Mathf.Clamp(currentAngle + bounceAngle, -maxBounceAngle, maxBounceAngle);
                Quaternion rotation = Quaternion.AngleAxis(newAngle, Vector3.forward);
                ball.rb.velocity = rotation * Vector2.up * ball.rb.velocity.magnitude;
            }
        }
    }
}
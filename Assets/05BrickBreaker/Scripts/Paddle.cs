using UnityEngine;

namespace BrickBreaker
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Paddle : MonoBehaviour
    {
        public  Rigidbody2D Rigidbody { get; private set; }
        public Vector2 Dir { get; private set; }
        [SerializeField] float speed = 30f;
        [SerializeField] float maxBounceAngle = 75;
        float hiz;
        private void Awake()
        {
            Rigidbody = GetComponent<Rigidbody2D>();
        }
        public void ResetPaddle()
        {
            this.transform.position = new Vector2(0, this.transform.position.y);
            this.Rigidbody.velocity = Vector2.zero;

        }
        private void Update()
        {
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
                Dir = Vector2.left;
            else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
                Dir = Vector2.right;
            else
                Dir = Vector2.zero;
        }
        private void FixedUpdate()
        {
            if (Dir != Vector2.zero)
            {
                Rigidbody.AddForce(Dir * speed);
            }
        }
        private void OnCollisionEnter2D(Collision2D other)
        {
            Ball ball = other.gameObject.GetComponent<Ball>();

            if (ball != null)
            {
                //paddle'in yerini aliyoruz.
                Vector2 paddlePos = transform.position;
                //top ile temas ettigi yeri aliyoruz.
                Vector2 contactPoint = other.GetContact(0).point;
                float offset = paddlePos.x - contactPoint.x;
                //paddle'in toplam uzunlugunun yarisi bize yetiyor 
                //cunku sola dogru ve saga dogru olmak uzere 2 kisma ayiriyoruz
                float width = other.otherCollider.bounds.size.x / 2;
                //topun gelis acisini aliyoruz
                float currentAngle = Vector2.SignedAngle(Vector2.up, ball.Rb.velocity);
                //topun temas ettigi yere gore aci eklemesi yapiyoruz.
                float bounceAngle = (offset / width) * maxBounceAngle;
                //topun gidecegiz yonun acisini tayin ediyoruz.
                float newAngle = Mathf.Clamp(currentAngle + bounceAngle, -maxBounceAngle, maxBounceAngle);
                Quaternion rotation = Quaternion.AngleAxis(newAngle, Vector3.forward);
                //gelen topun hizini degistirmek istemiyoruz.
                ball.Rb.velocity = rotation * Vector2.up * ball.Rb.velocity.magnitude;
            }
        }
    }
}
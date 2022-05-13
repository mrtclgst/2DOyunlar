using UnityEngine;

namespace BrickBreaker
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Ball : MonoBehaviour
    {
        public Rigidbody2D Rb { get; private set; }
        [SerializeField] float speed = 500f;
        private void Awake()
        {
            Rb = GetComponent<Rigidbody2D>();
        }
        private void Start()
        {
            //baslangicta topa asagi dogru kuvvet uyguluyoruz.
            ResetBall();
        }
        void SetRandomTrajectory()
        {
            Vector2 force = Vector2.zero;
            force.x = Random.Range(-1f, 1f);
            force.y = -1f;

            Rb.AddForce(force.normalized * speed);
        }
        public void ResetBall()
        {
            this.transform.position= new Vector2(0, -3f);
            this.Rb.velocity = Vector2.zero;

            Invoke("SetRandomTrajectory", 1f);
        }
    }
}
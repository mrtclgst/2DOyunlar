using UnityEngine;

namespace BrickBreaker
{
    public class Ball : MonoBehaviour
    {
        public Rigidbody2D rb { get; private set; }
        [SerializeField] float speed = 500f;
        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }
        private void Start()
        {
            //baslangicta topa asagi dogru kuvvet uyguluyoruz.
            Invoke("SetRandomTrajectory", 1f);
        }
        void SetRandomTrajectory()
        {
            Vector2 force = Vector2.zero;
            force.x = Random.Range(-1f, 1f);
            force.y = -1f;

            rb.AddForce(force.normalized * speed);
        }
    }
}
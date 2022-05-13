using System;
using UnityEngine;

namespace BrickBreaker
{
    public class Brick : MonoBehaviour
    {
        public SpriteRenderer SpriteRenderer { get; private set; }
        [SerializeField] Sprite[] states;
        public int Health { get; private set; }
        public bool unbreakable;
        public int point = 100;
        private void Awake()
        {
            this.SpriteRenderer = GetComponent<SpriteRenderer>();
        }
        private void Start()
        {
            if (!this.unbreakable)
            {
                Health = states.Length;
                //dizi indeksi 0 dan basladigi icin bir dusurduk.
                this.SpriteRenderer.sprite = this.states[this.Health - 1];
            }
        }
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.name == "Ball")
            {
                Hit();
            }
            FindObjectOfType<GameManager>().Hit(this);
        }

        private void Hit()
        {
            if (unbreakable)
                return;

            this.Health--;
            if (Health <= 0)
            {
                Destroy(this.gameObject);
            }
            else
            {
                this.SpriteRenderer.sprite = this.states[this.Health - 1];
            }
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DoodleJump
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Player : MonoBehaviour
    {
        [SerializeField] float movementSpeed = 0f;
        float movement;
        Rigidbody2D rb;
        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }
        private void Update()
        {
            movement = Input.GetAxis("Horizontal") * movementSpeed;
        }
        private void FixedUpdate()
        {
            Vector2 velocity = rb.velocity;
            velocity.x = movement;
            rb.velocity = velocity;
        }
    }
}
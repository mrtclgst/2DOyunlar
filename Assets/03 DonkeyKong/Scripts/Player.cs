using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DonkeyKong
{
    public class Player : MonoBehaviour
    {
        Rigidbody2D rg;
        bool jumped, isOnGround;
        Vector2 direction;
        [SerializeField] float moveSpeed, jumpForce;
        private void Awake()
        {
            AwakeRef();
        }
        private void Update()
        {
            CheckCollision();
            if (Input.GetButtonDown("Jump")) direction = Vector2.up * jumpForce;
            else direction += Physics2D.gravity * Time.deltaTime;
            //karakterimizi aasgi cekmek icin gravity ekliyoruz.

            direction.x = Input.GetAxis("Horizontal") * moveSpeed;
            direction.y = MathF.Max(direction.y, -1f);
            //gravity -9.81 diye gectiginden yukari gitmemek icin -1e kadar sinirli tuttuk.

            if (direction.x > 0f) transform.eulerAngles = Vector3.zero;
            else if (direction.x < 0f) transform.eulerAngles = Vector3.up * 180;
        }
        private void FixedUpdate()
        {
            rg.MovePosition(rg.position + direction * Time.fixedDeltaTime);
        }
        private void AwakeRef()
        {
            rg = GetComponent<Rigidbody2D>();
        }
        void CheckCollision()
        {

        }
    }
}


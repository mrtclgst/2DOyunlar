using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DoodleJump
{
    public class PlatformScript : MonoBehaviour
    {
        [SerializeField] float jumpForce = 5f;
        private void OnCollisionEnter2D(Collision2D other)
        {
            //colliderla ilk temasta kuvvet uygulamamak icin kullaniyoruz.
            if (other.relativeVelocity.y <= 0)//bagil hiz.
            {
                Rigidbody2D rb = other.collider.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    //addforce ile yaptigizda yukaridan gelen cismin kuvveti ile
                    //bizim kuvvetimiz birbirini sonumleyecegi icin 
                    //kuvvet olmayan velocity'i degistirdik.
                    Vector2 velocity = rb.velocity;
                    velocity.y = jumpForce;
                    rb.velocity = velocity;
                }
            }
        }
    }
}
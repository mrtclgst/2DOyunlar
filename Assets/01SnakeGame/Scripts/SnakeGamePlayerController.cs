using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeGamePlayerController : MonoBehaviour
{
    Vector2 _direction = Vector2.zero;
    private void Update() {
     if (Input.GetKeyDown(KeyCode.W))
     {
         _direction=Vector2.up;
     } 
     if (Input.GetKeyDown(KeyCode.S))
     {
         _direction=Vector2.down;
     } 
     if (Input.GetKeyDown(KeyCode.A))
     {
         _direction=Vector2.left;
     } 
     if (Input.GetKeyDown(KeyCode.D))
     {
         _direction=Vector2.right;
     }
}
float fixedDeltaTime;
private void Awake() {
    this.fixedDeltaTime=Time.fixedDeltaTime;
}
    private void FixedUpdate() {
        Time.fixedDeltaTime=this.fixedDeltaTime*5;
        transform.position=new Vector3(
            Mathf.Round(transform.position.x)+_direction.x,
            Mathf.Round(transform.position.y)+_direction.y
            ,0)
        ;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
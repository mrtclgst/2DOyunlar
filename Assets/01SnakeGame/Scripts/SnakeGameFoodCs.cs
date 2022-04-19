using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeGameFoodCs : MonoBehaviour
{
    [SerializeField] BoxCollider2D _gridArea;
    private void Start() {
        RandomizePosition();
    }
    private void OnTriggerEnter2D(Collider2D other) {
            RandomizePosition();
    }
    void RandomizePosition(){
        Bounds bounds=_gridArea.bounds;
        float x=Random.Range(bounds.min.x,bounds.max.x);
        float y=Random.Range(bounds.min.y,bounds.max.y);
        transform.position=new Vector3( Mathf.Round(x),Mathf.Round(y));
    }



}
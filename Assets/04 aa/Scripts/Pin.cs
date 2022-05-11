using UnityEngine;

namespace aa
{
    public class Pin : MonoBehaviour
    {
        private bool isPinned = false;
        [SerializeField] float speed = 20f;
        [SerializeField] Rigidbody2D rb;
        private void Update()
        {
            if (!isPinned)
                //transform.pos vector3 veriyor
                //biz vector 2 veren rb.position'u kullaniyoruz.
                rb.MovePosition(rb.position + speed * Time.deltaTime * Vector2.up);
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Rotator")
            {
                transform.SetParent(collision.transform);
                //collision.GetComponent<Rotater>().speed *= -1.1f;
                Score.pinCount++;
                isPinned = true;
            }
            else if (collision.tag=="Pin")
            {
                FindObjectOfType<GameManager>().EndGame();
            }
        }
    }
}
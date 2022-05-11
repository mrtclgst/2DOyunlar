using UnityEngine;

namespace DoodleJump
{
    public class Destroy : MonoBehaviour
    {
        [SerializeField] GameObject platform, player;
        GameObject myPlat;
        private void OnTriggerEnter2D(Collider2D collision)
        {
            Debug.Log("carpti");
            myPlat = Instantiate(platform,
             new Vector3(Random.Range(-3, +3)
             , player.transform.position.y + (14f + Random.Range(0.5f, 1))), Quaternion.identity);
            Destroy(collision.gameObject);
        }
    }
}
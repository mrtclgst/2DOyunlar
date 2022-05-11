using UnityEngine;

namespace DoodleJump
{
    public class CameraFollowScript : MonoBehaviour
    {
        [SerializeField] Transform player;
        float smoothSpeed = 0.3f;
        Vector3 currentVelocity;
        private void LateUpdate()
        {
            if (player.position.y > transform.position.y)
            {
                Vector3 newPos = new Vector3
                (transform.position.x, player.position.y, transform.position.z);
                //kamera gecisini yumusattigimiz yer.
                //transform.position = Vector3.Lerp(transform.position, newPos, smoothSpeed);
                transform.position =
                Vector3.SmoothDamp(transform.position, newPos, ref currentVelocity, smoothSpeed * Time.deltaTime);
            }
        }
    }
}
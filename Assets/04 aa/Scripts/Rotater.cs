using UnityEngine;

namespace aa
{
    public class Rotater : MonoBehaviour
    {
        public float speed = 100f;
        private void Update()
        {
            transform.Rotate(0, 0, speed * Time.deltaTime);
        }
    }
}
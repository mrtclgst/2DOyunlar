using UnityEngine;
namespace DonkeyKong
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] GameObject prefab;
        [SerializeField] float minTime = 2f;
        [SerializeField] float maxTime = 4f;
        private void Start()
        {
            Spawn();
        }
        private void Spawn()
        {
            Instantiate(prefab, transform.position, Quaternion.identity);
            Invoke(nameof(Spawn), Random.Range(minTime, maxTime));
        }
    }
}
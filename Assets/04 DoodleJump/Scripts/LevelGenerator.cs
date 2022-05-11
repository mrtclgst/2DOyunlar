using UnityEngine;

namespace DoodleJump
{
    public class LevelGenerator : MonoBehaviour
    {
        [SerializeField] GameObject platformPrefab;
        [SerializeField] int numberOfPlatform;
        [SerializeField] float levelWidth = 3f, minY = 0.2f, maxY = 1.5f;
        void Start()
        {
            Vector3 spawnPos = new Vector3();
            for (int i = 0; i < numberOfPlatform; i++)
            {
                spawnPos.y += Random.Range(minY, maxY);
                spawnPos.x = Random.Range(-levelWidth, levelWidth);
                Instantiate(platformPrefab, spawnPos, Quaternion.identity);
            }
        }
        void Update()
        {

        }
    }
}
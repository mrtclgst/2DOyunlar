using UnityEngine;
using UnityEngine.SceneManagement;

namespace BrickBreaker
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] int score = 0, lives = 3, level = 1;

        private void Awake()
        {
            DontDestroyOnLoad(this.gameObject);
        }
        private void Start()
        {
            NewGame();
        }
        private void NewGame()
        {
            this.score = 0;
            this.lives = 3;
            LoadLevel(1);
        }
        void LoadLevel(int levelIndex)
        {
            this.level = levelIndex;
            SceneManager.LoadScene("Level" + levelIndex);
        }
    }
}
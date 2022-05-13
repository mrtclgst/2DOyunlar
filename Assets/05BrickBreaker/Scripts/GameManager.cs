using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BrickBreaker
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] int score = 0, lives = 3, level = 1;
        public Ball ball { get; private set; }
        public Paddle paddle { get; private set; }
        public Brick[] bricks { get; private set; }
        int bricksayi;


        private void Awake()
        {
            DontDestroyOnLoad(this.gameObject);
            //paddle ile top sahne yuklendikten sonra geldigi icin event yazdik.
            SceneManager.sceneLoaded += OnLevelLoaded;

        }
        private void Start()
        {
            NewGame();
        }
        void OnLevelLoaded(Scene scene, LoadSceneMode loadSceneMode)
        {
            this.ball = FindObjectOfType<Ball>();
            this.paddle = FindObjectOfType<Paddle>();
            this.bricks = FindObjectsOfType<Brick>();
            bricksayi = this.bricks.Length;
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
        private bool Cleared()
        {
            for (int i = 0; i < bricksayi; i++)
            {
                if (bricks[i] != null)
                {
                    return false;
                }
            }
            return true;
        }
        public void Hit(Brick brick)
        {
            this.score += brick.point;
            if (Cleared())
            {
                LoadLevel(this.level + 1);
            }
        }
        public void Miss()
        {
            this.lives--;
            if (this.lives > 0)
            {
                ResetLevel();
            }
            else
            {
                GameOver();
            }
        }
        private void ResetLevel()
        {
            this.ball.ResetBall();
            this.paddle.ResetPaddle();
        }
        private void GameOver()
        {
            //SceneManager.LoadScene("GameOverScene");
            NewGame();
        }
        
    }
}
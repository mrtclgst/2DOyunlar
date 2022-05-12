using UnityEngine;
using UnityEngine.SceneManagement;

namespace aa
{
    public class GameManager : MonoBehaviour
    {
        private bool gameEnded = false;
        [SerializeField] Rotater rotater;
        [SerializeField] Spawner spawner;
        [SerializeField] Animator animator;
        public void EndGame()
        {
            if (gameEnded)
                return;

            rotater.enabled = false;
            spawner.enabled = false;

            animator.SetTrigger("EndGame");

            gameEnded = true;
        }
        public void RestartLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
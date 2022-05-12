using UnityEngine;
using TMPro;

namespace aa
{
    public class Score : MonoBehaviour
    {
        public static int pinCount=0;
        public TextMeshProUGUI text;
        private void Start()
        {
            pinCount = 0;
        }
        private void Update()
        {
            text.text = pinCount.ToString();
        }
    }
}
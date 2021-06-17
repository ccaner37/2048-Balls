using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace GleamGames.Balls2048.Managers
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField]
        private Text counter2048Text;
        [SerializeField]
        private Text failedText;
        [SerializeField]
        private Text currentLevelText, nextLevelText;

        [SerializeField]
        private Image fillBar;

        private int counter2048 = 0;

        [Inject]
        private LevelManager levelManager;

        public void UpdateCounterText()
        {
            counter2048++;
            counter2048Text.text = "2048 Counter: " + counter2048;
        }

        public void EnableFailedText()
        {
            failedText.gameObject.SetActive(true);
        }

        public void UpdateLevelBar()
        {
            float value = levelManager.ExperienceRequired / levelManager.NextLevelExpDifference;
            fillBar.fillAmount = value;
        }
        
        public void UpdateLevelText()
        {
            currentLevelText.text = levelManager.Level.ToString();
            nextLevelText.text = (levelManager.Level + 1).ToString();
        }
    }
}

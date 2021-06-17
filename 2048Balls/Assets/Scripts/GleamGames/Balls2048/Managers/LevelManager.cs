using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace GleamGames.Balls2048.Managers
{
    public class LevelManager : MonoBehaviour
    {
        public float Level = 1f;
        public float ExperienceRequired;
        public float NextLevelExpDifference = 100f;

        private float experience;
        private float goalExperience = 50f;
        private float levelMultiplier = 100f;
        private float experienceMultiplier = 0.3f;

        [Inject]
        private UIManager uiManager;

        public void GiveExperience(int score)
        {
            experience += score;
            ExperienceRequired = goalExperience - experience;
            uiManager.UpdateLevelBar();

            if (experience >= goalExperience)
            {
                Level++;
                goalExperience = Level * levelMultiplier * (Level * experienceMultiplier);
                ExperienceRequired = goalExperience - experience;

                float previousLevelGoal = (Level - 1) * levelMultiplier * ((Level - 1) * experienceMultiplier);

                NextLevelExpDifference = goalExperience - previousLevelGoal;

                uiManager.UpdateLevelText();
            }
        }
    }
}

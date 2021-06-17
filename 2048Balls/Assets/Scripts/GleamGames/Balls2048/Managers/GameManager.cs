using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GleamGames.Balls2048.Managers
{
    public class GameManager : MonoBehaviour
    {
        public void StopGame()
        {
            Time.timeScale = 0;
        }
    }
}

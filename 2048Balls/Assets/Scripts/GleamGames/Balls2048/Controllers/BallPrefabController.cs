using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GleamGames.Balls2048.Controllers
{
    public class BallPrefabController : MonoBehaviour
    {
        [System.Serializable]
        public class BallData
        {
            public int Score;
            public Transform Ball;
        }
        public BallData[] ballArray;
    }
}

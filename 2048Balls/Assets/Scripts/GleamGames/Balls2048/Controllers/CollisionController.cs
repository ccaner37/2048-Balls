using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using GleamGames.Balls2048.Managers;

namespace GleamGames.Balls2048.Controllers
{
    public class CollisionController : MonoBehaviour
    {
        private BallScoreController ballScoreController;
        private BallPrefabController.BallData[] ballArray;

        private int ballScore;
        private float zPosition = -6.677f;

        private static bool isColliding;

        public bool IsBallDrop;

        [Inject]
        private GameManager gameManager;

        [Inject]
        private UIManager uiManager;

        [Inject]
        private LevelManager levelManager;

        [Inject]
        private BallPrefabController prefabController;

        [Inject]
        private BallSpawnController ballSpawn;

        private void Start()
        {
            ballScoreController = gameObject.GetComponent<BallScoreController>();

            ballScore = ballScoreController.BallScore;
            ballArray = prefabController.ballArray;
        }

        private void OnCollisionStay(Collision collision)
        {
            CheckCollision(collision);
        }

        private void OnCollisionEnter(Collision collision)
        {
            CheckGameFailed(collision);

            CheckCollision(collision);
        }

        private void CheckCollision(Collision collision)
        {
            if (collision.gameObject.GetComponent<BallScoreController>() != null)
            {
                int collisionScore = collision.gameObject.GetComponent<BallScoreController>().BallScore;

                if (ballScore == collisionScore)
                {
                    if (isColliding) return;
                    isColliding = true;

                    StartCoroutine(CreateNextBall(collision.transform));
                    Destroy(collision.transform.parent.gameObject);
                }
            }
        }

        private IEnumerator CreateNextBall(Transform collisionObject)
        {
            for (int i = 0; i < ballArray.Length; i++)
            {
                if (ballArray[i].Score == ballScore * 2)
                {
                    Transform newBall = Instantiate(ballArray[i].Ball, collisionObject.position, ballArray[i].Ball.rotation);

                    Vector3 linePosition = new Vector3(newBall.GetChild(0).transform.position.x, newBall.GetChild(0).transform.position.y, zPosition);
                    newBall.GetChild(0).transform.position = linePosition;
                    newBall.GetChild(0).GetComponent<CollisionController>().IsBallDrop = true;

                    levelManager.GiveExperience(ballScore);
                    CheckUnlockBall16(ballScore * 2);
                    CheckReached2048(ballScore * 2);
                }
            }

            yield return new WaitForSeconds(0.01f);
            isColliding = false;
            Destroy(transform.parent.gameObject);
        }

        private void CheckUnlockBall16(int score)
        {
            if (score >= 16 && ballSpawn.IsBall16Unlocked == false)
            {
                ballSpawn.IsBall16Unlocked = true;
            }
        }

        private void CheckReached2048(int score)
        {
            if (score == 2048)
            {
                uiManager.UpdateCounterText();
            }
        }

        private void CheckGameFailed(Collision collision)
        {
            if (collision.gameObject.GetComponent<CollisionController>() != null && collision.gameObject.GetComponent<CollisionController>().IsBallDrop == false)
            {
                uiManager.EnableFailedText();
                gameManager.StopGame();
            }
        }
    }
}

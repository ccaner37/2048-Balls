using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GleamGames.Balls2048.Controllers
{
    public class BallThrowController : MonoBehaviour
    {
        private Vector3 targetPos;

        private float speed = 3.0f;

        private BallSpawnController ballSpawn;

        void Start()
        {
            ballSpawn = gameObject.GetComponent<BallSpawnController>();
            ballSpawn.SpawnBall();
            targetPos = ballSpawn.SpawnedBall.position;
        }

        private void Update()
        {
            MoveBall();
        }

        private void MoveBall()
        {
            if (Input.GetMouseButton(0) && ballSpawn.SpawnedBall != null)
            {
                float distance = ballSpawn.SpawnedBall.position.z - Camera.main.transform.position.z;
                targetPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
                targetPos = Camera.main.ScreenToWorldPoint(targetPos);

                Vector3 followXonly = new Vector3(targetPos.x, ballSpawn.SpawnedBall.position.y, ballSpawn.SpawnedBall.position.z);
                ballSpawn.SpawnedBall.position = Vector3.Lerp(ballSpawn.SpawnedBall.position, followXonly, speed * Time.deltaTime);
            }
            else if (Input.GetMouseButtonUp(0) && ballSpawn.IsBallDropped)
            {
                ballSpawn.SpawnedBall.transform.GetChild(0).GetComponent<Rigidbody>().useGravity = true;
                ballSpawn.MarkAsDropped();
                StartCoroutine(ballSpawn.SpawnNewBall());
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GleamGames.Balls2048.Controllers
{
    public class BallSpawnController : MonoBehaviour
    {
        [SerializeField]
        private Vector3 spawnPoint;

        [SerializeField]
        private Transform[] ballPrefabs;

        public Transform SpawnedBall;

        public bool IsBall16Unlocked;
        public bool IsBallDropped = true;

        public void SpawnBall()
        {
            SpawnedBall = Instantiate(ballPrefabs[0], spawnPoint, ballPrefabs[0].rotation);
            SpawnedBall.transform.GetChild(0).GetComponent<Rigidbody>().useGravity = false;
        }

        public IEnumerator SpawnNewBall()
        {
            IsBallDropped = false;
            SpawnedBall = null;
            yield return new WaitForSeconds(1f);

            int randomNumber;

            if (IsBall16Unlocked)
            {
                randomNumber = Random.Range(0, ballPrefabs.Length);
            }
            else
            {
                randomNumber = Random.Range(0, ballPrefabs.Length - 1);
            }

            SpawnedBall = Instantiate(ballPrefabs[randomNumber], spawnPoint, ballPrefabs[0].rotation);
            SpawnedBall.transform.GetChild(0).GetComponent<Rigidbody>().useGravity = false;
            IsBallDropped = true;
        }

        public void MarkAsDropped()
        {
            SpawnedBall.transform.GetChild(0).GetComponent<CollisionController>().IsBallDrop = true;
        }
    }
}

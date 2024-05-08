using Common.DataPersistence;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

namespace Common.Managers
{
    public class ObstacleSpawnManager : BaseSingleton<ObstacleSpawnManager>, IDataPersistence
    {
        [SerializeField] private List<GameObject> _obstaclePrefabs;
        [SerializeField] public int NumObstacles = 1;
        [SerializeField] private float _minSpawnDistance = 2;

        private float _minScale = 0.5f;
        private float _maxScale = .8f;

        private void Start()
        {
            SpawnObstacles();
        }

        private void SpawnObstacles()
        {
            for (int i = 0; i < NumObstacles; i++)
            {
                Vector3 spawnPosition = GetRandomSpawnPosition();
                GameObject obstacle = Instantiate(_obstaclePrefabs[i % _obstaclePrefabs.Count], spawnPosition, Quaternion.identity);
                obstacle.transform.parent = transform;

                float randomRotation = Random.Range(0f, 360f);

                obstacle.transform.rotation = Quaternion.Euler(0f, 0f, randomRotation);

                float randomScale = Random.Range(_minScale, _maxScale);

                obstacle.transform.localScale = new Vector3(randomScale, randomScale, 1f);
            }
        }

        private Vector3 GetRandomSpawnPosition()
        {
            Camera cam = Camera.main;
            float minX = cam.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;
            float maxX = cam.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;
            float minY = cam.ViewportToWorldPoint(new Vector3(0, 0, 0)).y;
            float maxY = cam.ViewportToWorldPoint(new Vector3(0, 1, 0)).y;

            Vector3 spawnPosition;
            bool isSpawnPositionValid = false;
            do
            {
                spawnPosition = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), 0);
                isSpawnPositionValid = !IsTooCloseToBigCandy(spawnPosition) && !IsTooCloseToOtherCandies(spawnPosition) && !IsObstacleInSpawnPosition(spawnPosition);
            } while (!isSpawnPositionValid);

            return spawnPosition;
        }

        private bool IsTooCloseToBigCandy(Vector3 position)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(position, _minSpawnDistance);
            foreach (Collider2D collider in colliders)
            {
                if (collider.gameObject.CompareTag("BigCandy"))
                {
                    return true;
                }
            }
            return false;
        }

        private bool IsTooCloseToOtherCandies(Vector3 position)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(position, _minSpawnDistance);
            return colliders.Length > 1;
        }

        private bool IsObstacleInSpawnPosition(Vector3 position)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(position, .1f);
            foreach (Collider2D collider in colliders)
            {
                if (collider.gameObject.CompareTag("Obstacle"))
                {
                    return true;
                }
            }
            return false;
        }

        public void LoadData(GameData data)
        {
            NumObstacles = data.ObstacleNumber;
        }

        public void SaveData(GameData data)
        {
            data.ObstacleNumber = NumObstacles;
        }
    }
}
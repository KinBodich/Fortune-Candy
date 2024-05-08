using Common;
using Common.DataPersistence;
using System.Collections.Generic;
using UnityEngine;

namespace Mechanics
{
    public class CandySpawnManager : BaseSingleton<CandySpawnManager>, IDataPersistence
    {
        [SerializeField] private List<GameObject> _candyPrefabs;
        [SerializeField] public int NumCandies = 5;
        [SerializeField] private float _minSpawnDistance = 1;
        public int CurrentLevel { get; set; } = 1;

        protected override void Awake()
        {
            base.Awake();
        }

        private void Start()
        {
            SpawnCandies();
        }

        public void SetCandyNumber(int newNumber)
        {
            NumCandies = newNumber;
        }

        private void SpawnCandies()
        {
            for (int i = 0; i < NumCandies; i++)
            {
                Vector3 spawnPosition = GetRandomSpawnPosition();
                GameObject candy = Instantiate(_candyPrefabs[i % _candyPrefabs.Count], spawnPosition, Quaternion.identity);
                candy.transform.parent = transform;
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
            NumCandies = data.CandiesNumber;
            CurrentLevel = data.CurrentLevel;
        }

        public void SaveData(GameData data)
        {
            data.CandiesNumber = NumCandies;
            data.CurrentLevel = CurrentLevel;
        }
    }
}

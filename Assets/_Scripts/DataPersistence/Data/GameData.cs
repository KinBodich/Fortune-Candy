using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common.DataPersistence
{
    [System.Serializable]
    public class GameData
    {
        public int CurrentLevel;
        public int ObstacleNumber;
        public int CandiesNumber;
        public float MaxTimer;

        public GameData()
        {
            CurrentLevel = 1;
            ObstacleNumber = 0;
            CandiesNumber = 5;
            MaxTimer = 15;
        }
    }
}
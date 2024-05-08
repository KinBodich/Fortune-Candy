using Common.DataPersistence;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common.Managers
{
    public class LevelManager : BaseSingleton<LevelManager>, IDataPersistence
    {
        private int _currentLevel = 0;

        public void LoadData(GameData data)
        {
            _currentLevel = data.CurrentLevel;
        }

        public void SaveData(GameData data)
        {
            data.CurrentLevel = _currentLevel;
        }
    }
}
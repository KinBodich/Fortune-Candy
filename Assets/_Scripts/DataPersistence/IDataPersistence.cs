using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common.DataPersistence
{
    public interface IDataPersistence
    {
        void LoadData(GameData data);
        void SaveData(GameData data);
    }
}
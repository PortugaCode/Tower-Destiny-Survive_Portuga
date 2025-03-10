using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;


public enum PoolUniqueID
{
    None = -1,
    // Enemy ========================
    Enemy_Top,
    Enemy_Middle,
    Enemy_Bottom,
    // Bullet ========================
    BasicBullet
}

[Serializable]
public class PoolDataTable
{
    public string discription;
    public PoolUniqueID enumKey;
    public GameObject[] prefab;

    public PoolUniqueID GetID()
    {
        return enumKey;
    }

    public GameObject GetObject(int index)
    {
        return prefab[index];
    }

    public GameObject GetObject()
    {
        return prefab[0];
    }
}


[CreateAssetMenu(fileName = "ObjectPoolTable_", menuName = "#ScriptableObject/ObjectPool")]
public class ObjectPoolData : ScriptableObject
{
    public PoolDataTable[] poolDataTables;

    public GameObject GetPrefab(PoolUniqueID poolUniqueID, int index)
    {
        for(int i = 0; i < poolDataTables.Length; i++)
        {
            if (poolDataTables[i].GetID() == poolUniqueID)
            {
                return poolDataTables[i].GetObject(index);
            }
        }

        return null;
    }

    public GameObject GetPrefab(PoolUniqueID poolUniqueID)
    {
        for (int i = 0; i < poolDataTables.Length; i++)
        {
            if (poolDataTables[i].GetID() == poolUniqueID)
            {
                return poolDataTables[i].GetObject();
            }
        }

        return null;
    }
}

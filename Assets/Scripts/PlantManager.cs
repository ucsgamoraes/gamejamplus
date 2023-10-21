using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlantManager : MonoBehaviour
{
    public enum PlantMaterial
    {
        Normal,
        Fluorescent
    }

    public enum FruitType
    {
        None = -1,
        Normal,
        Bomb
    }

    public List<GameObject> fruitPrefabs = new List<GameObject>();
    public List<GameObject> allPlants = new List<GameObject>();

    public static PlantManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
}

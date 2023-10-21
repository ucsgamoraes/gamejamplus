using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    public enum Material
    {
        Normal,
        Fluorescent
    }

    public enum FruitType
    {
        Normal,
        Bomb
    }

    public GameObject[] fruitTypesPrefabs;

    public float currentSize;
    public float currentFruitSize;
    public float growthRate;
    public float fruitGrowthRate;
    public Material plantMaterial;
    public FruitType fruitType;
    public Transform[] fruitPoints;
    public List<GameObject> instantiatedFruits = new List<GameObject>();

    public const float testValue = 1.0f;
    public float groundHeight;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < fruitPoints.Length; i++)
        {
            instantiatedFruits.Add(Instantiate(fruitTypesPrefabs[(int)fruitType], null, fruitPoints[i]));
        }
    }

    // Update is called once per frame
    void Update()
    {
        currentSize += growthRate/100.0f * Time.deltaTime;
        SetPlantSize(currentSize);

        if(currentSize > testValue)
        {
            currentFruitSize = fruitGrowthRate / 100.0f * Time.deltaTime;
            SetFruitsSize(currentFruitSize);
        }
    }

    void SetFruitsSize(float size)
    {
        for (int i = 0; i < instantiatedFruits.Count; i++)
        {
            instantiatedFruits[i].transform.localScale = Vector3.one - (Vector3.one * currentFruitSize);
            instantiatedFruits[i].transform.position = fruitPoints[i].position;
        }
    }

    void SetPlantSize(float size)
    {
        transform.localScale = new Vector3(transform.localScale.x, currentSize, transform.localScale.z);
        transform.position = new Vector3(transform.position.x, groundHeight + currentSize/2, transform.position.z);
    }
}

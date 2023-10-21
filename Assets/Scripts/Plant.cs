using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Plant : MonoBehaviour
{
    [HideInInspector]
    public float currentSize;
    [HideInInspector]
    public float currentFruitSize;

    public float growthRate;
    public float fruitGrowthRate;

    public PlantManager.PlantMaterial plantMaterial;
    public PlantManager.FruitType fruitType;

    public Transform[] fruitPoints;

    [HideInInspector]
    public List<GameObject> instantiatedFruits = new List<GameObject>();

    public UnityEvent onFruitsFinishGrown;
    public UnityEvent onPlantFinishGrown;
    public UnityEvent onTouched;
    public UnityEvent onDied;

    public bool isPlantFullSize;
    public bool areFruitsFullSize;

    public int lifePoints = 250;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < fruitPoints.Length; i++)
        {
            instantiatedFruits.Add(Instantiate(PlantManager.Instance.fruitPrefabs[(int) fruitType], null, fruitPoints[i]));
        }

        PlantManager.Instance.allPlants.Add(gameObject);

        //set to zero
        SetFruitsSize();
    }

    // Update is called once per frame
    void Update()
    {
        Grow();
    }

    public void TakeDamage(int amount)
    {
        if (lifePoints <= 0) return;

        lifePoints -= amount;

        if (lifePoints <= 0)
        {
            PlantManager.Instance.allPlants.Remove(gameObject);
            onDied.Invoke();
        }
    }

    void Grow ()
    {
        if (currentSize < 1.0f)
        {
            currentSize += growthRate / 100.0f * Time.deltaTime;
            SetPlantSize();
            return;
        }

        if (!isPlantFullSize)
        {
            onPlantFinishGrown.Invoke();
            isPlantFullSize = true;
        }

        if (fruitType == PlantManager.FruitType.None) return;

        if (currentFruitSize < 1.0f)
        {
            currentFruitSize += fruitGrowthRate / 100.0f * Time.deltaTime;
            SetFruitsSize();
            return;
        }

        if(!areFruitsFullSize){
            onFruitsFinishGrown.Invoke();
            areFruitsFullSize = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other != null)
        {
            onTouched.Invoke();
        }
    }

    void SetFruitsSize()
    {
        for (int i = 0; i < instantiatedFruits.Count; i++)
        {
            instantiatedFruits[i].transform.localScale = Vector3.one*currentFruitSize;
            instantiatedFruits[i].transform.position = fruitPoints[i].position;
        }
    }

    void SetPlantSize()
    {
        transform.localScale = new Vector3(transform.localScale.x, currentSize, transform.localScale.z);
        transform.position = new Vector3(transform.position.x, currentSize/2, transform.position.z);
    }
}

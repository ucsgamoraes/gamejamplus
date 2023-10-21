using UnityEngine;

public class PlantBehaviours : MonoBehaviour
{
    private Plant plant;

    public 
        GameObject fruitExplosion;


    // Start is called before the first frame update
    void Start()
    {
        plant = GetComponent<Plant>();
    }

    public void ExplodeFruits()
    {
        foreach (GameObject item in plant.instantiatedFruits)
        {
            if (plant.areFruitsFullSize)
            {
                Destroy(item);
                Instantiate(fruitExplosion, item.transform.position, Quaternion.identity);
            }
        }
    }
}

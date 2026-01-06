using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class patterHolder : MonoBehaviour
{
    public itemMimic[] itemMimics;
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            List<float> itemsDistances = new List<float>();
            List<float> itemsDistancesFromInstance = new List<float>();
            List<float> stringDistancesFromInstance = new List<float>();
            for (int i = 0; i < itemMimics.Length; i++)
            {
                for (int k = 0; k < manager.instance.items.Length; k++)
                {
                    //if()
                    itemsDistancesFromInstance.Add(Vector2.Distance(itemMimics[i].transform.position, manager.instance.items[k].transform.position));
                    itemsDistancesFromInstance.Sort();
                }
                itemsDistances.Add(itemsDistancesFromInstance[0]);
            }
            itemsDistances.Sort();
            for (int i = 0; i < itemsDistances.Count; i++)
            {
                Debug.Log(i + " " + itemsDistances[i]);
            }
        }
    }

}

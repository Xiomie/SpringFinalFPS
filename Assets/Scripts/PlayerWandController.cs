using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWandController : MonoBehaviour
{
    public List<GameObject> wandPrefabs;
    public Transform wandHold;
    public GameObject startingWand;
    private GameObject currentWand;
    public int currentWandIndex = -1;

    private void Start()
    {
        if (startingWand != null)
        {
            int startingWandIndex = wandPrefabs.IndexOf(startingWand);
            if (startingWandIndex >= 0)
            {
                EquipWand(startingWandIndex);
            }
        }
    }

    private void Update()
    {
        for (int i = 0; i < wandPrefabs.Count; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                EquipWand(i);
            }
        }
    }

    private void EquipWand(int wandIndex)
    {
        if (currentWandIndex == wandIndex) return;

        // If a wand is already equipped, remove it
        if (currentWand != null)
        {
            Destroy(currentWand);
        }

        // Instantiate the wand prefab and set its parent
        GameObject newWand = Instantiate(wandPrefabs[wandIndex], wandHold.position, wandHold.rotation);
        newWand.transform.SetParent(wandHold);

        // Set the new wand as the current wand
        currentWand = newWand;
        currentWandIndex = wandIndex;
    }
}
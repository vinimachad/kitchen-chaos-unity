using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlatesCounter : BaseCounter
{


    private List<PlateKitchenObject> plates;
    private int numberOfPlatesInCounter;
    [SerializeField] private int maxPlates;
    [SerializeField] private KitchenSO plateSO;

    private void Start()
    {
        numberOfPlatesInCounter = maxPlates;
        plates = new List<PlateKitchenObject>();

        InstantiatePlatesInCounter();
    }

    public override void Interact(Player player)
    {
        PickItems playerPickedItem = player.GetPlayerPickedItem();

        if (HasItem())
        {
            if (!playerPickedItem.HasItem())
            {
                player.PickItem(Item);
                numberOfPlatesInCounter--;
                if (numberOfPlatesInCounter > 0)
                {
                    SetItem(plates[numberOfPlatesInCounter - 1]);
                }
                else
                {
                    Item = null;
                }
            }
        }
    }

    private void InstantiatePlatesInCounter()
    {
        foreach (var index in Enumerable.Range(0, maxPlates))
        {
            if (index == 0)
            {
                AddPlateInList();
            }
            else
            {
                float plateHeight = .1f;
                Vector3 counterTopPointPosition = counterTopPoint.transform.position;
                float yPos = counterTopPointPosition.y + plateHeight * index;
                Vector3 newPos = new Vector3(counterTopPointPosition.x, yPos, counterTopPointPosition.z);

                AddPlateInList(newPos);
            }
        }

        int plateIndex = maxPlates - 1;
        SetItem(plates[plateIndex]);
    }

    private void AddPlateInList(Vector3? pos = null)
    {
        Transform plateTransform = null;

        if (pos != null)
        {
            plateTransform = Instantiate(plateSO.prefab, pos ?? Vector3.zero, counterTopPoint.rotation, counterTopPoint);
        }
        else
        {
            plateTransform = Instantiate(plateSO.prefab, counterTopPoint);
        }

        if (plateTransform != null)
        {
            PlateKitchenObject kitchenObjet = plateTransform.GetComponent<PlateKitchenObject>();
            plates.Add(kitchenObjet);
        }
    }
}

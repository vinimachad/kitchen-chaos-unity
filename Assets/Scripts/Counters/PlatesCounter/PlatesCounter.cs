using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlatesCounter : BaseCounter
{


    private List<KitchenObject> plates;
    private int numberOfPlatesInCounter;
    [SerializeField] private int maxPlates;
    [SerializeField] private KitchenSO plateSO;

    private void Start()
    {
        numberOfPlatesInCounter = maxPlates;
        plates = new List<KitchenObject>();

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
                print(numberOfPlatesInCounter);
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
                Transform plateTransform = Instantiate(plateSO.prefab, counterTopPoint);
                KitchenObject kitchenObjet = plateTransform.GetComponent<KitchenObject>();
                plates.Add(kitchenObjet);
            }
            else
            {
                float plateHeight = .1f;
                Vector3 counterTopPointPosition = counterTopPoint.transform.position;
                float yPos = counterTopPointPosition.y + plateHeight * index;
                Vector3 newPos = new Vector3(counterTopPointPosition.x, yPos, counterTopPointPosition.z);

                Transform plateTransform = Instantiate(plateSO.prefab, newPos, counterTopPoint.rotation);
                KitchenObject kitchenObjet = plateTransform.GetComponent<KitchenObject>();
                plates.Add(kitchenObjet);
            }
        }

        int plateIndex = maxPlates - 1;
        SetItem(plates[plateIndex]);
    }
}

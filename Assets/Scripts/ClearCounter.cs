using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter: MonoBehaviour, IPickUp
{

    [SerializeField] private KitchenSO kitchenObjectSO;
    [SerializeField] private Transform counterTopPoint;
    [SerializeField] private ClearCounter otherCounter;
    
    //private KitchenObject kitchenObject;

    public KitchenObject Item { get; set; }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T) && Item != null)
        {
            Item.SetPickedPlace(otherCounter);
        }
    }

    public void Interact(Player player)
    {
        if (Item == null)
        {
            Transform kitchenObjcTransform = Instantiate(kitchenObjectSO.prefab, counterTopPoint);
            kitchenObjcTransform.GetComponent<KitchenObject>().SetPickedPlace(this);
        } else
        {
            player.PickItem(Item);
        }
    }

    public Transform GetPositionPoint()
    {
        return counterTopPoint;
    }

    public void SetItem(KitchenObject item)
    {
        this.Item = item;
    }

    public KitchenObject GetItem()
    {
        return this.Item;
    }

    public bool HasItem()
    {
        return this.Item != null;
    }

    public void ClearItem()
    {
        this.Item = null;
    }

    //public Transform GetCounterTopPoint()
    //{
    //    return counterTopPoint;
    //}

    //public void SetKitchenObject(KitchenObject kitchenObject)
    //{
    //    this.kitchenObject = kitchenObject;
    //}

    //public KitchenObject GetKitchenObject()
    //{
    //    return kitchenObject;
    //}

    //public bool HasKitchenObject()
    //{
    //    return kitchenObject != null;
    //}

    //public void ClearKitchenObject()
    //{
    //    kitchenObject = null;
    //}
}

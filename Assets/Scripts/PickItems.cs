using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickItems : MonoBehaviour, IPickUp
{

    public KitchenObject Item { get; set; }

    public void PickItem(KitchenObject item)
    {
        if (this.Item != null)
        {
            Debug.LogError("Already exists item with player");
            return;
        }

        item.SetPickedPlace(this);
    }


    public void ClearItem()
    {
        Item = null;
    }

    public KitchenObject GetItem()
    {
        return Item;
    }

    public Transform GetPositionPoint()
    {
        return transform;
    }

    public bool HasItem()
    {
        return Item != null;
    }

    public void SetItem(KitchenObject item)
    {
        Item = item;
    }
}

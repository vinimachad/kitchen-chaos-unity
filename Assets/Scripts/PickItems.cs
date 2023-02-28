using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickItems : MonoBehaviour, IPickUp, IDropable
{

    public KitchenObject Item { get; set; }

    public void PickItem(KitchenObject item)
    {
        if (this.Item != null) return;

        item.SetPickedPlace(this);
    }

    public void DropItemTo(IPickUp pickableObject)
    {
        Item.SetPickedPlace(pickableObject);
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPickUp {

    KitchenObject Item { get; set; }

    public Transform GetPositionPoint();
    public void SetItem(KitchenObject item);
    public KitchenObject GetItem();
    public bool HasItem();
    public void ClearItem();
}

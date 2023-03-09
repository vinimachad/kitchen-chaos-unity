using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCounter : MonoBehaviour, IPickUp, ISelectable
{

    [SerializeField] protected Transform counterTopPoint;

    public KitchenObject Item { get; set; }
    public virtual void Interact(Player player)
    {
        Debug.Log("Interact");
    }

    public virtual void UtilitiesInteract(Player player, bool isHolding)
    {
        Debug.Log("Utilities Interact");
    }

    public virtual Transform GetPositionPoint()
    {
        return counterTopPoint;
    }

    public virtual void SetItem(KitchenObject item)
    {
        this.Item = item;
    }

    public virtual KitchenObject GetItem()
    {
        return this.Item;
    }

    public virtual bool HasItem()
    {
        return this.Item != null;
    }

    public virtual void ClearItem()
    {
        this.Item = null;
    }
}
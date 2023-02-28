using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{

    [SerializeField] private KitchenSO kitchenSO;

    private IPickUp pickUp;

    public KitchenSO GetKitchenObjectName()
    {
        return kitchenSO;
    }

    public void SetPickedPlace(IPickUp pickUp)
    {
        if (this.pickUp != null)
        {
            this.pickUp.ClearItem();
        }

        this.pickUp = pickUp;

        if (pickUp.HasItem()) {
            Debug.LogError("Kitchen object already exists in clear counter");
        }

        pickUp.SetItem(this);

        transform.parent = pickUp.GetPositionPoint();
        transform.localPosition = Vector3.zero;
    }

    public void ClearItem()
    {
        pickUp = null;
    }

    public IPickUp GetPlace()
    {
        return pickUp;
    }
}

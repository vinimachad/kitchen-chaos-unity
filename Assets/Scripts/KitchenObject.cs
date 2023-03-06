using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{

    [SerializeField] private KitchenSO kitchenSO;

    private IPickUp pickUp;

    public KitchenSO GetKitchenObjectSO()
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

    public void DestroyYourSelf()
    {
        Destroy(gameObject);
        ClearItem();
    }

    static public KitchenObject InstantiateItemAndPassTo(Transform prefab, Transform counterTopPoint, IPickUp place)
    {
        Transform kitchenObjecTransform = Instantiate(prefab, counterTopPoint);
        KitchenObject kitchenObject = kitchenObjecTransform.GetComponent<KitchenObject>();
        kitchenObject.SetPickedPlace(place);
        return kitchenObject;
    }
}

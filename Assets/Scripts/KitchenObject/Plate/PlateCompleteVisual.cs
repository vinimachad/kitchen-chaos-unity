using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

public class PlateCompleteVisual : MonoBehaviour
{

    [SerializeField] public List<PlateCompleteVisual_GameObject> kitchenObjectGameObjects;
    [SerializeField] private PlateKitchenObject plateKitchenObject;

    private void Awake()
    {
        plateKitchenObject.OnShowKitchenObjectInPlate += ShowKitchenObjectInPlate_PlateKitchenObject;

        foreach (var kitchenObjectGameObject in kitchenObjectGameObjects)
        {
            kitchenObjectGameObject.gameObject.SetActive(false);
        }
    }

    private void ShowKitchenObjectInPlate_PlateKitchenObject(object sender, KitchenSO kitchenSO)
    {
        foreach (var kitchenObjectGameObject in kitchenObjectGameObjects)
        {
            if (kitchenObjectGameObject.kitchenSO == kitchenSO)
            {
                kitchenObjectGameObject.gameObject.SetActive(true);
            }
        }
    }
}

[Serializable]
public struct PlateCompleteVisual_GameObject
{
    public GameObject gameObject;
    public KitchenSO kitchenSO;
}
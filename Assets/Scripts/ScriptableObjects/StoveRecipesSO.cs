using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class StoveRecipesSO : ScriptableObject
{
    public KitchenSO input;
    public KitchenSO output;
    public float timeToChangeState;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounterVisual : MonoBehaviour
{

    private CuttingCounter cuttinCounter;
    private Animator animator;

    private const string CUT = "Cut";
    void Start()
    {
        cuttinCounter = GetComponentInParent<CuttingCounter>();
        animator = GetComponent<Animator>();
        cuttinCounter.OnCuttingItem += CuttinCounter_OnCuttingItem; ;
    }

    private void CuttinCounter_OnCuttingItem(object sender, System.EventArgs e)
    {
        animator.SetTrigger(CUT);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounterVisual : MonoBehaviour
{

    private ContainerCounter containerCounter;
    private readonly string OPEN_CLOSE = "OpenClose";
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        containerCounter = GetComponentInParent<ContainerCounter>();
        containerCounter.OnGrabbItem += ContainerCounter_OnGrabbItem;
    }

    private void ContainerCounter_OnGrabbItem(object sender, EventArgs e)
    {
        animator.SetTrigger(OPEN_CLOSE);
    }
}

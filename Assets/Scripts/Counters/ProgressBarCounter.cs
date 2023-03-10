using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class ProgressBarCounter : BaseCounter
{
    public abstract event EventHandler<float> OnShowProgressBar;
    public abstract event EventHandler OnHideProgressBar;
}

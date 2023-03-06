using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{

    [SerializeField] Image barImage;
    [SerializeField] CuttingCounter counter;
    [SerializeField] GameObject progressBarCanvas;

    private void Start()
    {
        counter.OnShowProgressBar += Counter_OnShowProgressBar;
        counter.OnHideProgressBar += Counter_OnHideProgressBar;
        Hide();
    }

    private void Counter_OnHideProgressBar(object sender, System.EventArgs e)
    {
        Hide();
    }

    private void Counter_OnShowProgressBar(object sender, float amount)
    {
        Show();
        barImage.fillAmount = amount;
    }

    private void Show()
    {
        progressBarCanvas.SetActive(true);
    }

    private void Hide()
    {
        progressBarCanvas.SetActive(false);
        barImage.fillAmount = 0f;
    }
}

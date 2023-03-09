using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCounter : MonoBehaviour
{

    private ISelectable selectable;
    private bool isShowing = false;
    [SerializeField] private GameObject[] visualsSelectedClearCounter;

    private void Awake()
    {
        selectable = GetComponentInParent<ISelectable>();
    }

    private void Start()
    {
        Player.Instance.OnSelectedVisualCounterChanged += Player_OnSelectedVisualCounterChanged;
    }

    private void Update()
    {
        if (!isShowing)
        {
            Hide();
        }
    }

    private void Player_OnSelectedVisualCounterChanged(object sender, Player.SelectedVisualCounterArgs e)
    {
        isShowing = true;
        if (e.selectedCounter == selectable)
        {
            Show();
        }
        else
        {
            isShowing = false;
        }
    }

    public void Show()
    {
        foreach (GameObject visual in visualsSelectedClearCounter)
        {
            visual.SetActive(true);
        }
    }

    public void Hide()
    {
        foreach (GameObject visual in visualsSelectedClearCounter)
        {
            visual.SetActive(false);
        }
    }
}

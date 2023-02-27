using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCounter : MonoBehaviour
{

    [SerializeField] private ClearCounter clearCounter;
    [SerializeField] private GameObject visualSelectedClearCounter;

    private void Start()
    {
        Player.Instance.OnSelectedVisualCounterChanged += Player_OnSelectedVisualCounterChanged;
    }

    private void Player_OnSelectedVisualCounterChanged(object sender, Player.SelectedVisualCounterArgs e)
    {
        if (e.selectedCounter == clearCounter)
        {
            Show();
        } else
        {
            Hide();
        }
    }

    private void Show()
    {
        visualSelectedClearCounter.SetActive(true);

    }

    private void Hide()
    {
        visualSelectedClearCounter.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCounter : MonoBehaviour
{

    private BaseCounter counter;
    [SerializeField] private GameObject[] visualsSelectedClearCounter;

    private void Awake()
    {
        counter = GetComponentInParent<BaseCounter>();
    }

    private void Start()
    {
        Player.Instance.OnSelectedVisualCounterChanged += Player_OnSelectedVisualCounterChanged;
    }

    private void Player_OnSelectedVisualCounterChanged(object sender, Player.SelectedVisualCounterArgs e)
    {
        if (e.selectedCounter == counter)
        {
            Show();
        } else
        {
            Hide();
        }
    }

    private void Show()
    {
        foreach(GameObject visual in visualsSelectedClearCounter)
        {
            visual.SetActive(true);
        }
    }

    private void Hide()
    {
        foreach (GameObject visual in visualsSelectedClearCounter)
        {
            visual.SetActive(false);
        }
    }
}

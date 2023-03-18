using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecipesInPlateUI : MonoBehaviour
{

    [SerializeField] GameObject gridLayout;
    [SerializeField] GameObject baseIconPrefab;

    private void Start()
    {
        Hide();
    }

    public void AddIconInGridLayout(Sprite sprite)
    {
        Instantiate(BuildIconPrefab(sprite), gridLayout.transform);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    private GameObject BuildIconPrefab(Sprite sprite)
    {
        GameObject iconPrefab = baseIconPrefab;

        Image image = iconPrefab.GetComponent<Image>();
        image.sprite = sprite;

        return iconPrefab;
    }

}

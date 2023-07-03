using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecipeBookManager : MonoBehaviour
{
    public List<GameObject> pagesList;
    int pageNum;
    [SerializeField]
    Text pageNumText;

    void Start()
    {
        pageNum = 0;
        UpdatePageNum();
    }

    private void UpdatePageNum()
    {
        pageNumText.text = (pageNum + 1) + " / " + pagesList.Count;
    }

    public void GoPrevPage()
    {
        if (pageNum > 0)
        {
            pagesList[pageNum].SetActive(false);
            --pageNum;
            pagesList[pageNum].SetActive(true);

            UpdatePageNum();
        }
    }

    public void GoNextPage()
    {
        if (pageNum < pagesList.Count - 1)
        {
            pagesList[pageNum].SetActive(false);
            ++pageNum;
            pagesList[pageNum].SetActive(true);

            UpdatePageNum();
        }
    }
}

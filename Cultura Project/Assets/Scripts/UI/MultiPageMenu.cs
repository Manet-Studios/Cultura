using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Sirenix.OdinInspector;

public class MultiPageMenu : MonoBehaviour {

    [SerializeField] private GameObject[] pages;
    private int currentPage;
    [SerializeField] private GameObject currentPageText;
    private TextMeshProUGUI curPgtxt;
    [SerializeField] private Button leftArrow;
    [SerializeField] private Button rightArrow;

    private void Start()
    {
        if (pages.Length == 0)
        {
            Debug.Log(this.name + " does not have any pages.");
        }
        else if (currentPageText != null)
        {
            curPgtxt = currentPageText.GetComponent<TextMeshProUGUI>() ?? currentPageText.AddComponent<TextMeshProUGUI>();
            curPgtxt.text = pages[0].name;
        }
        else Debug.Log(this.name + " is missing a 'current page' text field.");

        if (pages.Length != 0)
        {
            leftArrow.interactable = false;
            rightArrow.interactable = true;
            currentPage = 0;
        } 
    }

    private void Update()
    {
        if (currentPage == 0) leftArrow.interactable = false;
        else leftArrow.interactable = true;
        if (currentPage == pages.Length - 1) rightArrow.interactable = false;
        else rightArrow.interactable = true;
        for(int i = 0; i < pages.Length; i++)
        {
            if (currentPage == i) pages[i].SetActive(true);
            else pages[i].SetActive(false);
        }
        curPgtxt.text = pages[currentPage].name;
    }

    public void GoLeft()
    {
        if (leftArrow.interactable)
        {
            currentPage -= 1;
        }
    }

    public void GoRight()
    {
        if (rightArrow.interactable)
        {
            currentPage += 1;
        }
    }


}

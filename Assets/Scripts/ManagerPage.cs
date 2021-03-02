using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerPage : MonoBehaviour
{
    public GameObject[] pages;
    [Range(0, 1)]
    public int currentPage = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnValidate()
    {
        foreach (GameObject page in pages)
        {
            page.SetActive(false);
        }

        GameObject newPage = pages[currentPage];
        newPage.SetActive(true);
    }
}

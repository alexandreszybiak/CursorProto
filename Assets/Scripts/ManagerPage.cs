using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerPage : MonoBehaviour
{
    public GameObject[] pages;
    [Range(0, 2)]
    public int currentPage = 0;
    public Transform stickerLayer;


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

    public void GotoNextPage() {
        GameObject page = pages[currentPage];
        page.SetActive (false);
        currentPage += 1;
        if(currentPage >= pages.Length ) {
            currentPage = 0;
        }
        page = pages[currentPage];
        page.SetActive (true);
        GameObject cursor = GameObject.Find("Cursor P1");
        cursor.SetActive (false);
        cursor.SetActive (true);
        cursor.GetComponent<Cursor> ().LookAroundForSnap ();
        cursor = GameObject.Find("Cursor P2");
        cursor.SetActive(false);
        cursor.SetActive(true);
        cursor.GetComponent<Cursor>().LookAroundForSnap();

        foreach(Transform sticker in stickerLayer){
            sticker.position = new Vector3(-100,0,0);
        }


    }
}

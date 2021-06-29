using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageHandler : MonoBehaviour
{
    [SerializeField] private List<Page> pages = null;

    private int index = 0;

    public void PageUp()
    {
        if (index > 0)
        {
            PagesOff();
            index--;
            pages[index].gameObject.SetActive(true);
        }
    }

    public void PageDown()
    {
        if (index < pages.Count -1)
        {
            PagesOff();
            index++;
            pages[index].gameObject.SetActive(true);
        }
    }

    public void GoToNextOrFirst()
    {
        if (index < pages.Count - 1)
        {
            PagesOff();
            index++;
            pages[index].gameObject.SetActive(true);
        }
        else
        {
            PagesOff();
            index = 0;
            pages[0].gameObject.SetActive(true);
        }
    }

    public void MoveToPage(int index)
    {
        if (pages.Count > 0 && index < pages.Count)//-1)
        {
            PagesOff();
            pages[index].gameObject.SetActive(true);
        }
    }

    private void PagesOff()
    {
        foreach (var page in pages)
        {
            page.gameObject.SetActive(false);
        }
    }
}

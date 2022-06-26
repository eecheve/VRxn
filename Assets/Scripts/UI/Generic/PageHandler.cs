using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageHandler : MonoBehaviour
{
    [SerializeField] private List<Page> pages = null;

    private int m_index = 0;

    public delegate void LastPageArrived();
    public event LastPageArrived OnLastPageArrived;

    public void PageUp()
    {
        if (m_index > 0)
        {
            PagesOff();
            m_index--;
            pages[m_index].gameObject.SetActive(true);
        }
    }

    public void PageDown()
    {
        if (m_index < pages.Count -1)
        {
            PagesOff();
            m_index++;
            pages[m_index].gameObject.SetActive(true);
            Debug.Log($"Setting the page because index is {m_index}");
            if(m_index == pages.Count)
            {
                OnLastPageArrived?.Invoke();
            }
        }
    }

    public void GoToNextOrFirst()
    {
        if (m_index < pages.Count - 1)
        {
            PagesOff();
            m_index++;
            pages[m_index].gameObject.SetActive(true);
        }
        else
        {
            PagesOff();
            m_index = 0;
            pages[0].gameObject.SetActive(true);
        }
    }

    public void MoveToPage(int index)
    {
        if (pages.Count > 0 && index < pages.Count)//-1)
        {
            PagesOff();
            m_index = index;
            pages[index].gameObject.SetActive(true);

            if (m_index == pages.Count)
            {
                OnLastPageArrived?.Invoke();
            }
        }
    }

    public void PagesOff()
    {
        foreach (var page in pages)
        {
            page.gameObject.SetActive(false);
        }
    }

    public void ResetPages()
    {
        foreach (var page in pages)
        {
            page.gameObject.SetActive(false);
        }
        m_index = 0;
    }
}

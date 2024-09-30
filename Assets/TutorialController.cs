using System.Collections.Generic;
using UnityEngine;

public class TutorialController : MonoBehaviour
{
    public List<GameObject> pages = new List<GameObject>();

    public int index = 0;
    public void Next()
    {
        if (index < pages.Count - 1) 
        {
            pages[index].SetActive(false);
            index++;
            pages[index].SetActive(true);
        }
    }

    public void Previous()
    {
        if (index > 0) 
        {
            pages[index].SetActive(false);
            index--;
            pages[index].SetActive(true);
        }
    }

}

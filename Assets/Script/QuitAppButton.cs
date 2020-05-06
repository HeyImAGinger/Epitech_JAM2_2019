using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitAppButton : MonoBehaviour
{
    private Drawer drawer;
    public GameObject saveBeforeQuit;

    void Start()
    {
        drawer = GameObject.Find("Fond").GetComponent<Drawer>();
    }

    public void QuitApp()
    {
        if (drawer.edit)
        {
            saveBeforeQuit.SetActive(true);
        }
        else
            Application.Quit();
    }
}

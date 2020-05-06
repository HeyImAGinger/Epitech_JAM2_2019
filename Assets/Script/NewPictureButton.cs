using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPictureButton : MonoBehaviour
{
    private Drawer drawer;
    public GameObject saveBeforeNew;

    public void Start()
    {
        drawer = GameObject.Find("Fond").GetComponent<Drawer>();
    }

    public void NewFile()
    {
        if (drawer.edit)
            saveBeforeNew.SetActive(true);
        else
            drawer.texture.LoadImage(System.IO.File.ReadAllBytes("Assets/Sprites/Backgrounds/mur de briques.jpg"));
    }
}

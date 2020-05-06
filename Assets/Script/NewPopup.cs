using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPopup : MonoBehaviour
{
    private Drawer drawer;
    public GameObject saver;

    void Start()
    {
        drawer = GameObject.Find("Fond").GetComponent<Drawer>();
    }

    public void SaveNew()
    {
        saver.GetComponent<SavePictureButton>().SaveFile();
        New();
        Cancel();
    }

    public void New()
    {
        drawer.texture.LoadImage(System.IO.File.ReadAllBytes("Assets/Sprites/Backgrounds/mur de briques.jpg"));
        Cancel();
    }

    public void Cancel()
    {
        gameObject.SetActive(false);
    }
}

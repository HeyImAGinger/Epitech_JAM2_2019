using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleFileBrowser;
using System.IO;
using System;

public class OpenPictureButton : MonoBehaviour
{
    private string filePathOpen;
    private Drawer drawer;

    void Start()
    {
        drawer = GameObject.Find("Fond").GetComponent<Drawer>();
    }

    public void OpenFile()
    {
        FileBrowser.SetFilters(true, new FileBrowser.Filter("Images", ".jpg", ".png"));
        FileBrowser.SetDefaultFilter(".png");
        FileBrowser.SetExcludedExtensions(".lnk", ".tmp", ".zip", ".rar", ".exe", ".txt");
        FileBrowser.AddQuickLink("Users", "C:\\Users", null);
        FileBrowser.ShowLoadDialog((path) => {
            filePathOpen = path;
            drawer.texture.LoadImage(System.IO.File.ReadAllBytes(path));
        }, null, false, Environment.GetFolderPath(Environment.SpecialFolder.MyPictures), "Select Image", "Select");
    }
}

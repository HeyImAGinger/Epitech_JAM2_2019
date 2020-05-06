using SimpleFileBrowser;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SavePictureButton : MonoBehaviour
{
    private string filePathSave;
    private Drawer drawer;

    void Start()
    {
        drawer = GameObject.Find("Fond").GetComponent<Drawer>();
    }

    public void SaveFile()
    {
        FileBrowser.SetFilters(true, new FileBrowser.Filter("JPEG", ".jpg"), new FileBrowser.Filter("PNG", ".png"));
        FileBrowser.SetDefaultFilter(".png");
        FileBrowser.SetExcludedExtensions(".lnk", ".tmp", ".zip", ".rar", ".exe", ".txt");
        FileBrowser.AddQuickLink("Users", "C:\\Users", null);
        FileBrowser.ShowSaveDialog((path) => {
            filePathSave = path;
            File.WriteAllBytes(filePathSave, drawer.texture.EncodeToPNG());
            drawer.edit = false;
        }, null, false, Environment.GetFolderPath(Environment.SpecialFolder.MyPictures), "Save As", "Save");

    }
}

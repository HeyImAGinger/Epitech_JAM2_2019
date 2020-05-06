using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using SimpleFileBrowser;

public class DropdownFile : MonoBehaviour
{
    private Drawer drawer;
    public GameObject saveBeforeQuit, saveBeforeNew;
    public string filePathOpen = "Assets/Sprites/Backgrounds/mur de briques.jpg";
    public string filePathSave { get; set; }

    public void Start()
    {
        drawer = GameObject.Find("Fond").GetComponent<Drawer>();
    }

    private void NewFile()
    {
        //if (drawer.edit)
        //    ;//saveBeforeNew.SetActive(true);
        //else
        drawer.texture.LoadImage(System.IO.File.ReadAllBytes("Assets/Sprites/Backgrounds/mur de briques.jpg"));
    }

    private void OpenFile()
    {
        FileBrowser.SetFilters(true, new FileBrowser.Filter("Images", ".jpg", ".png"));
        FileBrowser.SetDefaultFilter(".png");
        FileBrowser.SetExcludedExtensions(".lnk", ".tmp", ".zip", ".rar", ".exe", ".txt");
        FileBrowser.AddQuickLink("Users", "C:\\Users", null);
        FileBrowser.ShowLoadDialog((path) => {
                filePathOpen = path;
                drawer.texture.LoadImage(System.IO.File.ReadAllBytes(path));
            }, 
                                    () => { Debug.Log( "Canceled" ); }, 
                                    false, Directory.GetCurrentDirectory(), "Select Folder", "Select" );
    }

    public void SaveFile()
    {
        FileBrowser.SetFilters(true, new FileBrowser.Filter("JPEG", ".jpg"), new FileBrowser.Filter("PNG", ".png"));
        FileBrowser.SetDefaultFilter(".png");
        FileBrowser.SetExcludedExtensions(".lnk", ".tmp", ".zip", ".rar", ".exe", ".txt");
        FileBrowser.AddQuickLink("Users", "C:\\Users", null);
        FileBrowser.ShowSaveDialog((path) => { 
                                                Debug.Log("Saved as => " + path); 
                                                filePathSave = path;
                                                File.WriteAllBytes(filePathSave, drawer.texture.EncodeToPNG());
                                                drawer.edit = false;
                                    },
                                    () => { Debug.Log("Canceled"); },
                                    false, Directory.GetCurrentDirectory(), "Save As", "Save");
        
    }

    private void Quit()
    {
        if (drawer.edit)
            saveBeforeQuit.SetActive(true);
        else
            Application.Quit();
    }

    public void CallFileFunction(int value)
    {
        if (value == 0)
            NewFile();
        else if (value == 1)
            OpenFile();
        else if (value == 2)
            SaveFile();
        else if (value == 3)
            Quit();
        //if (value != -1)
        //    GetComponent<Dropdown>().value = -1;
    }
}

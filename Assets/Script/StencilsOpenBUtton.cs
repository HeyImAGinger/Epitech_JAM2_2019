using SimpleFileBrowser;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class StencilsOpenBUtton : MonoBehaviour
{
    public Pochoir stencil;

    public void OpenStencil()
    {
        FileBrowser.SetFilters(true, new FileBrowser.Filter("PNG", ".png"));
        FileBrowser.SetDefaultFilter(".png");
        FileBrowser.SetExcludedExtensions(".lnk", ".tmp", ".zip", ".rar", ".exe", ".txt", ".jpg");
        FileBrowser.AddQuickLink("Users", "C:\\Users", null);
        FileBrowser.ShowLoadDialog((path) => {
            stencil.LoadTexture(path);
        }, null, false, Directory.GetCurrentDirectory() + "/Assets/Sprites/Pochoir", "Select Stencil", "Select");
    }
}

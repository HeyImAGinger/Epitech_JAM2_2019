using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitPopup : MonoBehaviour
{
    public GameObject saver;

    public void SaveQuit()
    {
        saver.GetComponent<SavePictureButton>().SaveFile();
        Quit();
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Cancel()
    {
        gameObject.SetActive(false);
    }
}

using System;
using UnityEngine;
using UnityEngine.UI;

public class color_wheel : MonoBehaviour
{
    private Drawer drawer;
    private Texture2D text;
    private RectTransform myrectTransform;
    private Image colorIndicator;
    bool isInside = false;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Image>().sprite.texture;
        myrectTransform = GetComponent<RectTransform>();
        drawer = GameObject.Find("Fond").GetComponent<Drawer>();
        colorIndicator = GameObject.Find("color_wheel_indicator").GetComponent<Image>();
    }

    public void setIsInside(bool v)
    {
        isInside = v;
    } 

    // Update is called once per frame
    public void Update()
    {
        if (!isInside)
            return;
        Vector2Int pos = new Vector2Int((int)((Input.mousePosition.x - (transform.position.x - myrectTransform.rect.width / 2)) * (text.width / myrectTransform.rect.width)),
                                        (int)((Input.mousePosition.y - (transform.position.y - myrectTransform.rect.height / 2)) * (text.height / myrectTransform.rect.height)));
        if (pos.x >= 0 && pos.y >= 0 && pos.x <= text.width && pos.y <= text.height) {
            drawer.color = text.GetPixel(pos.x, pos.y);
            colorIndicator.color = drawer.color;
        }
    }
}
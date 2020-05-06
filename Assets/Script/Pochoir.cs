using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pochoir : MonoBehaviour
{
    public Rect rect;
    public Texture2D texture;
    private Image image;
    private RectTransform rectTransform;
    private Drawer drawer;

    private bool isDragging;
    private bool drawned;
    private Vector3 offset;

    void Start()
    {
        drawer = GameObject.Find("Fond").GetComponent<Drawer>();
        drawer.pochoir = this;
        image = GetComponent<Image>();
        rectTransform = GetComponent<RectTransform>();
        texture = new Texture2D(100, 100);
        image.sprite = Sprite.Create(texture, new Rect(0, 0, 100f, 100f), new Vector2(0.5f, 0.5f));
    }

    public void LoadTexture(string file)
    {
        image.color = Color.white;
        texture.LoadImage(System.IO.File.ReadAllBytes(file));
        image.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        rectTransform.localScale = new Vector3(1, 1, 1);
        rectTransform.offsetMax = new Vector2(200 * texture.width / texture.height + rectTransform.offsetMin.x, 
                                    200 + rectTransform.offsetMin.y);
        UpdateRect();
    }

    void UpdateRect()
    {
        rect = new Rect((transform.position.x+rectTransform.rect.x*rectTransform.localScale.x - (drawer.transform.position.x+drawer.rectTransform.rect.x)) * drawer.texture.width / drawer.rectTransform.rect.width,
                        (transform.position.y+rectTransform.rect.y*rectTransform.localScale.y - (drawer.transform.position.y+drawer.rectTransform.rect.y)) * drawer.texture.height / drawer.rectTransform.rect.height,
                        rectTransform.rect.width * drawer.texture.width / drawer.rectTransform.rect.width * rectTransform.localScale.x,
                        rectTransform.rect.height * drawer.texture.height / drawer.rectTransform.rect.height * rectTransform.localScale.y);
    }

    public void Draw(int x, int y, Color color)
    {
        drawned = true;
        Color oldColor = texture.GetPixel(x, y);
        texture.SetPixel(x, y, Color.Lerp(oldColor, color, ((Vector4)(color-oldColor)).magnitude));
    }
    
    public void PressedButton()
    {
        if (Input.GetMouseButtonDown(0))// && drawer.rectTransform.rect.Contains(Input.mousePosition))
            drawer.SetDraw(true);
        else if (Input.GetMouseButtonDown(1)) {
            isDragging = true;
            offset = Input.mousePosition - transform.position;
        }
    }

    public void ReleasedButton()
    {
        if (Input.GetMouseButtonUp(0))
            drawer.SetDraw(false);
        else if (Input.GetMouseButtonUp(1))
            isDragging = false;
    }

    void Update()
    {
        if (isDragging) {
            transform.position = Input.mousePosition - offset;
            UpdateRect();
            if (Input.mouseScrollDelta.y != 0) {
                if ((rectTransform.localScale.x < 3f && Input.mouseScrollDelta.y > 0) || (rectTransform.localScale.x > 0.5f && Input.mouseScrollDelta.y < 0)) {
                    rectTransform.localScale += new Vector3(Input.mouseScrollDelta.y/10, Input.mouseScrollDelta.y/10, Input.mouseScrollDelta.y/10);
                }
            }
        }
        if (drawned)
            texture.Apply();
    }
}

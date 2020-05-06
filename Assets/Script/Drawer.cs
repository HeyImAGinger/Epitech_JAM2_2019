using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Drawer : MonoBehaviour
{
    public Texture2D texture;
    public RectTransform rectTransform;

    public bool edit = false;
    public Color color = Color.black;
    private bool draw = false;
    private bool sprayDrawing = true;

    private Vector2 scale;
    private Vector2 oldPos;

    public Slider sizeSlider, pressureSlider;
    public Pochoir pochoir;

    private void DrawPen(int x, int y)
    {
        x = (int)(x * texture.width / rectTransform.rect.width);
        y = (int)(y * texture.height / rectTransform.rect.height);
        for (int i = -(int)sizeSlider.value; i != (int)sizeSlider.value; i++)
            for (int j = -(int)Math.Ceiling(sizeSlider.value/5); j != (int)sizeSlider.value/5; j++) {
                double angle = -Math.PI/4;
                Vector2 pos = new Vector2((float)(x+i*Math.Cos(angle)+j*Math.Sin(angle)), (float)(y+j*Math.Cos(angle)-i*Math.Sin(angle)));

                if (pochoir.rect.Contains(pos) && 
                    pochoir.texture.GetPixel((int)((pos.x-pochoir.rect.x)*pochoir.texture.width/pochoir.rect.width), 
                                             (int)((pos.y-pochoir.rect.y)*pochoir.texture.height/pochoir.rect.height)).a >= 0.5f) {
                    pochoir.Draw((int)((pos.x-pochoir.rect.x)*pochoir.texture.width/pochoir.rect.width), 
                                 (int)((pos.y-pochoir.rect.y)*pochoir.texture.height/pochoir.rect.height), color);
                    continue;
                }

                if (pos.x >= 0 && pos.x < texture.width && pos.y >= 0 && pos.y < texture.height) {
                    edit = true;
                    Color oldColor = texture.GetPixel((int)Math.Round(pos.x), (int)Math.Round(pos.y));
                    texture.SetPixel((int)Math.Round(pos.x), (int)Math.Round(pos.y), Color.Lerp(oldColor, color, ((Vector4)(color-oldColor)).magnitude));
                }
            }
    }

    private void DrawSpray(int x, int y)
    {
        x = (int)(x * texture.width / rectTransform.rect.width);
        y = (int)(y * texture.height / rectTransform.rect.height);
        for (int i = 0; i < sizeSlider.value*sizeSlider.value*pressureSlider.value; i++) {
            Vector2 pos = UnityEngine.Random.insideUnitCircle * sizeSlider.value * (float)(1-Math.Pow(UnityEngine.Random.value, 2));
            pos += new Vector2(x, y);

            if (pochoir.rect.Contains(pos) && 
                pochoir.texture.GetPixel((int)((pos.x-pochoir.rect.x)*pochoir.texture.width/pochoir.rect.width), 
                                         (int)((pos.y-pochoir.rect.y)*pochoir.texture.height/pochoir.rect.height)).a >= 0.5f) {
                pochoir.Draw((int)((pos.x-pochoir.rect.x)*pochoir.texture.width/pochoir.rect.width), 
                            (int)((pos.y-pochoir.rect.y)*pochoir.texture.height/pochoir.rect.height), color);
                continue;
            }

            if (pos.x >= 0 && pos.x < texture.width && pos.y >= 0 && pos.y < texture.height) {
                edit = true;
                Color oldColor = texture.GetPixel((int)Math.Round(pos.x), (int)Math.Round(pos.y));
                texture.SetPixel((int)Math.Round(pos.x), (int)Math.Round(pos.y), Color.Lerp(oldColor, color, ((Vector4)(color-oldColor)).magnitude));
            }
        }
    }

    private void AllDraw(int newX, int newY)
    {
        int nb = (int)Math.Ceiling(Vector2.Distance(oldPos, new Vector2(newX, newY)) / sizeSlider.value);
        if (!sprayDrawing)
            nb *= 5;
        if (nb == 0)
            nb = 1;
        for (int i = 0; i != nb; i++) {
            int x = (int)(oldPos.x + (newX-oldPos.x) * (i+1) / nb);
            int y = (int)(oldPos.y + (newY-oldPos.y) * (i+1) / nb);
            if (sprayDrawing)
                DrawSpray(x, y);
            else
                DrawPen(x, y);
        }
    }

    public void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        texture = new Texture2D((int)rectTransform.rect.width, (int)rectTransform.rect.height);
        texture.LoadImage(System.IO.File.ReadAllBytes("Assets/Sprites/Backgrounds/mur de briques.jpg"));
        
        Sprite s = Sprite.Create(texture, new Rect(0f, 0f, texture.width, texture.height), new Vector2(0.5f,0.5f), 100f);
        GetComponent<Image>().sprite = s;
        scale = new Vector2(rectTransform.rect.width / s.rect.width, rectTransform.rect.height / s.rect.height);
    }

    public void setSprayDraw(bool _value)
    {
        sprayDrawing = _value;
    }

    public void SetDraw(bool _draw)
    {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonUp(0))
            draw = _draw;
    }

    public void Update()
    {
        Vector2 pos = new Vector2(Input.mousePosition.x-(transform.position.x-rectTransform.rect.width/2),
                        Input.mousePosition.y-(transform.position.y-rectTransform.rect.height/2));
        if (draw) {
            AllDraw((int)pos.x, (int)pos.y);
            texture.Apply();
        }
        oldPos = pos;
        if (!sprayDrawing && pressureSlider.gameObject.activeInHierarchy)
            pressureSlider.gameObject.SetActive(false);
        else if (sprayDrawing && !pressureSlider.gameObject.activeInHierarchy)
            pressureSlider.gameObject.SetActive(true);
        //print(texture.GetPixel(0, 0));
        //print(color);
    }
}

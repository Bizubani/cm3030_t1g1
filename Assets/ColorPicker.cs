﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.Events;

[Serializable]
public class ColorEvent : UnityEvent<Color>{ }

public class ColorPicker : MonoBehaviour
{
    // public TextMeshProUGUI DebugText;
    public ColorEvent OnColorPreview;
    public ColorEvent OnColorSelect;
    RectTransform Rect;
    Texture2D ColorTexture;

    //Renderer rend;

    public Material mat;
    // Start is called before the first frame update
    void Start()
    {
        Rect = GetComponent<RectTransform>();
        ColorTexture = GetComponent<Image>().mainTexture as Texture2D;
        //rend = GameObject.Find("Cube Material").GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(RectTransformUtility.RectangleContainsScreenPoint(Rect, Input.mousePosition))
        {
            Vector2 delta;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(Rect, Input.mousePosition, null, out delta);

            string debug = "mousePosition" + Input.mousePosition;

            debug += "<br>delta" + delta;

            float width = Rect.rect.width;
            float height = Rect.rect.height;
            delta += new Vector2(width * 0.5f, height * 0.5f);
            debug += "<br>offset delta=" + delta;

            float x = Mathf.Clamp(delta.x / width, 0f, 1f);
            float y = Mathf.Clamp(delta.y / height, 0f, 1f);
            debug += "<br>x=" + x + " y=" + y;

            int texX = Mathf.RoundToInt(x * ColorTexture.width);
            int texY = Mathf.RoundToInt(y * ColorTexture.height);
            debug += "<br>texX=" + texX + " texY=" + texY;

            Color color = ColorTexture.GetPixel(texX, texY);

            // DebugText.color = color;
            // DebugText.text = debug;

            OnColorPreview?.Invoke(color);

            if(Input.GetMouseButtonDown(0))
            {
                OnColorSelect?.Invoke(color);
                mat.SetColor("_Color", color);
            }
        }
    }
}
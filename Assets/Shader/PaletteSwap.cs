using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaletteSwap : MonoBehaviour
{
    [SerializeField]
    Texture2D mColorSwapTex;
    [SerializeField]
    Color[] mSpriteColors;

    public Color base1 = Color.white;
    public List<Color> group1;
    public Color changeTo1 = Color.gray;
    
    

    public void InitColorSwapTex()
    {
        Texture2D colorSwapTex = new Texture2D(256, 1, TextureFormat.RGBA32, false, false);
        colorSwapTex.filterMode = FilterMode.Point;
        for (int i = 0; i < colorSwapTex.width; ++i)
            colorSwapTex.SetPixel(i, 0, new Color(0.0f, 0.0f, 0.0f, 0.0f));

        colorSwapTex.Apply();

        GetComponent<SpriteRenderer>().material.SetTexture("_SwapTex", colorSwapTex);

        mSpriteColors = new Color[colorSwapTex.width];
        mColorSwapTex = colorSwapTex;
    }

    public void SwapColor(int index, Color color)
    {

        mSpriteColors[(int)index] = color;
        mColorSwapTex.SetPixel((int)index, 0, color);
    }

    public void SwapColor(Color change, Color color)
    {

        mSpriteColors[(int)(change.r * 255)] = color;
        mColorSwapTex.SetPixel((int)(change.r * 255), 0, color);
    }

    public Color fromColor(Color c) {
        return new Color(c.r, c.g, c.b, c.a);
    }

    public Color fromShade(int shade) {
        return new Color((int)shade/255.0f, (int)shade / 255.0f, (int)shade / 255.0f);
    }

    protected virtual void SwapColors() {
        float h1, s1, v1;
        float h2, s2, v2;

        Color.RGBToHSV(base1, out h1, out s1, out v1);
        Color.RGBToHSV(changeTo1, out h2, out s2, out v2);

        float hdif = h2 - h1;
        float sdif = s2 - s1;
        float vdif = v2 - v1;

        foreach (Color c in group1) {
            //make new color
            float h_here, s_here, v_here;
            Color.RGBToHSV(c, out h_here, out s_here, out v_here);

            float h_new = h_here + hdif;
            if (h_new > 1.0f) {
                h_new = h_new % 1.0f;
            }
            else if (h_new < 0) {
                h_new += 1.0f; 
            }

            float s_new = s_here + sdif;
            if (s_new > 1.0f)
            {
                s_new = 1.0f;
            }
            else if (s_new < 0)
            {
                s_new = 0;
            }

            float v_new = v_here + vdif;
            if (v_new > 1.0f)
            {
                v_new = 1.0f;
            }
            else if (v_new < 0)
            {
                v_new = 0;
            }

            SwapColor(c, Color.HSVToRGB(h_new, s_new, v_new));
        }


        /*Color set1 = fromColor(target1),
            set2 = fromColor(target2),
            set3 = fromColor(target3),
            set4 = fromColor(target4);

        
        
        
        SwapColor(SHADES.Gray1, set1);
        SwapColor(SHADES.Gray2, set2);
        SwapColor(SHADES.Gray3, set3);
        SwapColor(SHADES.Gray4, set4);*/
        mColorSwapTex.Apply();
    }

    // Start is called before the first frame update
    void Awake()
    {
        InitColorSwapTex();
        SwapColors();
    }

    // Update is called once per frame
    void Update()
    {
        //SwapColors();
    }
}

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ImageUtil
{
    public static void ChangeSprite(GameObject gameObject, Sprite sprite)
    {
        Image image = gameObject.GetComponent(typeof(Image)) as Image;
        if (image != null)
        {
            image.overrideSprite = sprite;
            Debug.Log("overrided sprite");
        }
    }
    
    public static void ChangeAlpha(GameObject gameObject, float alpha)
    {
        Image image = gameObject.GetComponent(typeof(Image)) as Image;
        if (image != null)
        {
            Color color = image.color;
            color.a = alpha;
            image.color = color;    
            Debug.Log("alpha setted to " + alpha);
        }
    }
    
}


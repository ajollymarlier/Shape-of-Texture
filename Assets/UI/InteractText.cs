using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractText : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        rectTransform.offsetMin = new Vector2(rectTransform.offsetMin.x, -200);
        rectTransform.offsetMax = new Vector2(rectTransform.offsetMax.x, -200);
    }

}

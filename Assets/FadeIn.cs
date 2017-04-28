using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeIn : MonoBehaviour
{

    private CanvasGroup canvasGroup;

    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void Tween()
    {
        iTween.ValueTo(gameObject, iTween.Hash(
            "from", 0,
            "to", 1,
            "time", 2f,
            "onupdate", "OnUpdate"
        ));
    }

    void OnUpdate(float value)
    {
        canvasGroup.alpha = value;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CCoolDownUI : MonoBehaviour
{
    #region variable
    private float fCurrentValue;            // åªç›ÇÃêîíl
    private Image image;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        fCurrentValue = 0.0f;
        image = GetComponent<Image>();  
    }

    // Update is called once per frame
    void Update()
    {
        image.fillAmount = fCurrentValue;
    }

    public void GetCoolDownTime(float value)
    {
        fCurrentValue = value;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PauseMause : MonoBehaviour, IPointerEnterHandler
{
    PauseScrip PS;

    // Start is called before the first frame update
    void Start()
    {
        PS = GameObject.Find("EventSystem").GetComponent<PauseScrip>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (this.gameObject.name == "Option_Button")
        {
            PS.SetButton(0);
            PS.SetButtonAny();
        }
        else if (this.gameObject.name == "Controll_Button")
        {
            PS.SetButton(1);
            PS.SetButtonAny();
        }
        else if (this.gameObject.name == "Title_Button")
        {
            PS.SetButton(2);
            PS.SetButtonAny(); 
        }
    }

}

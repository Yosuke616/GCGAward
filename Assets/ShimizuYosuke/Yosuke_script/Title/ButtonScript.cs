using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonScript : MonoBehaviour, IPointerEnterHandler
{
    TitleScript TS;

    // Start is called before the first frame update
    void Start()
    {
        TS = GameObject.Find("EventSystem").GetComponent<TitleScript>();
    }

    // Update is called once per frame
    void Update()
    {
    
        
    }

    public void OnPointerEnter(PointerEventData eventData) {
        if (this.gameObject.name == "Start")
        {
            TS.SetButton(0);
            TS.SetButtonAny();
        } else if (this.gameObject.name == "Tutorial") {
            TS.SetButton(1);
            TS.SetButtonAny();
        }
        else if (this.gameObject.name == "Option")
        {
            TS.SetButton(2);
            TS.SetButtonAny();
        }
        else if (this.gameObject.name == "End")
        {
            TS.SetButton(3);
            TS.SetButtonAny();
        }
        else {
            TS.SetButton(4);
            TS.SetButtonAny();
        }
    }
}

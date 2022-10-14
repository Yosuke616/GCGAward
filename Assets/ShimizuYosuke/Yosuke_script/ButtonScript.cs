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
        }
        else if (this.gameObject.name == "Option")
        {
            TS.SetButton(1);

        }
        else if (this.gameObject.name == "End")
        {
            TS.SetButton(2);
        }
    }
}

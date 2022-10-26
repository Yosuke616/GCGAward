using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameOverMouse : MonoBehaviour, IPointerEnterHandler
{
    private GameOverScript GOS;

    // Start is called before the first frame update
    void Start()
    {
        GOS = GameObject.Find("EventSystem").GetComponent<GameOverScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (this.gameObject.name == "Retory") {
            GOS.SetButton(0);
            GOS.SetButtonAny();
        } else if (this.gameObject.name == "Title") {
            GOS.SetButton(1);
            GOS.SetButtonAny();
        }
    }
}

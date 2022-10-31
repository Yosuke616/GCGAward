using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CTutorialPlayer : MonoBehaviour
{
    private CSenaPlayer player;
    [SerializeField] FadeManager fadeManager;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<CSenaPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.nCurrentHp <= 0)
            fadeManager.SceneOut("TutorialScene");
    }
}

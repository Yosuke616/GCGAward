using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CWapeZone : MonoBehaviour
{
    [SerializeField] private FadeManager fadeManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        // �v���C���[������������Q�[���V�[���ɑJ�ڂ���
        if(collision.gameObject.tag == "Player")
        {
            fadeManager.SceneOut("GameScene");
        }
    }
}

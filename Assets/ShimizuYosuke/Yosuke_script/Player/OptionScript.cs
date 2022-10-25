using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class OptionScript : MonoBehaviour
{
    //���Ԑݒ�
    [Header("�ǂ̈ʂ̎��ԂŃ{�^�����䂷�邩")]
    [SerializeField] private int DELTTIME = 10;
    private int nDeltTime;

    //��~���ɓ�������I�u�W�F�N�g
    [SerializeField] private GameObject Option_UI;
    private bool pauseFlg = false;

    private void Awake()
    {
        //60fps
        Application.targetFrameRate = 60;
    }

    // Start is called before the first frame update
    void Start()
    {
        Option_UI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        nDeltTime++;
        pauseFlg = true;
        //���Ԃŉ�����悤�ɂ���
        if (nDeltTime > DELTTIME) {
            if (Input.GetKey(KeyCode.Escape)) {
                if (Mathf.Approximately(Time.timeScale, 1f))
                {
                    Time.timeScale = 0f;
                    Option_UI.SetActive(true);
                }
                else
                {
                    Time.timeScale = 1f;
                    Option_UI.SetActive(false);
                }
                nDeltTime = 0;
            }
            if (PlayerInputTest.GetControllerUse())
            {


                if (Gamepad.current.startButton.isPressed&&pauseFlg)
                {
                    pauseFlg = false;
                    if (Mathf.Approximately(Time.timeScale, 1f))
                    {
                        Time.timeScale = 0f;
                        Option_UI.SetActive(true);
                    }
                    else
                    {
                        Time.timeScale = 1f;
                        Option_UI.SetActive(false);
                    }
                    nDeltTime = 0;
                }
                else
                {
                    
                }
            }
        }
    }
}

/**
 * @file        Player_Walk
 * @author      Shimizu_Yosuke
 * @date        2022/10/5
 * @brief       �v���C���[�̈ړ��𐧌䂷��
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Walk : MonoBehaviour
{

    /** @brief �v���C���[�̈ړ����x*/
    [Header("�v���C���[�̈ړ��X�s�[�h")]
    [SerializeField] float fPlayerWalk = 2.0f;

    void Awake()
    {
        Application.targetFrameRate = 60; //60FPS�ɐݒ�
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /**WASD�Ńv���C���[�𓮂����܂�**/
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �v���W�F�N�g���̃V�[�������Ǘ�����N���X 
public class CSceneName : ScriptableObject
{
    public enum SCENENAME
    {
        TITLESCENE = 0,     // �^�C�g���V�[��
        SELECTSCENE,        // �X�e�[�W�Z���N�g�V�[��
        GAMESCENE,          // �Q�[���V�[��
        RESULTSCENE,        // ���U���g�V�[��
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CFieldUIController : MonoBehaviour
{
    #region serialize field
    [Header("�e�N�X�`��")]
    [SerializeField] private Sprite[] UITex;        // �\��A�j���[�V����
    [Header("�ؑ֊Ԋu����(�b)")]
    [SerializeField] private float nChangeTime;       // �؂�ւ��܂ł̎���
    #endregion

    #region variable
    private Image imgUI;        // �e�N�X�`����\��Image
    private float nCurrentTime;     // ���݂̌o�ߎ���
    private int nTexNum;            // �e�N�X�`���̍��v����
    private int nCurrentTexNum;     // ���ݕ\�����Ă���e�N�X�`���̔ԍ�
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        nCurrentTime = 0.0f;
        nCurrentTexNum = 0;
        imgUI = GetComponent<Image>();
        imgUI.sprite = UITex[nCurrentTexNum];
        nTexNum = UITex.Length;
    }

    // Update is called once per frame
    void Update()
    {
        nCurrentTime += Time.deltaTime;
        if(nCurrentTime > nChangeTime)
        {
            // �e�N�X�`���̐ؑ�
            ++nCurrentTexNum;
            if (nCurrentTexNum > nTexNum - 1)
                nCurrentTexNum = 0;
            imgUI.sprite = UITex[nCurrentTexNum];
            nCurrentTime = 0.0f;
        }
    }
}

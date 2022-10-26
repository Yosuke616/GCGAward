using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CTitleButton : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    [Header("�g�嗦")]
    [SerializeField, Range(1.0f, 2.0f)] private float magScale;      // mag��magnification(�{��)
    [Header("�e�N�X�`��(�m�[�}�����I����Ԃ̏���)")]
    public Sprite[] texture;
    [Header("�J�ڐ�V�[���̖��O")]
    [SerializeField] private string szSceneName;
    [SerializeField] private CSceneTitle cScene;        // �^�C�g���}�l�[�W���[�X�N���v�g

    private Vector2 defScale;       // �X�P�[���̏����l
    private Image image;            // �{�^���̉摜
    private bool isSelected;
    // Start is called before the first frame update
    void Awake()
    {
        isSelected = false;
        // �摜�̏����擾����
        image = GetComponent<Image>();
        // ������Ԃ̉摜��ݒ肷��
        image.sprite = texture[0];
        // �X�P�[���̏����l��ݒ肷��
        defScale = transform.localScale;
    }
    void Start()
    {   
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    // �{�^���������ꂽ�Ƃ��V�[���J�ڂ���
    public void OnClick()
    {
        if(szSceneName == "Option" || szSceneName == "Quit")
        {
            // �I�v�V��������or�Q�[���I������
        }
        else 
        {
            SceneManager.LoadScene(szSceneName.ToString());
        }
    }

    // �}�E�X��UI��ɗ�����
    #region on pointer enter
    public void OnPointerEnter(PointerEventData eventData)
    {
        //SetTexture(true);               // �e�N�X�`����ύX����
        cScene.MouseMove(true);         // �}�E�X���쒆�ɂ���
        isSelected = true;
    }
    #endregion

    // �}�E�X��UI�O�ɏo����
    #region on pointer exit
    public void OnPointerExit(PointerEventData eventData)
    {
        //SetTexture(false);                   // �e�N�X�`����ύX����
        cScene.MouseMove(false);         // �}�E�X���쒆��Ԃ���������
        isSelected = false;
    }
    #endregion

    // �I������Ă��鎞�ɌĂ΂��֐�
    #region set selected
    public void SetSelected(bool flg)
    {
        isSelected = flg;
        SetTexture(isSelected);
    }
    #endregion

    // �e�N�X�`���̕ύX
    #region set texture
    private void SetTexture(bool flg)
    {
        // �I������Ă����ԂɕύX����
        if(flg)
        {
            transform.localScale = new Vector2(transform.localScale.x * magScale, transform.localScale.y * magScale);
            image.sprite = texture[1];
        }
        // �I������Ă��Ȃ���ԂɕύX����
        else
        {
            transform.localScale = defScale;
            image.sprite = texture[0];
        }
    }
    #endregion

    // �I�𒆂��ǂ����̏���n��
    #region get is selected
    public bool GetIsSelected()
    {
        return isSelected;
    }
    #endregion 
}

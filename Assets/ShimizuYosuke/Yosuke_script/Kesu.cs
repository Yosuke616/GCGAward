using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Kesu : MonoBehaviour
{
    [SerializeField] private GameObject kemuri; 

    private const string key_isWalk = "isWalk";
    private const string key_isRun = "isRun";
    private const string key_isFrontWalk = "isFrontWalk";
    private const string key_isBackWalk = "isBackWalk";
    private const string key_isRightWalk = "isRightWalk";
    private const string key_isLeftkey = "isLeftWalk";
    private Animator animator;

    private bool flg = false;
    private bool walk = false;

    // Start is called before the first frame update
    void Start()
    {
        this.animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!Input.anyKey) {
            this.animator.SetBool(key_isWalk,false);
            this.animator.SetBool(key_isRun,false);
            this.animator.SetBool(key_isFrontWalk, false);
            this.animator.SetBool(key_isBackWalk, false);
            this.animator.SetBool(key_isRightWalk, false);
            this.animator.SetBool(key_isLeftkey, false);
            flg = false;
            walk = false;
        }

        if (!Input.GetMouseButton(0))
        {
            this.animator.SetBool(key_isFrontWalk, false);
            this.animator.SetBool(key_isBackWalk, false);
            this.animator.SetBool(key_isRightWalk, false);
            this.animator.SetBool(key_isLeftkey, false);
        }

        if (Input.GetKey(KeyCode.LeftShift)) {
            this.animator.SetBool(key_isWalk,false);
            this.animator.SetBool(key_isRun, false);
            this.animator.SetBool(key_isFrontWalk, false);
            this.animator.SetBool(key_isBackWalk, false);
            this.animator.SetBool(key_isRightWalk, false);
            this.animator.SetBool(key_isLeftkey, false);
            flg = false;
            walk = false;
        }

        if (Input.GetMouseButton(0))
        {
            walk = true;
            this.animator.SetBool(key_isFrontWalk, false);
            this.animator.SetBool(key_isBackWalk, false);
            this.animator.SetBool(key_isRightWalk, false);
            this.animator.SetBool(key_isLeftkey, false);
            if (Input.GetKey(KeyCode.W))
            {
                this.animator.SetBool(key_isFrontWalk, true);
            }
            if (Input.GetKey(KeyCode.S))
            {
                this.animator.SetBool(key_isBackWalk, true);
            }
            if (Input.GetKey(KeyCode.A))
            {
                this.animator.SetBool(key_isLeftkey, true);
            }
            if (Input.GetKey(KeyCode.D))
            {
                this.animator.SetBool(key_isRightWalk, true);
            }

        }

        if (!walk)
        {
            if (Input.GetKey(KeyCode.W))
            {
                this.animator.SetBool(key_isWalk, true);
                flg = true;
            }

            if (flg)
            {
                this.animator.SetBool(key_isWalk, true);
                this.animator.SetBool(key_isRun, false);

                if (Input.GetKey(KeyCode.LeftShift))
                {
                    this.animator.SetBool(key_isRun, true);
                }
            }
        }
    }

    public void KEMURI_R() {
        GameObject unity = GameObject.Find("IdlePlayer");

        GameObject obj =  Instantiate(kemuri,new Vector3(0,0,0),Quaternion.identity);

        obj.transform.position = unity.transform.position;
        obj.transform.position -= this.transform.up * 0.15f;
        obj.transform.position -= this.transform.forward * 0.25f;
        obj.transform.position += this.transform.right * 0.15f;

        obj.transform.localScale = new Vector3(0.03f,0.03f,0.03f);
        obj.AddComponent<KesuDeath>();
    }

    public void KEMURI_L()
    {
        GameObject unity = GameObject.Find("IdlePlayer");

        GameObject obj = Instantiate(kemuri, new Vector3(0, 0, 0), Quaternion.identity);

        obj.transform.position = unity.transform.position;
        obj.transform.position -= this.transform.up * 0.15f;
        obj.transform.position -= this.transform.forward * 0.25f;
        obj.transform.position -= this.transform.right * 0.15f;

        obj.transform.localScale = new Vector3(0.03f, 0.03f, 0.03f);
        obj.AddComponent<KesuDeath>();
    }

}

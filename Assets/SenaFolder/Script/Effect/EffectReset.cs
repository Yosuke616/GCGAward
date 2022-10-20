using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Effekseer;

public class EffectReset : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        EffekseerEffectAsset effect = this.GetComponent<EffekseerEmitter>().effectAsset;
        EffekseerHandle handle = EffekseerSystem.PlayEffect(effect, transform.position);
        handle.SetRotation(transform.rotation);
        handle.SetLocation(transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

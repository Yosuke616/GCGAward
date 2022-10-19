using Effekseer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEffectManager : MonoBehaviour
{
    [SerializeField] private EffekseerEmitter[] emittersEffect;
    
    public EffekseerEmitter[] GetEmittersEff()
    {
        return emittersEffect;
    }
}

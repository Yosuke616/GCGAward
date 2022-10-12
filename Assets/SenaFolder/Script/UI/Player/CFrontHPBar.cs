using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

#if UNITY_EDITOR
[CustomEditor(typeof(CHPBar))]
#endif

public class CFrontHPBar : CHPBar
{
   public override void AddValue(int num)
    {
        nCurrentValue += num;
        SetValue(nCurrentValue, nMaxValue);
    }
}

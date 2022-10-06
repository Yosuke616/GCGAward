using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// プロジェクト内のシーン名を管理するクラス 
public class CSceneName : ScriptableObject
{
    public enum SCENENAME
    {
        TITLESCENE = 0,     // タイトルシーン
        SELECTSCENE,        // ステージセレクトシーン
        GAMESCENE,          // ゲームシーン
        RESULTSCENE,        // リザルトシーン
    }
}

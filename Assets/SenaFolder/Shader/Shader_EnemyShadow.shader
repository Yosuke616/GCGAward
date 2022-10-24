Shader "Unlit/Shader_EnemyShadow"
{
    Properties
    {
       _Mask("Mask", Int) = 1
        _MainTex("Texture", 2D) = "white"{}
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" 
                "Queue" = "Transparent"
                "IgnoreProjector" = "True"}
        LOD 100
        Blend SrcAlpha OneMinusSrcAlpha
        ZTest Always

        Pass
        {
            Stencil{
            Ref[_Mask]
            Comp Equal
            }
            CGPROGRAM
            #pragma vertex vert_img
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            
        fixed4 frag(v2f_img i) : SV_Target
        {
            float alpha = tex2D(_MainTex, i.uv).a;
            fixed4 col = fixed4(1.0f, 0.49f, 0.44f, alpha);
            return col;
        }
            
            ENDCG
        }
    }
}

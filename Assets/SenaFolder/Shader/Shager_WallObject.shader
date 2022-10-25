Shader "Unlit/Shager_WallObject"
{
    Properties
    {
        _Mask("Mask", Int) = 1
        _MainTex("Texture", 2D) = "white"{}
    }
        SubShader
    {
        Tags { "RenderType" = "Transparent" "Queue" = "Geometry" }
        Blend SrcAlpha OneMinusSrcAlpha
        Pass
        {
            Stencil{
            Ref[_Mask]
            Comp always
            Pass replace
            }
            CGPROGRAM
            sampler2D _MainTex;
            #pragma vertex vert_img
            #pragma fragment frag

            #include "UnityCG.cginc"

            fixed4 frag(v2f_img i) : SV_Target
            {
                //fixed4 c = tex2D(_MainTex, i.uv);
                fixed4 c = fixed4(0.0092f, 0.1037f, 0.0176f,1.0f);

                return c;
            }
            ENDCG
        }
    }
}

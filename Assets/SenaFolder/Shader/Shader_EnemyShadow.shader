Shader "Unlit/Shader_EnemyShadow"
{
    Properties
    {
       _Mask("Mask", Int) = 1
        _MainTex("Texture", 2D) = "white"{}
    }
    SubShader
    {
        Tags { "RenderType" = "Transparent"
                "Queue" = "Transparent"
                "IgnoreProjector" = "True"}
        LOD 100
        Blend SrcAlpha OneMinusSrcAlpha
        ZTest Always

        // Pass01
        Pass
        {
          /*  ColorMask 0
            ZTest Always
            ZWrite Off*/
            Stencil
            {
                Ref[_Mask]
                Comp Equal
            }

        //// Pass02
        //Pass
        //{
        //    ColorMask 0
        //    Stencil
        //    {
        //        Ref 3
        //        Comp Always
        //        Pass Replace
        //    }
        //}

        //// Pass03
        //Pass
        //{
        //    ZTest Always
        //    Stencil
        //    {
        //        Ref 2
        //        Comp Equal
        //    }
        CGPROGRAM
        // •`‰æ
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

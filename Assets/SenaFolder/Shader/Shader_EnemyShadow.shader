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

        //// Pass01
        //Pass
        //{
        //  /*  ColorMask 0
        //    ZTest Always
        //    ZWrite Off*/
        //    Stencil
        //    {
        //        Ref[_Mask]
        //        Comp Equal
        //    }
        // Pass01
        Pass
        {
            // Pass01
            Stencil
            {
                Ref [_Mask]
                Comp Equal
            }
            /*ColorMask 0
            ZTest Always
            ZWrite Off*/

        // Pass02
      /*  Pass
        {
            Stencil
            {
                Ref 3
                Comp Always
                Pass Replace
            }
        }*/
        // Pass03
       /* Pass
        {
            Stencil
            {
                Ref 2
                Comp Equal
            }
            ZTest Always
        }*/
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

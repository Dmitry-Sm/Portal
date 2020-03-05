Shader "Unlit/crtShader"
{
    Properties
    {
        _Tex("InputTex", 2D) = "white" {}
    }
    SubShader
    {

        Lighting Off
        // Blend One One

        Pass
        {
            CGPROGRAM
            #pragma vertex CustomRenderTextureVertexShader
            #pragma fragment frag
            #pragma target 3.0
            #include "UnityCustomRenderTexture.cginc"

            sampler2D   _Tex;

            float4 frag(v2f_customrendertexture IN) : COLOR
            {
                float2 uv = IN.localTexcoord.xy;
                fixed4 st = tex2D(_SelfTexture2D, uv);
                // uv.x *= 2;
                fixed4 t = tex2D(_Tex, uv);
                // uv.x -= 1;
                // fixed4 t2 = tex2D(_Tex2, uv);
                // fixed4 col = tex2D(_SelfTexture2D, uv);
                
                // uv.x += col.r * 0.01;
                // uv.y -= col.g * 0.01;
                // fixed4 dcol = tex2D(_SelfTexture2D, uv);
                // dcol.r = lerp(dcol.r, col.r, 0.1);
                // t = lerp(t1, t2, step(0.5, uv.x));
                // float4 res = 0;
                // res.rg = uv* 0.01;
                // res.a = 1;
                // st.r = 0;
                t = lerp(st, t, 0.8);

                t.a = 1;
                

                return t;
            }
            ENDCG
        }
    }
}

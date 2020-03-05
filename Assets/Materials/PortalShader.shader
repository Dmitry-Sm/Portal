Shader "Unlit/PortalShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        LOD 100
        // BlendOp Add
        Blend SrcAlpha OneMinusSrcAlpha  
        Lighting Off


        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
				float4 screenPos : TEXCOORD1;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.screenPos = ComputeScreenPos(o.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);

                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                float time = _Time.y;
                float PI = 3.1415;
				i.screenPos /= i.screenPos.w;
                float2 uv = i.uv;
                float2 cuv = i.uv - float2(0.5, 0.5);
                float dist = length(cuv);
                float edge = 1. - smoothstep(0.35, 0.5, dist);
                float a = atan2(cuv.y, cuv.x);
                float disp1 = sin(a * 6. + dist * 120. - time * 6.);
                float disp2 = sin(-a * 4. + dist * 12. - time * 2.);
                float disp3 = lerp(disp1, disp2, sin(time) * 0.01 + 0.45);
                disp3 = smoothstep(-1. - edge, .0 - edge, disp3);
                disp3 *= clamp(sin(edge * PI), 0., 1.);
                float window = 1. - smoothstep(0.35, 0.4, dist);
                float4 col = 0;
                col.a = clamp(window + disp3 * edge, 0., 1.);

                float2 tuv = i.screenPos.xy + float2(disp3, disp3) * (1. - window) * 0.2;
                float4 t = tex2D(_MainTex, tuv);
                // float4 t = tex2D(_MainTex, tuv);

                float3 blue = float3(0.1, 0.3, 0.85);
                col.rgb = lerp(blue, t, smoothstep(0.8, 1., edge));

                // col.rgb = 0;
                // col.r = window;
                // col.r = edge;
                // col.g = disp3;

                return col;
            }
            ENDCG
        }
    }
}

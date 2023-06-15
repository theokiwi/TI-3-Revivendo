Shader "Custom/HiglightShader"
{
    Properties
    {
        _Color ("Color", Color) = (1, 1, 1, 1)
        _MainTex ("Texture", 2D) = "white" {}
        _HighlightColor ("Highlight Color", Color) = (1, 1, 1, 1)
        _HighlightStrength ("Highlight Strength", Range(0.0, 1.0)) = 0.0
        _Omega ("Oscillation Frequency", Float) = 1.0
        [Toggle] _Oscillating ("Oscillating", Float) = 0
    }
    SubShader
    {
        Tags {"RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #pragma shader_feature _OSCILLATING_ON

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
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _HighlightColor;
            float _HighlightStrength;
            fixed4 _Color;
            float _Omega;


            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv) * _Color;
                #if _OSCILLATING_ON
                    _HighlightStrength = 0.5 * sin(_Time * _Omega) + 0.7;  
                #endif
                col = lerp(col, _HighlightColor, _HighlightStrength);
                return col;
            }
            ENDCG
        }
    }
}

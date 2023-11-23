Shader "Unlit/Toon"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("Color", Color) = (0, 1, 0, 1)
        [HDR] _AmbientColor ("Ambient Color", Color) = (0, 0, 0, 1)
        [HDR] _EspecularColor ("Especular Color", Color) = (1, 1, 1, 1)
        _Glossiness ("Glossiness", float) = 32
        _RimAmount ("Rim Light Amount", Range(0, 1)) = 0.5
        [HDR] _RimColor ("Rim Color", Color) = (1, 1, 1, 1)
        _RimThreshold ("Rim Threshold", Range(0, 1)) = 0.5
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            Tags
            {
                "LightMode" = "ForwardBase"
                "PassFlags" = "OnlyDirectional"
            }
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #include "AutoLight.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float3 viewDir : TEXCOORD1;
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float3 worldNormal : NORMAL;
                SHADOW_COORDS(2)
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _Color;
            float4 _AmbientColor;
            float4 _EspecularColor;
            float _Glossiness;
            float _RimAmount;
            float4 _RimColor;
            float _RimThreshold;

            v2f vert (appdata v)
            {
                v2f o;
                o.viewDir = WorldSpaceViewDir(v.vertex);
                o.worldNormal = UnityObjectToWorldNormal(v.normal);
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float3 normal = normalize(i.worldNormal);
                float NdotL = dot(_WorldSpaceLightPos0, normal);
                float lightIntensity = smoothstep(0, 0.01, NdotL);
                float4 light = lightIntensity * _LightColor0;
                float3 viewDir = normalize(i.viewDir);

                float3 halfVector = normalize(_WorldSpaceLightPos0 + viewDir);
                float NdotH = dot(normal, halfVector);

                float specularIntensity = pow(NdotH * lightIntensity, _Glossiness * _Glossiness);
                float specularIntensitySmooth = smoothstep(0.005, 0.01, specularIntensity);
                float4 specular = specularIntensitySmooth + _EspecularColor;
                float4 rimDot = 1 - dot(viewDir,normal);
                float rimIntensity = rimDot * pow(NdotL, _RimThreshold);
                rimIntensity = smoothstep(_RimAmount - 0.01, _RimAmount + 0.01, rimIntensity);
                float4 rim = rimIntensity * _RimColor;

                float4 sample = tex2D(_MainTex, i.uv);

                return _Color * sample * (_AmbientColor + light + specular + rim);
            }
            ENDCG
        }
        UsePass "Legacy Shaders/VertexLit/SHADOWCASTER"
    }
}


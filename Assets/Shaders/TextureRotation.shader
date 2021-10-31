// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/UV rotation"
{
    Properties
    {
        [MainTexture]
        _MainTex ("Texture", 2D) = "white" { }
        _Color ("Color", Color) = (1, 1, 1, 1)
        _Speed ("Speed", Float) = 0.0
        _CutOff ("Cut off", float) = 0.1
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
       
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
 
            struct v2f {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
            };
 
            float _Speed;
            float _CutOff;
            fixed4 _Color;
 
            v2f vert(appdata_base v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
 
                // Pivot
                float2 pivot = float2(0.5, 0.5);
                // Rotation Matrix
                float cosAngle = cos(_Speed * _Time);
                float sinAngle = sin(_Speed * _Time);
                float2x2 rot = float2x2(cosAngle, -sinAngle, sinAngle, cosAngle);
 
                // Rotation consedering pivot
                float2 uv = v.texcoord.xy - pivot;
                o.uv = mul(rot, uv);
                o.uv += pivot;
                
                return o;
            }
 
            sampler2D _MainTex;
 
            fixed4 frag(v2f i) : SV_Target
            {
                fixed4 color = tex2D(_MainTex, i.uv);
                
                if (color.a < _CutOff)
                    discard;

                return color * _Color;
            }
 
            ENDCG
        }
    }
}
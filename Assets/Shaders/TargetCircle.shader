Shader "Custom/TargetCircle"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("Color", Color) = (1, 1, 1, 1)
        _CutOff("Cut off", Range(0, 1)) = 0
        _RotationSpeed("Rotation Speed", Float) = 1
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
            fixed4 _Color;
            half _CutOff;
            float _RotationSpeed;

            float2 rotateUv(float2 uv, float2 pivot, float speed)
            {
                float cosAngle = cos(speed * _Time);
                float sinAngle = sin(speed * _Time);

                float2x2 rotation = float2x2(cosAngle, -sinAngle, sinAngle, cosAngle);

                float2 rotatedUv = uv - pivot;
                rotatedUv = mul(rotation, rotatedUv);
                rotatedUv += pivot;

                return rotatedUv;
            }
            
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = rotateUv(v.uv, 0.5, _RotationSpeed);
                
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
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

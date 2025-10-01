Shader "Unlit/Gradient"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

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
            float4 _MainTex_ST;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // Define color stops
                float3 red = float3(1.0, 0.0, 0.0);
                float3 orange = float3(1.0, 0.5, 0.0);
                float3 green = float3(0.0, 1.0, 0.0);

                float3 color;

                if (i.uv.x < 0.5)
                {
                    // Interpolate from red to orange
                    float t = i.uv.x / 0.5;
                    color = lerp(red, orange, t);
                }
                else
                {
                    // Interpolate from orange to green
                    float t = (i.uv.x - 0.5) / 0.5;
                    color = lerp(orange, green, t);
                }

                return float4(color, 1.0);
            }
            ENDCG
        }
    }
}

Shader "Unlit/Map"
{
    Properties
    {
        _Color ("TintColor", Color) = (1,1,1,1)
        _LineColor ("LineColor", Color) = (1,1,1,1)
        _Value("Control",Vector) = (0,0,0,0)
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
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"
            float4 _Color;
            float4 _LineColor;
            float4 _Value;
            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv: TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 uv: TEXCOORD0;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float2 st = i.uv * _Value.y;
                float fx = frac(st.x);
                float fy = frac(st.y);
                float u = step(_Value.x,fx) * step(fx,1-_Value.x);
                float v = step(_Value.x,fy) * step(fy,1-_Value.x);
                return _Color * u*v + (1 - u*v) * _LineColor;
            }
            ENDCG
        }
    }
}

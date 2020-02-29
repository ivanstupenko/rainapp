Shader "Unlit/ToonRender"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
		_Threshold ("Threshold", Range (0,1)) = 0.5
		_Color ("Color", Color) = (0.0, 0.3, 0.7, 1.0)
		_WaveColor ("Wave Color", Color) = (1.0, 1.0, 1.0, 1.0)
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

			fixed _Threshold;
			fixed4 _Color;
			fixed4 _WaveColor;

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
				fixed4 toon = col.r > _Threshold? _WaveColor : _Color;
                return toon;
            }
            ENDCG
        }
    }
}

Shader "Unlit/Ring"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
		_Fade ("Fade Speed", Range(0,1)) = 0.5
		_Scale ("Scale", Float) = 2
		_WaveLength ("Wave Length", Float) = 1
		_Intensity ("Intensity", Float) = 10
	
    }
    SubShader
    {
        Tags 
		{
			"RenderType" = "Transparent"
			"Queue" = "Transparent"
			"PreviewType" = "Plane"
		}
        LOD 100

		Blend One One

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float3 uvt : TEXCOORD0;
            };

            struct v2f
            {
                float3 uvt : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uvt.xy = TRANSFORM_TEX(v.uvt.xy, _MainTex);
				o.uvt.z = v.uvt.z;
                return o;
            }

			float _Fade;
			float _WaveLength;
			float _Intensity;
			float _Scale;

            fixed4 frag (v2f i) : SV_Target
            {
				fixed2 uv = i.uvt.xy * 2.0 - 1.0;
				uv.y *= _Scale;
				float t = i.uvt.z;
				float r = length(uv);
				float l = _WaveLength / (t + 0.01);
				fixed4 col = sin((1.0 - r) / l) * (r < 1.0);

				fixed fade =  pow(_Fade, (1.0 - r)/l);
				float scale = t / _WaveLength;
				fixed distFade = saturate (1.0 / (scale));

				col *= fade * distFade * _Intensity * smoothstep(1.0,0.9,t);
                
				//return distFade;
				return col;
            }
            ENDCG
        }
    }
}

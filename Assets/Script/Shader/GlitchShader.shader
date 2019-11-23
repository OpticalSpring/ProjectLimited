Shader "Custom/GlitchShader"
{
	Properties
	{
		_MainTex("Main", 2D) = "white" {}
		_MainTex6("Main", 2D) = "white" {}
		_Redch("GlitchRed", float) = 0
		_Greench("GlitchGreen", float) = 0
		_Bluech("GlitchBlue", float) = 0
		_zero("zero", float) = 0
		_outlineonoff("outline onoff", float) = 0
		_glipower("glitch power", float) = 0
		_glitchuv("glitch uv", float) = 0
		_GlitchflowSpeed("glitch flowspeed", float) = 0
	}
		SubShader
		{
			Cull Off ZWrite Off ZTest Always

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

				v2f vert(appdata v)
				{
					v2f o;
					o.vertex = UnityObjectToClipPos(v.vertex);
					o.uv = v.uv;
					return o;
				}

				sampler2D _MainTex, _MainTex6;
				float _Redch, _Greench, _Bluech, _zero, _glipower,_GlitchflowSpeed, _glitchuv;
				float4 _CameraDepthNormalsTexture_TexelSize;

				fixed4 frag(v2f i) : SV_Target
				{
					//글리치
					fixed4 col = tex2D(_MainTex, i.uv);
					float2 newUV = i.uv + float2(0, -_Time.x * _GlitchflowSpeed);
					newUV.x *= _glitchuv;
					newUV.y *= _glitchuv;
					fixed4 GlitchTex = tex2D(_MainTex6, newUV);
					fixed4 colR = tex2D(_MainTex, i.uv + (GlitchTex.rgb * sin(_Time.y * 25 * _zero)) / (_Redch*_glipower)* _zero);
					fixed4 colG = tex2D(_MainTex, i.uv + (GlitchTex.rgb * cos(_Time.y * 25 * _zero)) / (_Greench*_glipower)* _zero);
					fixed4 colB = tex2D(_MainTex, i.uv + (GlitchTex.rgb * tan(_Time.y * 25 * _zero)) / (_Bluech*_glipower)* _zero);
					float3 Red = colR.rgb * float3(1, 0, 0);
					float3 Green = colG.rgb * float3(0, 1, 0);
					float3 Blue = colB.rgb * float3(0, 0, 1);
					float4 tex = float4((Red + Green + Blue), 1);
					//float4 tex2 = float4((Red + Green + Blue), 1);
					//float4 cell = ceil(tex2 * 5) / 5.0f;
					float4 texmain = float4((Red + Green + Blue), 1);

					//return tex*outlineFinal;
					return tex;

			}
			ENDCG
		}
		}
}

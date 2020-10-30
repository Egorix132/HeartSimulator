Shader "LiquidEffect/UnlitMod"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_BloodTex("BloodTexture", 2D) = "white" {}
		_Color("Danger Color", Color) = (1,1,1,1)

		_Cutoff("Alpha cutoff", Range(0,1)) = 0.5
		_DangerLevel("Danger Level", Range(0,1)) = 0

		_Stroke("Stroke Alpha", Range(0,1)) = 0.1
		_StrokeColor("Stroke Color", Color) = (1,1,1,1)
	}

	SubShader
	{
		Tags {"Queue" = "AlphaTest" "IgnoreProjector" = "True" "RenderType" = "TransparentCutout" }
		LOD 100

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
				float2 texcoord : TEXCOORD0;
			};

			struct v2f
			{
				float4 vertex : SV_POSITION;
				half2 texcoord : TEXCOORD0;
				half2 bloodTexcoord : TEXCOORD1;
			};

			sampler2D _MainTex;
			sampler2D _BloodTex;
			float4 _MainTex_ST;
			float4 _BloodTex_ST;

			v2f vert(appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
				o.bloodTexcoord = TRANSFORM_TEX(v.texcoord, _BloodTex);
				return o;
			}

			fixed _Cutoff;

			fixed _DangerLevel;

			half4 _Color;

			fixed _Stroke;
			half4 _StrokeColor;

			fixed4 frag(v2f i) : SV_Target
			{
				fixed4 alpha = tex2D(_MainTex, i.texcoord).a;
				fixed4 bloodCol = tex2D(_BloodTex, i.bloodTexcoord);
				clip(alpha - _Cutoff);
				fixed4 col = lerp(bloodCol, _Color, _DangerLevel);
				return col;
			}
			ENDCG
		}
	}
}
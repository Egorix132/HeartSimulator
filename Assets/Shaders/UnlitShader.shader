Shader "LiquidEffect/UnlitMod"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_BloodTex("BloodTexture", 2D) = "white" {}
		_MaskTex("Mask", 2D) = "white" {}
		_Color("Color", Color) = (1,1,1,1)

		_Cutoff("Alpha cutoff", Range(0,1)) = 0.5
		_MaskCutoff("Mask Alpha cutoff", Range(0,1)) = 0.5
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
				half2 maskTexcoord : TEXCOORD2;
			};

			sampler2D _MainTex;
			sampler2D _BloodTex;
			sampler2D _MaskTex;
			float4 _MainTex_ST;
			float4 _BloodTex_ST;
			float4 _MaskTex_ST;

			v2f vert(appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
				o.bloodTexcoord = TRANSFORM_TEX(v.texcoord, _BloodTex);
				o.maskTexcoord = TRANSFORM_TEX(v.texcoord, _MaskTex);
				return o;
			}

			fixed _Cutoff;
			fixed _MaskCutoff;

			half4 _Color;

			fixed4 frag(v2f i) : SV_Target
			{
				fixed alpha = tex2D(_MainTex, i.texcoord).a;
				fixed4 bloodCol = tex2D(_BloodTex, i.bloodTexcoord);
				fixed4 mask = tex2D(_MaskTex, i.maskTexcoord);
				clip(mask.a - _MaskCutoff);
				fixed4 col = bloodCol * _Color;
				fixed4 ncol;
				if (mask.a > 0.9) {
					ncol = mask;
				}
				else {
					clip(alpha - _Cutoff);
					ncol = lerp(col, mask, mask.a);
				}
				return ncol;
			}
			ENDCG
		}
	}
}
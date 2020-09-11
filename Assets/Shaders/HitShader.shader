Shader "Custom/HitShader"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex("Texture", 2D) = "white" {}
        _RedTex("RedTexture", 2D) = "white" {}
        _Transparency("Transparency", Range(0,1)) = 0
    }
    SubShader
    {
        Tags {"Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent"}
        LOD 100

        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            // Use shader model 3.0 target, to get nicer looking lighting
            #pragma target 3.0
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION; // vertex position
                float2 uv : TEXCOORD0; // texture coordinate
            };

            struct v2f
            {
                float2 uv : TEXCOORD0; // texture coordinate
                float4 vertex : SV_POSITION; // clip space position
            };

            float4 _Color;
            float _Transparency;
            sampler2D _MainTex;
            sampler2D _RedTex;

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                // sample texture and return it
                fixed4 base = tex2D(_MainTex, i.uv);
                fixed4 overlay = tex2D(_RedTex, i.uv);
                overlay = _Color * overlay;
                overlay *= _Transparency;
                fixed4 col = lerp(base, overlay, overlay.a);
                return col;
            }

            ENDCG
        }
    }
    FallBack "Diffuse"
}

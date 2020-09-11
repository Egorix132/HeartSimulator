﻿// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Unlit/SkyReflection Per Pixel"
{
    Properties {
        // normal map texture on the material,
        // default to dummy "flat surface" normalmap
        _MainTex("Texture", 2D) = "white" {}
        _BumpMap("Normal Map", 2D) = "bump" {}
    }
    SubShader
    {
        Pass
        {

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            #include "UnityLightingCommon.cginc" // for _LightColor0
             float4x4 inverse(float4x4 input)
 {
     #define minor(a,b,c) determinant(float3x3(input.a, input.b, input.c))
        //determinant(float3x3(input._22_23_23, input._32_33_34, input._42_43_44))

        float4x4 cofactors = float4x4(
             minor(_22_23_24, _32_33_34, _42_43_44),
            -minor(_21_23_24, _31_33_34, _41_43_44),
             minor(_21_22_24, _31_32_34, _41_42_44),
            -minor(_21_22_23, _31_32_33, _41_42_43),

            -minor(_12_13_14, _32_33_34, _42_43_44),
             minor(_11_13_14, _31_33_34, _41_43_44),
            -minor(_11_12_14, _31_32_34, _41_42_44),
             minor(_11_12_13, _31_32_33, _41_42_43),

             minor(_12_13_14, _22_23_24, _42_43_44),
            -minor(_11_13_14, _21_23_24, _41_43_44),
             minor(_11_12_14, _21_22_24, _41_42_44),
            -minor(_11_12_13, _21_22_23, _41_42_43),

            -minor(_12_13_14, _22_23_24, _32_33_34),
             minor(_11_13_14, _21_23_24, _31_33_34),
            -minor(_11_12_14, _21_22_24, _31_32_34),
             minor(_11_12_13, _21_22_23, _31_32_33)
        );
        #undef minor
        return transpose(cofactors) / determinant(input);
    }
            struct v2f {
                float3 worldPos : TEXCOORD0;
                // these three vectors will hold a 3x3 rotation matrix
                // that transforms from tangent to world space
                half3 tspace0 : TEXCOORD1; // tangent.x, bitangent.x, normal.x
                half3 tspace1 : TEXCOORD2; // tangent.y, bitangent.y, normal.y
                half3 tspace2 : TEXCOORD3; // tangent.z, bitangent.z, normal.z
                // texture coordinate for the normal map
                float2 uv : TEXCOORD4;
                float4 pos : SV_POSITION;
                fixed4 diff : COLOR0; // diffuse lighting color
            };

            // vertex shader now also needs a per-vertex tangent vector.
            // in Unity tangents are 4D vectors, with the .w component used to
            // indicate direction of the bitangent vector.
            // we also need the texture coordinate.
            v2f vert (float4 vertex : POSITION, float3 normal : NORMAL, float4 tangent : TANGENT, float2 uv : TEXCOORD0)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(vertex);
                o.worldPos = mul(unity_ObjectToWorld, vertex).xyz;
                half3 wNormal = UnityObjectToWorldNormal(normal);
                half3 wTangent = UnityObjectToWorldDir(tangent.xyz);
                // compute bitangent from cross product of normal and tangent
                half tangentSign = tangent.w * unity_WorldTransformParams.w;
                half3 wBitangent = cross(wNormal, wTangent) * tangentSign;
                // output the tangent space matrix
                o.tspace0 = half3(wTangent.x, wBitangent.x, wNormal.x);
                o.tspace1 = half3(wTangent.y, wBitangent.y, wNormal.y);
                o.tspace2 = half3(wTangent.z, wBitangent.z, wNormal.z);
                half nl = max(0, dot(wNormal, _WorldSpaceLightPos0.xyz));
                // factor in the light color
                o.diff = nl * _LightColor0;
                o.uv = uv;
                return o;
            }

            // normal map texture from shader properties
            sampler2D _BumpMap;
            sampler2D _MainTex;
        
            fixed4 frag (v2f i) : SV_Target
            {
                // sample the normal map, and decode from the Unity encoding
                half3 tnormal = UnpackNormal(tex2D(_BumpMap, i.uv));
                // transform normal from tangent to world space
                half3 worldNormal;
                worldNormal.x = dot(i.tspace0, tnormal);
                worldNormal.y = dot(i.tspace1, tnormal);
                worldNormal.z = dot(i.tspace2, tnormal);

                float4 worldLightPos = mul(inverse(UNITY_MATRIX_MV), unity_LightPosition[0]);
                half normalColor = max(0, dot(-worldNormal, _WorldSpaceLightPos0.xyz));
                fixed4 col = tex2D(_MainTex, i.uv);
                return col * normalColor;
            }
            ENDCG
        }
    }
}
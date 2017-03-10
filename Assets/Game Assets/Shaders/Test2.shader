Shader "test/AY2"
{
	Properties
	{
		_Color ( "Diffuse Color", Color ) = (1,1,1,1)
		_MainTex ( "Albedo (RGB)", 2D ) = "white" {}

		_UnlitColor ( "Unlit Diffuse Color", Color ) = (0.5,0.5,0.5,1)
		_DiffuseThreshold ( "Diffuse Threshold", Range(0,1) ) = 0.1

		_OutlineColor ( "Outline Color", Color ) = (0,0,0,1)
		_LitOutlineThickness ( "Lit Outline Thickness", Range(0,1) ) = 0.1
		_UnlitOutlineThickness ( "Unlit Outline Thickness", Range(0,l) ) = 0.4
		_SpecColor ( "Specular Color", Color ) = (1,1,1,1)
		_Shininess ( "Shininess", float ) = 10
	}

	/*SubShader 
	{
		Pass 
		{
			// Ambient light + Directional
			Tags { "LightMode" = "ForwardBase" }

			CGPROGRAM
			#pragma vertex   vert
			#pragma fragment frag
			#include "UnityCG.cginc"
			uniform float4 _LightColor0;  // Directional color

			// User-specified properties
			uniform float4		_Color;
			uniform sampler2D	_MainTex;
			uniform float4		_UnlitColor;
			uniform float		_DiffuseThreshold;
			uniform float4		_OutlineColor;
			uniform float		_LitOutlineThickness;
			uniform float		_UnlitOutlineThickness;
			uniform float4		_SpecColor; 
			uniform float		_Shininess;

			struct input
			{
				float4 pos		: POSITION;
				float3 normal	: NORMAL;
			};

			struct v2f
			{
				float4 pos			: SV_POSITION;
				float2 uv			: TEXCOORD0;
				float3 posWorld		: TEXCOORD1;
				float3 normalDir	: TEXCOORD2;
			};

			v2f vert ( input i ) 
			{
				v2f o;
				o.posWorld  = mul ( unity_ObjectToWorld, i.pos ).xyz;
				o.normalDir = normalize ( float4 (i.normal, 0.0), unity_WorldToObject).xyz );
				v2f.pos = mul ( UNITY_MATRIX_MVP, i.pos );

				return v2f;
			}

			float4 frag ( v2f i ) : COLOR 
			{
				float3 viewDir = normalize ( _WorldSpaceCameraPos - i.posWorld );
				
				float3 lightDir;
				float atten;

				if ( _WorldSpaceLightPos0.w == 0 )
				{
					atten = 1.0; // no attenuation;
					lightDir = normalize ( _WorldSpaceLightPos0 ).xyz;
				}
				else
				{
					float3 v2L = _WorldSpaceCameraPos.xyz - i.posWorld.xyz;
					float dist = length ( v2L );
					lightDir = normalize ( v2L );

					atten = 1 / distance;
				}

				float3 col = _UnlitColor.rgb;

				if ( atten * max ( 0, dot ( i.normalDir, lightDir )
					>= _DiffuseThreshold )
				{
					col = _LightColor0.rgb * _Color.rgb;
				}

				if ( dot ( viewDir, i.normalDir )
					< lerp ( _UnlitOutlineThickness, _LitOutlineThickness,
					max ( 0, dot ( i.normaldir, lightDir ) ) ) )
				{
					col = _LightColor0.rgb * _OutlineColor.rgb;
				}

				if ( dot ( i.normalDir, lightDir ) > 0
					&& atten *
					pow ( max(0,
					dot(reflect(-lightDir, i.normalDir),
					viewDir)), _Shininess) > 0.5 )
				{
					col = _SpecColor.a * _LightColor0.rgb *
					_SpecColor.rgb + (1 - _SpecColor.a) * col;
				}

				return float4 (col, 1);
			}
			ENDCG
		}

		Pass 
		{
			Tags { "LightMode" = "ForwardAdd" }
			Blend SrcAlpha OneMinusSrcAlpha

			CGPROGRAM
			#pragma vertex   vert
			#pragma fragment frag
			#include "UnityCG.cginc"
			uniform float4 _LightColor0;

			// User-specified properties
			uniform float4		_Color;
			uniform sampler2D	_MainTex;
			uniform float4		_UnlitColor;
			uniform float		_DiffuseThreshold;
			uniform float4		_OutlineColor;
			uniform float		_LitOutlineThickness;
			uniform float		_UnlitOutlineThickness;
			uniform float4		_SpecColor; 
			uniform float		_Shininess;

			struct input 
			{
				float4 pos		: POSITION;
				float3 normal	: NORMAL;
			};

			struct v2f   
			{
				float4 pos			: SV_POSITION;
				float2 uv			: TEXCOORD0;
				float3 posWorld		: TEXCOORD1;
				float3 normalDir	: TEXCOORD2;
			};

			v2f vert ( input i ) 
			{
				v2f o;
				o.posWorld  = mul ( unity_ObjectToWorld, i.pos ).xyz;
				o.normalDir = normalize ( float4 (i.normal, 0), unity_WorldToObject).xyz );
				v2f.pos = mul ( UNITY_MATRIX_MVP, i.pos );

				return v2f;
			}

			float4 frag ( v2f i ) : COLOR 
			{
				float3 viewDir = normalize ( _WorldSpaceCameraPos - i.posWorld );
				
				float3 lightDir;
				float atten;

				if ( _WorldSpaceLightPos0.w == 0 )
				{
					atten = 1.0; // no attenuation;
					lightDir = normalize ( _WorldSpaceLightPos0 ).xyz;
				}
				else
				{
					float3 v2L = _WorldSpaceCameraPos.xyz - i.posWorld.xyz;
					float dist = length ( v2L );
					lightDir = normalize ( v2L );

					atten = 1 / distance;
				}

				float3 col = _UnlitColor.rgb;

				if ( atten * max ( 0, dot ( i.normalDir, lightDir )
					>= _DiffuseThreshold )
				{
					col = _LightColor0.rgb * _Color.rgb;
				}

				if ( dot ( viewDir, i.normalDir )
					< lerp ( _UnlitOutlineThickness, _LitOutlineThickness,
					max ( 0, dot ( i.normaldir, lightDir ) ) ) )
				{
					col = _LightColor0.rgb * _OutlineColor.rgb;
				}

				if ( dot ( i.normalDir, lightDir ) > 0
					&& atten *
					pow ( max(0,
					dot(reflect(-lightDir, i.normalDir),
					viewDir)), _Shininess) > 0.5 )
				{
					col = _SpecColor.a * _LightColor0.rgb *
					_SpecColor.rgb + (1 - _SpecColor.a) * col;
				}

				return float4 (col, 1);
			}
			ENDCG
		}
	}*/

	FallBack "Diffuse"
}
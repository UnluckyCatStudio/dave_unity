Shader "Custom/Fog wall"
{
	Properties
	{
		_Main ("Color", Color) = (1,1,1,1)
	}
	SubShader
	{
		Tags { "RenderType"="Transparent" "Queue"="Transparent" }
		ZWrite Off

		Blend SrcAlpha OneMinusSrcAlpha
		Pass 
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			// make fog work
			#pragma multi_compile_fog
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
			};

			struct v2f
			{
				UNITY_FOG_COORDS(1)
				float4 vertex : SV_POSITION;
			};

			v2f vert (appdata v)
			{
			    v2f o;
			    o.vertex = mul (UNITY_MATRIX_MVP, v.vertex);
			    return o;
			}    
			 
			uniform fixed4 _Main;
			half4 frag (v2f i) : COLOR
			{
			    return _Main;
			}
			ENDCG
		}
	}
}

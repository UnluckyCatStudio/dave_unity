Shader "Unlit/StencilMask"
{
	SubShader
	{
		Tags { "Queue"="Geometry+1" }
		ZWrite Off

		Stencil
		{
			Ref 3
			Comp always
			Pass replace
		}

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			struct v2f
			{
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			
			v2f vert (appdata_base v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				return half4(1,1,1,1);
			}
			ENDCG
		}
	}
}

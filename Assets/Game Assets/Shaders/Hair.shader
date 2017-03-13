Shader "Custom/Hair"
{
	Properties
	{
		_Color		( "Color",				Color )				= (1,1,1,1)
		_MainTex	( "Alebdo (RGB)",		2D )				= "white" {}
		_FurLength	( "Fur Length",			Range(.0002, 1) )	= .25
		_Cutoff		( "Alpha cutoff",		Range(0,1) )		= 0.5
		_CutoffEnd	( "Alpha cutoff end",	Range(0,1) )		= 0.5
		_EdgeFade	( "Edge Fade",			Range(0,1) )		= 0.4
		_Gravity	( "Gravity direction",	Vector )			= (0,0,1,0)
		_GStrength	( "G strenght",			Range(-1,1) )		= 0.25
	}

	SubShader
	{

	}
}
	FallBack "Diffuse"
}

// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Hidden/VillagerColor"
{
	Properties
	{
		_MainTex("Sprite", 2D) = "white" {}
		_HairPaletteTex("Hair Palette", 2D) = "white" {}
		_Hair0("_HairColor0", Color) = (1,0,0,1)
		_HairC1("_HairColor1", Color) = (1,0,0,1)
		_Tolerance("Tolerance", float) = 0.001
	}

		SubShader
	{
		Tags
	{
		"RenderType" = "Opaque"
		"Queue" = "Transparent+1"
	}

		Pass
	{
		ZWrite Off
		Blend SrcAlpha OneMinusSrcAlpha

		CGPROGRAM
#pragma vertex vert
#pragma fragment frag
#pragma multi_compile DUMMY PIXELSNAP_ON

		sampler2D _MainTex;
		sampler2D _HairPaletteTex;
		float4 _Hair0;
		float4 _Hair1;
		float _Tolerance;
		half4 _In0;
		half4 _Out0;
		half4 _In1;
		half4 _Out1;
		half4 _In2;
		half4 _Out2;
		half4 _In3;
		half4 _Out3;

	struct Vertex
	{
		float4 vertex : POSITION;
		float2 uv_MainTex : TEXCOORD0;
		float2 uv2 : TEXCOORD1;
	};

	struct Fragment
	{
		float4 vertex : POSITION;
		float2 uv_MainTex : TEXCOORD0;
		float2 uv2 : TEXCOORD1;
	};

	Fragment vert(Vertex v)
	{
		Fragment o;

		o.vertex = UnityObjectToClipPos(v.vertex);
		o.uv_MainTex = v.uv_MainTex;
		o.uv2 = v.uv2;

		return o;
	}

	float4 frag(Fragment IN) : COLOR
	{
		half4 c = tex2D(_MainTex, IN.uv_MainTex);

		if (all(c.rgba == _Hair0.rgba))
			return tex2D(_HairPaletteTex, float2(1, 0));

		if (c.r >= _Hair0.r - _Tolerance && c.r <= _Hair0.r + _Tolerance
			&& c.g >= _Hair0.g - _Tolerance && c.g <= _Hair0.g + _Tolerance
			&& c.b >= _Hair0.b - _Tolerance && c.b <= _Hair0.b + _Tolerance
			&& c.a >= _Hair0.a - _Tolerance && c.a <= _Hair0.a + _Tolerance)
		{
			return tex2D(_HairPaletteTex, float2(1, 0));
		}

		return c;
	}
		ENDCG
	}
	}
}

// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/Sprite Outline" {
	Properties{
		_MainTex("Base (RGB)", 2D) = "white" {}
		_OutlineBaseColor("Outline Base Color", Color) = (1, 1, 1, 1)
		_OutlineColor("Outline Color", Color) = (1, 1, 1, 1)
	}
		SubShader{
		Tags{ "Queue" = "Transparent" "RenderType" = "Transparent" }
		Cull Off
		Blend One OneMinusSrcAlpha

		Pass{
		CGPROGRAM
#pragma vertex vert
#pragma fragment frag
#include "UnityCG.cginc"

	sampler2D _MainTex;

	struct appdata_t
			{
				float4 vertex   : POSITION;
				float4 color    : COLOR;
				float2 texcoord : TEXCOORD0;
			};

	struct v2f {
		float4 pos : SV_POSITION;
		float4 color : COLOR;
		half2 uv : TEXCOORD0;
	};

	v2f vert(appdata_t v) {
		v2f o;
		o.pos = UnityObjectToClipPos(v.vertex);
		o.uv = v.texcoord;
		o.color = v.color;
		return o;
	}

	fixed4 _OutlineColor;
	fixed4 _OutlineBaseColor;

	fixed4 frag(v2f i) : COLOR
	{
		fixed4 col = tex2D(_MainTex,i.uv);
		col.rgb *= col.a;
		if (col.r == _OutlineBaseColor.r) {
			return _OutlineColor;
		}

	return col * i.color;
	}

		ENDCG
	}
		}
			FallBack "Diffuse"
}
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/Sprite Outline" {
	Properties{
		_MainTex("Base (RGB)", 2D) = "white" {}
		_Color("Color", Color) = (1, 1, 1, 1)
		_OutlineColor("Outline Color", Color) = (1, 1, 1, 1)
		_OutlineWidth("Outline Width",Float) = 1
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

	fixed4 _Color;
	fixed4 _OutlineColor;
	float4 _MainTex_TexelSize;
	float _OutlineWidth;

	fixed4 frag(v2f i) : COLOR
	{
		half4 c = tex2D(_MainTex, i.uv);
		//c.rbg *= c.a;

		half4 outlineC = _OutlineColor;
		c *= i.color * c.a;
		//outlineC.a *= ceil(c.a);
		outlineC.rgb *= outlineC.a;

		fixed alpha_up = tex2D(_MainTex, i.uv + fixed2(0, _OutlineWidth)).a;
		fixed alpha_down = tex2D(_MainTex, i.uv - fixed2(0, _OutlineWidth)).a;
		fixed alpha_right = tex2D(_MainTex, i.uv + fixed2(_OutlineWidth, 0)).a;
		fixed alpha_left = tex2D(_MainTex, i.uv - fixed2(_OutlineWidth, 0)).a;

		return lerp(c * i.color , outlineC, c.a == 0 && alpha_up + alpha_down + alpha_right + alpha_left > 0);
	}

		ENDCG
	}
		}
			FallBack "Diffuse"
}
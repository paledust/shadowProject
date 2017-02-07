Shader "Custom/ColorReceiveShadw" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf ShadowOnly fullforwardshadows

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;

		struct Input {
			float2 uv_MainTex;
		};

		inline fixed4 LightingShadowOnly (SurfaceOutput s, fixed3 lightDir, fixed atten) 
		{
        	fixed4 color;
            color.rgb = s.Albedo * atten * _LightColor0;
            color.a = s.Alpha;
            return color;
        }

		fixed4 _Color;

		void surf (Input IN, inout SurfaceOutput o) 
		{
        	fixed4 color = tex2D(_MainTex, IN.uv_MainTex) * _Color;
        	o.Albedo = color.rgb;
        	o.Alpha = 1;
        }
		ENDCG
	}
	FallBack "Diffuse"
}

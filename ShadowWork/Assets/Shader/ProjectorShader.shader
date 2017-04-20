Shader "Custom/ProjectorShader" {
	Properties {
		_ShadowTex ("Albedo (RGB)", 2D) = "white" {}

		_TexWidth("Sheet Width", Float) = 0.0
		_CellAmount ("Cell Amount", Float) = 0.0
		_Speed("Speed", Range(0.01, 32)) = 12

		_FalloffTex ("FallOff", 2D) = "white" {}
	}
	SubShader {
		Tags { "Queue" = "Transparent" "RenderType"="Transparent" }
		LOD 200
		
		ZWrite Off
		Blend SrcAlpha OneMinusSrcAlpha 
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf NoShadow alpha

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;

		struct Input {
			float2 uv_MainTex;
		};

		half _TexWidth;
		half _CellAmount;
		half _Speed;

		inline float4 LightingNoShadow(SurfaceOutput s, fixed3 lightDir, fixed3 viewDir, fixed atten){
			fixed4 color;

            color.rgb = s.Albedo;
			color.a = s.Albedo;
			//color.rgb = cross(color.rgb,viewDir);
            return color;
		}

		void surf (Input IN, inout SurfaceOutput o) {
			float2 spriteUV = IN.uv_MainTex;

			float cellPixelWidth = _TexWidth/_CellAmount;
			float cellUVPercentage = cellPixelWidth/_TexWidth;

			float timeVal = fmod(_Time.y * _Speed, _CellAmount);
			timeVal = ceil(timeVal);
			float xValue = spriteUV.x;
			xValue += cellUVPercentage * timeVal * _CellAmount;
			xValue *= cellUVPercentage;

			spriteUV = float2(xValue, spriteUV.y);
			// Albedo comes from a texture tinted by color
			fixed4 c = tex2D (_MainTex, spriteUV);
			o.Albedo = c.rgb;
			o.Albedo = c.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}

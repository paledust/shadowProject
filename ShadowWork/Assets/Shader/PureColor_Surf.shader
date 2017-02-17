Shader "Custom/PureColor_Surf" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
	}
	SubShader {
		Tags {"Queue" = "Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
		LOD 200
		
		ZWrite off
		//Blend SrcAlpha OneMinusSrcAlpha
		AlphaToMask On
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf ShadowOnly fullforwardshadows alphatest:_Cutoff  
		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;

		struct Input {
			float2 uv_MainTex;
		};

		inline fixed4 LightingShadowOnly (SurfaceOutput s, fixed3 lightDir, fixed3 viewDir, fixed atten) 
		{
        	fixed4 color;
			float light;
			float temp;

			light = dot (s.Normal, lightDir);
			//temp = normalize(lightDir.x + lightDir.z)

			//light = light * light * light;

			//light = light*0.5 + 0.5;
			
			if(light < (lightDir.x-0.2))
				light = 0.0;
			else
				light = 1.0;
            color.rgb = s.Albedo * light * (atten)*_LightColor0;
			color.a = 1 - light;
			//color.rgb = cross(color.rgb,viewDir);
            return color;
        }

		fixed4 _Color;

		void surf (Input IN, inout SurfaceOutput o) 
		{
        	fixed4 color = tex2D(_MainTex, IN.uv_MainTex) * _Color;
        	o.Albedo = color.rgb;
        	o.Alpha = color.a;
        }
		ENDCG
	}
	FallBack "Diffuse"
}

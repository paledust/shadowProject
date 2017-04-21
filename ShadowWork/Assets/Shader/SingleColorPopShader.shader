Shader "Custom/SingleColorPopShader" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_ShadowColor("Shadow Color", Color) = (0,0,0,1)
		_ShadowStr("Strength of Shadow", Range(0,1)) = 1.0
		_ActiveFace("Active Shadow Face", Vector) = (1,1,1,1)
	}
	SubShader {
		Tags {"RenderType" = "Opaque"}
		LOD 200

		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf ShadowOnly fullforwardshadows
		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;
		fixed4 _ShadowColor;
		float _ShadowStr;
		fixed4 _Color;
		fixed4 _ActiveFace;

		struct Input {
			float2 uv_MainTex;
		};

		inline fixed4 LightingShadowOnly (SurfaceOutput s, fixed3 lightDir, fixed3 viewDir, fixed atten) 
		{
        	fixed4 color = (1,1,1,1);
			float light;
			float3 newNormal;
			fixed3 viewColor;

			newNormal = float3(s.Normal.x,s.Normal.y,s.Normal.z);
			light = dot (newNormal, lightDir);
			
			if(light <= 0.2)
				light = 0.0;
			else
				light = 1.0;

            color.rgb = s.Albedo * light * (atten)*_LightColor0;
			color.a = 1;
            return color;
        }

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

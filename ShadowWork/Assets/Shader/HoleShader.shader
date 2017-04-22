Shader "Unlit/HoleShader"
{
	Properties
	{
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_HoleSize("Hole Size", Int) = 1
	}
	SubShader
	{
		Pass{
			
		}
		Pass{

		}
		Tags {"RenderType" = "Opaque"}
		LOD 200

		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf ShadowOnly fullforwardshadows noambient 
		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;
		int _HoleSize;
		fixed4 _Color;

		struct Input {
			float2 uv_MainTex;
		};
		inline fixed4 LightingShadowOnly (SurfaceOutput s, fixed3 lightDir, fixed3 viewDir, fixed atten) 
		{
        	fixed4 color;
			float light;
			float3 newNormal;

			newNormal = float3(s.Normal.x,s.Normal.y,s.Normal.z);
			light = dot (newNormal, lightDir);
			//light = light * light * light;
			light = 1.0;

			if(atten < 1){
            	color.rgb =  (_ShadowStr) * _ShadowColor;
			}
			else{
				color.rgb = s.Albedo * light * atten *_LightColor0 * _Color;
			}
			color.a = 1;
			// color.rgb = cross(color.rgb,viewDir);
            return color;
        }

		void surf (Input IN, inout SurfaceOutput o) 
		{
        	fixed4 color = tex2D(_MainTex, IN.uv_MainTex);
        	o.Albedo = color.rgb;
        	o.Alpha = color.a;
        }
		ENDCG
	}
	FallBack "Diffuse"
}

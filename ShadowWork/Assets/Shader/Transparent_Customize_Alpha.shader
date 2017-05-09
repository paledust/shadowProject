Shader "Unlit/Transparent_Customize_Alpha"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_NoiseMap("Noise Map", 2D) = "white"{}
		_Alpha ("MaxAlpha", Range(0,1)) = 1.0
		_NoiseSpeed("NoiseSpeed", Range(0,1)) = 1.0
        [MaterialToggle] _UseNoisy ("Noisy_Open", Float ) = 0
	}
	SubShader
	{
		Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}

		Pass
		{
			ZWrite Off
			Blend SrcAlpha OneMinusSrcAlpha
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			// make fog work
			#pragma multi_compile_fog
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
				float2 uv_0 : TEXCOORD1;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float2 uv_0: TEXCOORD1;
				UNITY_FOG_COORDS(1)
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			sampler2D _NoiseMap;

			float _NoiseSpeed;

			float _Alpha;
			float4 _MainTex_ST;
			float4 _NoiseMap_ST;
			fixed _UseNoisy;

			float hash(float2 p)
			{
				return frac(sin(dot(p,float2(15.79, 81.93)) * 45678.9123));
			}

			float valueNoise(float2 p )
			{
				float2 i = floor(p);
				float2 f = frac(p);
				f = f * f * (3.0 - 2.0 * f);
				fixed bottomOfGrid = lerp(hash(i + fixed2(0.0,0.0)), hash(i + fixed2(1.0,0.0)), f.x);
				fixed topOfGrid = lerp(hash(i + fixed2(0.0, 1.0)), hash(i + fixed2(1.0, 1.0)), f.x);
				fixed t = lerp(bottomOfGrid, topOfGrid, f.y);
				return t;
			}

			float fbm(fixed2 uv )
			{
				fixed sum = 0.00;
				fixed amp = 0.7;
				for(int i = 0; i<=4; i++){
					sum += valueNoise(uv) * amp;
					uv += uv * 1.2;
					amp *= 0.f;
				}
				return sum;
			}

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				o.uv_0 = TRANSFORM_TEX(v.uv_0, _NoiseMap);
				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
				fixed4 col = tex2D(_MainTex, i.uv);
				fixed2 _Offset = fixed2(1, 0.05) * _Time * _NoiseSpeed;
				fixed4 _noise = tex2D(_NoiseMap, i.uv_0 + _Offset);
				float _a = (_noise.r * 0.5) * _UseNoisy;
				col.a = (_Alpha *(1 - _a)) * col.a;
				// apply fog
				UNITY_APPLY_FOG(i.fogCoord, col);
				return col;
			}
			ENDCG
		}
	}
}

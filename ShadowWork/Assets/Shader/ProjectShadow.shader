Shader "Unlit/ProjectShadow"
{
   Properties {
      _ShadowTex ("Projected Image", 2D) = "white" {}
	  _Color ("Shadow Color", Color) = (1,1,1,1)
   }
   SubShader {
      Pass {      
         Blend SrcAlpha OneMinusSrcAlpha  // attenuate color in framebuffer 

         ZWrite Off // don't change depths
         Offset -1, -1 // avoid depth fighting
         
         CGPROGRAM
 
         #pragma vertex vert  
         #pragma fragment frag 
 
         // User-specified properties
         uniform sampler2D _ShadowTex; 
         uniform float4 _LightColor0;
            uniform float4 _Color;
 
         // Projector-specific uniforms
         uniform float4x4 unity_Projector; // transformation matrix 
            // from object space to projector space 
 
          struct vertexInput {
            float4 vertex : POSITION;
            float3 normal : NORMAL;
         };
         struct vertexOutput {
            float4 pos : SV_POSITION;
            float4 posProj : TEXCOORD0;
               // position in projector space
         };
 
         vertexOutput vert(vertexInput input) 
         {
            vertexOutput output;
 
            output.posProj = mul(unity_Projector, input.vertex);
            output.pos = mul(UNITY_MATRIX_MVP, input.vertex);
            return output;
         }
 
 
         float4 frag(vertexOutput input) : COLOR
         {
            if (input.posProj.w > 0.0) // in front of projector?
            {
            	return tex2D(_ShadowTex , 
                  input.posProj.xy / input.posProj.w) * _Color * _LightColor0; 
            }
            else // behind projector
            {
               return float4(0,0,0,0);
            }
         }
 
         ENDCG
      }
   }  
   Fallback "Projector/Light"
}

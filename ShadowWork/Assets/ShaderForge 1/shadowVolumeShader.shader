// Shader created with Shader Forge v1.36 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.36;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:3,bdst:7,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:False,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:3138,x:32738,y:32100,varname:node_3138,prsc:2|emission-7490-OUT,alpha-743-OUT;n:type:ShaderForge.SFN_TexCoord,id:2243,x:31040,y:32501,varname:node_2243,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_ComponentMask,id:6341,x:31283,y:32536,varname:node_6341,prsc:2,cc1:1,cc2:-1,cc3:-1,cc4:-1|IN-2243-UVOUT;n:type:ShaderForge.SFN_Color,id:2202,x:31395,y:31872,ptovrint:False,ptlb:shadow color,ptin:_shadowcolor,varname:node_2202,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:0,c3:0,c4:1;n:type:ShaderForge.SFN_Power,id:3861,x:31489,y:32620,varname:node_3861,prsc:2|VAL-6341-OUT,EXP-2466-OUT;n:type:ShaderForge.SFN_Slider,id:3836,x:30632,y:32811,ptovrint:False,ptlb:shadow height,ptin:_shadowheight,varname:node_3836,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1.963788,max:10;n:type:ShaderForge.SFN_Divide,id:5726,x:31807,y:32649,varname:node_5726,prsc:2|A-3861-OUT,B-1485-OUT;n:type:ShaderForge.SFN_Slider,id:1485,x:31301,y:32882,ptovrint:False,ptlb:shadow fall off,ptin:_shadowfalloff,varname:_node_3836_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1.968582,max:10;n:type:ShaderForge.SFN_Exp,id:2466,x:31071,y:32743,varname:node_2466,prsc:2,et:1|IN-3836-OUT;n:type:ShaderForge.SFN_SwitchProperty,id:7490,x:31765,y:31971,ptovrint:False,ptlb:use texture,ptin:_usetexture,varname:node_7490,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,on:True|A-2202-RGB,B-835-OUT;n:type:ShaderForge.SFN_Time,id:4903,x:30164,y:32270,varname:node_4903,prsc:2;n:type:ShaderForge.SFN_Panner,id:9256,x:30710,y:32109,varname:node_9256,prsc:2,spu:0,spv:1|UVIN-8894-UVOUT,DIST-6520-OUT;n:type:ShaderForge.SFN_Tex2d,id:7116,x:30987,y:32042,ptovrint:False,ptlb:texture,ptin:_texture,varname:node_7116,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:ca4b9471997a00d42ac1810d2b9bc6f2,ntxv:0,isnm:False|UVIN-9256-UVOUT;n:type:ShaderForge.SFN_TexCoord,id:8894,x:30337,y:32099,varname:node_8894,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Multiply,id:6520,x:30455,y:32330,varname:node_6520,prsc:2|A-4903-T,B-526-OUT;n:type:ShaderForge.SFN_Slider,id:526,x:30030,y:32454,ptovrint:False,ptlb:scroll speed,ptin:_scrollspeed,varname:node_526,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Multiply,id:835,x:31395,y:32048,varname:node_835,prsc:2|A-7116-RGB,B-5648-RGB;n:type:ShaderForge.SFN_Color,id:5648,x:30998,y:32244,ptovrint:False,ptlb:texture tint,ptin:_texturetint,varname:node_5648,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:0,c3:0,c4:1;n:type:ShaderForge.SFN_Multiply,id:1768,x:32014,y:32374,varname:node_1768,prsc:2|A-7116-R,B-5726-OUT;n:type:ShaderForge.SFN_SwitchProperty,id:743,x:32287,y:32464,ptovrint:False,ptlb:use texture alpha,ptin:_usetexturealpha,varname:node_743,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,on:True|A-5726-OUT,B-1768-OUT;proporder:2202-3836-1485-7490-743-7116-5648-526;pass:END;sub:END;*/

Shader "Shader Forge/shadowVolumeShader" {
    Properties {
        _shadowcolor ("shadow color", Color) = (1,0,0,1)
        _shadowheight ("shadow height", Range(0, 10)) = 1.963788
        _shadowfalloff ("shadow fall off", Range(0, 10)) = 1.968582
        [MaterialToggle] _usetexture ("use texture", Float ) = 0
        [MaterialToggle] _usetexturealpha ("use texture alpha", Float ) = 0
        _texture ("texture", 2D) = "white" {}
        _texturetint ("texture tint", Color) = (1,0,0,1)
        _scrollspeed ("scroll speed", Range(0, 1)) = 0
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        Pass {
            ZWrite Off
            Lighting Off
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend SrcAlpha OneMinusSrcAlpha
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform float4 _TimeEditor;
            uniform float4 _shadowcolor;
            uniform float _shadowheight;
            uniform float _shadowfalloff;
            uniform fixed _usetexture;
            uniform sampler2D _texture; uniform float4 _texture_ST;
            uniform float _scrollspeed;
            uniform float4 _texturetint;
            uniform fixed _usetexturealpha;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
////// Lighting:
////// Emissive:
                float4 node_4903 = _Time + _TimeEditor;
                float2 node_9256 = (i.uv0+(node_4903.g*_scrollspeed)*float2(0,1));
                float4 _texture_var = tex2D(_texture,TRANSFORM_TEX(node_9256, _texture));
                float3 emissive = lerp( _shadowcolor.rgb, (_texture_var.rgb*_texturetint.rgb), _usetexture );
                float3 finalColor = emissive;
                float node_6341 = i.uv0.g;
                float node_5726 = (pow(node_6341,exp2(_shadowheight))/_shadowfalloff);
                return fixed4(finalColor,lerp( node_5726, (_texture_var.r*node_5726), _usetexturealpha ));
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}

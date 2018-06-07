// Shader created with Shader Forge v1.38 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:9361,x:33209,y:32712,varname:node_9361,prsc:2|custl-2684-OUT;n:type:ShaderForge.SFN_TexCoord,id:3920,x:31409,y:32662,varname:node_3920,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Tex2d,id:9911,x:31794,y:32608,ptovrint:False,ptlb:colorTexture,ptin:_colorTexture,varname:node_9911,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:ea871c563f78544f6b3baa2a7e7acbfc,ntxv:0,isnm:False|UVIN-3920-UVOUT;n:type:ShaderForge.SFN_LightVector,id:8777,x:31725,y:33295,varname:node_8777,prsc:2;n:type:ShaderForge.SFN_NormalVector,id:3138,x:31762,y:33424,prsc:2,pt:False;n:type:ShaderForge.SFN_Dot,id:4234,x:31951,y:33295,varname:node_4234,prsc:2,dt:4|A-8777-OUT,B-3138-OUT;n:type:ShaderForge.SFN_Multiply,id:6412,x:32136,y:33351,varname:node_6412,prsc:2|A-4234-OUT,B-4158-OUT;n:type:ShaderForge.SFN_LightAttenuation,id:4158,x:31936,y:33522,varname:node_4158,prsc:2;n:type:ShaderForge.SFN_Lerp,id:2684,x:32546,y:32899,cmnt:this shader needs some work,varname:node_2684,prsc:2|A-35-OUT,B-9911-RGB,T-8964-OUT;n:type:ShaderForge.SFN_Posterize,id:8964,x:32318,y:33185,cmnt:basic toon shader,varname:node_8964,prsc:2|IN-6412-OUT,STPS-5105-OUT;n:type:ShaderForge.SFN_ValueProperty,id:5105,x:32078,y:33185,ptovrint:False,ptlb:toonSteps,ptin:_toonSteps,varname:node_5105,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:2;n:type:ShaderForge.SFN_Color,id:2486,x:31775,y:32865,ptovrint:False,ptlb:shadowColor,ptin:_shadowColor,varname:node_2486,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.8088235,c2:0.8088235,c3:0.8088235,c4:1;n:type:ShaderForge.SFN_Multiply,id:35,x:32074,y:32814,varname:node_35,prsc:2|A-9911-RGB,B-2486-RGB;proporder:9911-2486-5105;pass:END;sub:END;*/

Shader "Shader Forge/toonSimple" {
    Properties {
        _colorTexture ("colorTexture", 2D) = "white" {}
        _shadowColor ("shadowColor", Color) = (0.8088235,0.8088235,0.8088235,1)
        _toonSteps ("toonSteps", Float ) = 2
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "Lighting.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles gles3 metal 
            #pragma target 3.0
            uniform sampler2D _colorTexture; uniform float4 _colorTexture_ST;
            uniform float _toonSteps;
            uniform float4 _shadowColor;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                LIGHTING_COORDS(3,4)
                UNITY_FOG_COORDS(5)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float4 _colorTexture_var = tex2D(_colorTexture,TRANSFORM_TEX(i.uv0, _colorTexture));
                float3 finalColor = lerp((_colorTexture_var.rgb*_shadowColor.rgb),_colorTexture_var.rgb,floor((0.5*dot(lightDirection,i.normalDir)+0.5*attenuation) * _toonSteps) / (_toonSteps - 1));
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "FORWARD_DELTA"
            Tags {
                "LightMode"="ForwardAdd"
            }
            Blend One One
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDADD
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "Lighting.cginc"
            #pragma multi_compile_fwdadd_fullshadows
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles gles3 metal 
            #pragma target 3.0
            uniform sampler2D _colorTexture; uniform float4 _colorTexture_ST;
            uniform float _toonSteps;
            uniform float4 _shadowColor;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                LIGHTING_COORDS(3,4)
                UNITY_FOG_COORDS(5)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float4 _colorTexture_var = tex2D(_colorTexture,TRANSFORM_TEX(i.uv0, _colorTexture));
                float3 finalColor = lerp((_colorTexture_var.rgb*_shadowColor.rgb),_colorTexture_var.rgb,floor((0.5*dot(lightDirection,i.normalDir)+0.5*attenuation) * _toonSteps) / (_toonSteps - 1));
                fixed4 finalRGBA = fixed4(finalColor * 1,0);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}

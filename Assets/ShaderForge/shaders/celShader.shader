// Shader created with Shader Forge v1.38 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:9361,x:33209,y:32712,varname:node_9361,prsc:2|custl-6161-OUT;n:type:ShaderForge.SFN_TexCoord,id:3920,x:31937,y:32628,varname:node_3920,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Tex2d,id:9911,x:32162,y:32628,ptovrint:False,ptlb:texture,ptin:_texture,varname:node_9911,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:ac0845f35a1b8465384c0339df4eb943,ntxv:0,isnm:False|UVIN-3920-UVOUT;n:type:ShaderForge.SFN_LightVector,id:8777,x:31597,y:33120,varname:node_8777,prsc:2;n:type:ShaderForge.SFN_NormalVector,id:3138,x:31634,y:33249,prsc:2,pt:False;n:type:ShaderForge.SFN_Dot,id:4234,x:31823,y:33120,varname:node_4234,prsc:2,dt:4|A-8777-OUT,B-3138-OUT;n:type:ShaderForge.SFN_Multiply,id:6412,x:32008,y:33176,varname:node_6412,prsc:2|A-4234-OUT,B-4158-OUT,C-6654-OUT;n:type:ShaderForge.SFN_LightAttenuation,id:4158,x:31531,y:33445,varname:node_4158,prsc:2;n:type:ShaderForge.SFN_Posterize,id:8964,x:32253,y:33010,cmnt:basic toon shader,varname:node_8964,prsc:2|IN-6412-OUT,STPS-5105-OUT;n:type:ShaderForge.SFN_ValueProperty,id:5105,x:31950,y:33010,ptovrint:False,ptlb:toonSteps,ptin:_toonSteps,varname:node_5105,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:2;n:type:ShaderForge.SFN_LightColor,id:3543,x:31695,y:33445,varname:node_3543,prsc:2;n:type:ShaderForge.SFN_Desaturate,id:6654,x:31916,y:33390,varname:node_6654,prsc:2|COL-3543-RGB,DES-1478-OUT;n:type:ShaderForge.SFN_ValueProperty,id:1478,x:31695,y:33586,ptovrint:False,ptlb:desaturateNum,ptin:_desaturateNum,varname:node_1478,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_Color,id:6799,x:32033,y:32819,ptovrint:False,ptlb:color,ptin:_color,varname:node_6799,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Multiply,id:4375,x:32504,y:32737,varname:node_4375,prsc:2|A-9911-RGB,B-6799-RGB;n:type:ShaderForge.SFN_Multiply,id:310,x:32668,y:32790,varname:node_310,prsc:2|A-4375-OUT,B-1748-RGB;n:type:ShaderForge.SFN_AmbientLight,id:1748,x:32412,y:32887,varname:node_1748,prsc:2;n:type:ShaderForge.SFN_Multiply,id:6161,x:32952,y:32813,varname:node_6161,prsc:2|A-310-OUT,B-8964-OUT;proporder:9911-5105-1478-6799;pass:END;sub:END;*/

Shader "Shader Forge/celShader" {
    Properties {
        _texture ("texture", 2D) = "white" {}
        _toonSteps ("toonSteps", Float ) = 2
        _desaturateNum ("desaturateNum", Float ) = 0
        _color ("color", Color) = (0.5,0.5,0.5,1)
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
            uniform sampler2D _texture; uniform float4 _texture_ST;
            uniform float _toonSteps;
            uniform float _desaturateNum;
            uniform float4 _color;
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
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float4 _texture_var = tex2D(_texture,TRANSFORM_TEX(i.uv0, _texture));
                float3 finalColor = (((_texture_var.rgb*_color.rgb)*UNITY_LIGHTMODEL_AMBIENT.rgb)*floor((0.5*dot(lightDirection,i.normalDir)+0.5*attenuation*lerp(_LightColor0.rgb,dot(_LightColor0.rgb,float3(0.3,0.59,0.11)),_desaturateNum)) * _toonSteps) / (_toonSteps - 1));
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
            uniform sampler2D _texture; uniform float4 _texture_ST;
            uniform float _toonSteps;
            uniform float _desaturateNum;
            uniform float4 _color;
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
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float4 _texture_var = tex2D(_texture,TRANSFORM_TEX(i.uv0, _texture));
                float3 finalColor = (((_texture_var.rgb*_color.rgb)*UNITY_LIGHTMODEL_AMBIENT.rgb)*floor((0.5*dot(lightDirection,i.normalDir)+0.5*attenuation*lerp(_LightColor0.rgb,dot(_LightColor0.rgb,float3(0.3,0.59,0.11)),_desaturateNum)) * _toonSteps) / (_toonSteps - 1));
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

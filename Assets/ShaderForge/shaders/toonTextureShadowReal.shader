// Shader created with Shader Forge v1.38 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.092443,fgcg:0.1056425,fgcb:0.169,fgca:1,fgde:0.02,fgrn:0,fgrf:50,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:9361,x:33209,y:32712,varname:node_9361,prsc:2|custl-759-OUT;n:type:ShaderForge.SFN_ScreenParameters,id:823,x:30425,y:33125,varname:node_823,prsc:2;n:type:ShaderForge.SFN_Divide,id:1185,x:30632,y:33184,varname:node_1185,prsc:2|A-823-PXH,B-823-PXW;n:type:ShaderForge.SFN_Multiply,id:2861,x:30800,y:33152,varname:node_2861,prsc:2|A-2877-V,B-1185-OUT;n:type:ShaderForge.SFN_Append,id:881,x:30880,y:33021,varname:node_881,prsc:2|A-2877-U,B-2861-OUT;n:type:ShaderForge.SFN_ScreenPos,id:2877,x:30632,y:32996,varname:node_2877,prsc:2,sctp:0;n:type:ShaderForge.SFN_Multiply,id:404,x:31079,y:33135,varname:node_404,prsc:2|A-881-OUT,B-9003-OUT;n:type:ShaderForge.SFN_ValueProperty,id:9003,x:30903,y:33315,ptovrint:False,ptlb:backSize,ptin:_backSize,varname:node_9003,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_RemapRange,id:8516,x:31250,y:33135,varname:node_8516,prsc:2,frmn:-1,frmx:1,tomn:0,tomx:1|IN-404-OUT;n:type:ShaderForge.SFN_Tex2d,id:5674,x:31450,y:33070,ptovrint:False,ptlb:backTexture,ptin:_backTexture,cmnt:all this stuff sets the background image on screen space,varname:node_5674,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:25bd03383f50e44568b8c0fb4dcb3a01,ntxv:0,isnm:False|UVIN-8516-OUT;n:type:ShaderForge.SFN_TexCoord,id:3920,x:31334,y:32604,varname:node_3920,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Tex2d,id:9911,x:31528,y:32533,ptovrint:False,ptlb:texture,ptin:_texture,varname:node_9911,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:ac0845f35a1b8465384c0339df4eb943,ntxv:0,isnm:False|UVIN-3920-UVOUT;n:type:ShaderForge.SFN_Lerp,id:5968,x:32016,y:32880,cmnt:this makes it so it only shows up in the shadow,varname:node_5968,prsc:2|A-2486-RGB,B-6402-OUT,T-5674-RGB;n:type:ShaderForge.SFN_LightVector,id:8777,x:31508,y:33251,varname:node_8777,prsc:2;n:type:ShaderForge.SFN_NormalVector,id:3138,x:31545,y:33380,prsc:2,pt:False;n:type:ShaderForge.SFN_Dot,id:4234,x:31734,y:33251,varname:node_4234,prsc:2,dt:4|A-8777-OUT,B-3138-OUT;n:type:ShaderForge.SFN_Multiply,id:6412,x:31919,y:33307,varname:node_6412,prsc:2|A-4234-OUT,B-4158-OUT;n:type:ShaderForge.SFN_LightAttenuation,id:4158,x:31545,y:33571,varname:node_4158,prsc:2;n:type:ShaderForge.SFN_Lerp,id:2684,x:32546,y:32899,varname:node_2684,prsc:2|A-5968-OUT,B-6402-OUT,T-5691-OUT;n:type:ShaderForge.SFN_Posterize,id:8964,x:32086,y:33141,cmnt:basic toon shader,varname:node_8964,prsc:2|IN-6412-OUT,STPS-5105-OUT;n:type:ShaderForge.SFN_ValueProperty,id:5105,x:31861,y:33141,ptovrint:False,ptlb:toonSteps,ptin:_toonSteps,varname:node_5105,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:2;n:type:ShaderForge.SFN_Color,id:2486,x:31603,y:32860,ptovrint:False,ptlb:backTextureColor,ptin:_backTextureColor,varname:node_2486,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.9191176,c2:0,c3:0,c4:1;n:type:ShaderForge.SFN_Relay,id:759,x:32761,y:32811,cmnt:this is a toon shader and shadow is a texture,varname:node_759,prsc:2|IN-2684-OUT;n:type:ShaderForge.SFN_Color,id:4809,x:31488,y:32724,ptovrint:False,ptlb:color,ptin:_color,varname:node_4809,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_Multiply,id:6402,x:31761,y:32657,varname:node_6402,prsc:2|A-9911-RGB,B-4809-RGB;n:type:ShaderForge.SFN_OneMinus,id:1651,x:32307,y:33170,varname:node_1651,prsc:2|IN-8964-OUT;n:type:ShaderForge.SFN_SwitchProperty,id:5691,x:32388,y:32986,ptovrint:False,ptlb:inverseShadow,ptin:_inverseShadow,varname:node_5691,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,on:False|A-8964-OUT,B-1651-OUT;proporder:9911-4809-5674-2486-9003-5105-5691;pass:END;sub:END;*/

Shader "Shader Forge/toonTextureShadowReal" {
    Properties {
        _texture ("texture", 2D) = "white" {}
        _color ("color", Color) = (1,1,1,1)
        _backTexture ("backTexture", 2D) = "white" {}
        _backTextureColor ("backTextureColor", Color) = (0.9191176,0,0,1)
        _backSize ("backSize", Float ) = 1
        _toonSteps ("toonSteps", Float ) = 2
        [MaterialToggle] _inverseShadow ("inverseShadow", Float ) = 0
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
            #pragma only_renderers d3d9 d3d11 glcore gles gles3 metal 
            #pragma target 3.0
            uniform float _backSize;
            uniform sampler2D _backTexture; uniform float4 _backTexture_ST;
            uniform sampler2D _texture; uniform float4 _texture_ST;
            uniform float _toonSteps;
            uniform float4 _backTextureColor;
            uniform float4 _color;
            uniform fixed _inverseShadow;
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
                float4 projPos : TEXCOORD3;
                LIGHTING_COORDS(4,5)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos( v.vertex );
                o.projPos = ComputeScreenPos (o.pos);
                COMPUTE_EYEDEPTH(o.projPos.z);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 normalDirection = i.normalDir;
                float2 sceneUVs = (i.projPos.xy / i.projPos.w);
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float4 _texture_var = tex2D(_texture,TRANSFORM_TEX(i.uv0, _texture));
                float3 node_6402 = (_texture_var.rgb*_color.rgb);
                float2 node_8516 = ((float2((sceneUVs * 2 - 1).r,((sceneUVs * 2 - 1).g*(_ScreenParams.g/_ScreenParams.r)))*_backSize)*0.5+0.5);
                float4 _backTexture_var = tex2D(_backTexture,TRANSFORM_TEX(node_8516, _backTexture)); // all this stuff sets the background image on screen space
                float node_8964 = floor((0.5*dot(lightDirection,i.normalDir)+0.5*attenuation) * _toonSteps) / (_toonSteps - 1); // basic toon shader
                float3 finalColor = lerp(lerp(_backTextureColor.rgb,node_6402,_backTexture_var.rgb),node_6402,lerp( node_8964, (1.0 - node_8964), _inverseShadow ));
                return fixed4(finalColor,1);
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
            #pragma only_renderers d3d9 d3d11 glcore gles gles3 metal 
            #pragma target 3.0
            uniform float _backSize;
            uniform sampler2D _backTexture; uniform float4 _backTexture_ST;
            uniform sampler2D _texture; uniform float4 _texture_ST;
            uniform float _toonSteps;
            uniform float4 _backTextureColor;
            uniform float4 _color;
            uniform fixed _inverseShadow;
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
                float4 projPos : TEXCOORD3;
                LIGHTING_COORDS(4,5)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos( v.vertex );
                o.projPos = ComputeScreenPos (o.pos);
                COMPUTE_EYEDEPTH(o.projPos.z);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 normalDirection = i.normalDir;
                float2 sceneUVs = (i.projPos.xy / i.projPos.w);
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float4 _texture_var = tex2D(_texture,TRANSFORM_TEX(i.uv0, _texture));
                float3 node_6402 = (_texture_var.rgb*_color.rgb);
                float2 node_8516 = ((float2((sceneUVs * 2 - 1).r,((sceneUVs * 2 - 1).g*(_ScreenParams.g/_ScreenParams.r)))*_backSize)*0.5+0.5);
                float4 _backTexture_var = tex2D(_backTexture,TRANSFORM_TEX(node_8516, _backTexture)); // all this stuff sets the background image on screen space
                float node_8964 = floor((0.5*dot(lightDirection,i.normalDir)+0.5*attenuation) * _toonSteps) / (_toonSteps - 1); // basic toon shader
                float3 finalColor = lerp(lerp(_backTextureColor.rgb,node_6402,_backTexture_var.rgb),node_6402,lerp( node_8964, (1.0 - node_8964), _inverseShadow ));
                return fixed4(finalColor * 1,0);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}

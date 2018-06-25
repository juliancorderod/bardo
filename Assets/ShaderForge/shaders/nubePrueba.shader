// Shader created with Shader Forge v1.38 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:True,fgod:False,fgor:False,fgmd:0,fgcr:0.092443,fgcg:0.1056425,fgcb:0.169,fgca:1,fgde:0.02,fgrn:0,fgrf:50,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:9361,x:33209,y:32712,varname:node_9361,prsc:2|custl-2486-RGB,alpha-6402-OUT;n:type:ShaderForge.SFN_ScreenParameters,id:823,x:31318,y:33052,varname:node_823,prsc:2;n:type:ShaderForge.SFN_Divide,id:1185,x:31525,y:33111,varname:node_1185,prsc:2|A-823-PXH,B-823-PXW;n:type:ShaderForge.SFN_Multiply,id:2861,x:31693,y:33079,varname:node_2861,prsc:2|A-2877-V,B-1185-OUT;n:type:ShaderForge.SFN_Append,id:881,x:31773,y:32948,varname:node_881,prsc:2|A-2877-U,B-2861-OUT;n:type:ShaderForge.SFN_ScreenPos,id:2877,x:31525,y:32923,varname:node_2877,prsc:2,sctp:0;n:type:ShaderForge.SFN_Multiply,id:404,x:31991,y:33048,varname:node_404,prsc:2|A-881-OUT,B-2602-OUT;n:type:ShaderForge.SFN_ValueProperty,id:9003,x:31918,y:33499,ptovrint:False,ptlb:backSize,ptin:_backSize,varname:node_9003,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:4;n:type:ShaderForge.SFN_RemapRange,id:8516,x:32197,y:32979,varname:node_8516,prsc:2,frmn:-1,frmx:1,tomn:0,tomx:1|IN-404-OUT;n:type:ShaderForge.SFN_Tex2d,id:5674,x:32369,y:32961,ptovrint:False,ptlb:backTexture,ptin:_backTexture,cmnt:all this stuff sets the background image on screen space,varname:node_5674,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:28c7aad1372ff114b90d330f8a2dd938,ntxv:0,isnm:False|UVIN-8516-OUT;n:type:ShaderForge.SFN_Color,id:2486,x:32385,y:32722,ptovrint:False,ptlb:TextureColor,ptin:_TextureColor,varname:node_2486,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_Multiply,id:6402,x:32962,y:33230,varname:node_6402,prsc:2|A-7233-VOUT,B-2486-A,C-5674-A,D-5967-OUT;n:type:ShaderForge.SFN_Multiply,id:4710,x:32646,y:32988,varname:node_4710,prsc:2|A-2486-RGB,B-5674-RGB;n:type:ShaderForge.SFN_RgbToHsv,id:7233,x:32493,y:33313,varname:node_7233,prsc:2|IN-4710-OUT;n:type:ShaderForge.SFN_ObjectPosition,id:7747,x:31639,y:33308,varname:node_7747,prsc:2;n:type:ShaderForge.SFN_Divide,id:2602,x:32088,y:33273,varname:node_2602,prsc:2|A-2538-OUT,B-9003-OUT;n:type:ShaderForge.SFN_Distance,id:2538,x:31864,y:33341,varname:node_2538,prsc:2|A-7747-XYZ,B-1493-XYZ;n:type:ShaderForge.SFN_ViewPosition,id:1493,x:31669,y:33438,varname:node_1493,prsc:2;n:type:ShaderForge.SFN_Fresnel,id:7904,x:32422,y:33477,varname:node_7904,prsc:2|EXP-2245-OUT;n:type:ShaderForge.SFN_OneMinus,id:2111,x:32575,y:33438,varname:node_2111,prsc:2|IN-7904-OUT;n:type:ShaderForge.SFN_Vector1,id:2245,x:32219,y:33499,varname:node_2245,prsc:2,v1:1;n:type:ShaderForge.SFN_Power,id:5967,x:32753,y:33496,varname:node_5967,prsc:2|VAL-2111-OUT,EXP-6257-OUT;n:type:ShaderForge.SFN_Vector1,id:6257,x:32591,y:33602,varname:node_6257,prsc:2,v1:3.5;proporder:5674-2486-9003;pass:END;sub:END;*/

Shader "Shader Forge/nubePrueba" {
    Properties {
        _backTexture ("backTexture", 2D) = "white" {}
        _TextureColor ("TextureColor", Color) = (1,1,1,1)
        _backSize ("backSize", Float ) = 4
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma only_renderers d3d9 d3d11 glcore gles gles3 metal 
            #pragma target 3.0
            uniform float _backSize;
            uniform sampler2D _backTexture; uniform float4 _backTexture_ST;
            uniform float4 _TextureColor;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float4 posWorld : TEXCOORD0;
                float3 normalDir : TEXCOORD1;
                float4 projPos : TEXCOORD2;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                float4 objPos = mul ( unity_ObjectToWorld, float4(0,0,0,1) );
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos( v.vertex );
                o.projPos = ComputeScreenPos (o.pos);
                COMPUTE_EYEDEPTH(o.projPos.z);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                float4 objPos = mul ( unity_ObjectToWorld, float4(0,0,0,1) );
                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float2 sceneUVs = (i.projPos.xy / i.projPos.w);
////// Lighting:
                float3 finalColor = _TextureColor.rgb;
                float2 node_8516 = ((float2((sceneUVs * 2 - 1).r,((sceneUVs * 2 - 1).g*(_ScreenParams.g/_ScreenParams.r)))*(distance(objPos.rgb,_WorldSpaceCameraPos)/_backSize))*0.5+0.5);
                float4 _backTexture_var = tex2D(_backTexture,TRANSFORM_TEX(node_8516, _backTexture)); // all this stuff sets the background image on screen space
                float3 node_4710 = (_TextureColor.rgb*_backTexture_var.rgb);
                float4 node_7233_k = float4(0.0, -1.0 / 3.0, 2.0 / 3.0, -1.0);
                float4 node_7233_p = lerp(float4(float4(node_4710,0.0).zy, node_7233_k.wz), float4(float4(node_4710,0.0).yz, node_7233_k.xy), step(float4(node_4710,0.0).z, float4(node_4710,0.0).y));
                float4 node_7233_q = lerp(float4(node_7233_p.xyw, float4(node_4710,0.0).x), float4(float4(node_4710,0.0).x, node_7233_p.yzx), step(node_7233_p.x, float4(node_4710,0.0).x));
                float node_7233_d = node_7233_q.x - min(node_7233_q.w, node_7233_q.y);
                float node_7233_e = 1.0e-10;
                float3 node_7233 = float3(abs(node_7233_q.z + (node_7233_q.w - node_7233_q.y) / (6.0 * node_7233_d + node_7233_e)), node_7233_d / (node_7233_q.x + node_7233_e), node_7233_q.x);;
                return fixed4(finalColor,(node_7233.b*_TextureColor.a*_backTexture_var.a*pow((1.0 - pow(1.0-max(0,dot(normalDirection, viewDirection)),1.0)),3.5)));
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}

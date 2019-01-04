// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "TerrainGround"
{
	Properties
	{
		[HideInInspector]_Control("Control", 2D) = "white" {}
		[HideInInspector]_Splat3("Splat3", 2D) = "white" {}
		[HideInInspector]_Splat2("Splat2", 2D) = "white" {}
		[HideInInspector]_Splat1("Splat1", 2D) = "white" {}
		[HideInInspector]_Splat0("Splat0", 2D) = "white" {}
		[HideInInspector]_Smoothness3("Smoothness3", Range( 0 , 1)) = 1
		[HideInInspector]_Smoothness1("Smoothness1", Range( 0 , 1)) = 1
		[HideInInspector]_Smoothness0("Smoothness0", Range( 0 , 1)) = 1
		[HideInInspector]_Smoothness2("Smoothness2", Range( 0 , 1)) = 1
		_lightAbsorb("lightAbsorb", Range( 0 , 1)) = 1
		_noisy("noisy", 2D) = "white" {}
		_Distance("Distance", Float) = -2
		_Falloff("Falloff", Float) = 10
		[Toggle(_REVERSE_NOISE_ON)] _reverse_noise("reverse_noise", Float) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" }
		Cull Back
		CGINCLUDE
		#include "UnityPBSLighting.cginc"
		#include "UnityCG.cginc"
		#include "UnityShaderVariables.cginc"
		#include "Lighting.cginc"
		#pragma target 3.0
		#pragma shader_feature _REVERSE_NOISE_ON
		#ifdef UNITY_PASS_SHADOWCASTER
			#undef INTERNAL_DATA
			#undef WorldReflectionVector
			#undef WorldNormalVector
			#define INTERNAL_DATA half3 internalSurfaceTtoW0; half3 internalSurfaceTtoW1; half3 internalSurfaceTtoW2;
			#define WorldReflectionVector(data,normal) reflect (data.worldRefl, half3(dot(data.internalSurfaceTtoW0,normal), dot(data.internalSurfaceTtoW1,normal), dot(data.internalSurfaceTtoW2,normal)))
			#define WorldNormalVector(data,normal) half3(dot(data.internalSurfaceTtoW0,normal), dot(data.internalSurfaceTtoW1,normal), dot(data.internalSurfaceTtoW2,normal))
		#endif
		struct Input
		{
			float2 uv_texcoord;
			float3 worldNormal;
			INTERNAL_DATA
			float3 worldPos;
			float eyeDepth;
		};

		struct SurfaceOutputCustomLightingCustom
		{
			half3 Albedo;
			half3 Normal;
			half3 Emission;
			half Metallic;
			half Smoothness;
			half Occlusion;
			half Alpha;
			Input SurfInput;
			UnityGIInput GIData;
		};

		uniform sampler2D _Control;
		uniform float4 _Control_ST;
		uniform float _Smoothness0;
		uniform sampler2D _Splat0;
		uniform float4 _Splat0_ST;
		uniform float _Smoothness1;
		uniform sampler2D _Splat1;
		uniform float4 _Splat1_ST;
		uniform float _Smoothness2;
		uniform sampler2D _Splat2;
		uniform float4 _Splat2_ST;
		uniform float _Smoothness3;
		uniform sampler2D _Splat3;
		uniform float4 _Splat3_ST;
		uniform float _lightAbsorb;
		uniform sampler2D _noisy;
		uniform float4 _noisy_ST;
		uniform float _Falloff;
		uniform float _Distance;


		float3 mod2D289( float3 x ) { return x - floor( x * ( 1.0 / 289.0 ) ) * 289.0; }

		float2 mod2D289( float2 x ) { return x - floor( x * ( 1.0 / 289.0 ) ) * 289.0; }

		float3 permute( float3 x ) { return mod2D289( ( ( x * 34.0 ) + 1.0 ) * x ); }

		float snoise( float2 v )
		{
			const float4 C = float4( 0.211324865405187, 0.366025403784439, -0.577350269189626, 0.024390243902439 );
			float2 i = floor( v + dot( v, C.yy ) );
			float2 x0 = v - i + dot( i, C.xx );
			float2 i1;
			i1 = ( x0.x > x0.y ) ? float2( 1.0, 0.0 ) : float2( 0.0, 1.0 );
			float4 x12 = x0.xyxy + C.xxzz;
			x12.xy -= i1;
			i = mod2D289( i );
			float3 p = permute( permute( i.y + float3( 0.0, i1.y, 1.0 ) ) + i.x + float3( 0.0, i1.x, 1.0 ) );
			float3 m = max( 0.5 - float3( dot( x0, x0 ), dot( x12.xy, x12.xy ), dot( x12.zw, x12.zw ) ), 0.0 );
			m = m * m;
			m = m * m;
			float3 x = 2.0 * frac( p * C.www ) - 1.0;
			float3 h = abs( x ) - 0.5;
			float3 ox = floor( x + 0.5 );
			float3 a0 = x - ox;
			m *= 1.79284291400159 - 0.85373472095314 * ( a0 * a0 + h * h );
			float3 g;
			g.x = a0.x * x0.x + h.x * x0.y;
			g.yz = a0.yz * x12.xz + h.yz * x12.yw;
			return 130.0 * dot( m, g );
		}


		void vertexDataFunc( inout appdata_full v, out Input o )
		{
			UNITY_INITIALIZE_OUTPUT( Input, o );
			float localCalculateTangentsStandard16_g9 = ( 0.0 );
			v.tangent.xyz = cross ( v.normal, float3( 0, 0, 1 ) );
			v.tangent.w = -1;
			float3 temp_cast_0 = (localCalculateTangentsStandard16_g9).xxx;
			v.vertex.xyz += temp_cast_0;
			o.eyeDepth = -UnityObjectToViewPos( v.vertex.xyz ).z;
		}

		inline half4 LightingStandardCustomLighting( inout SurfaceOutputCustomLightingCustom s, half3 viewDir, UnityGI gi )
		{
			UnityGIInput data = s.GIData;
			Input i = s.SurfInput;
			half4 c = 0;
			#ifdef UNITY_PASS_FORWARDBASE
			float ase_lightAtten = data.atten;
			if( _LightColor0.a == 0)
			ase_lightAtten = 0;
			#else
			float3 ase_lightAttenRGB = gi.light.color / ( ( _LightColor0.rgb ) + 0.000001 );
			float ase_lightAtten = max( max( ase_lightAttenRGB.r, ase_lightAttenRGB.g ), ase_lightAttenRGB.b );
			#endif
			#if defined(HANDLE_SHADOWS_BLENDING_IN_GI)
			half bakedAtten = UnitySampleBakedOcclusion(data.lightmapUV.xy, data.worldPos);
			float zDist = dot(_WorldSpaceCameraPos - data.worldPos, UNITY_MATRIX_V[2].xyz);
			float fadeDist = UnityComputeShadowFadeDistance(data.worldPos, zDist);
			ase_lightAtten = UnityMixRealtimeAndBakedShadows(data.atten, bakedAtten, UnityComputeShadowFade(fadeDist));
			#endif
			float2 uv_Control = i.uv_texcoord * _Control_ST.xy + _Control_ST.zw;
			float4 tex2DNode5_g9 = tex2D( _Control, uv_Control );
			float dotResult20_g9 = dot( tex2DNode5_g9 , float4(1,1,1,1) );
			float SplatWeight22_g9 = dotResult20_g9;
			float localSplatClip74_g9 = ( SplatWeight22_g9 );
			float SplatWeight74_g9 = SplatWeight22_g9;
			#if !defined(SHADER_API_MOBILE) && defined(TERRAIN_SPLAT_ADDPASS)
				clip(SplatWeight74_g9 == 0.0f ? -1 : 1);
			#endif
			float4 SplatControl26_g9 = ( tex2DNode5_g9 / ( localSplatClip74_g9 + 0.001 ) );
			float4 temp_output_59_0_g9 = SplatControl26_g9;
			float4 appendResult33_g9 = (float4(1.0 , 1.0 , 1.0 , _Smoothness0));
			float2 uv_Splat0 = i.uv_texcoord * _Splat0_ST.xy + _Splat0_ST.zw;
			float4 appendResult36_g9 = (float4(1.0 , 1.0 , 1.0 , _Smoothness1));
			float2 uv_Splat1 = i.uv_texcoord * _Splat1_ST.xy + _Splat1_ST.zw;
			float4 appendResult39_g9 = (float4(1.0 , 1.0 , 1.0 , _Smoothness2));
			float2 uv_Splat2 = i.uv_texcoord * _Splat2_ST.xy + _Splat2_ST.zw;
			float4 appendResult42_g9 = (float4(1.0 , 1.0 , 1.0 , _Smoothness3));
			float2 uv_Splat3 = i.uv_texcoord * _Splat3_ST.xy + _Splat3_ST.zw;
			float4 weightedBlendVar9_g9 = temp_output_59_0_g9;
			float4 weightedBlend9_g9 = ( weightedBlendVar9_g9.x*( appendResult33_g9 * tex2D( _Splat0, uv_Splat0 ) ) + weightedBlendVar9_g9.y*( appendResult36_g9 * tex2D( _Splat1, uv_Splat1 ) ) + weightedBlendVar9_g9.z*( appendResult39_g9 * tex2D( _Splat2, uv_Splat2 ) ) + weightedBlendVar9_g9.w*( appendResult42_g9 * tex2D( _Splat3, uv_Splat3 ) ) );
			float4 MixDiffuse28_g9 = weightedBlend9_g9;
			float4 temp_output_31_0 = ( MixDiffuse28_g9 * _lightAbsorb );
			float2 uv_noisy = i.uv_texcoord * _noisy_ST.xy + _noisy_ST.zw;
			float4 tex2DNode11 = tex2D( _noisy, uv_noisy );
			float3 ase_worldNormal = WorldNormalVector( i, float3( 0, 0, 1 ) );
			float3 ase_worldPos = i.worldPos;
			#if defined(LIGHTMAP_ON) && UNITY_VERSION < 560 //aseld
			float3 ase_worldlightDir = 0;
			#else //aseld
			float3 ase_worldlightDir = normalize( UnityWorldSpaceLightDir( ase_worldPos ) );
			#endif //aseld
			float dotResult68 = dot( ase_worldNormal , ase_worldlightDir );
			#if defined(LIGHTMAP_ON) && UNITY_VERSION < 560 //aselc
			float4 ase_lightColor = 0;
			#else //aselc
			float4 ase_lightColor = _LightColor0;
			#endif //aselc
			float clampResult70 = clamp( ase_lightColor.a , 0.0 , 2.0 );
			float4 lerpResult40 = lerp( temp_output_31_0 , ( temp_output_31_0 * tex2DNode11 ) , saturate( ( pow( ( 1.0 - dotResult68 ) , 2.0 ) + ( 1.0 - ( ase_lightAtten * clampResult70 ) ) ) ));
			float2 panner97 = ( 1.0 * _Time.y * float2( 1,1 ) + uv_noisy);
			float simplePerlin2D98 = snoise( ( panner97 * float2( 0.1,0.1 ) ) );
			float4 lerpResult110 = lerp( tex2DNode11 , float4(1,1,1,0) , (0.65 + (simplePerlin2D98 - 0.0) * (1.0 - 0.65) / (1.0 - 0.0)));
			float cameraDepthFade8 = (( i.eyeDepth -_ProjectionParams.y - _Distance ) / _Falloff);
			float clampResult24 = clamp( cameraDepthFade8 , 0.0 , 1.0 );
			#ifdef _REVERSE_NOISE_ON
				float staticSwitch38 = ( 1.0 - clampResult24 );
			#else
				float staticSwitch38 = clampResult24;
			#endif
			float4 lerpResult15 = lerp( lerpResult110 , tex2DNode11 , staticSwitch38);
			c.rgb = ( lerpResult40 * lerpResult15 ).xyz;
			c.a = 1;
			return c;
		}

		inline void LightingStandardCustomLighting_GI( inout SurfaceOutputCustomLightingCustom s, UnityGIInput data, inout UnityGI gi )
		{
			s.GIData = data;
		}

		void surf( Input i , inout SurfaceOutputCustomLightingCustom o )
		{
			o.SurfInput = i;
			o.Normal = float3(0,0,1);
		}

		ENDCG
		CGPROGRAM
		#pragma surface surf StandardCustomLighting keepalpha fullforwardshadows vertex:vertexDataFunc 

		ENDCG
		Pass
		{
			Name "ShadowCaster"
			Tags{ "LightMode" = "ShadowCaster" }
			ZWrite On
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 3.0
			#pragma multi_compile_shadowcaster
			#pragma multi_compile UNITY_PASS_SHADOWCASTER
			#pragma skip_variants FOG_LINEAR FOG_EXP FOG_EXP2
			#include "HLSLSupport.cginc"
			#if ( SHADER_API_D3D11 || SHADER_API_GLCORE || SHADER_API_GLES3 || SHADER_API_METAL || SHADER_API_VULKAN )
				#define CAN_SKIP_VPOS
			#endif
			#include "UnityCG.cginc"
			#include "Lighting.cginc"
			#include "UnityPBSLighting.cginc"
			struct v2f
			{
				V2F_SHADOW_CASTER;
				float3 customPack1 : TEXCOORD1;
				float4 tSpace0 : TEXCOORD2;
				float4 tSpace1 : TEXCOORD3;
				float4 tSpace2 : TEXCOORD4;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};
			v2f vert( appdata_full v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID( v );
				UNITY_INITIALIZE_OUTPUT( v2f, o );
				UNITY_TRANSFER_INSTANCE_ID( v, o );
				Input customInputData;
				vertexDataFunc( v, customInputData );
				float3 worldPos = mul( unity_ObjectToWorld, v.vertex ).xyz;
				half3 worldNormal = UnityObjectToWorldNormal( v.normal );
				half3 worldTangent = UnityObjectToWorldDir( v.tangent.xyz );
				half tangentSign = v.tangent.w * unity_WorldTransformParams.w;
				half3 worldBinormal = cross( worldNormal, worldTangent ) * tangentSign;
				o.tSpace0 = float4( worldTangent.x, worldBinormal.x, worldNormal.x, worldPos.x );
				o.tSpace1 = float4( worldTangent.y, worldBinormal.y, worldNormal.y, worldPos.y );
				o.tSpace2 = float4( worldTangent.z, worldBinormal.z, worldNormal.z, worldPos.z );
				o.customPack1.xy = customInputData.uv_texcoord;
				o.customPack1.xy = v.texcoord;
				o.customPack1.z = customInputData.eyeDepth;
				TRANSFER_SHADOW_CASTER_NORMALOFFSET( o )
				return o;
			}
			half4 frag( v2f IN
			#if !defined( CAN_SKIP_VPOS )
			, UNITY_VPOS_TYPE vpos : VPOS
			#endif
			) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID( IN );
				Input surfIN;
				UNITY_INITIALIZE_OUTPUT( Input, surfIN );
				surfIN.uv_texcoord = IN.customPack1.xy;
				surfIN.eyeDepth = IN.customPack1.z;
				float3 worldPos = float3( IN.tSpace0.w, IN.tSpace1.w, IN.tSpace2.w );
				half3 worldViewDir = normalize( UnityWorldSpaceViewDir( worldPos ) );
				surfIN.worldPos = worldPos;
				surfIN.worldNormal = float3( IN.tSpace0.z, IN.tSpace1.z, IN.tSpace2.z );
				surfIN.internalSurfaceTtoW0 = IN.tSpace0.xyz;
				surfIN.internalSurfaceTtoW1 = IN.tSpace1.xyz;
				surfIN.internalSurfaceTtoW2 = IN.tSpace2.xyz;
				SurfaceOutputCustomLightingCustom o;
				UNITY_INITIALIZE_OUTPUT( SurfaceOutputCustomLightingCustom, o )
				surf( surfIN, o );
				#if defined( CAN_SKIP_VPOS )
				float2 vpos = IN.pos;
				#endif
				SHADOW_CASTER_FRAGMENT( IN )
			}
			ENDCG
		}
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=15800
426;125;923;722;2961.73;111.3637;2.313762;True;False
Node;AmplifyShaderEditor.CommentaryNode;47;-2386.847,-351.0933;Float;False;1218.14;641.2961;this calculates shadows for texture overlay;13;76;77;75;72;71;65;66;68;69;73;74;70;67;;1,0.9919769,0,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;111;-2481.421,325.4589;Float;False;1020.683;288.2396;windy/ cloudy pattern;5;108;98;104;97;103;;0,0,0,1;0;0
Node;AmplifyShaderEditor.WorldNormalVector;66;-2330.462,-289.1498;Float;False;False;1;0;FLOAT3;0,0,1;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.TextureCoordinatesNode;103;-2471.087,461.1838;Float;False;0;11;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.CommentaryNode;45;-1396.646,437.9987;Float;False;1041.899;651.2589;this calculates noise from camera;9;38;28;37;24;8;22;23;10;9;;0.2836168,1,0.189,1;0;0
Node;AmplifyShaderEditor.WorldSpaceLightDirHlpNode;65;-2379.024,-125.0137;Float;False;False;1;0;FLOAT;0;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.LightColorNode;67;-2375.357,123.9885;Float;False;0;3;COLOR;0;FLOAT3;1;FLOAT;2
Node;AmplifyShaderEditor.RangedFloatNode;9;-1345.848,619.1422;Float;False;Property;_Falloff;Falloff;21;0;Create;True;0;0;False;0;10;20.44;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;10;-1350.904,791.0443;Float;False;Property;_Distance;Distance;20;0;Create;True;0;0;False;0;-2;-2.22;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.PannerNode;97;-2240.956,480.1627;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;1,1;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.DotProductOpNode;68;-2091.062,-233.6595;Float;False;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;70;-2188.699,129.6603;Float;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;2;False;1;FLOAT;0
Node;AmplifyShaderEditor.LightAttenuation;69;-2315.564,30.71575;Float;False;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;23;-1025.525,981.4857;Float;False;Constant;_Float1;Float 1;3;0;Create;True;0;0;False;0;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;46;-1229.412,-839.1256;Float;False;788.8998;455.5098;this gets texture from terrain data;3;31;39;58;;1,0.2712016,0,1;0;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;104;-2055.776,482.4843;Float;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0.1,0.1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;73;-2026.089,-11.77029;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;71;-1935.165,-85.67777;Float;False;Constant;_Float9;Float 9;5;0;Create;True;0;0;False;0;2;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;22;-1178.026,893.5044;Float;False;Constant;_Float0;Float 0;3;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.CameraDepthFade;8;-1167.14,674.4888;Float;False;3;2;FLOAT3;0,0,0;False;0;FLOAT;1;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;72;-1962.202,-181.2035;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;74;-1776.942,-22.08532;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;24;-781.1348,739.049;Float;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.NoiseGeneratorNode;98;-1874.488,432.3284;Float;False;Simplex2D;1;0;FLOAT2;0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;58;-1151.797,-561.4392;Float;False;Property;_lightAbsorb;lightAbsorb;18;0;Create;True;0;0;False;0;1;0.8;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.PowerNode;75;-1725.96,-166.9344;Float;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;39;-1189.832,-792.041;Float;False;Four Splats First Pass Terrain;0;;9;37452fdfb732e1443b7e39720d05b708;0;6;59;FLOAT4;0,0,0,0;False;60;FLOAT4;0,0,0,0;False;61;FLOAT3;0,0,0;False;57;FLOAT;0;False;58;FLOAT;0;False;62;FLOAT;0;False;6;FLOAT4;0;FLOAT3;14;FLOAT;56;FLOAT;45;FLOAT;19;FLOAT;17
Node;AmplifyShaderEditor.SimpleAddOpNode;76;-1527.173,-124.4608;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;37;-615.262,889.8755;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCRemapNode;108;-1646.189,374.5055;Float;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;0.65;False;4;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;11;-1115.629,-102.2576;Float;True;Property;_noisy;noisy;19;0;Create;True;0;0;False;0;None;bdbe94d7623ec3940947b62544306f1c;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;31;-680.0952,-655.8647;Float;False;2;2;0;FLOAT4;0,0,0,0;False;1;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.ColorNode;28;-920.3715,515.9144;Float;False;Constant;_Color0;Color 0;4;0;Create;True;0;0;False;0;1,1,1,0;0,0,0,0;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;110;-790.8743,121.5165;Float;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;48;-587.0631,-270.6596;Float;False;2;2;0;FLOAT4;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.StaticSwitch;38;-600.2347,622.4034;Float;False;Property;_reverse_noise;reverse_noise;22;0;Create;True;0;0;False;0;0;0;0;True;;Toggle;2;Key0;Key1;9;1;FLOAT;0;False;0;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT;0;False;7;FLOAT;0;False;8;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;77;-1386.088,-180.9007;Float;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;15;-490.3856,12.22778;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;40;-364.3376,-277.1085;Float;False;3;0;FLOAT4;0,0,0,0;False;1;FLOAT4;0,0,0,0;False;2;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;29;-252.1299,-43.56798;Float;True;2;2;0;FLOAT4;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;31.28291,-132.4449;Float;False;True;2;Float;ASEMaterialInspector;0;0;CustomLighting;TerrainGround;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;97;0;103;0
WireConnection;68;0;66;0
WireConnection;68;1;65;0
WireConnection;70;0;67;2
WireConnection;104;0;97;0
WireConnection;73;0;69;0
WireConnection;73;1;70;0
WireConnection;8;0;9;0
WireConnection;8;1;10;0
WireConnection;72;0;68;0
WireConnection;74;0;73;0
WireConnection;24;0;8;0
WireConnection;24;1;22;0
WireConnection;24;2;23;0
WireConnection;98;0;104;0
WireConnection;75;0;72;0
WireConnection;75;1;71;0
WireConnection;76;0;75;0
WireConnection;76;1;74;0
WireConnection;37;0;24;0
WireConnection;108;0;98;0
WireConnection;31;0;39;0
WireConnection;31;1;58;0
WireConnection;110;0;11;0
WireConnection;110;1;28;0
WireConnection;110;2;108;0
WireConnection;48;0;31;0
WireConnection;48;1;11;0
WireConnection;38;1;24;0
WireConnection;38;0;37;0
WireConnection;77;0;76;0
WireConnection;15;0;110;0
WireConnection;15;1;11;0
WireConnection;15;2;38;0
WireConnection;40;0;31;0
WireConnection;40;1;48;0
WireConnection;40;2;77;0
WireConnection;29;0;40;0
WireConnection;29;1;15;0
WireConnection;0;13;29;0
WireConnection;0;11;39;17
ASEEND*/
//CHKSM=7C7CEBD8F58237D4FE88AA8C81AE553732A594B6
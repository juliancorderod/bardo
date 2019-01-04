// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "customGrain"
{
	Properties
	{
		_MainTex("MainTex", 2D) = "white" {}
		_strength("strength", Range( 0 , 1)) = 0.41
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
	}
	
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100
		CGINCLUDE
		#pragma target 3.0
		ENDCG
		Blend Off
		Cull Back
		ColorMask RGBA
		ZWrite On
		ZTest LEqual
		Offset 0 , 0
		
		

		Pass
		{
			Name "Unlit"
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_instancing
			#include "UnityCG.cginc"
			#include "UnityShaderVariables.cginc"


			struct appdata
			{
				float4 vertex : POSITION;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				float4 ase_texcoord : TEXCOORD0;
			};
			
			struct v2f
			{
				float4 vertex : SV_POSITION;
				float4 ase_texcoord : TEXCOORD0;
				UNITY_VERTEX_OUTPUT_STEREO
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			uniform sampler2D _MainTex;
			uniform float4 _MainTex_ST;
			uniform float _strength;
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
			
			
			v2f vert ( appdata v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
				UNITY_TRANSFER_INSTANCE_ID(v, o);

				o.ase_texcoord.xy = v.ase_texcoord.xy;
				
				//setting value to unused interpolator channels and avoid initialization warnings
				o.ase_texcoord.zw = 0;
				
				v.vertex.xyz +=  float3(0,0,0) ;
				o.vertex = UnityObjectToClipPos(v.vertex);
				return o;
			}
			
			fixed4 frag (v2f i ) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID(i);
				fixed4 finalColor;
				float2 uv_MainTex = i.ase_texcoord.xy * _MainTex_ST.xy + _MainTex_ST.zw;
				float4 tex2DNode1 = tex2D( _MainTex, uv_MainTex );
				float simplePerlin2D16 = snoise( ( uv_MainTex * float2( 500,500 ) ) );
				float4 lerpResult25 = lerp( unity_FogColor , tex2DNode1 , pow( simplePerlin2D16 , 3.0 ));
				float4 lerpResult44 = lerp( tex2DNode1 , lerpResult25 , _strength);
				
				
				finalColor = lerpResult44;
				return finalColor;
			}
			ENDCG
		}
	}
	CustomEditor "ASEMaterialInspector"
	
	
}
/*ASEBEGIN
Version=16100
273;131;849;722;848.297;851.3586;1.287809;True;False
Node;AmplifyShaderEditor.TextureCoordinatesNode;19;-1407.624,-428.4727;Float;False;0;1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;18;-1186.763,-304.0613;Float;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;500,500;False;1;FLOAT2;0
Node;AmplifyShaderEditor.NoiseGeneratorNode;16;-1008.332,-207.183;Float;False;Simplex2D;1;0;FLOAT2;0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;1;-930.199,-708.7971;Float;True;Property;_MainTex;MainTex;0;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.FogAndAmbientColorsNode;22;-1025.624,-419.4727;Float;False;unity_FogColor;0;1;COLOR;0
Node;AmplifyShaderEditor.PowerNode;28;-816.7526,-197.9629;Float;True;2;0;FLOAT;0;False;1;FLOAT;3;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;45;-387.4175,-628.1384;Float;False;Property;_strength;strength;3;0;Create;True;0;0;False;0;0.41;0.41;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;25;-552.5072,-235.1708;Float;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;11;-916.0272,206.9291;Float;False;Property;_Distance;Distance;1;0;Create;True;0;0;False;0;-2;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;44;-162.0731,-400.8994;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;9;-952.8721,80.05939;Float;False;Property;_Falloff;Falloff;2;0;Create;True;0;0;False;0;10;0.34;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ScreenDepthNode;35;-599.6144,284.0674;Float;False;1;False;1;0;FLOAT4;0,0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;15;-191.9839,-163.9857;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.ClampOpNode;14;-430.1391,15.92849;Float;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;36;-434.4713,138.3605;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0.3;False;1;FLOAT;0
Node;AmplifyShaderEditor.CameraDepthFade;10;-727.3226,44.39012;Float;False;3;2;FLOAT3;0,0,0;False;0;FLOAT;1;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;42;4.275471,-165.087;Float;False;True;2;Float;ASEMaterialInspector;0;1;customGrain;0770190933193b94aaa3065e307002fa;0;0;Unlit;2;True;0;1;False;-1;0;False;-1;0;1;False;-1;0;False;-1;True;0;False;-1;0;False;-1;True;0;False;-1;True;True;True;True;True;0;False;-1;True;False;255;False;-1;255;False;-1;255;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;True;1;False;-1;True;3;False;-1;True;True;0;False;-1;0;False;-1;True;1;RenderType=Opaque=RenderType;True;2;0;False;False;False;False;False;False;False;False;False;False;0;;0;0;Standard;0;2;0;FLOAT4;0,0,0,0;False;1;FLOAT3;0,0,0;False;0
WireConnection;18;0;19;0
WireConnection;16;0;18;0
WireConnection;28;0;16;0
WireConnection;25;0;22;0
WireConnection;25;1;1;0
WireConnection;25;2;28;0
WireConnection;44;0;1;0
WireConnection;44;1;25;0
WireConnection;44;2;45;0
WireConnection;15;0;1;0
WireConnection;15;1;25;0
WireConnection;15;2;14;0
WireConnection;14;0;10;0
WireConnection;36;0;35;0
WireConnection;36;1;9;0
WireConnection;10;0;9;0
WireConnection;10;1;11;0
WireConnection;42;0;44;0
ASEEND*/
//CHKSM=A272BA239FFF988FB2D6A66EECB55826B9CC83B8
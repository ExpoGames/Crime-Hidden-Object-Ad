Shader "LiquidVolume/Bump" {
	Properties {
		[HideInInspector] _Color1 ("Color 1", Color) = (1,0,0,0.1)
		[HideInInspector] _FoamColor ("Foam Color", Color) = (1,1,1,0.9)
		[HideInInspector] _Color2 ("Color 2", Color) = (1,0,0,0.3)
		[HideInInspector] _FlaskColor ("Flask Color", Color) = (0,0,0)
		[HideInInspector] _FlaskThickness ("Flask Thickness", Vector) = (0.05,0.05,0.05)
		[HideInInspector] _Glossiness ("Smoothness", Range(0,1)) = 0.5
		[HideInInspector] _GlossinessInt ("Internal Smoothness", Vector) = (0.5, 180, 0.3)
		[HideInInspector] _Muddy ("Muddy", Range(0,1)) = 1.0
		[HideInInspector] _Turbulence ("Turbulence", Vector) = (1.0,1.0,1.0,0)
		[HideInInspector] _TurbulenceSpeed("Turbulence Speed", Float) = 1
		[HideInInspector] _SparklingIntensity ("Sparkling Intensity", Range(0,1)) = 1.0
		[HideInInspector] _SparklingThreshold ("Sparkling Threshold", Range(0,1)) = 0.85
		[HideInInspector] _EmissionColor ("Emission Color", Color) = (0,0,0)

		[HideInInspector] _DeepAtten("Deep Atten", Range(0,10)) = 2.0
		[HideInInspector] _LiquidRaySteps ("Liquid Ray Steps", Int) = 10
		[HideInInspector] _SmokeColor ("Smoke Color", Color) = (0.7,0.7,0.7,0.1)
		[HideInInspector] _SmokeAtten("Smoke Atten", Range(0,10)) = 2.0
		[HideInInspector] _SmokeRaySteps ("Smoke Ray Steps", Int) = 10
		[HideInInspector] _SmokeSpeed ("Smoke Speed", Range(0,20)) = 5.0
		_NoiseTex2D ("Noise Tex 2D", 2D) = "white"
		[HideInInspector] _FlaskBlurIntensity ("Flask Blur Intensity", Float) = 1.0
		[HideInInspector] _FoamRaySteps ("Foam Ray Steps", Int) = 15
		[HideInInspector] _FoamWeight ("Foam Weight", Float) = 10.0
		[HideInInspector] _FoamBottom ("Foam Visible From Bottom", Float) = 1.0
		[HideInInspector] _FoamTurbulence ("Foam Turbulence", Float) = 1.0
		[HideInInspector] _Scale ("Scale", Vector) = (0.25, 0.2, 1, 5.0)
		_BumpMap ("Normal Map", 2D) = "bump" {}
		_DispMap ("Displacement Map", 2D) = "bump" {}
		[HideInInspector] _FlaskTex ("Flask Texture", 2D) = "black" {}
		[HideInInspector] _DispAmount ("Distortion Amount", Float) = 1.0
		[HideInInspector] _FlaskTexAlpha ("Flask Texture Alpha", Float) = 1.0
		
		[HideInInspector] _CullMode ("Cull Mode", Int) = 2
		[HideInInspector] _ZTestMode ("ZTest Mode", Int) = 4

		[HideInInspector] _FoamDensity ("Foam Density", Float) = 1
		[HideInInspector] _AlphaCombined ("Alpha Combined", Float) = 1.0
		[HideInInspector] _FoamMaxPos("Foam Max Pos", Float) = 0
		[HideInInspector] _LevelPos ("Level Pos", Float) = 0
		[HideInInspector] _UpperLimit ("Upper Limit", Float) = 1
		[HideInInspector] _LowerLimit ("Lower Limit", Float) = -1
		[HideInInspector] _NoiseTex ("Noise Tex", 3D) = "white"
		[HideInInspector] _Center ("Center", Vector) = (1,1,1)
		[HideInInspector] _Size ("Size", Vector) = (1,1,1,0.5)
		[HideInInspector] _DoubleSidedBias ("Double Sided Bias", Float) = 0
	}
	SubShader {
	Tags { "Queue" = "Transparent+1" "IgnoreProjector"="True" "RenderType"="Transparent" "DisableBatching"="True" }

	// Shadow ==========================================================================================================================================================
	Pass {	
		Cull Front
		Tags { "LightMode" = "ShadowCaster"  }
		
		CGPROGRAM
		#pragma vertex vert
		#pragma fragment frag
        #pragma multi_compile_shadowcaster
        #pragma fragmentoption ARB_precision_hint_fastest
		#include "LVShadowPass.cginc"
		ENDCG
	} 

	Pass { // Blur background ======================
		ZWrite Off
		Blend SrcAlpha OneMinusSrcAlpha
		Cull [_CullMode]

		CGPROGRAM
		#pragma vertex vert
		#pragma fragment frag
		#pragma target 3.0
		#pragma fragmentoption ARB_precision_hint_fastest
		#pragma multi_compile __ LIQUID_VOLUME_DEPTH_AWARE
		#include "LVBlurPass.cginc"
		ENDCG
	}
	
	// PBS Liquid ====================================================================================================================================
		ZWrite Off
		Cull [_CullMode]
		ZTest [_ZTestMode]
		
		CGPROGRAM
		#pragma vertex vert
		#pragma surface surf WrappedSpecular alpha nolightmap nofog noinstancing noforwardadd
		#pragma fragmentoption ARB_precision_hint_fastest
		#pragma target 3.0
		#pragma multi_compile LIQUID_VOLUME_SPHERE LIQUID_VOLUME_CUBE LIQUID_VOLUME_CYLINDER LIQUID_VOLUME_IRREGULAR
		#pragma multi_compile __ LIQUID_VOLUME_NON_AABB LIQUID_VOLUME_IGNORE_GRAVITY
		#pragma multi_compile __ LIQUID_VOLUME_DEPTH_AWARE
		#pragma multi_compile __ LIQUID_VOLUME_DEPTH_AWARE_PASS
		#include "LVLiquidPass3D.cginc"
		ENDCG
		
		
		// Flask ====================================================================================================================================
		ZWrite Off Cull Back
		
		CGPROGRAM
		#pragma surface surf Standard alpha nolightmap nofog noinstancing noforwardadd
		#pragma target 3.0
				
		struct Input {
			float3 worldPos;
			float2 uv_FlaskTex;
			float2 uv_BumpMap;
		};

		half _Glossiness;
		fixed4 _FlaskTint;
		sampler2D _BumpMap;
		sampler2D _FlaskTex;
		half _FlaskTexAlpha;
				
		void surf (Input IN, inout SurfaceOutputStandard o) {
			half4 p = tex2D(_FlaskTex, IN.uv_FlaskTex);
			o.Alpha = p.a * _FlaskTexAlpha;
			o.Albedo = p.rgb;
			o.Normal = UnpackNormal (tex2D (_BumpMap, IN.uv_BumpMap));
			o.Metallic = 0;
			o.Emission = _FlaskTint.rgb;
			o.Smoothness = _Glossiness;
		}
		ENDCG
	}
	
	Fallback "Transparent/VertexLit"
}

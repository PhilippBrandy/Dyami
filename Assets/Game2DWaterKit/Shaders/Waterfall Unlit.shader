Shader "Game2DWaterKit/Waterfall Unlit"
{
	Properties
	{
		// Base Color Propeties
		[Enum(Solid,0,Gradient,1)] _BaseColor_Mode ("Base Color - Mode", Int) = 0
		_BaseColor ("Base Color", Color) = (0.0, 0.75, 1.0, 0.5)
		_BaseColor_Gradient_Start ("Base Color Gradient - Start", Color) = (0.0, 0.75,1.0,0.5)
		_BaseColor_Gradient_End ("Base Color Gradient - End", Color) = (0.0, 0.35,1.0,0.5)

		// Main Texture
		_MainTex ("Main Texture", 2D) = "black" {}
		_MainTexOpacity ("Main Texture Opacity", Range(0.0,1.0)) = 1.0
		_MainTexScrollSpeed ("Main Texture Scroll Speed", float) = 1.0
		_MainTexNoiseScaleOffset ("Main Texture Noise Scale Offset", vector) = (1.0,1.0,0.0,0.0)
		_MainTexNoiseStrength ("Main Texture Noise Strength", float) = 0.2
		_MainTexNoiseSpeed ("Main Texture Noise Speed", float) = 1.0
		_MainTexNoiseTiling ("Main Texture Noise Tiling", float) = 1.0

		// Top Edge Texture
		_TopEdgeThickness ("Top Edge Thickness", Range(0.0,1.0)) = 0.15
		_TopEdgeTex ("Top Edge Texture", 2D) = "black" {}
		_TopEdgeTexOpacity ("Top Edge Texture Opacity", Range(0.0,1.0)) = 0.75
		_TopEdgeTexNoiseStrength ("Top Edge Texture Noise Strength", float) = 0.2
		_TopEdgeTexNoiseSpeed ("Top Edge Texture Noise Speed", float) = 1.0
		_TopEdgeTexNoiseTiling ("Top Edge Texture Noise Tiling", float) = 1.0
		_TopEdgeTexNoiseScaleOffset ("Top Edge Texture Noise Scale Offset", vector) = (1.0,1.0,0.0,0.0)
		_TopEdgeTexSheetRows ("Top Edge Texture Sheet Rows", Int) = 1 
		_TopEdgeTexSheetFramesPerSecond ("Top Edge Texture Sheet Frames Per Second", Int) = 1

		// Left-Right Edges Texture
		_LeftRightEdgesThickness ("Left-Right Edges Thickness", Range(0.0,1.0)) = 0.15
		_LeftRightEdgesTex ("Left-Right Edges Texture", 2D) = "black" {}
		_LeftRightEdgesTexOpacity ("Left-Right Edges Texture Opacity", Range(0.0,1.0)) = 0.75
		_LeftRightEdgesTexScrollSpeed ("Left-Right Edges Texture Scroll Speed", float) = 1.0
		_LeftRightEdgesTexNoiseStrength ("Left-Right Edges Texture Noise Strength", float) = 0.2
		_LeftRightEdgesTexNoiseSpeed ("Left-Right Edges Texture Noise Speed", float) = 1.0
		_LeftRightEdgesTexNoiseTiling ("Left-Right Edges Texture Tiling", float) = 1.0
		_LeftRightEdgesTexNoiseScaleOffset ("Left-Right Edges Texture Noise Scale Offset", vector) = (1.0,1.0,0.0,0.0)

		// Noise Texture
		_NoiseTex ("Noise Texture", 2D) = "black" {}

		// Keywords States
		_Waterfall2D_MainTexture_ApplyNoise_KeywordState ("Main Texture Distortion Effect Toggle", Int) = 0
		_Waterfall2D_TopEdgeTexture_ApplyNoise_KeywordState ("Top Edge Texture Distortion Effect Toggle", Int) = 0
		_Waterfall2D_TopEdgeTexture_KeywordState ("Top Edge Texture Toggle", Int) = 0
		_Waterfall2D_TopEdgeTextureSheet_KeywordState ("Top Edge Texture Sheet Toggle", Int) = 0
		_Waterfall2D_LeftRightEdgesTexture_KeywordState ("Left-Right Edges Texture Toggle", Int) = 0
		_Waterfall2D_LeftRightEdgesTexture_ApplyNoise_KeywordState ("Left-Right Edges Texture Distortion Effect Toggle", Int) = 0

		// Stencil Options
		[Enum(None,0,Visible Inside Mask,4,Visible Outside Mask,5)] _SpriteMaskInteraction ("Sprite Mask Interaction", Int) = 0
	}
	SubShader
	{
		Tags { "Queue"="Transparent" "RenderType"="Transparent" "IgnoreProjector"="True" }
		LOD 100

		Stencil
		{
			Ref 1
			Comp [_SpriteMaskInteraction]
		}

		Pass
		{
			Cull Off
			ZWrite Off
			Blend SrcAlpha OneMinusSrcAlpha

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			// make fog work
			#pragma multi_compile_fog

			// Base Color shader features
			#pragma shader_feature _ Waterfall2D_BaseColor_Gradient
			// Main Texture shader features
			#pragma shader_feature _ Waterfall2D_MainTexture_ApplyNoise
			// Top Edge shader features
			#pragma shader_feature _ Waterfall2D_TopEdgeTexture
			#pragma shader_feature _ Waterfall2D_TopEdgeTextureSheet
			#pragma shader_feature _ Waterfall2D_TopEdgeTexture_ApplyNoise
			// Left-Right Edges shader features
			#pragma shader_feature _ Waterfall2D_LeftRightEdgesTexture
			#pragma shader_feature _ Waterfall2D_LeftRightEdgesTexture_ApplyNoise
			
			#include "UnityCG.cginc"
			
			// Base Color Variables
			#if defined(Waterfall2D_BaseColor_Gradient)
				fixed4 _BaseColor_Gradient_Start;
				fixed4 _BaseColor_Gradient_End;
			#else
				fixed4 _BaseColor;
			#endif

			// Main Texture Variables
			sampler2D _MainTex;
			float4 _MainTex_ST;
			fixed _MainTexOpacity;
			half _MainTexScrollSpeed;

			#if defined(Waterfall2D_MainTexture_ApplyNoise)
				half _MainTexNoiseSpeed;
				half _MainTexNoiseStrength;
				half _MainTexNoiseTiling;
			#endif

			// Top Edge Texture Variables
			#if defined(Waterfall2D_TopEdgeTexture)
				sampler2D _TopEdgeTex;
				float4 _TopEdgeTex_ST;
				fixed _TopEdgeTexOpacity;
				fixed _TopEdgeThickness;

				#if defined(Waterfall2D_TopEdgeTexture_ApplyNoise)
					half _TopEdgeTexNoiseSpeed;
					half _TopEdgeTexNoiseStrength;
					half _TopEdgeTexNoiseTiling;
				#endif

				#if defined(Waterfall2D_TopEdgeTextureSheet)
					half _TopEdgeTexSheetRows;
					half _TopEdgeTexSheetFramesPerSecond;
				#endif
			#endif

			// Left-Right Edges Texture Variables
			#if defined(Waterfall2D_LeftRightEdgesTexture)
				sampler2D _LeftRightEdgesTex;
				float4 _LeftRightEdgesTex_ST;
				fixed _LeftRightEdgesTexOpacity;
				fixed _LeftRightEdgesThickness;
				half _LeftRightEdgesTexScrollSpeed;

				#if defined(Waterfall2D_LeftRightEdgesTexture_ApplyNoise)
					half _LeftRightEdgesTexNoiseSpeed;
					half _LeftRightEdgesTexNoiseStrength;
					half _LeftRightEdgesTexNoiseTiling;
				#endif
			#endif

			// Noise Texture
			#if defined(Waterfall2D_MainTexture_ApplyNoise) || defined(Waterfall2D_TopEdgeTexture_ApplyNoise) || defined(Waterfall2D_LeftRightEdgesTexture_ApplyNoise)
				sampler2D _NoiseTex;
			#endif

			uniform vector _Waterfall_Time;

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;

				#if defined(Waterfall2D_MainTexture_ApplyNoise)
					float4 mainTextureUV : TEXCOORD1;
				#else
					float2 mainTextureUV : TEXCOORD1;
				#endif
				
				UNITY_FOG_COORDS(2)
				float4 vertex : SV_POSITION;
			};

			v2f vert (appdata v)
			{
				v2f o;

				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;

				o.mainTextureUV.xy = TRANSFORM_TEX(v.uv, _MainTex);
				o.mainTextureUV.y += _Waterfall_Time.x * _MainTexScrollSpeed;

				#if defined(Waterfall2D_MainTexture_ApplyNoise)
					o.mainTextureUV.zw = v.uv * _MainTexNoiseTiling + _Waterfall_Time.x * _MainTexNoiseSpeed;
				#endif

				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 finalColor;

				// Applying base color
				#if defined(Waterfall2D_BaseColor_Gradient)
					finalColor = lerp(_BaseColor_Gradient_End, _BaseColor_Gradient_Start, i.uv.y);
				#else
					finalColor = _BaseColor;
				#endif

				// Applying main Texture
				#if defined(Waterfall2D_MainTexture_ApplyNoise)
					float mainTextureDistortion = tex2D(_NoiseTex, i.mainTextureUV.zw).r * _MainTexNoiseStrength;
					i.mainTextureUV.xy += mainTextureDistortion;
				#endif
				fixed4 mainTextureColor = tex2D(_MainTex, i.mainTextureUV.xy);
				finalColor = lerp(finalColor, mainTextureColor, mainTextureColor.a * _MainTexOpacity);

				// Applying Left-Right Edges Texture
				#if defined(Waterfall2D_LeftRightEdgesTexture)
					float oneMinusRightEdgeThickness = 1.0 - _LeftRightEdgesThickness;
					if((i.uv.x < _LeftRightEdgesThickness) || (i.uv.x > oneMinusRightEdgeThickness))
					{
						float2 leftRightEdgesTextureUV;

						leftRightEdgesTextureUV.x = (i.uv.x < _LeftRightEdgesThickness ? i.uv.x : oneMinusRightEdgeThickness - i.uv.x) / _LeftRightEdgesThickness;
						leftRightEdgesTextureUV.x = leftRightEdgesTextureUV.x * _LeftRightEdgesTex_ST.x + _LeftRightEdgesTex_ST.z;
						leftRightEdgesTextureUV.y = (i.uv.y * _LeftRightEdgesTex_ST.y + _LeftRightEdgesTex_ST.w) + _Waterfall_Time.x * _LeftRightEdgesTexScrollSpeed;

						#if defined(Waterfall2D_LeftRightEdgesTexture_ApplyNoise)
							float leftRightEdgesTextureDistortion = tex2D(_NoiseTex, i.uv * _LeftRightEdgesTexNoiseTiling + _Waterfall_Time.x * _LeftRightEdgesTexNoiseSpeed).b * _LeftRightEdgesTexNoiseStrength;
							leftRightEdgesTextureUV.xy += leftRightEdgesTextureDistortion;
						#endif

						fixed4 leftRightEdgesTextureColor = tex2D(_LeftRightEdgesTex, leftRightEdgesTextureUV);
						finalColor = lerp(finalColor, leftRightEdgesTextureColor, leftRightEdgesTextureColor.a * _LeftRightEdgesTexOpacity);
					}
				#endif
				
				// Applying top Texture
				#if defined(Waterfall2D_TopEdgeTexture)
					float oneMinusTopEdgeThickness = 1.0 - _TopEdgeThickness;
					if(i.uv.y > oneMinusTopEdgeThickness){
						float2 topEdgeTextureUV;

						topEdgeTextureUV.x = i.uv.x * _TopEdgeTex_ST.x + _TopEdgeTex_ST.z;
						topEdgeTextureUV.y = ((i.uv.y - oneMinusTopEdgeThickness) / _TopEdgeThickness) * _TopEdgeTex_ST.y + _TopEdgeTex_ST.w;

						#if defined(Waterfall2D_TopEdgeTextureSheet)
							topEdgeTextureUV.y = (1.0 / _TopEdgeTexSheetRows) * (frac(topEdgeTextureUV.y) + floor(_Waterfall_Time.y * _TopEdgeTexSheetFramesPerSecond));
						#endif

						#if defined(Waterfall2D_TopEdgeTexture_ApplyNoise)
							float topEdgeTextureDistortion = tex2D(_NoiseTex, i.uv * _TopEdgeTexNoiseTiling + _Waterfall_Time.x * _TopEdgeTexNoiseSpeed).g * _TopEdgeTexNoiseStrength;
							topEdgeTextureUV.xy += topEdgeTextureDistortion;
						#endif

						fixed4 topEdgeTextureColor = tex2D(_TopEdgeTex, topEdgeTextureUV);
						finalColor = lerp(finalColor, topEdgeTextureColor, topEdgeTextureColor.a * _TopEdgeTexOpacity);
					}
				#endif

				// Applying fog
				UNITY_APPLY_FOG(i.fogCoord, finalColor);
				return finalColor;
			}
			ENDCG
		}
	}

	CustomEditor "WaterfallShaderGUI"
}

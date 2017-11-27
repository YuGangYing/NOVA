Shader "Nova/diff_spe_nor_mask" 
{
	Properties 
	{
		_DiffuseTex ("Diffuse (RGB)", 2D) = "white" {}
		_MaskTex ("Mask (RGB)", 2D) = "white" {}
		_MaskDiffuseTex ("MaskDiffuse (RGB)", 2D) = "white" {}
		_NormalTex ("Normal", 2D) = "bump" {}
		_Cube ("Cubemap", CUBE) = "" {}
	
		//_SpecularPower ("SpecularPower", Range(0.03, 1.0)) = 0.078125
	}

	SubShader 
	{
		Tags { "RenderType"="Opaque"}

		CGPROGRAM
		#pragma surface surf BlinnPhong2
		#pragma target 3.0 
		#pragma debug
		#pragma glsl

		sampler2D _DiffuseTex;
		sampler2D _MaskTex;
		sampler2D _MaskDiffuseTex;
		sampler2D _NormalTex;
		samplerCUBE _Cube;
		//half _SpecularSharp;
		//half _SpecularPower;

		struct Input 
		{
			float2 uv_DiffuseTex;
			float2 uv_MaskTex;
			float2 uv_MaskDiffuseTex;
			float2 uv_NormalTex;
			float3 worldRefl;
			INTERNAL_DATA
			//float2 uv2_MaskTex;
		};
		
		fixed4 LightingBlinnPhong2 (SurfaceOutput s, fixed3 lightDir, fixed3 halfDir, fixed atten)
		{
			fixed diff = max (0, dot (s.Normal, lightDir));
			fixed nh = max (0, dot (s.Normal, halfDir));
			fixed spec = pow (nh, s.Specular*128) * s.Gloss;
	
			fixed4 c;
			c.rgb = (s.Albedo * _LightColor0.rgb * diff + _LightColor0.rgb * spec) * (atten*2);
			c.a = 0.0;
			return c;
		}

		void surf (Input IN, inout SurfaceOutput o) 
		{
			fixed4 diffuseColor = tex2D( _DiffuseTex, IN.uv_DiffuseTex );
			fixed4 maskColor = tex2D (_MaskTex, IN.uv_MaskTex);
			fixed4 maskDiffuseColor = tex2D (_MaskDiffuseTex, IN.uv_MaskDiffuseTex);
			fixed3 normalColor = UnpackNormal ( tex2D ( _NormalTex, IN.uv_NormalTex ) );
			o.Albedo = diffuseColor.rgb * maskDiffuseColor.rgb * maskColor.r 
			+ (1 - maskColor.r) * diffuseColor.rgb
			+ (maskColor.g) * texCUBE (_Cube, WorldReflectionVector (IN, o.Normal)).rgb;
			o.Gloss = diffuseColor.a;
			o.Normal = normalColor;
			o.Specular = 0.1;
			// o.Specular = 1 - diffuseColor.a;
			//o.Emission = texCUBE (_Cube, WorldReflectionVector (IN, o.Normal)).rgb;
		}
		ENDCG
	} 
	FallBack "Diffuse"
}

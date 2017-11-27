Shader "Ghost/GhostSceneGroundMixSpecular"
{
	Properties 
	{
		_GroundTex1 ("GroundTex1 (RGBA)", 2D) = "white" {}
		_GroundTex2 ("GroundTex2 (RGBA)", 2D) = "white" {}
		_GroundTex3 ("GroundTex3 (RGBA)", 2D) = "white" {}
		_MaskTex ("MaskTex (RGB)", 2D) = "white" {}
		_LightMap ("Lightmap (RGB)", 2D) = "lightmap" { LightmapMode }
		_SpecularSharp ("SpecularSharp", Range(0.0,128.0)) = 128
		_SpecularPower ("SpecularPower", Range(0.0,4.0)) = 1
	}

	SubShader 
	{
		Tags {  "Queue"="Geometry" "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		#pragma surface surf SimpleSpecular

		sampler2D _GroundTex1;
		sampler2D _GroundTex2;
		sampler2D _GroundTex3;
		sampler2D _MaskTex;
		sampler2D _LightMap;
		float _SpecularSharp;
		float _SpecularPower;

		half4 LightingSimpleSpecular (SurfaceOutput s, half3 lightDir, half3 viewDir, half atten)
		{
			half3 h = normalize (lightDir + viewDir);
			float nh = max (0, dot (s.Normal, h));
			float spec = pow (nh, _SpecularSharp ) * s.Specular;
			half4 c;
			c.rgb = s.Albedo * atten + _LightColor0.rgb * spec * _SpecularPower;
			c.a = s.Alpha;
			return c;
		}

		struct Input
		{
			float2 uv_GroundTex1;
			float2 uv2_MaskTex;
		};

		void surf (Input IN, inout SurfaceOutput o) 
		{
			half4 groundColor1 = tex2D (_GroundTex1, IN.uv_GroundTex1);
			half4 groundColor2 = tex2D (_GroundTex2, IN.uv_GroundTex1);
			half4 groundColor3 = tex2D (_GroundTex3, IN.uv_GroundTex1);
			half4 maskColor = tex2D (_MaskTex, IN.uv2_MaskTex);
			half4 groundColor = groundColor1 * maskColor.r + groundColor2 * maskColor.g + groundColor3 * maskColor.b;
			half4 lightColor = tex2D( _LightMap, IN.uv2_MaskTex );
			o.Specular = groundColor1.a * maskColor.r + groundColor2.a * maskColor.g + groundColor3.a * maskColor.b;
			o.Albedo = groundColor.rgb * lightColor.rgb;
		}
		ENDCG
	} 
	FallBack "Diffuse"
}
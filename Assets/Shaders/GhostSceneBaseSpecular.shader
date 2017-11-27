Shader "Ghost/GhostSceneBaseSpecular" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_SepcularTex ("Specular (RGB)", 2D) = "white" {}
		_LightMap ("Lightmap (RGB)", 2D) = "lightmap" { LightmapMode }
		_SpecularSharp ("SpecularSharp", Range(0.0,128.0)) = 128
		_SpecularPower ("SpecularPower", Range(0.0,4.0)) = 1
	}

	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		#pragma surface surf SimpleSpecular

		sampler2D _MainTex;
		sampler2D _SepcularTex;
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

		struct Input {
			float2 uv_MainTex;
			float2 uv2_LightMap;
		};

		void surf (Input IN, inout SurfaceOutput o) {
			half4 diffuseColor = tex2D (_MainTex, IN.uv_MainTex);
			half4 specularColor = tex2D (_SepcularTex, IN.uv_MainTex);
			half4 lightColor = tex2D( _LightMap, IN.uv2_LightMap );
			o.Specular = specularColor.r;
			o.Albedo =  diffuseColor * lightColor;
		}
		ENDCG
	} 
	FallBack "Diffuse"
}
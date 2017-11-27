Shader "Ghost/GhostSceneBase" 
{
	Properties 
	{
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_LightMap ("Lightmap (RGB)", 2D) = "lightmap" { LightmapMode }
	}

	SubShader 
	{
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		#pragma surface surf SimpleLambert

		sampler2D _MainTex;
		sampler2D _LightMap;

		fixed4 LightingSimpleLambert (SurfaceOutput s, fixed3 lightDir, fixed atten)
		{
			fixed4 c;
			c.rgb = s.Albedo * atten;
			c.a = s.Alpha;
			return c;
		}

		struct Input 
		{
			float2 uv_MainTex;
			float2 uv2_LightMap;
		};

		void surf (Input IN, inout SurfaceOutput o) 
		{
			half4 diffuseColor = tex2D (_MainTex, IN.uv_MainTex);
			half4 lightColor = tex2D( _LightMap, IN.uv2_LightMap ) * 1.0f;
			o.Albedo =  diffuseColor * lightColor;
		}
		ENDCG
	} 
	FallBack "Diffuse"
}
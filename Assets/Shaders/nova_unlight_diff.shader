Shader "Nova/unlight_diff" 
{
	Properties 
	{
		_DiffuseMap ("Diffuse (RGB)", 2D) = "white" {}
	}

	SubShader 
	{
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		#pragma surface surf SimpleLambert

		sampler2D _DiffuseMap;

		half4 LightingSimpleLambert (SurfaceOutput s, half3 lightDir, half atten)
		{
			half4 c;
			//c.rgb = s.Albedo * _LightColor0.rgb;
			c.rgb = s.Albedo;
			c.a = s.Alpha;
			return c;
		}

		struct Input 
		{
			float2 uv_DiffuseMap;
		};

		void surf (Input IN, inout SurfaceOutput o) 
		{
			half4 diffuseColor = tex2D (_DiffuseMap, IN.uv_DiffuseMap);
			o.Albedo = diffuseColor.rgb;
		}
		ENDCG
	} 
	FallBack "Diffuse"
}

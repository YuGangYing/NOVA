Shader "Nova/diff_spe" 
{
	Properties 
	{
		_DiffuseMap ("Diffuse (RGB)", 2D) = "white" {}
		_SpecularMap ("Specular", 2D) = "black" {}
		_SpecularSharp ("SpecularSharp", Range(0.0,128.0)) = 128
		_SpecularPower ("SpecularPower", Range(0.0,1.0)) = 1
	}

	SubShader 
	{
		Tags { "RenderType"="Opaque"}

		CGPROGRAM
		#pragma surface surf BlinnPhong2
		 
		sampler2D _DiffuseMap;
		sampler2D _SpecularMap;
		float _SpecularSharp;
		float _SpecularPower;

		struct Input 
		{
			float2 uv_DiffuseMap;
		};
		
		fixed4 LightingBlinnPhong2 (SurfaceOutput s, fixed3 lightDir, half3 viewDir, fixed atten)
		{
			half3 h = normalize (lightDir + viewDir);
	
			fixed diff = max (0, dot (s.Normal, lightDir));
	
			float nh = max (0, dot (s.Normal, h));
			float spec = pow (nh, s.Specular * _SpecularSharp) * _SpecularPower;
	
			fixed4 c;
			c.rgb = (s.Albedo * _LightColor0.rgb * diff + _LightColor0.rgb * spec) * (atten * 2);
			c.a = s.Alpha + _LightColor0.a * spec * atten;
			return c;
		}

		void surf (Input IN, inout SurfaceOutput o) 
		{
			half4 diffuseColor = tex2D( _DiffuseMap, IN.uv_DiffuseMap );
			half4 specularColor = tex2D( _SpecularMap, IN.uv_DiffuseMap );
			o.Albedo = diffuseColor.rgb;
			o.Specular = specularColor.r;
		}
		ENDCG
	} 
	FallBack "Diffuse"
}

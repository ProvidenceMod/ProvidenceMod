texture SwirlTexture;
float2 offset;
float2 conversion;

sampler2D SpriteSampler;

sampler2D SwirlSampler = sampler_state
{
	Texture = <SwirlTexture>;
	AddressU = wrap;
	AddressV = wrap;
};

struct VertexShaderOutput
{
	float4 Position : SV_POSITION;
	float4 Color : COLOR0;
	float2 TextureCoordinates : TEXCOORD0;
};

float4 DivinityShader(VertexShaderOutput input) : COLOR0
{
	float4 color = tex2D(SpriteSampler, input.TextureCoordinates);
	float4 color2 = tex2D(SwirlSampler, input.TextureCoordinates + offset);
	if (color.a > 0.0f)
	{
		float alpha = clamp(1.0f - color2.r - (1.0f - color.r), 0.0f, 1.0f);
		return float4(0.0f, 0.0f, 0.0f, alpha);
	}
	return color;
}

technique Technique1
{
	pass DivinityShader
	{
		PixelShader = compile ps_2_0 DivinityShader();
	}
}
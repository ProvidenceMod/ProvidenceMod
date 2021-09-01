struct VertexShaderOutput
{
	float4 Position : SV_POSITION;
	float4 Color : COLOR0;
	float2 TextureCoordinates : TEXCOORD0;
};

texture SwirlTexture;
float2 offset;

sampler2D SwirlSampler = sampler_state
{
	Texture = <SwirlTexture>;
	AddressU = wrap;
	AddressV = wrap;
};

float4 DivinityShader(VertexShaderOutput input) : COLOR0
{
	float4 color = tex2D(SwirlSampler, input.TextureCoordinates);
	//color.a == 1.0f - color.r;
	
	return color;
}

technique Technique1
{
	pass DivinityShader
	{
		PixelShader = compile ps_2_0 DivinityShader();
	}
}
sampler2D SpriteSampler;
float2 conversion;

struct VertexShaderOutput
{
	float4 Position : SV_POSITION;
	float4 Color : COLOR0;
	float2 TextureCoordinates : TEXCOORD0;
};

float4 Chroma(VertexShaderOutput input) : COLOR0
{
	if (input.Color.a == 0.0f)
		return input.Color;

	float2 coord2 = float2(input.TextureCoordinates.x + conversion.x, input.TextureCoordinates.y + conversion.y);

	float2 coord3 = float2(input.TextureCoordinates.x - conversion.x, input.TextureCoordinates.y - conversion.y);

	float4 color1 = tex2D(SpriteSampler, input.TextureCoordinates);
	float4 color2 = tex2D(SpriteSampler, coord2);
	float4 color3 = tex2D(SpriteSampler, coord3);

	float alpha = input.Color.a / 3.0f;

	float4 finalTex = float4(color1.r, color2.g, color3.b, alpha);

	return finalTex;
}

technique Technique1
{
	pass Chroma
	{
		PixelShader = compile ps_2_0 Chroma();
	}
}
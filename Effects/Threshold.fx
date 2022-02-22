sampler2D SpriteSampler;
float threshold;
float4 color;

struct VertexShaderOutput
{
	float4 Position : SV_POSITION;
	float4 Color : COLOR0;
	float2 TextureCoordinates : TEXCOORD0;
};

float4 Threshold(VertexShaderOutput input) : COLOR0
{
	float4 color2 = tex2D(SpriteSampler, input.TextureCoordinates);
	if(color2.r < threshold)
	{
		return float4(0.0f, 0.0f, 0.0f, 0.0f);
	}
	return color;
}

technique Technique1
{
	pass Threshold
	{
		PixelShader = compile ps_2_0 Threshold();
	}
}
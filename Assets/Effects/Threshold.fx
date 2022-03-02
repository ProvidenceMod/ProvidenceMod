float4 mask;
float threshold;

sampler2D samplerTex;

struct VertexShaderInput
{
	float4 Position : POSITION;
	float2 TexCoords : TEXCOORD0;
	float4 Color : COLOR0;
};

struct VertexShaderOutput
{
	float4 Position : POSITION;
	float2 TexCoords : TEXCOORD0;
	float4 Color : COLOR0;
};

float4 Threshold(VertexShaderOutput input) : COLOR
{
	float4 color = tex2D(samplerTex, input.TexCoords);
	if (any(color > threshold))
		return mask;
	return float4(0.0f, 0.0f, 0.0f, 0.0f);
}

technique Technique1
{
	pass Threshold
	{
		PixelShader = compile ps_2_0 Threshold();
	}
};
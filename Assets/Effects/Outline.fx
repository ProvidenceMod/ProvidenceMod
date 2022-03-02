float2 conversion;
float4 inner;
float4 border;

matrix transformMatrix;

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

float4 Outline(VertexShaderOutput input) : COLOR
{
	float4 color = tex2D(samplerTex, input.TexCoords);
	float4 up = tex2D(samplerTex, input.TexCoords + float2(0.0f, conversion.y));
	float4 down = tex2D(samplerTex, input.TexCoords - float2(0.0f, conversion.y));
	float4 left = tex2D(samplerTex, input.TexCoords + float2(conversion.x, 0.0f));
	float4 right = tex2D(samplerTex, input.TexCoords - float2(conversion.x, 0.0f));

	if ((up.a == 1.0 || down.a == 1.0 || left.a == 1.0 || right.a == 1.0)  && color.a == 0.0)
		return border;
	return color;
}

technique Technique1
{
	pass Outline
	{
		PixelShader = compile ps_2_0 Outline();
	}
};
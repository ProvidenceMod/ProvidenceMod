float2 conversion;

sampler2D SpriteSampler;

struct VertexShaderOutput
{
	float4 Position : SV_POSITION;
	float4 Color : COLOR0;
	float2 TextureCoordinates : TEXCOORD0;
};

float4 DivinityShader(VertexShaderOutput input) : COLOR0
{
	float4 color = tex2D(SpriteSampler, input.TextureCoordinates);
	if(color.a == 0.0f)
	{
		float4 up = tex2D(SpriteSampler, input.TextureCoordinates + float2(0.0f, conversion.y));
		float4 down = tex2D(SpriteSampler, input.TextureCoordinates - float2(0.0f, conversion.y));
		float4 left = tex2D(SpriteSampler, input.TextureCoordinates + float2(conversion.x, 0.0f));
		float4 right = tex2D(SpriteSampler, input.TextureCoordinates - float2(conversion.x, 0.0f));
		float4 border = (1.0f, 1.0f, 1.0f, 1.0f);
		if (up.a > 0.0)
		{
			return border;
		}
		else if (down.a > 0.0)
		{
			return border;
		}
		else if (left.a > 0.0)
		{
			return border;
		}
		else if (right.a > 0.0)
		{
			return border;
		}
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
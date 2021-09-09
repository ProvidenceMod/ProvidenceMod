const float4 black = float4(0.0, 0.0, 0.0, 0.0);
const float4 codedColor = float4(0.0, 1.0, 0.0, 1.0);

sampler2D SpriteTextureSampler;
// Border color
float4 border = float4(1.0, 0.67, 0.2, 1.0);
// Texture width
float width;
// Texture height
float height;

struct VertexShaderOutput
{
	float4 Position : SV_POSITION;
	float4 Color : COLOR0;
	float2 TextureCoordinates : TEXCOORD0;
};
// Returns the coordinate to the 0.0 - 1.0 scale
float2 scaleBack(float2 pos)
{
	return float2(pos.x / width, pos.y / height);
}

float4 MainPS(VertexShaderOutput input) : COLOR
{
	// Color at coordinates
	float4 color = tex2D(SpriteTextureSampler, input.TextureCoordinates);

	// Border check array
	float nearby[4]; // right then clockwise

  // Converts the coordinates to pixel-based coordinates
	float2 pos = float2(input.TextureCoordinates.x * width, input.TextureCoordinates.y * height);

	// Border checking, right, up, left, down
	nearby[0] = tex2D(SpriteTextureSampler, scaleBack(pos + float2(1, 0))).g;
	nearby[1] = tex2D(SpriteTextureSampler, scaleBack(pos + float2(0, 1))).g;
	nearby[2] = tex2D(SpriteTextureSampler, scaleBack(pos + float2(-1, 0))).g;
	nearby[3] = tex2D(SpriteTextureSampler, scaleBack(pos + float2(0, -1))).g;

	if (nearby[0] == 1.0 && color.g < 1.0)
	{
		return border;
	}
	else if (nearby[1] == 1.0 && color.g < 1.0)
	{
		return border;
	}
	else if (nearby[2] == 1.0 && color.g < 1.0)
	{
		return border;
	}
	else if (nearby[3] == 1.0 && color.g < 1.0)
	{
		return border;
	}

	return color;
}

technique SpriteDrawing
{
	pass P0
	{
		PixelShader = compile ps_2_0 MainPS();
	}
};

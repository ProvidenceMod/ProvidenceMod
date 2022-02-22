sampler uImage0 : register(s0);
sampler uImage1 : register(s1);
sampler uImage2 : register(s2);
sampler uImage3 : register(s3);
float3 uColor;
float3 uSecondaryColor;
float2 uScreenResolution;
float2 uScreenPosition;
float2 uTargetPosition;
float2 uDirection;
float uOpacity;
float uTime;
float uIntensity;
float uProgress;
float2 uImageSize1;
float2 uImageSize2;
float2 uImageSize3;
float2 uImageOffset;
float uSaturation;
float4 uSourceRect;
float2 uZoom;

// This vector should be in motion in radiant to achieve the desired effect.
float2 DisplacementMotionVector;
float SampleWavelength;
float Frequency; // .51  .3
float RefractiveIndex;
float2 RefractionVector;
float RefractionVectorRange;

Texture2D Texture : register(t0);
sampler TextureSampler : register(s0)
{
	Texture = (Texture);
};


Texture2D DisplacementTexture;
sampler2D DisplacementSampler = sampler_state
{
	magfilter = linear;
	minfilter = linear;
	AddressU = Wrap;
	AddressV = Wrap;
	Texture = <DisplacementTexture>;
};

float4 Forcefield(float4 position : SV_Position, float4 color : COLOR0, float2 texCoord : TEXCOORD0) : COLOR0
{
    // Determine distance to the anti Refract position.
	float dist = saturate(distance(position.xy, RefractionVector) / RefractionVectorRange);
	float warpedCoords = (tex2D(DisplacementSampler, texCoord * SampleWavelength + DisplacementMotionVector).xy - float2(0.5f, 0.5f)) * Frequency;
	float2 lerpedCoords = (warpedCoords) * (1.0f - dist) * (1.0f - dist) + texCoord;
	float4 col = tex2D(TextureSampler, saturate(lerpedCoords)) * color;
    // Visually highlight effect range.
    //col.r += (1.0f - dist) * (1.0f - dist);
    //col.b += (dist) * (dist);
	return col;
}


technique Refraction
{
	pass Pass1
	{
		PixelShader = compile ps_2_0 Forcefield();
	}
}
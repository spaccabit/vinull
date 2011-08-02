float4x4 World;
float4x4 View;
float4x4 Projection;
texture Texture;

// Shader spec 2.0 allows for a max of 59 bones
#define MaxBones 59
float4x4 Bones[MaxBones];

sampler TexSampler = sampler_state
{
    Texture = (Texture);
};

float3 LightDirection1 = normalize(float3(1, 1, 2));
float3 LightDirection2 = normalize(float3(1, 1, -2));
float3 LightColor = float3(1, 1, 1);

struct VertexShaderInput
{
    float4 Position : POSITION0;
    float3 Normal : NORMAL0;
    float2 TextureCoord : TEXCOORD0;
    float4 BoneIndex : BLENDINDICES0;
    float4 BoneWeight : BLENDWEIGHT0;
};

struct VertexShaderOutput
{
    float4 Position : POSITION0;
    float2 TextureCoord : TEXCOORD0;
    float3 Light1 : COLOR0;
    float3 Light2 : COLOR1;
};


VertexShaderOutput VertexShaderFunction(VertexShaderInput input)
{
    VertexShaderOutput output;
	output.TextureCoord = input.TextureCoord;

    float4x4 boneTransform = 0;
    boneTransform += Bones[input.BoneIndex.x] * input.BoneWeight.x;
    boneTransform += Bones[input.BoneIndex.y] * input.BoneWeight.y;
    boneTransform += Bones[input.BoneIndex.z] * input.BoneWeight.z;
    boneTransform += Bones[input.BoneIndex.w] * input.BoneWeight.w;

	float4 bonePosition = mul(input.Position, boneTransform);
    float4 worldPosition = mul(bonePosition, World);
    float4 viewPosition = mul(worldPosition, View);
    output.Position = mul(viewPosition, Projection);

    float3 normal = normalize(mul(input.Normal, boneTransform));
    output.Light1 = max(dot(normal, LightDirection1), 0) * LightColor;
    output.Light2 = max(dot(normal, LightDirection2), 0) * LightColor;

    return output;
}

float4 PixelShaderFunction(VertexShaderOutput input) : COLOR0
{
	float4 color = tex2D(TexSampler, input.TextureCoord);
	color.rgb *= (input.Light1 + input.Light2);
    return color;
}

technique Technique1
{
    pass Pass1
    {
        VertexShader = compile vs_2_0 VertexShaderFunction();
        PixelShader = compile ps_2_0 PixelShaderFunction();
    }
}

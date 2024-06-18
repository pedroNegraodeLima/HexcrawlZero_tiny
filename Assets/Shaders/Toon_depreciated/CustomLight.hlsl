void CustomLighting_float(in float3 WorldPos, in float3 ClipSpacePos, out float3 Direction, out float3 Color, out float ShadowAtten)
{
    #ifdef SHADERGRAPH_PREVIEW
        Direction = float3(0.5, 0.5, 0);
        Color = float3(1, 1, 1);
        ShadowAtten = 1.0;
    #else
        #if SHADOW_SCREEN
            float4 shadowCoord = ComputeScreenPos(ClipSpacePos);
        #else 
            float4 shadowCoord = TransformWorldToShadowCoord(WorldPos);
        #endif

        #if _MAIN_LIGHT_SHADOWS_CASCADE || _MAIN_LIGHT_SHADOWS
            Light light = GetMainLight(shadowCoord);
        #else
            Light light = GetMainLight();
        #endif

        Direction = light.direction;
        Color = light.color;
        ShadowAtten = light.shadowAttenuation;
    #endif
}
    

#ifndef CUSTOM_LIGHTING_INCLUDED
#define CUSTOM_LIGHTING_INCLUDED

void CalculateMainLight_float(float3 WordPos, out float3 Direction, out float3 Color, out half DistanceAtten, out half ShadowAtten)
{
    #if SHADERGRAPH_PREVIEW
        Direction = float3(0.5, 0.5, 0);
        Color = float3(1, 1, 1);;
        DistanceAtten = 1.0;
        ShadowAtten = 1.0;
    #else
        #if SHADOW_SCREEN
            half4 clipPos = TranformWorldToHClip(WordPos);
            half4 shadowCoord = ComputeScreenPos(clipPos);
        #else
            half4 shadowCoord = TransformWorldToShadowCoord(WordPos);
        #endif
        
        #if _MAIN_LIGHT_SHADOWS_CASCADE || _MAIN_LIGHT_SHADOWS
            Light mainLight = GetMainLight(shadowCoord);
        #else
            Light mainLight = GetMainLight();
        #endif

        Direction = mainLight.direction;
        Color = mainLight.color;
        DistanceAtten = mainLight.distanceAttenuation;
        ShadowAtten = mainLight.shadowAttenuation;

    #endif
}

void AddAdditionalLights_float(float3 WorldPos, float3 WorldNormal, float3 WorldView, float MainDiffuse, float3 MainColor, out float Diffuse, out float3 Color) 
{
    Diffuse = MainDiffuse;
    Color = MainColor * MainDiffuse;
        
    #ifndef SHADERGRAPH_PREVIEW
        int pixelLightCount = GetAdditionalLightsCount();
        for (int i = 0; i < pixelLightCount; ++i)
        {
            Light light = GetAdditionalLight(i, WorldPos);
            float NdotL = saturate(dot(WorldNormal, light.direction));
            float atten = light.distanceAttenuation * light.shadowAttenuation;
            float thisDiffuse = atten * NdotL;
            Diffuse += thisDiffuse;
            Color += light.color * thisDiffuse;
        }
    #endif

    Color = Diffuse <= 0.0 ? MainColor : Color / Diffuse;
}

#endif
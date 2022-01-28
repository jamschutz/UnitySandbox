// taken from: https://docs.unity3d.com/Packages/com.unity.postprocessing@2.1/manual/Writing-Custom-Effects.html
Shader "Hidden/Custom/CameraBlend"
{
    HLSLINCLUDE

        #include "Packages/com.unity.postprocessing/PostProcessing/Shaders/StdLib.hlsl"

        TEXTURE2D_SAMPLER2D(_MainTex, sampler_MainTex);
        TEXTURE2D_SAMPLER2D(_BlendCamera , sampler_BlendCamera);
        int _BlendMode;


        float3 blend(float4 cam1, float4 cam2, int blendMode) {
            float3 col = cam1.rgb;

            // darken
            if(_BlendMode == 0) {

            }
            // multiply 
            else if(_BlendMode == 1) {
                
            }
            // color burn 
            else if(_BlendMode == 2) {
                
            }
            // linear burn 
            else if(_BlendMode == 3) {
                
            }
            // lighten 
            else if(_BlendMode == 4) {
                
            }
            // screen 
            else if(_BlendMode == 5) {
                
            }
            // color dodge 
            else if(_BlendMode == 6) {
                
            }
            // linear dodge 
            else if(_BlendMode == 7) {
                
            }
            // overlay 
            else if(_BlendMode == 8) {
                
            }
            // soft light 
            else if(_BlendMode == 9) {
                
            }
            // hard light 
            else if(_BlendMode == 10) {
                
            }
            // vivid light 
            else if(_BlendMode == 11) {
                
            }
            // linear light 
            else if(_BlendMode == 12) {
                
            }
            // pinlight 
            else if(_BlendMode == 13) {
                
            }
            // difference 
            else if(_BlendMode == 14) {
                
            }
            // exclusion 
            else if(_BlendMode == 15) {
                
            }

            return col;
        }

        float4 Frag(VaryingsDefault i) : SV_Target
        {
            float4 cam1 = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.texcoord);
            float4 cam2 = SAMPLE_TEXTURE2D(_BlendCamera, sampler_BlendCamera, i.texcoord);

            cam1.rgb = blend(cam1, cam2, _BlendMode);            
            return cam1;
        }

    ENDHLSL

    SubShader
    {
        Cull Off ZWrite Off ZTest Always

        Pass
        {
            HLSLPROGRAM

                #pragma vertex VertDefault
                #pragma fragment Frag

            ENDHLSL
        }
    }
}

// public enum BlendMode {
//     Darken = 0,
//     Multiply = 1,
//     ColorBurn = 2,
//     LinearBurn = 3,
//     Lighten = 4,
//     Screen = 5,
//     ColorDodge = 6,
//     LinearDodge = 7,
//     Overlay = 8,
//     SoftLight = 9,
//     HardLight = 10,
//     VividLight = 11,
//     LinearLight = 12,
//     PinLight = 13,
//     Difference = 14,
//     Exclusion = 15
// }
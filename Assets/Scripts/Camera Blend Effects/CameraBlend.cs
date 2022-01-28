using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

[Serializable]
[PostProcess(typeof(CameraBlendRenderer), PostProcessEvent.AfterStack, "Custom/CameraBlend")]
public sealed class CameraBlend : PostProcessEffectSettings
{
    [Range(0, 15), Tooltip("Blend mode")]
    public IntParameter blendMode = new IntParameter { value = 0 };

    [Tooltip("Render texture for the camera")]
    public TextureParameter camera = new TextureParameter();
}

public sealed class CameraBlendRenderer : PostProcessEffectRenderer<CameraBlend>
{
    public override void Render(PostProcessRenderContext context)
    {
        var sheet = context.propertySheets.Get(Shader.Find("Hidden/Custom/CameraBlend"));
        sheet.properties.SetInt("_BlendMode", settings.blendMode);
        sheet.properties.SetTexture("_BlendCamera", settings.camera);
        context.command.BlitFullscreenTriangle(context.source, context.destination, sheet, 0);
    }
}

public enum BlendMode {
    Darken = 0,
    Multiply = 1,
    ColorBurn = 2,
    LinearBurn = 3,
    Lighten = 4,
    Screen = 5,
    ColorDodge = 6,
    LinearDodge = 7,
    Overlay = 8,
    SoftLight = 9,
    HardLight = 10,
    VividLight = 11,
    LinearLight = 12,
    PinLight = 13,
    Difference = 14,
    Exclusion = 15
}
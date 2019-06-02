using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.AnimatedValues;

public class WaterfallShaderGUI : ShaderGUI {

    const int NOISE_TEXTURE_DIMENSIONS = 128;

    private static GUIStyle _helpBoxStyle;
    private static GUIStyle _groupBoxStyle;

    private MaterialEditor _materialEditor;
    private MaterialProperty[] _materialProperties;

    private Texture2D[] _noiseTexturePreviews = new Texture2D[3];

    private AnimBool _mainTextureNoisePropertiesFoldoutState;
    private AnimBool _topEdgeTextureFoldoutState;
    private AnimBool _topEdgeTextureNoisePropertiesFoldoutState;
    private AnimBool _topEdgeTextureSheetPropertiesFoldoutState;
    private AnimBool _leftRightEdgesTextureFoldoutState;
    private AnimBool _leftRightEdgesTextureNoisePropertiesFoldoutState;

    private int _openBoxGroupsCount = 0;

    public override void OnGUI(MaterialEditor materialEditor, MaterialProperty[] properties)
    {
        _materialEditor = materialEditor;
        _materialProperties = properties;

        DrawProperties();
    }

    private void DrawProperties()
    {
        DrawSaveNoiseTextureAsAssetBox();
        DrawRenderingOptions();
        DrawBaseColorProperties();
        DrawMainTextureProperties();
        DrawTopEdgeTextureProperties();
        DrawLeftRightEdgesTextureProperties();
    }

    private void DrawSaveNoiseTextureAsAssetBox()
    {
        var noiseTexProperty = FindProperty("_NoiseTex", _materialProperties);
        if(noiseTexProperty.textureValue != null)
        {
            bool textureAssetAlreadyExist = AssetDatabase.Contains(noiseTexProperty.textureValue);
            bool materialAssetAlreadyExist = AssetDatabase.Contains(_materialEditor.target);

            if (materialAssetAlreadyExist && !textureAssetAlreadyExist)
            {
                BeginBoxGroup();
                EditorGUILayout.HelpBox("The material is saved as an asset in your project. Please save the noise texture as well! Otherwise, the distortion effects might stop working at runtime!", MessageType.Warning);
                if (GUILayout.Button("Save Noise Texture"))
                {
                    string materialPath = System.IO.Path.ChangeExtension(AssetDatabase.GetAssetPath(_materialEditor.target), null);
                    AssetDatabase.CreateAsset(noiseTexProperty.textureValue, materialPath + "_noiseTexture.asset");
                }
                EndBoxGroup();
            }
        }
    }

    private void DrawRenderingOptions()
    {
        BeginBoxGroup("Rendering Options");

        // Render Queue Property
        _materialEditor.RenderQueueField();

        // Sprite Mask Interaction
        var spriteMaskInteractionProperty = FindProperty("_SpriteMaskInteraction", _materialProperties);
        _materialEditor.ShaderProperty(spriteMaskInteractionProperty, "Mask Interaction");

        if ((_materialEditor.target as Material).renderQueue != 3000)
            EditorGUILayout.HelpBox("Material render queue should be set to \"Transparent\" for the sprite mask interaction to work properly!", MessageType.Info);

        EndBoxGroup();
    }

    private void DrawBaseColorProperties()
    {
        BeginBoxGroup("Base Color");

        //Color Mode Property Enum
        var colorModeProperty = FindProperty("_BaseColor_Mode", _materialProperties);
        EditorGUI.BeginChangeCheck();
        _materialEditor.ShaderProperty(colorModeProperty, "Color Mode");
        if (EditorGUI.EndChangeCheck())
        {
            SetKeywordState("Waterfall2D_BaseColor_Gradient", colorModeProperty.floatValue != 0f);
        }

        //Color Value(s) Property/Properties
        if(colorModeProperty.floatValue != 0f)
        {
            var baseColorGradientStartProperty = FindProperty("_BaseColor_Gradient_Start", _materialProperties);
            var baseColorGradientEndProperty = FindProperty("_BaseColor_Gradient_End", _materialProperties);
            _materialEditor.ColorProperty(baseColorGradientStartProperty, "Start Color");
            _materialEditor.ColorProperty(baseColorGradientEndProperty, "End Color");
        } else
        {
            var baseColorProperty = FindProperty("_BaseColor", _materialProperties);
            _materialEditor.ColorProperty(baseColorProperty, "Color");
        }

        EndBoxGroup();
    }

    private void DrawMainTextureProperties()
    {
        BeginBoxGroup("Main Texture");

        BeginBoxGroup(null, false);

        // Texture Slot with Tiling and Offset Properties
        var textureProperty = FindProperty("_MainTex", _materialProperties);
        _materialEditor.TextureProperty(textureProperty, "Texture");

        if (textureProperty.textureValue != null && textureProperty.textureValue.wrapMode == TextureWrapMode.Clamp)
            EditorGUILayout.HelpBox("Please make sure that the texture wrap mode is set to Repeat or Mirror!", MessageType.Warning);

        // Texture Opacity Property
        var textureOpacityProperty = FindProperty("_MainTexOpacity", _materialProperties);
        _materialEditor.RangeProperty(textureOpacityProperty, "Opacity");

        // Texture Scroll Speed Property
        var textureScrollSpeedProperty = FindProperty("_MainTexScrollSpeed", _materialProperties);
        _materialEditor.FloatProperty(textureScrollSpeedProperty, "Scroll Speed");

        EndBoxGroup();

        // Texture Distortion Effect Properties
        var textureNoiseKeywordState = FindProperty("_Waterfall2D_MainTexture_ApplyNoise_KeywordState", _materialProperties);

        bool distortionEffectState = BeginBoxGroup("Distortion Effect", false, true, true, textureNoiseKeywordState.floatValue == 1.0f);

        if (_mainTextureNoisePropertiesFoldoutState == null)
            _mainTextureNoisePropertiesFoldoutState = new AnimBool(distortionEffectState, _materialEditor.Repaint);

        if(textureNoiseKeywordState.floatValue != (distortionEffectState ? 1.0f : 0.0f))
        {
            textureNoiseKeywordState.floatValue = distortionEffectState ? 1.0f : 0.0f;
            SetKeywordState("Waterfall2D_MainTexture_ApplyNoise", distortionEffectState);

            _mainTextureNoisePropertiesFoldoutState.target = distortionEffectState;
        }

        using (var group = new EditorGUILayout.FadeGroupScope(_mainTextureNoisePropertiesFoldoutState.faded))
        {
            if (group.visible)
            {
                var textureNoiseScaleOffsetProperty = FindProperty("_MainTexNoiseScaleOffset", _materialProperties);
                var textureNoiseStrengthProperty = FindProperty("_MainTexNoiseStrength", _materialProperties);
                var textureNoiseSpeedProperty = FindProperty("_MainTexNoiseSpeed", _materialProperties);
                var textureNoiseTilingProperty = FindProperty("_MainTexNoiseTiling", _materialProperties);

                DrawDistortionEffectProperties(0, textureNoiseScaleOffsetProperty, textureNoiseStrengthProperty, textureNoiseSpeedProperty, textureNoiseTilingProperty);
            }
        }

        EndBoxGroup(); // Distortion Effect Box Group

        EndBoxGroup();
    }

    private void DrawTopEdgeTextureProperties()
    {
        var topEdgeKeywordState = FindProperty("_Waterfall2D_TopEdgeTexture_KeywordState", _materialProperties);
        
        bool topEdgeState = BeginBoxGroup("Top Edge Texture", true, false, true, topEdgeKeywordState.floatValue == 1.0f, true);

        if (_topEdgeTextureFoldoutState == null)
            _topEdgeTextureFoldoutState = new AnimBool(topEdgeState, _materialEditor.Repaint);

        if(topEdgeKeywordState.floatValue != (topEdgeState ? 1.0f : 0.0f))
        {
            topEdgeKeywordState.floatValue = topEdgeState ? 1.0f : 0.0f;
            SetKeywordState("Waterfall2D_TopEdgeTexture", topEdgeState);

            _topEdgeTextureFoldoutState.target = topEdgeState;
        }

        using(var topEdgeFoldoutGroup = new EditorGUILayout.FadeGroupScope(_topEdgeTextureFoldoutState.faded))
        {
            if (topEdgeFoldoutGroup.visible)
            {

                BeginBoxGroup(null, false);

                //Edge Thickness
                var edgeThicknessProperty = FindProperty("_TopEdgeThickness", _materialProperties);
                _materialEditor.RangeProperty(edgeThicknessProperty, "Thickness");

                EndBoxGroup();

                BeginBoxGroup(null, false);

                // Texture Slot with Tiling and Offset Properties
                var textureProperty = FindProperty("_TopEdgeTex", _materialProperties);
                _materialEditor.TextureProperty(textureProperty, "Texture");

                if (textureProperty.textureValue != null && textureProperty.textureValue.wrapMode == TextureWrapMode.Clamp)
                    EditorGUILayout.HelpBox("Please make sure that the texture wrap mode is set to Repeat or Mirror!", MessageType.Warning);

                // Texture Opacity Property
                var textureOpacityProperty = FindProperty("_TopEdgeTexOpacity", _materialProperties);
                _materialEditor.RangeProperty(textureOpacityProperty, "Opacity");

                // Texture Sheet Property
                var textureSheetKeywordState = FindProperty("_Waterfall2D_TopEdgeTextureSheet_KeywordState", _materialProperties);

                bool textureSheetState = BeginBoxGroup("Is A Texture Sheet", true, true, true, textureSheetKeywordState.floatValue == 1.0f, false, false);

                if (_topEdgeTextureSheetPropertiesFoldoutState == null)
                    _topEdgeTextureSheetPropertiesFoldoutState = new AnimBool(textureSheetState, _materialEditor.Repaint);

                if(textureSheetKeywordState.floatValue != (textureSheetState ? 1.0f : 0.0f))
                {
                    textureSheetKeywordState.floatValue = textureSheetState ? 1.0f : 0.0f;
                    SetKeywordState("Waterfall2D_TopEdgeTextureSheet", textureSheetState);

                    _topEdgeTextureSheetPropertiesFoldoutState.target = textureSheetState;
                }

                using(var group = new EditorGUILayout.FadeGroupScope(_topEdgeTextureSheetPropertiesFoldoutState.faded))
                {
                    if (group.visible)
                    {
                        var textureSheetRowsProperty = FindProperty("_TopEdgeTexSheetRows", _materialProperties);
                        var textureSheetFramesPerSecond = FindProperty("_TopEdgeTexSheetFramesPerSecond", _materialProperties);
                        _materialEditor.ShaderProperty(textureSheetRowsProperty, "Rows Count");
                        _materialEditor.ShaderProperty(textureSheetFramesPerSecond, "Frames Per Second");
                    }
                }

                EndBoxGroup(); // Texture Sheet Properties

                EndBoxGroup();

                // Texture Distortion Effect Properties
                var textureNoiseKeywordState = FindProperty("_Waterfall2D_TopEdgeTexture_ApplyNoise_KeywordState", _materialProperties);

                bool distortionEffectState = BeginBoxGroup("Distortion Effect", false, true, true, textureNoiseKeywordState.floatValue == 1.0f);

                if (_topEdgeTextureNoisePropertiesFoldoutState == null)
                    _topEdgeTextureNoisePropertiesFoldoutState = new AnimBool(distortionEffectState, _materialEditor.Repaint);

                if (textureNoiseKeywordState.floatValue != (distortionEffectState ? 1.0f : 0.0f))
                {
                    textureNoiseKeywordState.floatValue = distortionEffectState ? 1.0f : 0.0f;
                    SetKeywordState("Waterfall2D_TopEdgeTexture_ApplyNoise", distortionEffectState);

                    _topEdgeTextureNoisePropertiesFoldoutState.target = distortionEffectState;
                }

                using (var group = new EditorGUILayout.FadeGroupScope(_topEdgeTextureNoisePropertiesFoldoutState.faded))
                {
                    if (group.visible)
                    {
                        var textureNoiseScaleOffsetProperty = FindProperty("_TopEdgeTexNoiseScaleOffset", _materialProperties);
                        var textureNoiseStrengthProperty = FindProperty("_TopEdgeTexNoiseStrength", _materialProperties);
                        var textureNoiseSpeedProperty = FindProperty("_TopEdgeTexNoiseSpeed", _materialProperties);
                        var textureNoiseTilingProperty = FindProperty("_TopEdgeTexNoiseTiling", _materialProperties);

                        DrawDistortionEffectProperties(1, textureNoiseScaleOffsetProperty, textureNoiseStrengthProperty, textureNoiseSpeedProperty, textureNoiseTilingProperty);
                    }
                }

                EndBoxGroup(); // Distortion Effect Box Group
            }
        }

        EndBoxGroup();
    }

    private void DrawLeftRightEdgesTextureProperties()
    {
        var leftRightEdgesKeywordState = FindProperty("_Waterfall2D_LeftRightEdgesTexture_KeywordState", _materialProperties);

        bool leftRightEdgeState = BeginBoxGroup("Left-Right Edges Texture", true, false, true, leftRightEdgesKeywordState.floatValue == 1.0f, true);

        if (_leftRightEdgesTextureFoldoutState == null)
            _leftRightEdgesTextureFoldoutState = new AnimBool(leftRightEdgeState, _materialEditor.Repaint);

        if (leftRightEdgesKeywordState.floatValue != (leftRightEdgeState ? 1.0f : 0.0f))
        {
            leftRightEdgesKeywordState.floatValue = leftRightEdgeState ? 1.0f : 0.0f;
            SetKeywordState("Waterfall2D_LeftRightEdgesTexture", leftRightEdgeState);

            _leftRightEdgesTextureFoldoutState.target = leftRightEdgeState;
        }

        using(var leftRightEdgesFoldoutGroup = new EditorGUILayout.FadeGroupScope(_leftRightEdgesTextureFoldoutState.faded))
        {
            if (leftRightEdgesFoldoutGroup.visible)
            {
                BeginBoxGroup(null, false);

                // Left-Right Edges Thickness Property
                var edgesThickness = FindProperty("_LeftRightEdgesThickness", _materialProperties);
                _materialEditor.RangeProperty(edgesThickness, "Thickness");

                EndBoxGroup();

                BeginBoxGroup(null, false);

                // Texture Slot with Tiling and Offset Properties
                var textureProperty = FindProperty("_LeftRightEdgesTex", _materialProperties);
                _materialEditor.TextureProperty(textureProperty, "Texture");

                if (textureProperty.textureValue != null && textureProperty.textureValue.wrapMode == TextureWrapMode.Clamp)
                    EditorGUILayout.HelpBox("Please make sure that the texture wrap mode is set to Repeat or Mirror!", MessageType.Warning);

                // Texture Opacity Property
                var textureOpacityProperty = FindProperty("_LeftRightEdgesTexOpacity", _materialProperties);
                _materialEditor.RangeProperty(textureOpacityProperty, "Opacity");

                // Texture Scroll Speed Property
                var textureScrollSpeedProperty = FindProperty("_LeftRightEdgesTexScrollSpeed", _materialProperties);
                _materialEditor.FloatProperty(textureScrollSpeedProperty, "Scroll Speed");

                EndBoxGroup();

                // Texture Distortion Effect Properties
                var textureNoiseKeywordState = FindProperty("_Waterfall2D_LeftRightEdgesTexture_ApplyNoise_KeywordState", _materialProperties);

                bool distortionEffectState = BeginBoxGroup("Distortion Effect", false, true, true, textureNoiseKeywordState.floatValue == 1.0f);

                if (_leftRightEdgesTextureNoisePropertiesFoldoutState == null)
                    _leftRightEdgesTextureNoisePropertiesFoldoutState = new AnimBool(distortionEffectState, _materialEditor.Repaint);

                if (textureNoiseKeywordState.floatValue != (distortionEffectState ? 1.0f : 0.0f))
                {
                    textureNoiseKeywordState.floatValue = distortionEffectState ? 1.0f : 0.0f;
                    SetKeywordState("Waterfall2D_LeftRightEdgesTexture_ApplyNoise", distortionEffectState);

                    _leftRightEdgesTextureNoisePropertiesFoldoutState.target = distortionEffectState;
                }

                using (var group = new EditorGUILayout.FadeGroupScope(_leftRightEdgesTextureNoisePropertiesFoldoutState.faded))
                {
                    if (group.visible)
                    {
                        var textureNoiseScaleOffsetProperty = FindProperty("_LeftRightEdgesTexNoiseScaleOffset", _materialProperties);
                        var textureNoiseStrengthProperty = FindProperty("_LeftRightEdgesTexNoiseStrength", _materialProperties);
                        var textureNoiseSpeedProperty = FindProperty("_LeftRightEdgesTexNoiseSpeed", _materialProperties);
                        var textureNoiseTilingProperty = FindProperty("_LeftRightEdgesTexNoiseTiling", _materialProperties);

                        DrawDistortionEffectProperties(2, textureNoiseScaleOffsetProperty, textureNoiseStrengthProperty, textureNoiseSpeedProperty, textureNoiseTilingProperty);
                    }
                }

                EndBoxGroup(); // Distortion Effect Box Group
            }
        }

        EndBoxGroup();
    }

    private void DrawDistortionEffectProperties(int channelIndex, MaterialProperty scaleOffsetProperty, MaterialProperty strengthProperty, MaterialProperty speedProperty, MaterialProperty tilingProperty)
    {
        var noiseTexturePreview = _noiseTexturePreviews[channelIndex];
        if (noiseTexturePreview == null)
            noiseTexturePreview = GenerateNoiseTexturePreview(channelIndex);

        _materialEditor.FloatProperty(strengthProperty, "Strength");
        _materialEditor.FloatProperty(speedProperty, "Speed");
        _materialEditor.FloatProperty(tilingProperty, "Tiling");

        float previewTextureDimension = 2f * EditorGUIUtility.singleLineHeight;
        Rect rect = EditorGUILayout.GetControlRect(true, previewTextureDimension);

        rect.width -= previewTextureDimension + 3f;

        Rect offsetPropertyRect = new Rect(rect);
        offsetPropertyRect.height = EditorGUIUtility.singleLineHeight;
        offsetPropertyRect.y += EditorGUIUtility.singleLineHeight + 1f;
        Rect scalePropertyRect = new Rect(offsetPropertyRect);
        scalePropertyRect.y -= EditorGUIUtility.singleLineHeight + 1;

        Vector4 scaleOffset = scaleOffsetProperty.vectorValue;
        EditorGUI.BeginChangeCheck();
        EditorGUI.showMixedValue = scaleOffsetProperty.hasMixedValue;
        scalePropertyRect = EditorGUI.PrefixLabel(scalePropertyRect, new GUIContent("Scale"));
        Vector2 scale = EditorGUI.Vector2Field(scalePropertyRect, string.Empty, new Vector2(scaleOffset.x, scaleOffset.y));
        offsetPropertyRect = EditorGUI.PrefixLabel(offsetPropertyRect, new GUIContent("Offset"));
        Vector2 offset = EditorGUI.Vector2Field(offsetPropertyRect, string.Empty, new Vector2(scaleOffset.z, scaleOffset.w));
        EditorGUI.showMixedValue = false;
        if (EditorGUI.EndChangeCheck())
        {
            scaleOffsetProperty.vectorValue = new Vector4(scale.x, scale.y, offset.x, offset.y);

            GenerateNoiseTexture();
            GenerateNoiseTexturePreview(channelIndex);
        }

        rect.x += rect.width + 3f;
        rect.width = previewTextureDimension;
        GUI.DrawTexture(rect, noiseTexturePreview, ScaleMode.StretchToFill);
    }

    private Texture2D GenerateNoiseTexturePreview(int channelIndex)
    {
        var noiseTextureProperty = FindProperty("_NoiseTex", _materialProperties);

        if (noiseTextureProperty.textureValue == null)
            noiseTextureProperty.textureValue = GenerateNoiseTexture();

        if(_noiseTexturePreviews[channelIndex] == null)
            _noiseTexturePreviews[channelIndex] = new Texture2D(NOISE_TEXTURE_DIMENSIONS, NOISE_TEXTURE_DIMENSIONS, TextureFormat.ARGB32, false, true);

        var previewTex = _noiseTexturePreviews[channelIndex];

        Color[] noiseTexPixels = (noiseTextureProperty.textureValue as Texture2D).GetPixels();
        Color[] previewTexPixels = new Color[NOISE_TEXTURE_DIMENSIONS * NOISE_TEXTURE_DIMENSIONS];

        for (int i = 0; i < NOISE_TEXTURE_DIMENSIONS; i++)
        {
            for (int j = 0; j < NOISE_TEXTURE_DIMENSIONS; j++)
            {
                int pixelIndex = i * NOISE_TEXTURE_DIMENSIONS + j;

                float colorValue = noiseTexPixels[pixelIndex][channelIndex];

                previewTexPixels[pixelIndex] = new Color(colorValue, colorValue, colorValue, 1.0f);
            }
        }

        previewTex.SetPixels(previewTexPixels);
        previewTex.Apply();

        return previewTex;
    }

    private Texture2D GenerateNoiseTexture()
    {
        var noiseTextureProperty = FindProperty("_NoiseTex", _materialProperties);

        if (noiseTextureProperty.textureValue == null)
            noiseTextureProperty.textureValue = new Texture2D(NOISE_TEXTURE_DIMENSIONS, NOISE_TEXTURE_DIMENSIONS, TextureFormat.RGBA32, false, true);

        Texture2D noiseTexture = noiseTextureProperty.textureValue as Texture2D;
        noiseTexture.wrapMode = TextureWrapMode.Mirror;
        noiseTexture.filterMode = FilterMode.Bilinear;

        Vector4 mainTexScaleOffset = FindProperty("_MainTexNoiseScaleOffset", _materialProperties).vectorValue;
        Vector4 topEdgeTexScaleOffset = FindProperty("_TopEdgeTexNoiseScaleOffset", _materialProperties).vectorValue;
        Vector4 leftRightEdgesTexScaleOffset = FindProperty("_LeftRightEdgesTexNoiseScaleOffset", _materialProperties).vectorValue;

        Color[] noiseTexPixels = new Color[NOISE_TEXTURE_DIMENSIONS * NOISE_TEXTURE_DIMENSIONS];

        for (int i = 0; i < NOISE_TEXTURE_DIMENSIONS; i++)
        {
            for (int j = 0; j < NOISE_TEXTURE_DIMENSIONS; j++)
            {
                Color color;

                color.r = GetPerlinNoise(NOISE_TEXTURE_DIMENSIONS, i, j, mainTexScaleOffset);
                color.g = GetPerlinNoise(NOISE_TEXTURE_DIMENSIONS, i, j, topEdgeTexScaleOffset);
                color.b = GetPerlinNoise(NOISE_TEXTURE_DIMENSIONS, i, j, leftRightEdgesTexScaleOffset);
                color.a = 0f;

                noiseTexPixels[i * NOISE_TEXTURE_DIMENSIONS + j] = color;
            }
        }

        noiseTexture.SetPixels(noiseTexPixels);
        noiseTexture.Apply();

        noiseTextureProperty.textureValue = noiseTexture;

        return noiseTexture;
    }

    private float GetPerlinNoise(int texDimension, int i, int j, Vector4 scaleOffset)
    {
        float x = (j / (float)(1 - texDimension)) * scaleOffset.x + scaleOffset.z;
        float y = (i / (float)(1 - texDimension)) * scaleOffset.y + scaleOffset.w;

        return Mathf.PerlinNoise(x, y);
    }

    private void SetKeywordState(string keyword, bool activate)
    {
        foreach (Material material in _materialEditor.targets)
        {
            if (activate)
                material.EnableKeyword(keyword);
            else
                material.DisableKeyword(keyword);
        }
    }

    private bool BeginBoxGroup(string groupName = null, bool usHelpBoxStyle = true, bool groupNameInside = false, bool drawToggle = false, bool toggleState = true, bool hideOnInactive = false, bool useBoldFont = true)
    {
        bool isActive = toggleState;

        if (_helpBoxStyle == null)
            _helpBoxStyle = new GUIStyle("HelpBox");

        if (_groupBoxStyle == null)
            _groupBoxStyle = new GUIStyle("GroupBox");
        
        if(groupNameInside && (!drawToggle || (!hideOnInactive || toggleState)))
        {
            EditorGUILayout.BeginVertical(usHelpBoxStyle ? _helpBoxStyle : _groupBoxStyle);
            _openBoxGroupsCount++;
        }

        if (groupName != null)
        {
            if (drawToggle)
                isActive = EditorGUILayout.ToggleLeft(groupName, toggleState, useBoldFont ? EditorStyles.boldLabel : EditorStyles.label);
            else
                EditorGUILayout.LabelField(groupName, useBoldFont ? EditorStyles.boldLabel : EditorStyles.label);
        }

        if(!groupNameInside && (!drawToggle || (!hideOnInactive || toggleState)))
        {
            EditorGUILayout.BeginVertical(usHelpBoxStyle ? _helpBoxStyle : _groupBoxStyle);
            _openBoxGroupsCount++;
        }

        return isActive;
    }

    private void EndBoxGroup()
    {
        if(_openBoxGroupsCount > 0)
        {
            EditorGUILayout.EndVertical();
            _openBoxGroupsCount--;
        }
    }
}

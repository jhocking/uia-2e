Shader "Skybox/Blended" {
Properties {
    _Tint ("Tint Color", Color) = (.5, .5, .5, .5)
    _Blend ("Blend", Range(0.0,1.0)) = 0.5
    _FrontTex ("Front (+Z)", 2D) = "white" {}
    _BackTex ("Back (-Z)", 2D) = "white" {}
    _LeftTex ("Left (+X)", 2D) = "white" {}
    _RightTex ("Right (-X)", 2D) = "white" {}
    _UpTex ("Up (+Y)", 2D) = "white" {}
    _DownTex ("Down (-Y)", 2D) = "white" {}
    _FrontTex2("2 Front (+Z)", 2D) = "white" {}
    _BackTex2("2 Back (-Z)", 2D) = "white" {}
    _LeftTex2("2 Left (+X)", 2D) = "white" {}
    _RightTex2("2 Right (-X)", 2D) = "white" {}
    _UpTex2("2 Up (+Y)", 2D) = "white" {}
    _DownTex2("2 Down (-Y)", 2D) = "white" {}
}

SubShader {
    Tags { "Queue" = "Background" }
    Cull Off
    Fog { Mode Off }
    Lighting Off
    Color [_Tint]
    Pass {
        SetTexture [_FrontTex] { combine texture }
        SetTexture [_FrontTex2] { constantColor (0,0,0,[_Blend]) combine texture lerp(constant) previous }
    }
    Pass {
        SetTexture [_BackTex] { combine texture }
        SetTexture [_BackTex2] { constantColor (0,0,0,[_Blend]) combine texture lerp(constant) previous }
    }
    Pass {
        SetTexture [_LeftTex] { combine texture }
        SetTexture [_LeftTex2] { constantColor (0,0,0,[_Blend]) combine texture lerp(constant) previous }
    }
    Pass {
        SetTexture [_RightTex] { combine texture }
        SetTexture [_RightTex2] { constantColor (0,0,0,[_Blend]) combine texture lerp(constant) previous }
    }
    Pass {
        SetTexture [_UpTex] { combine texture }
        SetTexture [_UpTex2] { constantColor (0,0,0,[_Blend]) combine texture lerp(constant) previous }
    }
    Pass {
        SetTexture [_DownTex] { combine texture }
        SetTexture [_DownTex2] { constantColor (0,0,0,[_Blend]) combine texture lerp(constant) previous }
    }
}

Fallback "Skybox/6 Sided", 1
}
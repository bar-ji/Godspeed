Shader "Custom/Water"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
        _Wavelength ("Wave Length", float) = 1.0
        _Amplitude ("Wave Amplitude", float) = 1.0
        _Speed ("Speed", float) = 1.0
    }
    SubShader
    {
        Tags { "Queue" = "Transparent" "RenderType" = "Transparent" }
        LOD 200
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha

        CGPROGRAM
        
        #pragma surface surf Standard fullforwardshadows alpha vertex:vert addshadow
        #pragma target 3.0

        struct Input
        {
            float2 uv_MainTex;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;
        float _Wavelength;
        float _Amplitude;
        float _Speed;

        void vert (inout appdata_full v) {
          float3 p = v.vertex.xyz;
          float k = 2 * UNITY_PI / _Wavelength;
          float f = k * ((p.x * p.z) - _Speed * _Time.y);
          p.y = sin(f) * _Amplitude;

          float3 tangent = normalize(float3(1, k * _Amplitude * cos(f), 0));
          float3 normal = float3(-tangent.y, tangent.x, 0);
            
          v.vertex.xyz = p;
          v.normal = normal;
        }
        
        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            o.Albedo = _Color;
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = _Color.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}

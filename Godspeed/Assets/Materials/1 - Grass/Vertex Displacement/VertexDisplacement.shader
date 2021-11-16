Shader "Custom/VertexDisplacement"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
        _DisplacementStrength ("Displacement Strength", float) = 1.0
        _DisplacementSpeed("Displacement Speed", float) = 1.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows vertex:vert addshadow
        
        #pragma target 3.0

        struct Input
        {
            float2 uv_MainTex;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;
        float _DisplacementStrength;
        float _DisplacementSpeed;
        
        UNITY_INSTANCING_BUFFER_START(Props)
        UNITY_INSTANCING_BUFFER_END(Props)

        void vert(inout appdata_full v)
        {
            float3 p = v.vertex;
            p.x += sin(p.y * _Time.y * _DisplacementSpeed) * _DisplacementStrength;
            p.z += cos(p.y * _Time.y * _DisplacementSpeed) * _DisplacementStrength;
            v.vertex.xyz = p;
        }

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            o.Albedo = _Color;
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = _Color[3];
        }
        ENDCG
    }
    FallBack "Diffuse"
}

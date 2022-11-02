Shader "Custom/MultiTerrainShader"
{
    Properties
    {
        _Color("Color", Color) = (1,1,1,1)
        _MainTex_1("MainTexture_1", 2D) = "white" {}
        _MainTex_2("MainTexture_2", 2D) = "white" {}
        _MainTex_3("MainTexture_3", 2D) = "white" {}
        _MainTex_4("MainTexture_4", 2D) = "white" {}
        _BumpTex("BumpTex", 2D) = "white" {}
        _Glossiness("Smoothness", Range(0,1)) = 0.5
        _Metallic("Metallic", Range(0,1)) = 0.0
    }
        SubShader
        {
            Tags { "RenderType" = "Opaque" }
            LOD 200
            CGPROGRAM

            #pragma surface surf Standard fullforwardshadows                   
            #pragma target 3.0

            sampler2D _MainTex_1;
            sampler2D _MainTex_2;
            sampler2D _MainTex_3;
            sampler2D _MainTex_4;
            sampler2D _BumpTex;
            struct Input
            {
                float2 uv_MainTex_1;
                float2 uv_MainTex_2;
                float2 uv_MainTex_3;
                float2 uv_MainTex_4;
                float2 uv_BumpTex;
                float4 color:Color;
            };
            half _Glossiness;
            half _Metallic;
            fixed4 _Color;


            UNITY_INSTANCING_BUFFER_START(Props)
            UNITY_INSTANCING_BUFFER_END(Props)

            void surf(Input IN, inout SurfaceOutputStandard o)
            {
                fixed4 c1 = tex2D(_MainTex_1, IN.uv_MainTex_1) * _Color;
                fixed4 c2 = tex2D(_MainTex_2, IN.uv_MainTex_2) * _Color;
                fixed4 c3 = tex2D(_MainTex_3, IN.uv_MainTex_3) * _Color;
                fixed4 c4 = tex2D(_MainTex_4, IN.uv_MainTex_4) * _Color;
                fixed3 t1 = lerp(c1.rgb, c2.rgb, IN.color.r);
                fixed3 t2 = lerp(t1.rgb, c3.rgb, IN.color.g);
                o.Albedo = lerp(t2.rgb, c4.rgb, IN.color.b);
                fixed3 n = UnpackNormal(tex2D(_BumpTex, IN.uv_BumpTex));
                o.Normal = n;
                o.Metallic = _Metallic;
                o.Smoothness = _Glossiness;
                o.Alpha = c1.a;
            }
            ENDCG
        }
            FallBack "Diffuse"
}

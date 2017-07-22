Shader "Unlit/s_BasicTerrain"
{
	Properties
	{
		_MainTex("Sys_Texture", 2D) = "white" {}
		_MainTexBlur("Sys_Texture2", 2D) = "white" {}
		_TileSet("TerrainTilset", 2D) = "white" {}
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			// make fog work
			#pragma multi_compile_fog
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				UNITY_FOG_COORDS(1)
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			sampler2D _MainTexBlur;
			sampler2D _TileSet;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);

				o.uv = v.uv;


				/*
#if !defined (SHADER_API_OPENGL)
				float2 tile = tex2Dlod(_MainTex, float4(v.uv, 0, 0)).rg * 0.76;
				o.uv = tile;
				o.uv.g = 0;
#endif
				//o.uv = 0;
				
				*/
				
				
				
				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;



			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
				fixed4 col = tex2D(_MainTex, i.uv) * .99; // mulitply by 0.99 not to be on the edge, at the perfect distance between two tiles

				col = tex2D(_TileSet, float2(col.r, 0.5));
				col *= min(tex2D(_MainTexBlur, i.uv), 0.5);
				//col = float4(i.uv, 0, 1);
				// apply fog
				UNITY_APPLY_FOG(i.fogCoord, col);
				return col;
			}
			ENDCG
		}
	}
}

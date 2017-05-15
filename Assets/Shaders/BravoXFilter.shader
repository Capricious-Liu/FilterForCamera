Shader "Hidden/BravoX" {
Properties {
	_MainTex ("Base (RGB)", RECT) = "white" {}
	
	Saturation("Master Saturation", Float) = 1      //饱和度
	Saturation_r("Saturation(Red)", Float) = 1
	Saturation_g("Saturation(Green)", Float) = 1
	Saturation_b("Saturation(Blue)", Float) = 1
	
	Brightness("Master Brightness", Float) = 1      //亮度
	Bright_d("Brightness(Dark)", Float) = 0
	Bright_g("Brightness(Grey)", Float) = 0
	Bright_l("Brightness(light)", Float) = 0
	
	ColBalanceR_l("RedBalance(light)", Float) = 0   //平衡度
	ColBalanceG_l("GreenBalance(light)", Float) = 0
	ColBalanceB_l("BlueBalance(light)", Float) = 0
	
	ColBalanceR_d("RedBalance(Dark)", Float) = 0
	ColBalanceG_d("GreenBalance(Dark)", Float) = 0
	ColBalanceB_d("BlueBalance(Dark)", Float) = 0
	
	LookUpRamp("Base (RGB)", RECT) = "white" {}
}

SubShader {
	Pass { // #0 
		ZTest Always Cull Off ZWrite Off
		Fog { Mode off }

		CGPROGRAM
		#pragma vertex vert_img
		#pragma fragment frag
		#pragma fragmentoption ARB_precision_hint_fastest
		#include "UnityCG.cginc"

		uniform sampler2D LookUpRamp;
		uniform sampler2D _MainTex;
		
		uniform float Saturation, Saturation_r, Saturation_g, Saturation_b;
		uniform float Brightness, Bright_l, Bright_g, Bright_d;
		uniform float ColBalanceR_l,ColBalanceG_l,ColBalanceB_l;
		uniform float ColBalanceR_d,ColBalanceG_d,ColBalanceB_d;
		
		half4 frag (v2f_img i) : COLOR {

			fixed4 original = tex2D(_MainTex, i.uv);
			fixed4 o = original;
			
			fixed lumi = min (1, Luminance(original));
			fixed c_l = min(min(original.r,original.g),original.b);
			
			ColBalanceR_l >= 0 ?
				( o.r += o.r * ColBalanceR_l * lumi) :
				( o.gb += o.gb * -ColBalanceR_l * lumi);
			
			ColBalanceG_l >= 0 ?
				( o.g += o.g * ColBalanceG_l * lumi) :
				( o.rb += o.rb * -ColBalanceG_l * lumi);
				
			ColBalanceB_l >= 0 ?
				( o.b += o.b * ColBalanceB_l * lumi) :
				( o.rg += o.rg * -ColBalanceB_l * lumi);
				
			ColBalanceR_d >= 0 ?
				( o.r += o.r * ColBalanceR_d * (1- lumi) ):
				( o.gb += o.gb * -ColBalanceR_d * (1- lumi) );
			
			ColBalanceG_d >= 0 ?
				( o.g += o.g * ColBalanceG_d * (1- lumi)) :
				( o.rb += o.rb * -ColBalanceG_d * (1- lumi));
				
			ColBalanceB_d >= 0 ?
				( o.b += o.b * ColBalanceB_d * (1- lumi)) :
				( o.rg += o.rg * -ColBalanceB_d * (1- lumi));
				
			o = lerp(lumi, o, Saturation);
			o.r = min(1, lerp(c_l, o.r, Saturation_r));
			o.g = min(1, lerp(c_l, o.g, Saturation_g));
			o.b = min(1, lerp(c_l, o.b, Saturation_b));
			
			o += o * ( ( tex2D(LookUpRamp, fixed2(lumi,.6)))* Bright_l
			 			+ tex2D(LookUpRamp, fixed2(lumi,.3))*Bright_g
			  			+ tex2D(LookUpRamp, fixed2(lumi,1))*Bright_d );	
			o *=  Brightness ;
			o.rgb = min(fixed3(1,1,1), o.rgb);
			o.a = original.a;
		
			return o;
		}
		ENDCG
	}
}

Fallback off

}
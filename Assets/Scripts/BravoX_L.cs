/****************************************************
Script is changed by Liu Zhili depend on Brovo!X Image Color Filter
Now it`s free for Unity PRO.

****************************************************/
/****************************************************

Bravo!X Image Color Filter write by Razor Yhang 2014
	
# Intro:
Bravo!X is based on the GNU GPL Open Source Licence. You can change it as you needed. With Bravo!X Filter, you can 
simply and easily manipulate the color of your final graphics, including brightness, saturation, hue and color 
balance. All the parameters are Photoshop-like and accessible from other scripts, which means you can easily 
control it on the go.The API onRenderImage() is used by Bravo!X so this Filter require UNITY PRO.
		
# How to use:
1.Drag the BravoX.cs script to the camera.
2.That's it:)

p.s:Please drag the shader "BravoXFilter" to the parameter "Shader" in the   BravoX script if the shader is not 
set by default sometimes. Also, we use a LUT map(look up table)to make things work,

# Notice:
 This filter is almost done but still in develop(that means it is poorly tested), I have tested it in 4.5.2 / 4.3 
/ 4.0 with my PC and it works fine. However I don't have environment to test it in MAC or any mobile device. You 
can change any implement details of it and make it work in your environment and project. And,it would be very 
helpful if you can tell me some platform unadaptable&failed problems.
			
# Questions and sugestions? Please contact: 709230917@qq.com

****************************************************/
using UnityEngine;

[ExecuteInEditMode]
public class BravoX_L : MonoBehaviour
{
    public Texture2D LookUpRamp;		// Do not change it if you know how it works inside out.	
    public Shader shader;
    private Material curMaterial;
	public float MasterSaturation 		= 1;		// Main switch of the saturation.
	public float Saturation_Red 		= 1;		// Saturation switch for Red Channel.
	public float Saturation_Green 		= 1;		// Saturation switch for Green Channel.
	public float Saturation_Blue 		= 1;		// Saturation switch for Blue Channel.
													
	
	public float MasterBrightness 		= 1;		// Main switch of the Brightness.
	public float BrightBias_Light 		= 0;		// Brightness bias switch for light part of the image.
	public float BrightBias_Grey 		= 0;		// Brightness bias switch for middle tone part of the image.
	public float BrightBias_Dark	    = 0;		// Brightness bias switch for shadow part of the image.
													// [NOTICE]: Wanna change contrast? Try to mix and match the brightness of
													// light / middle tone / shadow part of the image to get that.

	public float BlanceRedCyan_Light 	= 0;		// Color Balance of the Red - Cyan (R - GB)channel of the light part in image.
	public float BlanceGreenMagenta_Light = 0;		// Color Balance of the Green - Magenta (G - RB)channel of the light part in image.
	public float BlanceBlueYellow_Light = 0;		// Color Balance of the Blue - Yellow (B - RG)channel of the light part in image.

	public float BlanceRedCyan_Dark 	= 0;		// Color Balance of the Red - Cyan (R - GB)channel of the dark part in image.
	public float BlanceGreenMagenta_Dark = 0;		// Color Balance of the Green - Magenta (G - RB)channel of the dark part in image.
	public float BlanceBlueYellow_Dark 	= 0;        // Color Balance of the Blue - Yellow (B - RG)channel of the dark part in image.


    #region Properties  
    public Material material
    {
        get
        {
            if (curMaterial == null)
            {
                curMaterial = new Material(shader);
                material.hideFlags = HideFlags.HideAndDontSave;
            }
            return curMaterial;
        }
    }
    #endregion



    void Start () {
		if(!shader){
			shader = Shader.Find("Hidden/BravoX");
		}

		if(!shader){
			this.enabled = false;
		}
	}
	// Clamp params for the filter. You can fully change it as you needed.
	void Update() {
		MasterSaturation = Mathf.Clamp( MasterSaturation, 0, 4 );
		Saturation_Red = Mathf.Clamp( Saturation_Red, 0, 5 );
		Saturation_Green = Mathf.Clamp( Saturation_Green, 0, 5 );
		Saturation_Blue = Mathf.Clamp( Saturation_Blue, 0, 5 );
		
		MasterBrightness = Mathf.Clamp( MasterBrightness, 0, 5 );
		BrightBias_Light = Mathf.Clamp( BrightBias_Light, -1, 1 );
		BrightBias_Grey = Mathf.Clamp( BrightBias_Grey, -1, 1 );
		BrightBias_Dark = Mathf.Clamp( BrightBias_Dark, -1, 1 );
		
		BlanceRedCyan_Light = Mathf.Clamp( BlanceRedCyan_Light, -0.5f, 0.5f );
		BlanceGreenMagenta_Light = Mathf.Clamp( BlanceGreenMagenta_Light, -0.5f, 0.5f );
		BlanceBlueYellow_Light = Mathf.Clamp( BlanceBlueYellow_Light, -0.5f, 0.5f );
		BlanceRedCyan_Dark = Mathf.Clamp( BlanceRedCyan_Dark, -0.5f, 0.5f );
		BlanceGreenMagenta_Dark = Mathf.Clamp( BlanceGreenMagenta_Dark, -0.5f, 0.5f );
		BlanceBlueYellow_Dark = Mathf.Clamp( BlanceBlueYellow_Dark, -0.5f, 0.5f );
	}

	// Called by camera to apply image effect.
    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {

		material.SetTexture("LookUpRamp", LookUpRamp );

		material.SetFloat( "Saturation", MasterSaturation );
		material.SetFloat( "Saturation_r", Saturation_Red );
		material.SetFloat( "Saturation_g", Saturation_Green );
		material.SetFloat( "Saturation_b", Saturation_Blue );

		material.SetFloat( "ColBalanceR_l", BlanceRedCyan_Light );
		material.SetFloat( "ColBalanceG_l", BlanceGreenMagenta_Light );
		material.SetFloat( "ColBalanceB_l", BlanceBlueYellow_Light );
		material.SetFloat( "ColBalanceR_d", BlanceRedCyan_Dark );
		material.SetFloat( "ColBalanceG_d", BlanceGreenMagenta_Dark );
		material.SetFloat( "ColBalanceB_d", BlanceBlueYellow_Dark );

		material.SetFloat( "Brightness", MasterBrightness );
		material.SetFloat( "Bright_l", BrightBias_Light );
		material.SetFloat( "Bright_g", BrightBias_Grey );
		material.SetFloat( "Bright_d", BrightBias_Dark );

        Graphics.Blit( source, destination, material, 0 );
    }
}
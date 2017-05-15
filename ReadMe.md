# FilterForCamera
#### 实现滤镜效果：

1. 下载后用Unity打开
2. 根据下图操作后看到右边的参数调节区域<br>
参数申明如下：


>

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



![Image text](https://raw.githubusercontent.com/Capricious-Liu/FilterForCamera/master/hint.png)

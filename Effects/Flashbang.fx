sampler uImage0 : register(s0);	//Currently rendered frame	
sampler uImage1 : register(s1);	//A noise map, random (but generated with a seed from the current frame)
sampler uImage2 : register(s2);	//No fucking clue	(seems like a graph of updates, or like motion idk)
sampler uImage3 : register(s3);	//Pure black
float3 uColor;
float3 uSecondaryColor;
float2 uScreenResolution;
float2 uScreenPosition;
float2 uTargetPosition;
float2 uDirection;
float uOpacity;
float uTime;
float uIntensity;
float uProgress;
float2 uImageSize1;
float2 uImageSize2;
float2 uImageSize3;
float2 uImageOffset;
float uSaturation;
float4 uSourceRect;
float2 uZoom;

float4 Flashbang(float2 coords : TEXCOORD0) : COLOR0		// this is a function
{
    float4 color = tex2D(uImage0, coords);
    color += uIntensity;
	return color;
}

technique Technique1		// this is a shader run on a pixel
{
    pass Flashbang
    {
        PixelShader = compile ps_2_0 Flashbang();
    }
}

// IMPORTANT KNOWLEDGE			// Main take away, this is just really weird c#
// There is no ^, do x*x or whatever youre number is
// There is a sqrt(), that is the syntax, just put the number or function in the paranthesis
// There is if, else if, else, for, while, do, try, and catch (probably missed some lol)
// Var types int, bool, string???, float, float1,float2,float3,float4, maybe arrays( didnt test them tho), short, long, and double (no byte), again probably missed some but those are all the primatives i could think of
// for a time dependant function you want to cycle low high low (sine wave) use sin(uTime) (start at 0) or cos(uTime) (start at 1) (to normalize either add 1 to the product and divide by 2)
// For a time dependant function you want to cycle low high low (sawtooth) use frac(uTime) this will go 0-0.99 then drop straight to 0 again
// coords.x is the current pixels x coord, coords.y is the y
// if you want to draw a shape on the screen, find a x,y dependant function, graph it in 3d then take the z value which corosponds to the starting value you want the shape to start at, see above example
// coords start at top left with 0,0 and bottom right with 1,1
// yes the screen is only 1,1 i dont get it either
// add more techniques (keyword technique) to add additional effects
// other info as i get it


//Formula for noiseCoords, generats a noise map
//float2 noiseCoords = (coords * uImageSize0 - uSourceRect.xy) / uImageSize1;

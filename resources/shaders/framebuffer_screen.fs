#version 330 core
out vec4 FragColor;

in vec2 TexCoords;

uniform sampler2D screenTexture;
//bloom
uniform sampler2D bloomBlurTexture;


uniform bool ProtanopiaON;
uniform bool DeuteranopiaON;
uniform bool TritanopiaON;
uniform bool hdrAndBloom;
uniform float exposure;

const float offset =1.0 /300.0;
const float gamma = 2.2;


//Transform from Linear RGB color space into LMS color space
const mat3 TfromLRGBtoLMS = mat3(0.31399022, 0.63951294, 0.04649755,
                                 0.15537241, 0.75789446, 0.08670142,
                                 0.01775239, 0.10944209, 0.87256922);

//Transform from LMS color space into Linear RGB color space
const mat3 TfromLMStoLRGB = mat3(5.47221206, -4.6419601, 0.16963708,
                                 -1.1252419, 2.29317094, -0.1678952,
                                 0.02980165, -0.19318073, 1.16364789);
// Matrix for converting into Protanomaly while ine LMS color space
const mat3 Sp = mat3(0, 1.05118294, -0.05116099,
                     0,     1,          0,
                     0,     0,          1);

// Matrix for converting into Deuteranomaly while ine LMS color space
const mat3 Sd = mat3(1,     0,          0,
                     0.9513092, 0,  0.04866992,
                     0,     0,          1);

// Matrix for converting into Protanomaly while ine LMS color space
const mat3 St = mat3(1,     0,          0,
                     0,     1,          0,
                     -0.86744736, 1.86727089, 0);

void main()
{
#if 0
vec2 offsets[9] =vec2[](
        vec2(-offset, offset), // top/left
        vec2(0.0f, offset), // top-center
        vec2(offset, offset),
        vec2(-offset, 0.0f),
        vec2(0.0f, 0.0f),
        vec2(0.0f, offset),
        vec2(-offset, -offset),
        vec2(0.0f, -offset),
        vec2(offset, -offset)
    );

    float kernel[9] = float[](
            1.0 / 16, 2.0/16, 1.0/ 16,
            2.0 / 16, 4.0/16, 2.0/ 16,
            1.0 / 16, 2.0/16, 1.0/ 16
    );

    vec3 sampleTex[9];
    for(int i =0 ; i < 9; i++)
    {
        sampleTex[i] = vec3 (texture(screenTexture, TexCoords.st + offsets[i]));
    }

    vec3 col =vec3(0.0);
    for(int i =0; i< 9; i++)
    {
        col += sampleTex[i] * kernel[i];
    }
    FragColor = vec4(col, 1.0);

#elif 0
    FragColor = vec4(vec3(1.0 - texture(screenTexture, TexCoords)), 1.0);
#elif 0
    FragColor = texture(screenTexture, TexCoords);
    float average =(0.2 * FragColor.r + 0.7 * FragColor.g + 0.07 * FragColor.b) / 3.0;
    FragColor = vec4(average, average, average, 1.0);
#else

 if(ProtanopiaON)//Protanopia
 {
//remove gama correction
    vec3 col = texture(screenTexture, TexCoords).rgb;
    col =  col * TfromLMStoLRGB * Sp * TfromLRGBtoLMS;
    //col = pow(col, 1/vec3(gamma));
    FragColor = vec4(col, 1.0);

   }
else if(DeuteranopiaON) //Deuteranopia
{
//remove gama correction
    vec3 col = texture(screenTexture, TexCoords).rgb;
    col =  col * TfromLMStoLRGB * Sd * TfromLRGBtoLMS;
    //col = pow(col, 1/vec3(gamma));
    FragColor = vec4(col, 1.0);
}
else if(TritanopiaON) //Tritanopia
{
//remove gama correction
    vec3 col = texture(screenTexture, TexCoords).rgb;
    col =  col * TfromLMStoLRGB * St * TfromLRGBtoLMS;
    FragColor = vec4(col, 1.0);

}
else
{
    //vec2 tex_offset = 1.0 / textureSize(screenTexture, 0);
    //vec2 koord = TexCoords + tex_offset*(-250, -250);
    vec3 col        = texture(screenTexture, TexCoords).rgb;
    //vec3 bloomColor = texture(bloomBlurTexture, koord).rgb;
    vec3 bloomColor = texture(bloomBlurTexture, TexCoords).rgb;
    if(hdrAndBloom)
    {
        col+=bloomColor;

    }
    col = vec3(1.0) - exp(-col*exposure);
    col = pow(col, vec3(1.0 / gamma));
    FragColor = vec4(col, 1.0);






}
#endif
}
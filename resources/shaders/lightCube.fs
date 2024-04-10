#version 330 core

//in VS_OUT {
//   vec3 FragPos;
//   vec3 Normal;
//    vec2 TexCoords;
//} fs_in;

layout(location=0) out vec4 FragColor;
layout(location=1) out vec4 BrightColor;

uniform vec3 lightColor;


void main()
{
    FragColor = vec4(lightColor, 1.0);
    float brightness = dot(FragColor.rgb, vec3(0.2126, 0.7152, 0.0722));
    if(brightness > 1.0)
        {
            BrightColor = vec4(FragColor.rgb, 1.0);
            //BrightColor = vec4(0.63, 0.31, 0.0, 0.0);
            //FragColor = vec4(vec3(1.0)-lightColor, 1.0);
            FragColor = vec4(lightColor, 1.0);
         }
    else
        BrightColor = vec4(0.0,0.0,0.0, 1.0);
}
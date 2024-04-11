#version 330 core
out vec4 FragColor;

in vec2 TexCoords;

uniform sampler2D texture1;
uniform bool IsRed;
void main()
{
    vec4 col = texture(texture1, TexCoords);
    FragColor = vec4(IsRed? 1.0f : col.r/10.0, col.g, !IsRed? 1.0 : col.b, col.a+0.1);

}
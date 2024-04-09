#version 330 core
in vec2 TexCoords;
out vec4 FragColor;

uniform sampler2D texture1;

void main()
{

    //if((TexCoords.x >= 1.0f && TexCoords.y >= 1.00f ) || (TexCoords.x >= 1.0f && TexCoords.y <= 1.00f ) || TexCoords.x <= 1.0f && TexCoords.y >= 1.00f )
    //{
    //    FragColor = vec4(1.0f);
    //}
    //else

        FragColor = texture(texture1, TexCoords/2);
}
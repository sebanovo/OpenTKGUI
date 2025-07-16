#version 330 core

layout (location = 0) in vec3 aPosition;
layout (location = 1) in vec2 aTexCoord;
layout (location = 2) in vec3 normal;

out vec2 TexCoord;

uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;

void main()
{
    vec3 noborrar1 = normal;
    TexCoord = aTexCoord * 2.0;
    mat4 PVM = projection * view * model;
    gl_Position = PVM * vec4(aPosition, 1.0);

}

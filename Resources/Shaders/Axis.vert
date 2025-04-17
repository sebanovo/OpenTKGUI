#version 330 core
// GLSL
layout (location = 0) in vec3 aPosition;

uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;

void main()
{
    mat4 PVM = projection * view * model;
    gl_Position = PVM * vec4(aPosition, 1.0);
}

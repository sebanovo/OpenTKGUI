#version 330 core

layout (location = 0) in vec3 aPosition;
layout (location = 1) in vec2 aTexCoord;
layout (location = 2) in vec3 normal;

out vec2 TexCoord;
out vec3 surfaceNormal;
out vec3 toLigthVector;
out vec3 toCameraVector;

uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;
uniform vec3 ligthPosition;

void main()
{
    TexCoord = aTexCoord;
    mat4 PVM = projection * view * model;
    gl_Position = PVM * vec4(aPosition, 1.0);

    vec4 worldPosition = model * vec4(aPosition, 1.0);
    surfaceNormal = (model * vec4(normal, 0.0)).xyz; 
    toLigthVector = ligthPosition - worldPosition.xyz;
    toCameraVector = (inverse(view) * vec4(0, 0, 0, 1.0)).xyz - worldPosition.xyz;
}

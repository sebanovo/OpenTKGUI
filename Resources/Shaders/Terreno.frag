#version 330 core

in vec2 TexCoord;
uniform sampler2D u_Texture;

void main() {
    gl_FragColor =  texture(u_Texture, TexCoord);
}
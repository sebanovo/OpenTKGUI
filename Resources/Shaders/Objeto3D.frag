#version 330 core

in vec2 TexCoord;
uniform sampler2D u_Texture;

void main() {
    // gl_FragColor = vec4(ourColor, 0.1);
    // gl_FragColor = texture(ourTexture, TexCoord);
    // gl_FragColor = texture(texture1, TexCoord) * vec4(color, 1.0); 
    // gl_FragColor = mix(texture(texture1, TexCoord), texture(texture2, TexCoord), 0.2f) * vec4(ourColor, 1.0f);
    gl_FragColor =  texture(u_Texture, TexCoord);
}
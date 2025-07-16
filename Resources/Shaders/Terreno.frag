#version 330 core

in vec2 TexCoord;
in vec3 surfaceNormal;
in vec3 toLigthVector;
in vec3 toCameraVector;

uniform sampler2D u_Texture;
uniform vec3 ligthColor;
uniform float shineDamper;
uniform float reflectivity;

void main() {
    // gl_FragColor = vec4(ourColor, 0.1);
    // gl_FragColor = texture(ourTexture, TexCoord);
    // gl_FragColor = texture(texture1, TexCoord) * vec4(color, 1.0); 
    // gl_FragColor = mix(texture(texture1, TexCoord), texture(texture2, TexCoord), 0.2f) * vec4(ourColor, 1.0f);

    // Para que el compilador no me borre las varibales
    vec3 noBorrar1 = ligthColor; 
    float noBorrar2 = shineDamper; 
    float noBorrar3 = reflectivity;

    gl_FragColor =  texture(u_Texture, TexCoord);
}
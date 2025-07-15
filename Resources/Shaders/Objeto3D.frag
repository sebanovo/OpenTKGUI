#version 330 core

in vec2 TexCoord;
in vec3 surfaceNormal;
in vec3 toLigthVector;

uniform sampler2D u_Texture;
uniform vec3 ligthColor;

void main() {
    // gl_FragColor = vec4(ourColor, 0.1);
    // gl_FragColor = texture(ourTexture, TexCoord);
    // gl_FragColor = texture(texture1, TexCoord) * vec4(color, 1.0); 
    // gl_FragColor = mix(texture(texture1, TexCoord), texture(texture2, TexCoord), 0.2f) * vec4(ourColor, 1.0f);
    vec3 unitNormal = normalize(surfaceNormal);
    vec3 unitLightVector = normalize(toLigthVector);
    float nDot = dot(unitNormal, unitLightVector);
    float brightness = max(nDot, 0.0);
    vec3 diffuse = brightness * ligthColor;
    gl_FragColor =  vec4(diffuse, 1.0) * texture(u_Texture, TexCoord);
}
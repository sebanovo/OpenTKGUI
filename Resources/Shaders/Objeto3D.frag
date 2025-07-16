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
    vec3 unitNormal = normalize(surfaceNormal);
    vec3 unitLightVector = normalize(toLigthVector);
    float nDot = dot(unitNormal, unitLightVector);
    float brightness = max(nDot, 0.6);
    vec3 diffuse = brightness * ligthColor;

    vec3 unitVectorToCamera = normalize(toCameraVector);
    vec3 lightDirection = -unitLightVector;
    vec3 reflectedLigthDirection = reflect(lightDirection, unitNormal);

    float specularFactor = dot(reflectedLigthDirection, unitVectorToCamera);
    specularFactor = max(specularFactor, 0.0);
    float dampedFactor = pow(specularFactor, shineDamper);
    vec3 finalSpecular = dampedFactor * reflectivity * ligthColor;

    vec4 textureColor = texture(u_Texture, TexCoord);
    if(textureColor.a < 0.5) 
    {
        discard;
    }
    gl_FragColor =  vec4(diffuse, 1.0) * textureColor + vec4(finalSpecular, 1.0);
}
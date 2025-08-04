#version 330 core

in vec2 TexCoord;
uniform sampler2D u_BackgroundTexture;
uniform sampler2D u_RTexture;
uniform sampler2D u_GTexture;
uniform sampler2D u_BTexture;
uniform sampler2D u_BlendMap;

void main() {
    vec4 blendMapColor = texture2D(u_BlendMap, TexCoord);
    float backTextureAmount = 1 - (blendMapColor.r + blendMapColor.g + blendMapColor.b);
    vec2 tiledCoords = TexCoord * 40.0; 
    vec4 BackgroundTextureColor = texture(u_BackgroundTexture, tiledCoords) * backTextureAmount;
    vec4 rTextureColor = texture(u_RTexture, tiledCoords) * blendMapColor.r;
    vec4 gTextureColor = texture(u_GTexture, tiledCoords) * blendMapColor.g;
    vec4 bTextureColor = texture(u_BTexture, tiledCoords) * blendMapColor.b;

    vec4 totalColor = BackgroundTextureColor + rTextureColor + gTextureColor + bTextureColor;
    gl_FragColor = totalColor; 
}
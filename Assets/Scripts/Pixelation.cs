//FROM PROJECT ROROPE || REAL GAMES STUDIO
//************************************************
//https://realgamesss.weebly.com
//https://gamejolt.com/@RealGamesss
//https://realgamesss.newgrounds.com/
//https://real-games.itch.io/
//https://youtube.com/channel/UC_Adg-mo-IPg6uLacuQCZCQ
//************************************************

using UnityEngine;



[ExecuteInEditMode]
[AddComponentMenu("Image Effects/Pixelation")]
public class Pixelation : MonoBehaviour
{
    public int w = 720;
    int h;
    bool flag;

    protected void Start()
    {
        if (!SystemInfo.supportsImageEffects)
        {
            enabled = false;
            return;
        }
    }
    
    void Update()
    {   
        float ratio = ((float)Camera.main.pixelHeight / (float)Camera.main.pixelWidth);

        h = Mathf.RoundToInt(w * ratio);
    }
    
    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        source.filterMode = FilterMode.Point;
        RenderTexture buffer = RenderTexture.GetTemporary(w, h, -1);

        buffer.filterMode = FilterMode.Point;
        Graphics.Blit(source, buffer);
        Graphics.Blit(buffer, destination);
        RenderTexture.ReleaseTemporary(buffer);
    } 
}
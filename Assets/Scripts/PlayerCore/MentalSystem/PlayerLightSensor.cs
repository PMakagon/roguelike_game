using TMPro;
using UnityEngine;

namespace LiftGame.PlayerCore.MentalSystem
{
    public class PlayerLightSensor : MonoBehaviour
    {
        [SerializeField] private RenderTexture renderTexture;
        [SerializeField] private TextMeshProUGUI litLevelUI;
        public float litLevel;
        public int _light;

        public int Light => ReadRenderTexture();

        private int ReadRenderTexture()
        {
            RenderTexture tmp = RenderTexture.GetTemporary(
                renderTexture.width,
                renderTexture.height,
                0,
                RenderTextureFormat.Default,
                RenderTextureReadWrite.Linear);

            Graphics.Blit(renderTexture, tmp);
            RenderTexture previous = RenderTexture.active;
            RenderTexture.active = tmp;

            Texture2D myTexture2D = new Texture2D(renderTexture.width, renderTexture.height);

            myTexture2D.ReadPixels(new Rect(0, 0, tmp.width, tmp.height), 0, 0);
            myTexture2D.Apply();

            RenderTexture.active = previous;
            RenderTexture.ReleaseTemporary(tmp);

            Color32[] colors = myTexture2D.GetPixels32();
            Destroy(myTexture2D);
            litLevel = 0;
            for(int i = 0; i < colors.Length; i++)
            {
                litLevel += (0.2126f * colors[i].r) + (0.7152f * colors[i].g) + (0.0722f * colors[i].b);
            }
            // litLevel -= 259330;
            litLevel = litLevel / colors.Length;
            _light = Mathf.RoundToInt(litLevel);
            litLevelUI.text = _light.ToString();
            return _light;
        }
    }
}
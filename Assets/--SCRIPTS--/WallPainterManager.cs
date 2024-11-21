using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// I've had help here from ChatGPT
/// Modified slightly
/// I didn't have time to go into full details.
/// </summary>
namespace Managers
{
    public class WallPainterManager : Manager<WallPainterManager>
    {
        public Renderer WallRenderer;        // Renderer of the wall
        public TextMeshProUGUI percentageText;         // UI text for painted percentage
        public Transform PaintingPlace;//Where the character should stand.


        private Texture2D wallTexture;      // Texture used for painting
        private int _brushSize = 10;         // Default brush size
        private int _textureResolution = 512; // Resolution of the texture
        private Color currentColor = Color.red; // Default color
        private int paintedPixels = 0;     // Pixels that have been painted
        private int totalPixels;           // Total number of pixels on the texture

        void Start()
        {
            InitializeWallTexture();
        }

        private void InitializeWallTexture()
        {
            wallTexture = new Texture2D(_textureResolution, _textureResolution, TextureFormat.RGBA32, false);
            totalPixels = _textureResolution * _textureResolution;

            // Fill the texture with white (unpainted)
            Color[] clearPixels = new Color[totalPixels];
            for (int i = 0; i < clearPixels.Length; i++)
            {
                clearPixels[i] = Color.white;
            }

            wallTexture.SetPixels(clearPixels);
            wallTexture.Apply();

            WallRenderer.material.mainTexture = wallTexture;
        }


        public void PaintOnWall(Vector2 textureCoord)
        {
            // Ensure texture coordinates are within the valid range (0 to 1)
            textureCoord.x = Mathf.Clamp01(textureCoord.x);
            textureCoord.y = Mathf.Clamp01(textureCoord.y);

            // Convert normalized coordinates (0 to 1) to pixel coordinates
            int x = Mathf.FloorToInt(textureCoord.x * _textureResolution);

            // Invert the y-coordinate to fix the vertical direction
            int y = Mathf.FloorToInt(textureCoord.y * _textureResolution);

            // Paint within the brush size
            for (int i = -_brushSize; i <= _brushSize; i++)
            {
                for (int j = -_brushSize; j <= _brushSize; j++)
                {
                    int pixelX = Mathf.Clamp(x + i, 0, _textureResolution - 1);
                    int pixelY = Mathf.Clamp(y + j, 0, _textureResolution - 1);

                    // Set the pixel color if it hasn't already been painted
                    if (wallTexture.GetPixel(pixelX, pixelY) != currentColor)
                    {
                        wallTexture.SetPixel(pixelX, pixelY, currentColor);
                        paintedPixels++;
                    }
                }
            }

            wallTexture.Apply();
            UpdatePercentage();
        }


        private void UpdatePercentage()
        {
            float paintedPercentage = (float)paintedPixels / totalPixels * 100f;
            percentageText.text = $"Painted: {paintedPercentage:F2}%";

            if(paintedPercentage >= 100f)
            {
                GameManager.TriggerGameEnd?.Invoke();
            }
        }

        public void ChangeBrushColor(string myColor)
        {
            switch (myColor)
            {
                case "blue":
                    currentColor = Color.blue;
                    break;
                case "red":
                    currentColor = Color.red;
                    break;
                case "yellow":
                    currentColor = Color.yellow;
                    break;
                default:
                    currentColor = Color.red;
                    break;
            }

            // Reset paintedPixels to prevent the percentage from exceeding 100%
            paintedPixels = 0;

            // Clear the wall texture and start over (optional, if you want to reset the painting)
            Color[] clearPixels = new Color[totalPixels];
            for (int i = 0; i < clearPixels.Length; i++)
            {
                clearPixels[i] = Color.white;
            }

            wallTexture.SetPixels(clearPixels);
            wallTexture.Apply();

            // Update the percentage display immediately after changing color
            UpdatePercentage();
        }

        public void AdjustBrushSize(float newSize)
        {
            _brushSize = Mathf.RoundToInt(newSize);
        }
    }
}


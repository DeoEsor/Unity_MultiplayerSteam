using Steamworks.Data;
using UnityEngine;
using Color = UnityEngine.Color;

namespace Networking.Steam
{
    public static class ConvertSteamAvatar
    {
        public static Texture2D Convert(this Image image)
        {
            // Create a new Texture2D
            var avatar = new Texture2D((int)image.Width, (int)image.Height, TextureFormat.ARGB32, false)
             {
                 // Set filter type, or else its really blury
                 filterMode = FilterMode.Trilinear
             };

            // Flip image
            for (var x = 0; x < image.Width; x++)
                for (var y = 0; y < image.Height; y++)
                {
                    var p = image.GetPixel(x, y);
                    avatar.SetPixel(x, (int)image.Height - y,
                        new Color(p.r / 255.0f, p.g / 255.0f, p.b / 255.0f, p.a / 255.0f));
                }

            avatar.Apply();
            return avatar;
        }
    }
}
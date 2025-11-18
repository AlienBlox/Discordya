using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace Discordya.AlienBloxBrandingTemplate
{
	public class AlienBloxMessageDisplayPlayer : ModPlayer
	{
		public override void OnEnterWorld()
		{
			this.QuickLocalizedTextDisplay("MessageAlienBlox.Message1", Color.SteelBlue);
			this.QuickLocalizedTextDisplay("MessageAlienBlox.Message2", Color.LightSteelBlue);
			this.QuickLocalizedTextDisplay("MessageAlienBlox.Message3", Color.LightSteelBlue);
			this.QuickLocalizedTextDisplay("MessageAlienBlox.Message4", Color.LightSteelBlue);
			this.QuickLocalizedTextDisplay("MessageAlienBlox.Message5", Color.LightSteelBlue);
			this.QuickLocalizedTextDisplay("MessageAlienBlox.Message6", Color.LightSteelBlue);
		}
	}
}
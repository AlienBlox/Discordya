using Microsoft.Xna.Framework;
using Terraria;

namespace Discordya.AlienBloxBrandingTemplate
{
	/// <summary>
	/// Utility class made by AlienBlox, not part of the default Discord Rich Presence Mod,
	/// Used to quickly display text
	/// </summary>
	public static class QuickTextDisplay
	{
		/// <summary>
		/// Quick and dirty displays texts
		/// </summary>
		/// <param name="o">For the extension to work</param>
		/// <param name="key">The locale key</param>
		public static void QuickLocalizedTextDisplay(this object o, string key, Color? TextColor = null)
		{
			if (TextColor.HasValue)
			{
				Main.NewText(Terraria.Localization.Language.GetOrRegister($"Mods.Discordya.{key}"), TextColor);
			}
			else
			{
				Main.NewText(Terraria.Localization.Language.GetOrRegister($"Mods.Discordya.{key}"));
			}

			DiscordyaMod._discordyaMod.Logger.Warn(o);
		}
	}
}
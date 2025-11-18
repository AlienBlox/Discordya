using Terraria;
using Terraria.ModLoader;

namespace DiscordyaV2
{
	public class DiscordyaSystem : ModSystem
	{
		private readonly DiscordyaMod _discordyaMod = DiscordyaMod._discordyaMod;

		public override void OnWorldUnload()
		{
			if (Main.dedServ)
				return;
			_discordyaMod.GetPresenceUtils().SetMainMenuPresence();
		}
	}
}

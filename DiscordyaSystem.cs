// Decompiled with JetBrains decompiler
// Type: DiscordyaV2.DiscordyaSystem
// Assembly: DiscordyaV2, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7EE41C03-D9DA-4741-BA28-096DF00C64EC
// Assembly location: C:\Users\Alien\OneDrive\文档\DiscordyaV2\DiscordyaV2.dll

using Terraria;
using Terraria.ModLoader;

#nullable disable
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

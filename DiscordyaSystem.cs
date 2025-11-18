// Decompiled with JetBrains decompiler
// Type: Discordya.DiscordyaSystem
// Assembly: Discordya, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7EE41C03-D9DA-4741-BA28-096DF00C64EC
// Assembly location: C:\Users\Alien\OneDrive\文档\Discordya\Discordya.dll

using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace Discordya
{
	public class DiscordyaSystem : ModSystem
	{
		private readonly DiscordyaMod _discordyaMod = DiscordyaMod._discordyaMod;

		public override void OnWorldUnload()
		{
			if (Main.dedServ)
				return;
			this._discordyaMod.GetPresenceUtils().SetMainMenuPresence();
		}
	}
}

// Decompiled with JetBrains decompiler
// Type: DiscordyaV2.DiscordClient.Presence.DiscordPresence
// Assembly: DiscordyaV2, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7EE41C03-D9DA-4741-BA28-096DF00C64EC
// Assembly location: C:\Users\Alien\OneDrive\文档\DiscordyaV2\DiscordyaV2.dll

using DiscordRPC;

#nullable disable
namespace DiscordyaV2.DiscordClient.Presence
{
	public class DiscordPresence
	{
		private readonly DiscordClientHelper _discordClientHelper;
		private readonly RichPresence _presence;

		internal DiscordPresence(DiscordClientHelper discordClientHelper)
		{
			_discordClientHelper = discordClientHelper;
			_presence = new RichPresence();
		}

		internal void SetClientStatus(
		  string details = "",
		  string state = "",
		  string bigImageKey = null,
		  string bigImageText = null,
		  string smallImageKey = null,
		  string smallImageText = null)
		{
			RichPresence presence = _presence;
			if (((BaseRichPresence)presence).Assets == null)
			{
				Assets assets;
				((BaseRichPresence)presence).Assets = assets = new Assets();
			}
		  ((BaseRichPresence)_presence).State = state;
			((BaseRichPresence)_presence).Details = details;
			if (bigImageKey != null)
			{
				((BaseRichPresence)_presence).Assets.LargeImageKey = bigImageKey;
				((BaseRichPresence)_presence).Assets.LargeImageText = bigImageText;
			}
			else
			{
				((BaseRichPresence)_presence).Assets.LargeImageKey = (string)null;
				((BaseRichPresence)_presence).Assets.LargeImageText = (string)null;
			}
			if (smallImageKey != null)
			{
				((BaseRichPresence)_presence).Assets.SmallImageKey = smallImageKey;
				((BaseRichPresence)_presence).Assets.SmallImageText = smallImageText;
			}
			else
			{
				((BaseRichPresence)_presence).Assets.SmallImageKey = (string)null;
				((BaseRichPresence)_presence).Assets.SmallImageText = (string)null;
			}
		}

		internal void SetTimestamp() => ((BaseRichPresence)_presence).Timestamps = Timestamps.Now;

		internal void UpdateClientPresence()
		{
			DiscordRpcClient discordRpcClient = _discordClientHelper.GetRpcClient();
			if (discordRpcClient == null)
			{
				DiscordyaMod._discordyaMod.GetModLogger().Log("RPC is null, making a new one. (this is rare, congrats!)");
				discordRpcClient = _discordClientHelper.MakeClient();
			}
			if (discordRpcClient.IsDisposed)
				return;
			discordRpcClient.SetPresence(_presence);
		}
	}
}

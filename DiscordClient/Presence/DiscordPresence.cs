// Decompiled with JetBrains decompiler
// Type: Discordya.DiscordClient.Presence.DiscordPresence
// Assembly: Discordya, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7EE41C03-D9DA-4741-BA28-096DF00C64EC
// Assembly location: C:\Users\Alien\OneDrive\文档\Discordya\Discordya.dll

using DiscordRPC;

#nullable disable
namespace Discordya.DiscordClient.Presence
{
	public class DiscordPresence
	{
		private readonly DiscordClientHelper _discordClientHelper;
		private readonly RichPresence _presence;

		internal DiscordPresence(DiscordClientHelper discordClientHelper)
		{
			this._discordClientHelper = discordClientHelper;
			this._presence = new RichPresence();
		}

		internal void SetClientStatus(
		  string details = "",
		  string state = "",
		  string bigImageKey = null,
		  string bigImageText = null,
		  string smallImageKey = null,
		  string smallImageText = null)
		{
			RichPresence presence = this._presence;
			if (((BaseRichPresence)presence).Assets == null)
			{
				Assets assets;
				((BaseRichPresence)presence).Assets = assets = new Assets();
			}
		  ((BaseRichPresence)this._presence).State = state;
			((BaseRichPresence)this._presence).Details = details;
			if (bigImageKey != null)
			{
				((BaseRichPresence)this._presence).Assets.LargeImageKey = bigImageKey;
				((BaseRichPresence)this._presence).Assets.LargeImageText = bigImageText;
			}
			else
			{
				((BaseRichPresence)this._presence).Assets.LargeImageKey = (string)null;
				((BaseRichPresence)this._presence).Assets.LargeImageText = (string)null;
			}
			if (smallImageKey != null)
			{
				((BaseRichPresence)this._presence).Assets.SmallImageKey = smallImageKey;
				((BaseRichPresence)this._presence).Assets.SmallImageText = smallImageText;
			}
			else
			{
				((BaseRichPresence)this._presence).Assets.SmallImageKey = (string)null;
				((BaseRichPresence)this._presence).Assets.SmallImageText = (string)null;
			}
		}

		internal void SetTimestamp() => ((BaseRichPresence)this._presence).Timestamps = Timestamps.Now;

		internal void UpdateClientPresence()
		{
			DiscordRpcClient discordRpcClient = this._discordClientHelper.GetRpcClient();
			if (discordRpcClient == null)
			{
				DiscordyaMod._discordyaMod.GetModLogger().Log("RPC is null, making a new one. (this is rare, congrats!)");
				discordRpcClient = this._discordClientHelper.MakeClient();
			}
			if (discordRpcClient.IsDisposed)
				return;
			discordRpcClient.SetPresence(this._presence);
		}
	}
}

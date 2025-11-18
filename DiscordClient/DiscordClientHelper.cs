// Decompiled with JetBrains decompiler
// Type: DiscordyaV2.DiscordClient.DiscordClientHelper
// Assembly: DiscordyaV2, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7EE41C03-D9DA-4741-BA28-096DF00C64EC
// Assembly location: C:\Users\Alien\OneDrive\文档\DiscordyaV2\DiscordyaV2.dll

using DiscordRPC;
using DiscordRPC.IO;
using DiscordRPC.Logging;
using DiscordyaV2.CrossMod;
using DiscordyaV2.DiscordClient.Presence;
using System;

#nullable disable
namespace DiscordyaV2.DiscordClient
{
	public class DiscordClientHelper
	{
		private DiscordyaMod _discordyaMod;
		private DiscordClientStorage _discordClientStorage;
		private DiscordRpcClient _discordRpcClient;
		private DiscordPresence _discordPresence;

		internal DiscordClientHelper(DiscordyaMod discordyaMod)
		{
			this._discordyaMod = discordyaMod;
			this._discordClientStorage = new DiscordClientStorage();
			this._discordClientStorage.AddApplicationId("default", "839928944164732942");
			this._discordPresence = new DiscordPresence(this);
		}

		internal DiscordRpcClient MakeClient()
		{
			string applicationId = this._discordClientStorage.GetApplicationId("default");
			this._discordyaMod.GetModLogger().Log("Making a RPC instance " + applicationId + "...");
			this._discordRpcClient = new DiscordRpcClient(applicationId, -1, (ILogger)null, false, (INamedPipeClient)null);
			this._discordRpcClient.Initialize();
			this._discordRpcClient.Invoke();
			this._discordyaMod.GetModLogger().Log("RPC client is now ready.");
			return this._discordRpcClient;
		}

		internal void ChangeClient(string applicationIdentifier)
		{
			this._discordyaMod.GetModLogger().Log("Changing to a a new RPC instance " + applicationIdentifier + "...");
			this._discordRpcClient?.Dispose();
			this._discordRpcClient = new DiscordRpcClient(this._discordClientStorage.GetApplicationId(applicationIdentifier), -1, (ILogger)null, false, (INamedPipeClient)null);
			this._discordRpcClient.Initialize();
			this._discordRpcClient.Invoke();
			this._discordyaMod.GetModLogger().Log("New RPC client is now ready.");
		}

		internal void SetupCrossModClient()
		{
			CrossModCompatibility modCompatibility = this._discordyaMod.GetCrossModCompatibility();
			if (modCompatibility.GetModList().Count == 0)
				return;
			modCompatibility.GetModList().ForEach((Action<CrossMods>)(mod => this._discordClientStorage.AddApplicationId(mod.GetModIdentifier(), mod.GetApplicationIdentifier())));
			this.ChangeClient(modCompatibility.GetPreferredMod().GetModIdentifier());
		}

		internal void KillClient()
		{
			this._discordRpcClient?.Dispose();
			this._discordRpcClient = (DiscordRpcClient)null;
			this._discordClientStorage = (DiscordClientStorage)null;
			this._discordyaMod.GetModLogger().Log("Killed DiscordRPC helper and client.");
		}

		internal DiscordRpcClient GetRpcClient()
		{
			if (this._discordRpcClient != null)
				return this._discordRpcClient;
			this._discordyaMod.GetModLogger().Log("RPC not found, making a new one...", (byte)2);
			return this.MakeClient();
		}

		internal DiscordPresence GetDiscordPresence() => this._discordPresence;
	}
}

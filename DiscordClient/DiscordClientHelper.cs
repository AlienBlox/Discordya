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
			_discordyaMod = discordyaMod;
			_discordClientStorage = new DiscordClientStorage();
			_discordClientStorage.AddApplicationId("default", "839928944164732942");
			_discordPresence = new DiscordPresence(this);
		}

		internal DiscordRpcClient MakeClient()
		{
			string applicationId = _discordClientStorage.GetApplicationId("default");
			_discordyaMod.GetModLogger().Log("Making a RPC instance " + applicationId + "...");
			_discordRpcClient = new DiscordRpcClient(applicationId, -1, (ILogger)null, false, (INamedPipeClient)null);
			_discordRpcClient.Initialize();
			_discordRpcClient.Invoke();
			_discordyaMod.GetModLogger().Log("RPC client is now ready.");
			return _discordRpcClient;
		}

		internal void ChangeClient(string applicationIdentifier)
		{
			_discordyaMod.GetModLogger().Log("Changing to a a new RPC instance " + applicationIdentifier + "...");
			_discordRpcClient?.Dispose();
			_discordRpcClient = new DiscordRpcClient(_discordClientStorage.GetApplicationId(applicationIdentifier), -1, (ILogger)null, false, (INamedPipeClient)null);
			_discordRpcClient.Initialize();
			_discordRpcClient.Invoke();
			_discordyaMod.GetModLogger().Log("New RPC client is now ready.");
		}

		internal void SetupCrossModClient()
		{
			CrossModCompatibility modCompatibility = _discordyaMod.GetCrossModCompatibility();
			if (modCompatibility.GetModList().Count == 0)
				return;
			modCompatibility.GetModList().ForEach((Action<CrossMods>)(mod => _discordClientStorage.AddApplicationId(mod.GetModIdentifier(), mod.GetApplicationIdentifier())));
			ChangeClient(modCompatibility.GetPreferredMod().GetModIdentifier());
		}

		internal void KillClient()
		{
			_discordRpcClient?.Dispose();
			_discordRpcClient = (DiscordRpcClient)null;
			_discordClientStorage = (DiscordClientStorage)null;
			_discordyaMod.GetModLogger().Log("Killed DiscordRPC helper and client.");
		}

		internal DiscordRpcClient GetRpcClient()
		{
			if (_discordRpcClient != null)
				return _discordRpcClient;
			_discordyaMod.GetModLogger().Log("RPC not found, making a new one...", (byte)2);
			return MakeClient();
		}

		internal DiscordPresence GetDiscordPresence() => _discordPresence;
	}
}

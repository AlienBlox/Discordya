// Decompiled with JetBrains decompiler
// Type: DiscordyaV2.Player.DiscordyaPlayer
// Assembly: DiscordyaV2, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7EE41C03-D9DA-4741-BA28-096DF00C64EC
// Assembly location: C:\Users\Alien\OneDrive\文档\DiscordyaV2\DiscordyaV2.dll

using DiscordRPC;
using DiscordyaV2.Config;
using DiscordyaV2.DiscordClient;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

#nullable disable
namespace DiscordyaV2.Player
{
	public class DiscordyaPlayer : ModPlayer
	{
		private readonly DiscordyaMod _discordyaMod = DiscordyaMod._discordyaMod;
		private readonly DiscordClientHelper _discordClientHelper = DiscordyaMod._discordyaMod.GetDiscordClientHelper();
		private uint _prevCount;
		private bool _pauseUpdate;
		private bool _dead;
		private bool shouldReloadCache = true;

		private uint cooldown => 60;

		public override void PostUpdate() => this.ClientUpdate();

		private void ClientUpdate()
		{
			if (Main.gameMenu || Main.dedServ)
				return;
			if (Main.gamePaused || Main.gameInactive)
			{
				this._pauseUpdate = true;
			}
			else
			{
				++this._prevCount;
				this._pauseUpdate = false;
			}
			if (this._prevCount % this.cooldown != 0U || this._pauseUpdate)
				return;
			if (this.shouldReloadCache)
			{
				this.shouldReloadCache = false;
				this._discordyaMod.GetGamePresenceProcessor().ReloadCache();
			}
			this.ClientUpdatePlayer();
			this.ClientForceUpdate();
		}

		internal void ClientUpdatePlayer()
		{
			if (Main.LocalPlayer == null)
				return;
			(string bigImageKey, string state, string _) = this._discordyaMod.GetGamePresenceProcessor().GetBoss();
			string details = "";
			if (!Main.LocalPlayer.GetModPlayer<DiscordyaPlayer>()._dead)
			{
				if (ModContent.GetInstance<DiscordyaConfig>().DisplayHealth)
					details = this._discordyaMod.GetLanguageHelper().GetText("Generic.Health") + Main.LocalPlayer.statLife.ToString();
				if (ModContent.GetInstance<DiscordyaConfig>().DisplayMana)
					details = details + " " + this._discordyaMod.GetLanguageHelper().GetText("Generic.Mana") + Main.LocalPlayer.statMana.ToString();
				if (ModContent.GetInstance<DiscordyaConfig>().DisplayDefense)
					details = details + " " + this._discordyaMod.GetLanguageHelper().GetText("Generic.Defense") + Main.LocalPlayer.statDefense.ToString();
				if (string.IsNullOrWhiteSpace(details))
					details = this._discordyaMod.GetLanguageHelper().GetText("Generic.Playing");
			}
			else
			{
				details = this._discordyaMod.GetLanguageHelper().GetText("Generic.Dead");
				state = (string)null;
			}
			string smallImageKey = (string)null;
			string smallImageText = (string)null;
			if (ModContent.GetInstance<DiscordyaConfig>().DisplayCurrentItem)
				(smallImageKey, smallImageText) = this.GetItemStatFields();
			this._discordClientHelper.GetDiscordPresence().SetClientStatus(details, state, bigImageKey, this._discordyaMod.worldInfo, smallImageKey, smallImageText);
			this._discordClientHelper.GetDiscordPresence().UpdateClientPresence();
		}

		private void ClientForceUpdate()
		{
			DiscordRpcClient rpcClient = this._discordClientHelper.GetRpcClient();
			if (rpcClient == null || rpcClient.IsDisposed || rpcClient.IsInitialized)
				return;
			rpcClient.Initialize();
		}

		private (string, string) GetItemStatFields()
		{
			int num = -1;
			string str1 = (string)null;
			string str2 = (string)null;
			string str3 = "";
			Item heldItem = Main.LocalPlayer?.HeldItem;
			if (heldItem != null)
			{
				DamageClass damageType = heldItem.DamageType;
				str2 = heldItem.Name;
				num = Main.player[Main.myPlayer].GetWeaponDamage(Main.player[Main.myPlayer].HeldItem, false);
				if (num == 0)
					return ((string)null, (string)null);
				str3 = damageType == DamageClass.Melee || damageType == DamageClass.MeleeNoSpeed ? "Melee" : (damageType != DamageClass.Ranged ? (damageType != DamageClass.Magic ? (damageType != DamageClass.Summon ? "Melee" : "Summon") : "Magic") : "Ranger");
			}
			if (num >= 0)
			{
				str1 = "atk_" + str3.ToLower();
				str2 = str2 + " (" + num.ToString() + " " + this._discordyaMod.GetLanguageHelper().GetText("Generic.Damage") + ")";
			}
			return (str1, str2);
		}

		public override void OnEnterWorld()
		{
			if (((Entity)this.Player).whoAmI != Main.myPlayer)
				return;
			string worldName = Main.worldName;
			bool expertMode = Main.expertMode;
			string str = !Main.masterMode ? (!expertMode ? " " + this._discordyaMod.GetLanguageHelper().GetText("Generic.NormalMode") : " " + this._discordyaMod.GetLanguageHelper().GetText("Generic.ExpertMode")) : " " + this._discordyaMod.GetLanguageHelper().GetText("Generic.MasterMode");
			this._discordyaMod.worldInfo = string.Format(this._discordyaMod.GetLanguageHelper().GetText("Generic.PlayingWorld"), (object)worldName, (object)str);
		}

		public override void Kill(
		  double damage,
		  int hitDirection,
		  bool pvp,
		  PlayerDeathReason damageSource)
		{
			if (Main.player[Main.myPlayer] != this.Player)
				return;
			this._dead = true;
		}

		public override void OnRespawn()
		{
			if (((Entity)this.Player).whoAmI != Main.myPlayer)
				return;
			this._dead = false;
		}
	}
}

using DiscordRPC;
using DiscordyaV2.Config;
using DiscordyaV2.DiscordClient;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

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

		public override void PostUpdate() => ClientUpdate();

		private void ClientUpdate()
		{
			if (Main.gameMenu || Main.dedServ)
				return;
			if (Main.gamePaused || Main.gameInactive)
			{
				_pauseUpdate = true;
			}
			else
			{
				++_prevCount;
				_pauseUpdate = false;
			}
			if (_prevCount % cooldown != 0U || _pauseUpdate)
				return;
			if (shouldReloadCache)
			{
				shouldReloadCache = false;
				_discordyaMod.GetGamePresenceProcessor().ReloadCache();
			}
			ClientUpdatePlayer();
			ClientForceUpdate();
		}

		internal void ClientUpdatePlayer()
		{
			if (Main.LocalPlayer == null)
				return;
			(string bigImageKey, string state, string _) = _discordyaMod.GetGamePresenceProcessor().GetBoss();
			string details = "";
			if (!Main.LocalPlayer.GetModPlayer<DiscordyaPlayer>()._dead)
			{
				if (ModContent.GetInstance<DiscordyaConfig>().DisplayHealth)
					details = _discordyaMod.GetLanguageHelper().GetText("Generic.Health") + Main.LocalPlayer.statLife.ToString();
				if (ModContent.GetInstance<DiscordyaConfig>().DisplayMana)
					details = details + " " + _discordyaMod.GetLanguageHelper().GetText("Generic.Mana") + Main.LocalPlayer.statMana.ToString();
				if (ModContent.GetInstance<DiscordyaConfig>().DisplayDefense)
					details = details + " " + _discordyaMod.GetLanguageHelper().GetText("Generic.Defense") + Main.LocalPlayer.statDefense.ToString();
				if (string.IsNullOrWhiteSpace(details))
					details = _discordyaMod.GetLanguageHelper().GetText("Generic.Playing");
			}
			else
			{
				details = _discordyaMod.GetLanguageHelper().GetText("Generic.Dead");
				state = (string)null;
			}
			string smallImageKey = (string)null;
			string smallImageText = (string)null;
			if (ModContent.GetInstance<DiscordyaConfig>().DisplayCurrentItem)
				(smallImageKey, smallImageText) = GetItemStatFields();
			_discordClientHelper.GetDiscordPresence().SetClientStatus(details, state, bigImageKey, _discordyaMod.worldInfo, smallImageKey, smallImageText);
			_discordClientHelper.GetDiscordPresence().UpdateClientPresence();
		}

		private void ClientForceUpdate()
		{
			DiscordRpcClient rpcClient = _discordClientHelper.GetRpcClient();
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
				str2 = str2 + " (" + num.ToString() + " " + _discordyaMod.GetLanguageHelper().GetText("Generic.Damage") + ")";
			}
			return (str1, str2);
		}

		public override void OnEnterWorld()
		{
			if (((Entity)Player).whoAmI != Main.myPlayer)
				return;
			string worldName = Main.worldName;
			bool expertMode = Main.expertMode;
			string str = !Main.masterMode ? (!expertMode ? " " + _discordyaMod.GetLanguageHelper().GetText("Generic.NormalMode") : " " + _discordyaMod.GetLanguageHelper().GetText("Generic.ExpertMode")) : " " + _discordyaMod.GetLanguageHelper().GetText("Generic.MasterMode");
			_discordyaMod.worldInfo = string.Format(_discordyaMod.GetLanguageHelper().GetText("Generic.PlayingWorld"), (object)worldName, (object)str);
		}

		public override void Kill(
		  double damage,
		  int hitDirection,
		  bool pvp,
		  PlayerDeathReason damageSource)
		{
			if (Main.player[Main.myPlayer] != Player)
				return;
			_dead = true;
		}

		public override void OnRespawn()
		{
			if (((Entity)Player).whoAmI != Main.myPlayer)
				return;
			_dead = false;
		}
	}
}

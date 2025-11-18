using Terraria.ID;
using Terraria.ModLoader;

namespace DiscordyaV2.AlienBloxBrandingTemplate
{
	public class InfoItem : ModItem, ILocalizedModType
	{
		public new string LocalizationCategory => "Items";

		public override void SetDefaults()
		{
			Item.rare = ItemRarityID.Blue;
		}
	}
}
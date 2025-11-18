namespace DiscordyaV2.Logger
{
	public class ModLogger
	{
		private DiscordyaMod _discordyaMod;

		public ModLogger(DiscordyaMod discordyaMod) => _discordyaMod = discordyaMod;

		public void Log(string msg, byte level = 0)
		{
			switch (level)
			{
				case 1:
					_discordyaMod.Logger.Debug((object)msg);
					break;
				case 2:
					_discordyaMod.Logger.Warn((object)msg);
					break;
				case 3:
					_discordyaMod.Logger.Error((object)msg);
					break;
				default:
					_discordyaMod.Logger.Info((object)msg);
					break;
			}
		}
	}
}

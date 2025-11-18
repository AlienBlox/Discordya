namespace DiscordyaV2.CrossMod
{
	public class CrossMods
	{
		private readonly string _modIdentifier;
		private readonly string _applicationIdentifier;
		private readonly string _applicationBigImage;

		internal CrossMods(
		  string modIdentifier,
		  string applicationIdentifier,
		  string applicationBigImage)
		{
			_modIdentifier = modIdentifier;
			_applicationIdentifier = applicationIdentifier;
			_applicationBigImage = applicationBigImage;
		}

		internal string GetModIdentifier() => _modIdentifier;

		internal string GetApplicationIdentifier() => _applicationIdentifier;

		internal string GetApplicationBigImage() => _applicationBigImage;
	}
}

// Decompiled with JetBrains decompiler
// Type: Discordya.DiscordClient.DiscordClientStorage
// Assembly: Discordya, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7EE41C03-D9DA-4741-BA28-096DF00C64EC
// Assembly location: C:\Users\Alien\OneDrive\文档\Discordya\Discordya.dll

using System.Collections.Generic;

#nullable disable
namespace Discordya.DiscordClient
{
  public class DiscordClientStorage
  {
    private readonly Dictionary<string, string> _discordApplicationDictionary;

    internal DiscordClientStorage()
    {
      this._discordApplicationDictionary = new Dictionary<string, string>();
    }

    internal void AddApplicationId(string applicationIdentifier, string applicationId)
    {
      this._discordApplicationDictionary.Add(applicationIdentifier, applicationId);
    }

    internal string GetApplicationId(string applicationIdentifier)
    {
      return this._discordApplicationDictionary[applicationIdentifier];
    }
  }
}

Xenia Compatibility Bot
=======================

[![Build Status](https://dev.azure.com/xenia-canary/xenia-canary/_apis/build/status/xenia-canary.discord-bot?branchName=xenia)](https://dev.azure.com/xenia-canary/xenia-canary/_build/latest?definitionId=3&branchName=xenia) [![Xenia discord server](https://discordapp.com/api/guilds/308194948048486401/widget.png)](https://discord.me/xenia-emulator)

This is a tech support / moderation / crowd entertainment bot for the [Xenia discord server](https://discord.me/xenia-emulator).

You can read the design and implementation notes by visiting the folders in the web interface, or from the [architecture overview notes](architecture.md).

Development Requirements
------------------------
* [.NET Core 2.1 SDK](https://www.microsoft.com/net/download/windows) or newer
* Any text editor, but here are some recommends:
  * [Visual Studio](https://visualstudio.microsoft.com/) (Windows and Mac only, has free Community edition)
  * [Visual Studio Code](https://code.visualstudio.com/) (cross-platform, free)
  * [JetBrains Rider](https://www.jetbrains.com/rider/) (cross-platform)

Runtime Requirements
--------------------
* [.NET Core 2.1 SDK](https://www.microsoft.com/net/download/windows) or newer to run from sources
  * needs `dotnet` command available (i.e. alias for the Snap package)
* [.NET Core 2.1 Runtime](https://www.microsoft.com/net/download/windows) or newer for compiled version
* Optionally Google API credentials to access Google Drive:
  * Create new project in the [Google Cloud Resource Manager](https://console.developers.google.com/cloud-resource-manager)
  * Select the project and enable [Google Drive API](https://console.developers.google.com/apis/library/drive.googleapis.com)
  * Open [API & Services Credentials](https://console.developers.google.com/apis/credentials)
  * Create new credentials:
    * **Service account** credentials
    * New service account
      * if you select an existing account, **new** credentials will be generated **in addition** to previous any ones
    * Role **Project > Viewer**
    * Key type **JSON**
    * **Create** will generate a configuration file
  * Save said configuration file as `credentials.json` in [user secrets](https://docs.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-2.2#how-the-secret-manager-tool-works) folder
    * e.g on Linux this will be `~/.microsoft/usersecrets/c2e6548b-b215-4a18-a010-958ef294b310/credentials.json`

How to Build
------------
* Change configuration for test server in `CompatBot/Properties/launchSettings.json`
* Note that token could be set in the settings _or_ supplied as a launch argument (higher priority)
* If you've changed the database model, add a migration
	* `$ cd CompatBot`
	* `$ dotnet ef migrations add -c [BotDb|ThumbnailDb] MigrationName`
	* `$ cd ..`
* `$ cd CompatBot`
* `$ dotnet run [token]`

How to Run in Production
------------------------

### Running from source
* Change configuration if needed (probably just token):
  * use `$ dotnet user-secrets set Token <your_token_here>`
  * for available configuration variables, see [Config.cs](CompatBot/Config.cs#L31)
* Put `bot.db` in `CompatBot/` if you have one
* `$ cd CompatBot`
* `$ dotnet run -c Release`

### Running with Docker
* Official image is hosted on [Docker Hub](https://hub.docker.com/r/rpcs3/discord-bot).
* You should pull images tagged with `release-latest` (same thing as `latest`)
* Please take a look at the [docker-compose.yml](docker-compose.yml) for required configuration (bot token and mounting points for persistent data).

External resources that need manual updates
-------------------------------------------
* [Unicode confusables](http://www.unicode.org/Public/security/latest/confusables.txt) gzipped, for Homoglyph checks
* Optionally [Redump disc key database](http://redump.org/downloads/) in text format (requires membership)

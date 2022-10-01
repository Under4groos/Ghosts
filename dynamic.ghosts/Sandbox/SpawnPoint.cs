using System;
using SandboxEditor;

namespace Sandbox
{
	// Token: 0x0200014A RID: 330
	[Library("info_player_start")]
	[HammerEntity]
	[EditorModel("models/editor/playerstart.vmdl", "white", "white", FixedBounds = true)]
	[Title("Player Spawnpoint")]
	[Category("Player")]
	[Icon("place")]
	[Description("This entity defines the spawn point of the player in first person shooter gamemodes.")]
	public class SpawnPoint : Entity
	{
	}
}

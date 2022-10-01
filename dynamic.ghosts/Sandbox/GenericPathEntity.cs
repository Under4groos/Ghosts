using System;
using SandboxEditor;

namespace Sandbox
{
	// Token: 0x0200016F RID: 367
	[Library("path_generic")]
	[HammerEntity]
	[Path("path_generic_node", false)]
	[Title("Generic Path")]
	[Category("Gameplay")]
	[Icon("moving")]
	[Description("A generic multi-purpose path that compiles all nodes as a single entity.<br /> This entity can be used with entities like ent_path_platform.")]
	public class GenericPathEntity : BasePathEntity<BasePathNode>
	{
	}
}

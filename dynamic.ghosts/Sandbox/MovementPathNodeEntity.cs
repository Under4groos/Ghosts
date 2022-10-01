using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Sandbox
{
	// Token: 0x02000181 RID: 385
	[Library("movement_path_node")]
	[Description("A movement path node.")]
	public class MovementPathNodeEntity : BasePathNodeEntity
	{
		// Token: 0x17000511 RID: 1297
		// (get) Token: 0x0600123E RID: 4670 RVA: 0x00046D91 File Offset: 0x00044F91
		// (set) Token: 0x0600123F RID: 4671 RVA: 0x00046D99 File Offset: 0x00044F99
		[Property]
		[global::DefaultValue(0)]
		[Description("When passing this node, the moving entity will have its speed set to this value. 0 or less mean do not change.")]
		public float Speed { get; set; }

		// Token: 0x17000512 RID: 1298
		// (get) Token: 0x06001240 RID: 4672 RVA: 0x00046DA2 File Offset: 0x00044FA2
		// (set) Token: 0x06001241 RID: 4673 RVA: 0x00046DAA File Offset: 0x00044FAA
		[Property]
		[Category("Alternative Path")]
		[Title("Enabled")]
		[Description("Whether the alternative path is enabled or not.")]
		public bool AlternativePathEnabled { get; set; }

		// Token: 0x17000513 RID: 1299
		// (get) Token: 0x06001242 RID: 4674 RVA: 0x00046DB3 File Offset: 0x00044FB3
		// (set) Token: 0x06001243 RID: 4675 RVA: 0x00046DBB File Offset: 0x00044FBB
		[Property]
		[Category("Alternative Path")]
		[Title("Forward Path")]
		[Description("Alternative node when moving forwards, for path changing.")]
		public EntityTarget AlternativeNodeForwards { get; set; }

		// Token: 0x17000514 RID: 1300
		// (get) Token: 0x06001244 RID: 4676 RVA: 0x00046DC4 File Offset: 0x00044FC4
		// (set) Token: 0x06001245 RID: 4677 RVA: 0x00046DCC File Offset: 0x00044FCC
		[Property]
		[Category("Alternative Path")]
		[Title("Backward Path")]
		[Description("Alternative node when moving backwards, for path changing.")]
		public EntityTarget AlternativeNodeBackwards { get; set; }

		// Token: 0x17000515 RID: 1301
		// (get) Token: 0x06001246 RID: 4678 RVA: 0x00046DD5 File Offset: 0x00044FD5
		// (set) Token: 0x06001247 RID: 4679 RVA: 0x00046DDD File Offset: 0x00044FDD
		[Description("Fired when an entity passes this node, depending on the entity implementation.")]
		public Entity.Output OnPassed { get; set; }

		// Token: 0x06001248 RID: 4680 RVA: 0x00046DE6 File Offset: 0x00044FE6
		[Input]
		[Description("Enables the alternative path.")]
		public void EnableAlternativePath()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.AlternativePathEnabled = true;
		}

		// Token: 0x06001249 RID: 4681 RVA: 0x00046DF4 File Offset: 0x00044FF4
		[Input]
		[Description("Disables the alternative path.")]
		public void DisablesAlternativePath()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.AlternativePathEnabled = false;
		}

		// Token: 0x0600124A RID: 4682 RVA: 0x00046E02 File Offset: 0x00045002
		[Input]
		[Description("Toggles the alternative path.")]
		public void ToggleAlternativePath()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.AlternativePathEnabled = !this.AlternativePathEnabled;
		}

		// Token: 0x0600124B RID: 4683 RVA: 0x00046E18 File Offset: 0x00045018
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected override void CreateHammerOutputs()
		{
			this.OnPassed = new Entity.Output(this, "OnPassed");
			base.CreateHammerOutputs();
		}
	}
}

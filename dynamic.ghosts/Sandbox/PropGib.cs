using System;
using System.Runtime.CompilerServices;

namespace Sandbox
{
	// Token: 0x02000148 RID: 328
	[Title("Gib")]
	[Icon("broken_image")]
	[Category("Gibs")]
	public class PropGib : Prop
	{
		// Token: 0x17000421 RID: 1057
		// (get) Token: 0x06000F16 RID: 3862 RVA: 0x0003CF0F File Offset: 0x0003B10F
		// (set) Token: 0x06000F17 RID: 3863 RVA: 0x0003CF17 File Offset: 0x0003B117
		[Description("Used to track individual break pieces for the purposes of Model Break Commands. ModelDoc guarantees that these names will be unique.")]
		public string BreakpieceName { get; set; }

		// Token: 0x06000F18 RID: 3864 RVA: 0x0003CF20 File Offset: 0x0003B120
		public override void Spawn()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Spawn();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Tags.Add(new string[]
			{
				"gib",
				"debris"
			});
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Tags.Remove("prop");
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Tags.Remove("solid");
		}
	}
}

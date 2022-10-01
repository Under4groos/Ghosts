using System;
using System.Runtime.CompilerServices;
using Sandbox;
using Sandbox.Internal;

namespace StalkerRP.PostProcessing
{
	// Token: 0x02000067 RID: 103
	public class StalkerPostProcessingBase : Entity
	{
		// Token: 0x17000111 RID: 273
		// (get) Token: 0x06000491 RID: 1169 RVA: 0x00017C87 File Offset: 0x00015E87
		// (set) Token: 0x06000492 RID: 1170 RVA: 0x00017C8F File Offset: 0x00015E8F
		protected StandardPostProcess StandardPostProcess { get; set; }

		// Token: 0x06000493 RID: 1171 RVA: 0x00017C98 File Offset: 0x00015E98
		[Event.HotloadAttribute]
		public virtual void RefreshPostProcess()
		{
			if (GlobalGameNamespace.PostProcess == null)
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			GlobalGameNamespace.PostProcess.Remove<StandardPostProcess>(this.StandardPostProcess);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.StandardPostProcess = new StandardPostProcess();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			GlobalGameNamespace.PostProcess.Add<StandardPostProcess>(this.StandardPostProcess);
		}
	}
}

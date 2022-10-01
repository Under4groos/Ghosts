using System;
using System.Runtime.CompilerServices;
using Sandbox;

namespace StalkerRP
{
	// Token: 0x0200004F RID: 79
	public class Carriable : BaseCarriable, IUse
	{
		// Token: 0x06000337 RID: 823 RVA: 0x0001277C File Offset: 0x0001097C
		public override void CreateViewModel()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Host.AssertClient("CreateViewModel");
			if (string.IsNullOrEmpty(this.ViewModelPath))
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.ViewModelEntity = new ViewModel
			{
				Position = this.Position,
				Owner = this.Owner,
				EnableViewmodelRendering = true
			};
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.ViewModelEntity.SetModel(this.ViewModelPath);
		}

		// Token: 0x06000338 RID: 824 RVA: 0x000127EB File Offset: 0x000109EB
		public bool OnUse(Entity user)
		{
			return false;
		}

		// Token: 0x06000339 RID: 825 RVA: 0x000127EE File Offset: 0x000109EE
		public virtual bool IsUsable(Entity user)
		{
			return this.Owner == null;
		}
	}
}

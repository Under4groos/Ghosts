using System;
using System.Runtime.CompilerServices;

namespace Sandbox
{
	// Token: 0x0200018A RID: 394
	[Library]
	public class Duck : BaseNetworkable
	{
		// Token: 0x06001310 RID: 4880 RVA: 0x0004A01E File Offset: 0x0004821E
		public Duck(BasePlayerController controller)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Controller = controller;
		}

		// Token: 0x06001311 RID: 4881 RVA: 0x0004A034 File Offset: 0x00048234
		public virtual void PreTick()
		{
			bool wants = Input.Down(InputButton.Duck);
			if (wants != this.IsActive)
			{
				if (wants)
				{
					this.TryDuck();
				}
				else
				{
					this.TryUnDuck();
				}
			}
			if (this.IsActive)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Controller.SetTag("ducked");
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Controller.EyeLocalPosition *= 0.5f;
			}
		}

		// Token: 0x06001312 RID: 4882 RVA: 0x0004A0A1 File Offset: 0x000482A1
		protected virtual void TryDuck()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.IsActive = true;
		}

		// Token: 0x06001313 RID: 4883 RVA: 0x0004A0B0 File Offset: 0x000482B0
		protected virtual void TryUnDuck()
		{
			if (this.Controller.TraceBBox(this.Controller.Position, this.Controller.Position, this.originalMins, this.originalMaxs, 0f).StartedSolid)
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.IsActive = false;
		}

		// Token: 0x06001314 RID: 4884 RVA: 0x0004A103 File Offset: 0x00048303
		public virtual void UpdateBBox(ref Vector3 mins, ref Vector3 maxs, float scale)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.originalMins = mins;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.originalMaxs = maxs;
			if (this.IsActive)
			{
				maxs = maxs.WithZ(36f * scale);
			}
		}

		// Token: 0x06001315 RID: 4885 RVA: 0x0004A142 File Offset: 0x00048342
		public virtual float GetWishSpeed()
		{
			if (!this.IsActive)
			{
				return -1f;
			}
			return 64f;
		}

		// Token: 0x04000619 RID: 1561
		public BasePlayerController Controller;

		// Token: 0x0400061A RID: 1562
		public bool IsActive;

		// Token: 0x0400061B RID: 1563
		private Vector3 originalMins;

		// Token: 0x0400061C RID: 1564
		private Vector3 originalMaxs;
	}
}

using System;
using System.Runtime.CompilerServices;
using Sandbox;

namespace StalkerRP
{
	// Token: 0x0200002C RID: 44
	public class Duck : BaseNetworkable
	{
		// Token: 0x06000153 RID: 339 RVA: 0x0000A15A File Offset: 0x0000835A
		public Duck(BasePlayerController controller)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Controller = controller;
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000154 RID: 340 RVA: 0x0000A17E File Offset: 0x0000837E
		private float duckTime
		{
			get
			{
				return 0.2f;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000155 RID: 341 RVA: 0x0000A185 File Offset: 0x00008385
		private float unDuckTime
		{
			get
			{
				return 0.2f;
			}
		}

		// Token: 0x06000156 RID: 342 RVA: 0x0000A18C File Offset: 0x0000838C
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
				float frac = this.timeSinceDuckChanged / this.duckTime;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				frac = frac.Clamp(0f, 1f);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Controller.EyeLocalPosition *= 1f - frac * 0.5f;
				return;
			}
			float frac2 = this.timeSinceDuckChanged / this.unDuckTime;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			frac2 = frac2.Clamp(0f, 1f);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Controller.EyeLocalPosition *= 0.5f + 0.5f * frac2;
		}

		// Token: 0x06000157 RID: 343 RVA: 0x0000A27C File Offset: 0x0000847C
		protected virtual void TryDuck()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.IsActive = true;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.timeSinceDuckChanged = 0f;
		}

		// Token: 0x06000158 RID: 344 RVA: 0x0000A2A0 File Offset: 0x000084A0
		protected virtual void TryUnDuck()
		{
			if (this.Controller.TraceBBox(this.Controller.Position, this.Controller.Position, this.originalMins, this.originalMaxs, 0f).StartedSolid)
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.IsActive = false;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.timeSinceDuckChanged = 0f;
		}

		// Token: 0x06000159 RID: 345 RVA: 0x0000A308 File Offset: 0x00008508
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

		// Token: 0x0600015A RID: 346 RVA: 0x0000A347 File Offset: 0x00008547
		public virtual float GetWishSpeed()
		{
			if (!this.IsActive)
			{
				return -1f;
			}
			return 64f;
		}

		// Token: 0x04000082 RID: 130
		public BasePlayerController Controller;

		// Token: 0x04000083 RID: 131
		public bool IsActive;

		// Token: 0x04000084 RID: 132
		private TimeSince timeSinceDuckChanged = 0f;

		// Token: 0x04000085 RID: 133
		private Vector3 originalMins;

		// Token: 0x04000086 RID: 134
		private Vector3 originalMaxs;
	}
}

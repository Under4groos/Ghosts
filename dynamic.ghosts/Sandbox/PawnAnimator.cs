using System;
using System.Runtime.CompilerServices;

namespace Sandbox
{
	// Token: 0x02000187 RID: 391
	public abstract class PawnAnimator : PawnController
	{
		// Token: 0x1700053D RID: 1341
		// (get) Token: 0x060012DB RID: 4827 RVA: 0x000498AE File Offset: 0x00047AAE
		public AnimatedEntity AnimPawn
		{
			get
			{
				return base.Pawn as AnimatedEntity;
			}
		}

		// Token: 0x060012DC RID: 4828 RVA: 0x000498BC File Offset: 0x00047ABC
		[Description("We'll convert Position to a local position to the players eyes and set the param on the animgraph.")]
		public virtual void SetLookAt(string name, Vector3 Position)
		{
			Vector3 localPos = (Position - base.Pawn.EyePosition) * base.Rotation.Inverse;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.SetAnimParameter(name, localPos);
		}

		// Token: 0x060012DD RID: 4829 RVA: 0x000498FB File Offset: 0x00047AFB
		[Description("Sets the param on the animgraph")]
		public virtual void SetAnimParameter(string name, Vector3 val)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			AnimatedEntity animPawn = this.AnimPawn;
			if (animPawn == null)
			{
				return;
			}
			animPawn.SetAnimParameter(name, val);
		}

		// Token: 0x060012DE RID: 4830 RVA: 0x00049914 File Offset: 0x00047B14
		[Description("Sets the param on the animgraph")]
		public virtual void SetAnimParameter(string name, float val)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			AnimatedEntity animPawn = this.AnimPawn;
			if (animPawn == null)
			{
				return;
			}
			animPawn.SetAnimParameter(name, val);
		}

		// Token: 0x060012DF RID: 4831 RVA: 0x0004992D File Offset: 0x00047B2D
		[Description("Sets the param on the animgraph")]
		public virtual void SetAnimParameter(string name, bool val)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			AnimatedEntity animPawn = this.AnimPawn;
			if (animPawn == null)
			{
				return;
			}
			animPawn.SetAnimParameter(name, val);
		}

		// Token: 0x060012E0 RID: 4832 RVA: 0x00049946 File Offset: 0x00047B46
		[Description("Sets the param on the animgraph")]
		public virtual void SetAnimParameter(string name, int val)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			AnimatedEntity animPawn = this.AnimPawn;
			if (animPawn == null)
			{
				return;
			}
			animPawn.SetAnimParameter(name, val);
		}

		// Token: 0x060012E1 RID: 4833 RVA: 0x0004995F File Offset: 0x00047B5F
		[Description("Calls SetParam( name, true ). It's expected that your animgraph has a \"name\" param with the auto reset property set.")]
		public virtual void Trigger(string name)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.SetAnimParameter(name, true);
		}

		// Token: 0x060012E2 RID: 4834 RVA: 0x0004996E File Offset: 0x00047B6E
		[Description("Resets all params to default values on the animgraph")]
		public virtual void ResetParameters()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			AnimatedEntity animPawn = this.AnimPawn;
			if (animPawn == null)
			{
				return;
			}
			animPawn.ResetAnimParameters();
		}
	}
}

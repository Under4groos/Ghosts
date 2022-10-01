using System;
using System.Runtime.CompilerServices;
using Sandbox;
using Sandbox.Internal;

namespace StalkerRP
{
	// Token: 0x02000038 RID: 56
	public class LeanComponent : EntityComponent<StalkerPlayer>
	{
		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000232 RID: 562 RVA: 0x0000E6D9 File Offset: 0x0000C8D9
		// (set) Token: 0x06000233 RID: 563 RVA: 0x0000E6E7 File Offset: 0x0000C8E7
		[Net]
		[Predicted]
		public unsafe float Lean
		{
			get
			{
				return *this._repback__Lean.GetValue();
			}
			set
			{
				this._repback__Lean.SetValue(value);
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000234 RID: 564 RVA: 0x0000E6F6 File Offset: 0x0000C8F6
		// (set) Token: 0x06000235 RID: 565 RVA: 0x0000E704 File Offset: 0x0000C904
		[Net]
		[Predicted]
		[DefaultValue(LeanComponent.LeanMode.None)]
		private unsafe LeanComponent.LeanMode Mode
		{
			get
			{
				return *this._repback__Mode.GetValue();
			}
			set
			{
				this._repback__Mode.SetValue(value);
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000236 RID: 566 RVA: 0x0000E713 File Offset: 0x0000C913
		private float MaxAngle
		{
			get
			{
				return 20f;
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000237 RID: 567 RVA: 0x0000E71A File Offset: 0x0000C91A
		private float LerpSpeed
		{
			get
			{
				return 4.5f;
			}
		}

		// Token: 0x06000238 RID: 568 RVA: 0x0000E724 File Offset: 0x0000C924
		public void Simulate(Client client)
		{
			if (Input.Pressed(InputButton.Use))
			{
				this.Mode = ((this.Mode == LeanComponent.LeanMode.Right) ? LeanComponent.LeanMode.None : LeanComponent.LeanMode.Right);
			}
			if (Input.Pressed(InputButton.Menu))
			{
				this.Mode = ((this.Mode == LeanComponent.LeanMode.Left) ? LeanComponent.LeanMode.None : LeanComponent.LeanMode.Left);
			}
			if (base.Entity.StaminaComponent.IsRunning)
			{
				this.Mode = LeanComponent.LeanMode.None;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.UpdateLean();
		}

		// Token: 0x06000239 RID: 569 RVA: 0x0000E798 File Offset: 0x0000C998
		private void UpdateLean()
		{
			switch (this.Mode)
			{
			case LeanComponent.LeanMode.None:
				this.Lean = this.Lean.LerpTo(0f, Time.Delta * this.LerpSpeed, true);
				break;
			case LeanComponent.LeanMode.Left:
				this.Lean = this.Lean.LerpTo(-1f, Time.Delta * this.LerpSpeed, true);
				break;
			case LeanComponent.LeanMode.Right:
				this.Lean = this.Lean.LerpTo(1f, Time.Delta * this.LerpSpeed, true);
				break;
			}
			float normal = 1f - (1f - this.Lean) / 2f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Entity.Animator.SetAnimParameter("leaning", normal);
		}

		// Token: 0x0600023A RID: 570 RVA: 0x0000E864 File Offset: 0x0000CA64
		public Vector3 GetLeanOffset(Rotation rotation)
		{
			float absLean = MathF.Abs(this.Lean);
			return rotation.Down * 7f * absLean + rotation.Right * 17.5f * this.Lean;
		}

		// Token: 0x0600023B RID: 571 RVA: 0x0000E8B5 File Offset: 0x0000CAB5
		public Rotation GetLeanRotation()
		{
			return Rotation.FromRoll(this.Lean * this.MaxAngle);
		}

		// Token: 0x0600023C RID: 572 RVA: 0x0000E8C9 File Offset: 0x0000CAC9
		public override void BuildNetworkTable(NetworkTable builder)
		{
			builder.Register<VarUnmanaged<float>>(ref this._repback__Lean, "Lean", true, false);
			builder.Register<VarUnmanaged<LeanComponent.LeanMode>>(ref this._repback__Mode, "Mode", true, false);
			base.BuildNetworkTable(builder);
		}

		// Token: 0x040000C6 RID: 198
		private VarUnmanaged<float> _repback__Lean = new VarUnmanaged<float>();

		// Token: 0x040000C7 RID: 199
		private VarUnmanaged<LeanComponent.LeanMode> _repback__Mode = new VarUnmanaged<LeanComponent.LeanMode>(LeanComponent.LeanMode.None);

		// Token: 0x020001F4 RID: 500
		private enum LeanMode
		{
			// Token: 0x04000813 RID: 2067
			None,
			// Token: 0x04000814 RID: 2068
			Left,
			// Token: 0x04000815 RID: 2069
			Right
		}
	}
}

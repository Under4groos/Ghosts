using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Sandbox;
using Sandbox.Internal;

namespace StalkerRP
{
	// Token: 0x0200004E RID: 78
	[Title("Base Weapon")]
	[Icon("sports_martial_arts")]
	[Description("A common base we can use for weapons so we don't have to implement the logic over and over again. Feel free to not use this and to implement it however you want to.")]
	public class BaseWeapon : BaseCarriable
	{
		// Token: 0x1700009F RID: 159
		// (get) Token: 0x06000325 RID: 805 RVA: 0x000124E0 File Offset: 0x000106E0
		public virtual float PrimaryRate
		{
			get
			{
				return 5f;
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06000326 RID: 806 RVA: 0x000124E7 File Offset: 0x000106E7
		public virtual float SecondaryRate
		{
			get
			{
				return 15f;
			}
		}

		// Token: 0x06000327 RID: 807 RVA: 0x000124EE File Offset: 0x000106EE
		public override void Spawn()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Spawn();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Tags.Add("item");
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x06000328 RID: 808 RVA: 0x00012510 File Offset: 0x00010710
		// (set) Token: 0x06000329 RID: 809 RVA: 0x00012522 File Offset: 0x00010722
		[Net]
		[Predicted]
		public unsafe TimeSince TimeSincePrimaryAttack
		{
			get
			{
				return *this._repback__TimeSincePrimaryAttack.GetValue();
			}
			set
			{
				this._repback__TimeSincePrimaryAttack.SetValue(value);
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x0600032A RID: 810 RVA: 0x00012531 File Offset: 0x00010731
		// (set) Token: 0x0600032B RID: 811 RVA: 0x00012543 File Offset: 0x00010743
		[Net]
		[Predicted]
		public unsafe TimeSince TimeSinceSecondaryAttack
		{
			get
			{
				return *this._repback__TimeSinceSecondaryAttack.GetValue();
			}
			set
			{
				this._repback__TimeSinceSecondaryAttack.SetValue(value);
			}
		}

		// Token: 0x0600032C RID: 812 RVA: 0x00012554 File Offset: 0x00010754
		public override void Simulate(Client player)
		{
			if (this.CanReload())
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Reload();
			}
			if (!this.Owner.IsValid())
			{
				return;
			}
			if (this.CanPrimaryAttack())
			{
				using (Entity.LagCompensation())
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.TimeSincePrimaryAttack = 0f;
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.AttackPrimary();
				}
			}
			if (!this.Owner.IsValid())
			{
				return;
			}
			if (this.CanSecondaryAttack())
			{
				using (Entity.LagCompensation())
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.TimeSinceSecondaryAttack = 0f;
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.AttackSecondary();
				}
			}
		}

		// Token: 0x0600032D RID: 813 RVA: 0x00012620 File Offset: 0x00010820
		public virtual bool CanReload()
		{
			return this.Owner.IsValid() && Input.Down(InputButton.Reload);
		}

		// Token: 0x0600032E RID: 814 RVA: 0x0001263F File Offset: 0x0001083F
		public virtual void Reload()
		{
		}

		// Token: 0x0600032F RID: 815 RVA: 0x00012644 File Offset: 0x00010844
		public virtual bool CanPrimaryAttack()
		{
			if (!this.Owner.IsValid() || !Input.Down(InputButton.PrimaryAttack))
			{
				return false;
			}
			float rate = this.PrimaryRate;
			return rate <= 0f || this.TimeSincePrimaryAttack > 1f / rate;
		}

		// Token: 0x06000330 RID: 816 RVA: 0x00012692 File Offset: 0x00010892
		public virtual void AttackPrimary()
		{
		}

		// Token: 0x06000331 RID: 817 RVA: 0x00012694 File Offset: 0x00010894
		public virtual bool CanSecondaryAttack()
		{
			if (!this.Owner.IsValid() || !Input.Down(InputButton.SecondaryAttack))
			{
				return false;
			}
			float rate = this.SecondaryRate;
			return rate <= 0f || this.TimeSinceSecondaryAttack > 1f / rate;
		}

		// Token: 0x06000332 RID: 818 RVA: 0x000126E2 File Offset: 0x000108E2
		public virtual void AttackSecondary()
		{
		}

		// Token: 0x06000333 RID: 819 RVA: 0x000126E4 File Offset: 0x000108E4
		[Description("Does a trace from start to end, does bullet impact effects. Coded as an IEnumerable so you can return multiple hits, like if you're going through layers or ricocet'ing or something.")]
		public virtual IEnumerable<TraceResult> TraceBullet(Vector3 start, Vector3 end, float radius = 2f)
		{
			bool underWater = Trace.TestPoint(start, "water", 0.5f);
			Trace trace2 = Trace.Ray(start, end).UseHitboxes(true).WithAnyTags(new string[]
			{
				"solid",
				"player",
				"npc"
			});
			Entity entity = this;
			bool flag = true;
			trace2 = trace2.Ignore(entity, flag);
			Vector3 vector = radius;
			Trace trace = trace2.Size(vector);
			if (!underWater)
			{
				trace = trace.WithAnyTags(new string[]
				{
					"water"
				});
			}
			TraceResult tr = trace.Run();
			if (tr.Hit)
			{
				yield return tr;
			}
			yield break;
		}

		// Token: 0x06000334 RID: 820 RVA: 0x00012709 File Offset: 0x00010909
		public override Sound PlaySound(string soundName, string attachment)
		{
			if (this.Owner.IsValid())
			{
				return this.Owner.PlaySound(soundName, attachment);
			}
			return base.PlaySound(soundName, attachment);
		}

		// Token: 0x06000335 RID: 821 RVA: 0x0001272E File Offset: 0x0001092E
		public override void BuildNetworkTable(NetworkTable builder)
		{
			builder.Register<VarUnmanaged<TimeSince>>(ref this._repback__TimeSincePrimaryAttack, "TimeSincePrimaryAttack", true, false);
			builder.Register<VarUnmanaged<TimeSince>>(ref this._repback__TimeSinceSecondaryAttack, "TimeSinceSecondaryAttack", true, false);
			base.BuildNetworkTable(builder);
		}

		// Token: 0x040000F6 RID: 246
		private VarUnmanaged<TimeSince> _repback__TimeSincePrimaryAttack = new VarUnmanaged<TimeSince>();

		// Token: 0x040000F7 RID: 247
		private VarUnmanaged<TimeSince> _repback__TimeSinceSecondaryAttack = new VarUnmanaged<TimeSince>();
	}
}

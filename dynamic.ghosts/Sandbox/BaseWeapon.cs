using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Sandbox.Internal;

namespace Sandbox
{
	// Token: 0x0200015F RID: 351
	[Title("Base Weapon")]
	[Icon("sports_martial_arts")]
	[Description("A common base we can use for weapons so we don't have to implement the logic over and over again. Feel free to not use this and to implement it however you want to.")]
	public class BaseWeapon : BaseCarriable
	{
		// Token: 0x17000461 RID: 1121
		// (get) Token: 0x06000FEC RID: 4076 RVA: 0x0003F4F0 File Offset: 0x0003D6F0
		public virtual float PrimaryRate
		{
			get
			{
				return 5f;
			}
		}

		// Token: 0x17000462 RID: 1122
		// (get) Token: 0x06000FED RID: 4077 RVA: 0x0003F4F7 File Offset: 0x0003D6F7
		public virtual float SecondaryRate
		{
			get
			{
				return 15f;
			}
		}

		// Token: 0x06000FEE RID: 4078 RVA: 0x0003F4FE File Offset: 0x0003D6FE
		public override void Spawn()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Spawn();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Tags.Add("item");
		}

		// Token: 0x17000463 RID: 1123
		// (get) Token: 0x06000FEF RID: 4079 RVA: 0x0003F520 File Offset: 0x0003D720
		// (set) Token: 0x06000FF0 RID: 4080 RVA: 0x0003F532 File Offset: 0x0003D732
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

		// Token: 0x17000464 RID: 1124
		// (get) Token: 0x06000FF1 RID: 4081 RVA: 0x0003F541 File Offset: 0x0003D741
		// (set) Token: 0x06000FF2 RID: 4082 RVA: 0x0003F553 File Offset: 0x0003D753
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

		// Token: 0x06000FF3 RID: 4083 RVA: 0x0003F564 File Offset: 0x0003D764
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

		// Token: 0x06000FF4 RID: 4084 RVA: 0x0003F630 File Offset: 0x0003D830
		public virtual bool CanReload()
		{
			return this.Owner.IsValid() && Input.Down(InputButton.Reload);
		}

		// Token: 0x06000FF5 RID: 4085 RVA: 0x0003F64F File Offset: 0x0003D84F
		public virtual void Reload()
		{
		}

		// Token: 0x06000FF6 RID: 4086 RVA: 0x0003F654 File Offset: 0x0003D854
		public virtual bool CanPrimaryAttack()
		{
			if (!this.Owner.IsValid() || !Input.Down(InputButton.PrimaryAttack))
			{
				return false;
			}
			float rate = this.PrimaryRate;
			return rate <= 0f || this.TimeSincePrimaryAttack > 1f / rate;
		}

		// Token: 0x06000FF7 RID: 4087 RVA: 0x0003F6A2 File Offset: 0x0003D8A2
		public virtual void AttackPrimary()
		{
		}

		// Token: 0x06000FF8 RID: 4088 RVA: 0x0003F6A4 File Offset: 0x0003D8A4
		public virtual bool CanSecondaryAttack()
		{
			if (!this.Owner.IsValid() || !Input.Down(InputButton.SecondaryAttack))
			{
				return false;
			}
			float rate = this.SecondaryRate;
			return rate <= 0f || this.TimeSinceSecondaryAttack > 1f / rate;
		}

		// Token: 0x06000FF9 RID: 4089 RVA: 0x0003F6F2 File Offset: 0x0003D8F2
		public virtual void AttackSecondary()
		{
		}

		// Token: 0x06000FFA RID: 4090 RVA: 0x0003F6F4 File Offset: 0x0003D8F4
		[Description("Does a trace from start to end, does bullet impact effects. Coded as an IEnumerable so you can return multiple hits, like if you're going through layers or ricocheting or something.")]
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

		// Token: 0x06000FFB RID: 4091 RVA: 0x0003F719 File Offset: 0x0003D919
		public override Sound PlaySound(string soundName, string attachment)
		{
			if (this.Owner.IsValid())
			{
				return this.Owner.PlaySound(soundName, attachment);
			}
			return base.PlaySound(soundName, attachment);
		}

		// Token: 0x06000FFC RID: 4092 RVA: 0x0003F73E File Offset: 0x0003D93E
		public override void BuildNetworkTable(NetworkTable builder)
		{
			builder.Register<VarUnmanaged<TimeSince>>(ref this._repback__TimeSincePrimaryAttack, "TimeSincePrimaryAttack", true, false);
			builder.Register<VarUnmanaged<TimeSince>>(ref this._repback__TimeSinceSecondaryAttack, "TimeSinceSecondaryAttack", true, false);
			base.BuildNetworkTable(builder);
		}

		// Token: 0x04000506 RID: 1286
		private VarUnmanaged<TimeSince> _repback__TimeSincePrimaryAttack = new VarUnmanaged<TimeSince>();

		// Token: 0x04000507 RID: 1287
		private VarUnmanaged<TimeSince> _repback__TimeSinceSecondaryAttack = new VarUnmanaged<TimeSince>();
	}
}

using System;
using System.Runtime.CompilerServices;
using Sandbox;

namespace StalkerRP
{
	// Token: 0x0200001F RID: 31
	public class TelekinesisThrowComponent : EntityComponent, ISingletonComponent
	{
		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000100 RID: 256 RVA: 0x00009038 File Offset: 0x00007238
		public static Vector3 HoverOffset
		{
			get
			{
				return Vector3.Up * 55f;
			}
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00009049 File Offset: 0x00007249
		protected override void OnActivate()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.timeSinceActivated = 0f;
			if (base.Entity.IsClient)
			{
				this.AddEffects();
			}
			if (base.Entity.IsServer)
			{
				this.StartTelekinesis();
			}
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00009086 File Offset: 0x00007286
		public void SetUp(Entity _target, float _holdTime = 2f, float _force = 300f)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.target = _target;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.holdTime = _holdTime;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.force = _force;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.initialised = true;
		}

		// Token: 0x06000103 RID: 259 RVA: 0x000090B8 File Offset: 0x000072B8
		protected override void OnDeactivate()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Particles particles = this.effects;
			if (particles == null)
			{
				return;
			}
			particles.Destroy(false);
		}

		// Token: 0x06000104 RID: 260 RVA: 0x000090D0 File Offset: 0x000072D0
		public static bool CanAddToEntity(Entity entity, Entity target)
		{
			if (entity.Tags.Has("BurerGrabbed"))
			{
				return false;
			}
			Prop y = entity as Prop;
			if (y == null)
			{
				return false;
			}
			PhysicsBody body = y.PhysicsBody;
			return body != null && body.BodyType != PhysicsBodyType.Keyframed && body.BodyType != PhysicsBodyType.Static && entity.CanSee(target);
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00009128 File Offset: 0x00007328
		private void AddEffects()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.effects = Particles.Create("particles/stalker/burer_tele_hold.vpcf", base.Entity, true);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.effects.SetPosition(0, base.Entity.WorldSpaceBounds.Center);
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00009178 File Offset: 0x00007378
		private void StartTelekinesis()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.host = (base.Entity as Prop);
			if (this.host == null)
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.mass = this.host.PhysicsBody.Mass;
			if (base.Entity.PhysicsGroup != null)
			{
				for (int i = 0; i < base.Entity.PhysicsGroup.BodyCount; i++)
				{
					base.Entity.PhysicsGroup.GetBody(i).GravityEnabled = false;
				}
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.mass = this.host.PhysicsGroup.Mass;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Sound.FromEntity("poltergeist.tele.grab", base.Entity);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.host.PhysicsBody.GravityEnabled = false;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.host.PhysicsBody.ApplyAngularImpulse(Vector3.Random * 30f * this.host.PhysicsBody.Mass);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.host.Tags.Add("BurerGrabbed");
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.targetPos = this.host.Position + TelekinesisThrowComponent.HoverOffset;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.timeSinceStartedTelekinesis = 0f;
		}

		// Token: 0x06000107 RID: 263 RVA: 0x000092D0 File Offset: 0x000074D0
		public void Release()
		{
			if (this.host != null)
			{
				if (this.host.PhysicsBody.IsValid())
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.host.PhysicsBody.GravityEnabled = true;
					if (this.host.PhysicsGroup != null)
					{
						for (int i = 0; i < this.host.PhysicsGroup.BodyCount; i++)
						{
							this.host.PhysicsGroup.GetBody(i).GravityEnabled = true;
						}
					}
				}
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.host.Tags.Remove("BurerGrabbed");
			}
		}

		// Token: 0x06000108 RID: 264 RVA: 0x0000936C File Offset: 0x0000756C
		private void DoThrow()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Release();
			Vector3 throwForce = (this.target.Position + Vector3.Up * 35f - this.host.Position).Normal * this.force * this.mass;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.host.ApplyAbsoluteImpulse(throwForce);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Sound.FromEntity("poltergeist.tele.throw", base.Entity);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Remove();
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00009404 File Offset: 0x00007604
		private void HoverObject()
		{
			Vector3 c = this.targetPos + Vector3.Random * 50f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Vector3 dif = c + Vector3.Up * MathF.Sin(Time.Now * 5f) * 5f - this.host.Position;
			float length = dif.Length;
			Vector3 dir = dif.Normal;
			float speed = (length / TelekinesisThrowComponent.HoverOffset.Length).Clamp(0.1f, 1f) * 250f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.host.Velocity = this.host.Velocity.LerpTo(dir * speed, Time.Delta * 5f);
			if (this.timeSinceStartedTelekinesis > this.holdTime)
			{
				this.DoThrow();
			}
		}

		// Token: 0x0600010A RID: 266 RVA: 0x000094EC File Offset: 0x000076EC
		[Event.Tick.ServerAttribute]
		private void Update()
		{
			if (this.timeSinceActivated > 5f)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				base.Remove();
				return;
			}
			if (!this.initialised)
			{
				return;
			}
			if (!this.target.IsValid() || this.target.LifeState == LifeState.Dead)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Release();
				RuntimeHelpers.EnsureSufficientExecutionStack();
				base.Remove();
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.HoverObject();
		}

		// Token: 0x0400005E RID: 94
		public const string TelekinesisTag = "BurerGrabbed";

		// Token: 0x0400005F RID: 95
		public const string TelekinesisGrabSound = "poltergeist.tele.grab";

		// Token: 0x04000060 RID: 96
		public const string TelekinesisThrowSound = "poltergeist.tele.throw";

		// Token: 0x04000061 RID: 97
		private float holdTime;

		// Token: 0x04000062 RID: 98
		private float force;

		// Token: 0x04000063 RID: 99
		private float mass;

		// Token: 0x04000064 RID: 100
		private Particles effects;

		// Token: 0x04000065 RID: 101
		private Entity target;

		// Token: 0x04000066 RID: 102
		private bool initialised;

		// Token: 0x04000067 RID: 103
		private Prop host;

		// Token: 0x04000068 RID: 104
		private Vector3 targetPos;

		// Token: 0x04000069 RID: 105
		private TimeSince timeSinceStartedTelekinesis;

		// Token: 0x0400006A RID: 106
		private TimeSince timeSinceActivated;
	}
}

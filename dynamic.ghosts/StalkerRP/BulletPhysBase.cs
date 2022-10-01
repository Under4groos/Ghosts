using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using Sandbox;
using Sandbox.Debug;
using Sandbox.Internal;
using StalkerRP.Debug;
using StalkerRP.Inventory;

namespace StalkerRP
{
	// Token: 0x02000051 RID: 81
	public class BulletPhysBase : Entity
	{
		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x0600033C RID: 828 RVA: 0x00012829 File Offset: 0x00010A29
		// (set) Token: 0x0600033B RID: 827 RVA: 0x00012801 File Offset: 0x00010A01
		[ConVar.ReplicatedAttribute(null)]
		public static float stalker_bullets_speed_mult
		{
			get
			{
				return BulletPhysBase._repback__stalker_bullets_speed_mult;
			}
			set
			{
				if (BulletPhysBase._repback__stalker_bullets_speed_mult == value)
				{
					return;
				}
				BulletPhysBase._repback__stalker_bullets_speed_mult = value;
				if (Host.IsServer)
				{
					ConsoleSystem.SetValue("stalker_bullets_speed_mult", value);
				}
			}
		}

		// Token: 0x0600033D RID: 829 RVA: 0x00012830 File Offset: 0x00010A30
		public override void Spawn()
		{
			float gravityRatio = 385.82697f / GlobalGameNamespace.Map.Physics.Gravity.Length;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.gravScale = gravityRatio;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Transmit = TransmitType.Never;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Tags.Add("phys_bullet_tag");
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Tags.Add("nocollide");
		}

		// Token: 0x0600033E RID: 830 RVA: 0x000128A0 File Offset: 0x00010AA0
		public void Fire(Vector3 position, Vector3 direction, float speed, Entity owner, Entity _weapon, AmmoItemResource _ammo, float force, float _damage, bool _doServerImpactEffect = false)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Position = position;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Owner = owner;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Velocity = direction * speed * BulletPhysBase.stalker_bullets_speed_mult + this.Owner.Velocity * 0.7f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.lastPosition = position;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.lastPosForDebugDraw = position;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.particlePos = position;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.weapon = _weapon;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.ammo = _ammo;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.damage = _damage;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.extraForce = force;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.doServerImpactEffect = _doServerImpactEffect;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.IsRetired = false;
			if (DebugCommands.stalker_debug_bullets_path)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.startPos = this.Position;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				Draw.ForSeconds(10f).Line(position, position + direction * 10000f);
			}
			if (base.IsClientOnly)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.CreateBulletFX();
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.RetireAfter(this.liveTime);
		}

		// Token: 0x0600033F RID: 831 RVA: 0x000129D2 File Offset: 0x00010BD2
		private void CreateBulletFX()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Particles particles = this.bulletTracer;
			if (particles == null)
			{
				return;
			}
			particles.Destroy(true);
		}

		// Token: 0x06000340 RID: 832 RVA: 0x000129EC File Offset: 0x00010BEC
		[Event.FrameAttribute]
		private void FrameUpdate()
		{
			if (this.IsRetired)
			{
				return;
			}
			if (DebugCommands.stalker_debug_bullets_path)
			{
				if (this.lastPosForDebugDraw.Distance(this.Position) > 800f)
				{
					return;
				}
				RuntimeHelpers.EnsureSufficientExecutionStack();
				Draw.ForSeconds(10f).Line(this.lastPosForDebugDraw, this.Position);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.lastPosForDebugDraw = this.Position;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.particlePos = this.particlePos.LerpTo(this.Position, Time.Delta * 80f);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Particles particles = this.bulletTracer;
			if (particles == null)
			{
				return;
			}
			particles.SetPosition(1, this.Velocity);
		}

		// Token: 0x06000341 RID: 833 RVA: 0x00012A9C File Offset: 0x00010C9C
		[Event.Tick.ServerAttribute]
		private void BulletPhysics()
		{
			if (this.IsRetired)
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.lastPosition = this.Position;
			float dt = Time.Delta;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Velocity += GlobalGameNamespace.Map.Physics.Gravity * this.gravScale * dt;
			float drag = 2.007E-05f * MathF.Pow(this.Velocity.Length, 2f) * this.ammo.DragCoefficient * this.ammo.CrossSectionalArea;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			drag /= 0.02f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Velocity -= this.Velocity.Normal * drag * dt;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Position += this.Velocity * dt;
			if (DebugCommands.stalker_debug_bullets_path)
			{
				Draw.ForSeconds(10f).Line(this.lastPosition, this.Position);
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.DoTraceCheck();
		}

		// Token: 0x06000342 RID: 834 RVA: 0x00012BC8 File Offset: 0x00010DC8
		private void BulletHit(TraceResult tr)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			tr.Surface.DoBulletImpact(tr);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Particles particles = this.bulletTracer;
			if (particles != null)
			{
				particles.Destroy(true);
			}
			if (base.IsClient)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				BulletPhysBase.Pool.Retire(this);
				return;
			}
			if (DebugCommands.stalker_debug_bullets_path)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				Draw.ForSeconds(10f).Circle(this.lastPosition, this.Velocity.Normal, 10f, 32, 360f);
				if (base.IsServer)
				{
					float distance = this.startPos.Distance(this.Position) / 39.3701f;
					RuntimeHelpers.EnsureSufficientExecutionStack();
					GlobalGameNamespace.DebugOverlay.ScreenText(distance.ToString(CultureInfo.InvariantCulture), Rand.Int(20), 5f);
				}
			}
			if (!base.IsServer)
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			BulletPhysBase.Pool.Retire(this);
			if (this.doServerImpactEffect)
			{
				tr.Surface.DoBulletImpact(tr);
			}
			if (!tr.Entity.IsValid())
			{
				return;
			}
			float force = this.extraForce;
			DamageInfo damageInfo2 = default(DamageInfo);
			damageInfo2 = damageInfo2.WithPosition(tr.EndPosition);
			damageInfo2 = damageInfo2.WithForce(tr.Direction * force);
			damageInfo2 = damageInfo2.WithFlag(DamageFlags.Bullet);
			damageInfo2 = damageInfo2.UsingTraceResult(tr);
			damageInfo2 = damageInfo2.WithAttacker(this.Owner, null);
			DamageInfo damageInfo = damageInfo2.WithWeapon(this.weapon);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			damageInfo.Damage = this.damage;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			tr.Entity.TakeDamage(damageInfo);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.TryPenetration(tr);
		}

		// Token: 0x06000343 RID: 835 RVA: 0x00012D70 File Offset: 0x00010F70
		private void TryPenetration(TraceResult tr)
		{
			Vector3 vector = tr.HitPosition + tr.Direction * this.ammo.Penetration;
			Vector3 vector2 = tr.HitPosition + tr.Direction * 250f;
			Trace trace = Trace.Ray(vector, vector2).UseHitboxes(true);
			Entity owner = this.Owner;
			bool flag = true;
			trace = trace.Ignore(owner, flag).WithoutTags(new string[]
			{
				"phys_bullet_tag"
			});
			Vector3 vector3 = 0f;
			TraceResult penTrace = trace.Size(vector3).WithAnyTags(new string[]
			{
				"player",
				"entity",
				"npc",
				"weapon",
				"solid"
			}).Run();
			if (DebugCommands.stalker_debug_bullets_path)
			{
				GlobalGameNamespace.DebugOverlay.TraceResult(penTrace, 10f);
			}
			if (!penTrace.Entity.IsValid())
			{
				return;
			}
			if (penTrace.Entity == tr.Entity && !penTrace.Entity.IsWorld && tr.HitboxIndex == penTrace.HitboxIndex)
			{
				return;
			}
			float force = this.extraForce;
			DamageInfo damageInfo2 = default(DamageInfo);
			damageInfo2 = damageInfo2.WithPosition(penTrace.EndPosition);
			damageInfo2 = damageInfo2.WithForce(penTrace.Direction * force);
			damageInfo2 = damageInfo2.WithFlag(DamageFlags.Bullet);
			damageInfo2 = damageInfo2.UsingTraceResult(penTrace);
			damageInfo2 = damageInfo2.WithAttacker(this.Owner, null);
			DamageInfo damageInfo = damageInfo2.WithWeapon(this.weapon);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			damageInfo.Damage = this.damage;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			penTrace.Entity.TakeDamage(damageInfo);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			penTrace.Surface.DoBulletImpact(penTrace);
		}

		// Token: 0x06000344 RID: 836 RVA: 0x00012F40 File Offset: 0x00011140
		private void DoTraceCheck()
		{
			Vector3 vector = this.Position;
			Trace trace = Trace.Ray(this.lastPosition, vector).UseHitboxes(true).EntitiesOnly();
			Vector3 vector2 = 200f;
			trace = trace.Size(vector2).WithTag("player");
			Entity owner = this.Owner;
			bool flag = true;
			TraceResult[] whizTraces = trace.Ignore(owner, flag).RunAll();
			if (whizTraces != null)
			{
				foreach (TraceResult traceResult in whizTraces)
				{
					StalkerPlayer player = traceResult.Entity as StalkerPlayer;
					if (player != null)
					{
						BulletWhizzComponent bulletWhizzComponent = player.BulletWhizzComponent;
						Vector3 endPosition = traceResult.EndPosition;
						vector = this.Velocity;
						bulletWhizzComponent.Trigger(endPosition, vector.Length, this.ammo.FlyBySound);
					}
				}
			}
			vector = this.Position;
			trace = Trace.Ray(this.lastPosition, vector).UseHitboxes(true);
			owner = this.Owner;
			flag = true;
			trace = trace.Ignore(owner, flag).WithoutTags(new string[]
			{
				"phys_bullet_tag"
			});
			vector2 = 0f;
			TraceResult tr = trace.Size(vector2).WithAnyTags(new string[]
			{
				"player",
				"entity",
				"npc",
				"weapon",
				"solid",
				"glass"
			}).Run();
			if (tr.Hit)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.BulletHit(tr);
			}
		}

		// Token: 0x06000345 RID: 837 RVA: 0x000130D4 File Offset: 0x000112D4
		private void RetireAfter(float seconds)
		{
			BulletPhysBase.<RetireAfter>d__30 <RetireAfter>d__;
			<RetireAfter>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<RetireAfter>d__.<>4__this = this;
			<RetireAfter>d__.seconds = seconds;
			<RetireAfter>d__.<>1__state = -1;
			<RetireAfter>d__.<>t__builder.Start<BulletPhysBase.<RetireAfter>d__30>(ref <RetireAfter>d__);
		}

		// Token: 0x04000102 RID: 258
		public const string BULLET_TAG = "phys_bullet_tag";

		// Token: 0x04000103 RID: 259
		public const float InchesPerMeter = 39.3701f;

		// Token: 0x04000104 RID: 260
		public const float AirDensity = 2.007E-05f;

		// Token: 0x04000105 RID: 261
		private const float targetGravity = 385.82697f;

		// Token: 0x04000106 RID: 262
		public static readonly BulletPool Pool = new BulletPool();

		// Token: 0x04000107 RID: 263
		public bool IsRetired;

		// Token: 0x04000108 RID: 264
		private readonly Vector3 hullSize = 0.1f;

		// Token: 0x04000109 RID: 265
		private float gravScale = 1f;

		// Token: 0x0400010A RID: 266
		private float damage;

		// Token: 0x0400010B RID: 267
		private float extraForce;

		// Token: 0x0400010C RID: 268
		private float liveTime = 5f;

		// Token: 0x0400010D RID: 269
		private Entity weapon;

		// Token: 0x0400010E RID: 270
		private AmmoItemResource ammo;

		// Token: 0x0400010F RID: 271
		private Particles bulletTracer;

		// Token: 0x04000110 RID: 272
		private Vector3 particlePos;

		// Token: 0x04000111 RID: 273
		private Vector3 startPos;

		// Token: 0x04000112 RID: 274
		private Vector3 lastPosition;

		// Token: 0x04000113 RID: 275
		private bool doServerImpactEffect;

		// Token: 0x04000114 RID: 276
		private Vector3 lastPosForDebugDraw;

		// Token: 0x04000115 RID: 277
		public static float _repback__stalker_bullets_speed_mult = 1f;
	}
}

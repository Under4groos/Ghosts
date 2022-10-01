using System;
using System.Runtime.CompilerServices;
using Sandbox;
using Sandbox.Debug;

namespace StalkerRP.NPC
{
	// Token: 0x02000087 RID: 135
	public class BurerPsyCrushState : NPCState<BurerNPC>
	{
		// Token: 0x1700019D RID: 413
		// (get) Token: 0x06000614 RID: 1556 RVA: 0x0001C454 File Offset: 0x0001A654
		private float Cooldown
		{
			get
			{
				return 5f;
			}
		}

		// Token: 0x06000615 RID: 1557 RVA: 0x0001C45B File Offset: 0x0001A65B
		public BurerPsyCrushState(BurerNPC host) : base(host)
		{
		}

		// Token: 0x06000616 RID: 1558 RVA: 0x0001C47C File Offset: 0x0001A67C
		public override void OnStateEntered()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter("bHasPath", false);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter("eAttackType", 1);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter("ePsychicType", 1);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter("bAttacking", true);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.Steer = null;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.PsyTarget = this.Host.Target;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.BeginHover();
		}

		// Token: 0x06000617 RID: 1559 RVA: 0x0001C513 File Offset: 0x0001A713
		public override void OnStateExited()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SetAnimParameter("bAttacking", false);
		}

		// Token: 0x06000618 RID: 1560 RVA: 0x0001C52C File Offset: 0x0001A72C
		public override void Update(float deltaTime)
		{
			if (this.PsyTarget.IsValid())
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Host.FaceTarget();
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.HoverTarget();
				return;
			}
			if (this.Host.TargetingComponent.ValidateTarget())
			{
				this.Host.SM.SetState(this.Host.ChaseState);
				return;
			}
			this.Host.SM.SetState(this.Host.IdleState);
		}

		// Token: 0x06000619 RID: 1561 RVA: 0x0001C5AB File Offset: 0x0001A7AB
		public override void OnAnimAttackEvent(int intData, float floatDta, Vector3 vectorData, string stringData)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.CrushTarget();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Host.SM.SetState(this.Host.IdleState);
		}

		// Token: 0x0600061A RID: 1562 RVA: 0x0001C5D8 File Offset: 0x0001A7D8
		public override void OnDestroyed()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.DropTarget();
		}

		// Token: 0x0600061B RID: 1563 RVA: 0x0001C5E5 File Offset: 0x0001A7E5
		public override void OnKilled()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.DropTarget();
		}

		// Token: 0x0600061C RID: 1564 RVA: 0x0001C5F4 File Offset: 0x0001A7F4
		public bool CanDoCrushAttack()
		{
			if (!base.Target.IsValid())
			{
				return false;
			}
			if (this.TimeSinceExited <= this.Cooldown)
			{
				return false;
			}
			StalkerPlayer player = base.Target as StalkerPlayer;
			if (player != null)
			{
				if (player.HealthComponent.GetEffectiveHealthFraction() > 0.3f)
				{
					return false;
				}
			}
			else if (base.Target.Health >= 40f)
			{
				return false;
			}
			return this.Host.TargetingComponent.DistanceToTarget <= this.Host.TelekinesisCrushRange;
		}

		// Token: 0x0600061D RID: 1565 RVA: 0x0001C67C File Offset: 0x0001A87C
		private void DropTarget()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.PsyTarget.Tags.Remove(this.PsyCrushTag);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Particles hoverParticles = this.HoverParticles;
			if (hoverParticles == null)
			{
				return;
			}
			hoverParticles.Destroy(false);
		}

		// Token: 0x0600061E RID: 1566 RVA: 0x0001C6B0 File Offset: 0x0001A8B0
		private void BeginHover()
		{
			if (base.Target is IPsyIllusion)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				base.Target.OnKilled();
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Host.SM.SetState(this.Host.IdleState);
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.PsyTarget.GroundEntity = null;
			Vector3 position = this.PsyTarget.Position;
			Vector3 vector = this.PsyTarget.Position + Vector3.Up * this.HoverHeight;
			TraceResult tr = Trace.Ray(position, vector).WorldOnly().Run();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.TargetPoint = tr.EndPosition;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			position = this.PsyTarget.Position;
			this.InitialDistance = position.Distance(this.TargetPoint);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.PsyTarget.Position += Vector3.Up * 10f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.PsyTarget.Tags.Add(this.PsyCrushTag);
			NPCBase npc = this.PsyTarget as NPCBase;
			if (npc != null)
			{
				DeathRagdoll newTarg = npc.CreateDeathRagdollAndRemove();
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.PsyTarget = newTarg;
				for (int i = 0; i < newTarg.PhysicsGroup.BodyCount; i++)
				{
					newTarg.PhysicsGroup.GetBody(i).GravityEnabled = false;
				}
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.HoverParticles = Particles.Create("particles/stalker/burer_tele_hold.vpcf", this.PsyTarget, null, true);
		}

		// Token: 0x0600061F RID: 1567 RVA: 0x0001C840 File Offset: 0x0001AA40
		private void HoverTarget()
		{
			Vector3 dif = this.TargetPoint + Vector3.Up * MathF.Sin(Time.Now * 5f) * 8f - this.PsyTarget.Position;
			float length = dif.Length;
			Vector3 dir = dif.Normal;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Draw.Once.Arrow(this.PsyTarget.Position, this.TargetPoint, Vector3.Up, 8f);
			float speed = (length / this.InitialDistance).Clamp(0.02f, 1f) * 1500f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.PsyTarget.Velocity = this.PsyTarget.Velocity.LerpTo(dir * speed, Time.Delta * 5f);
			DeathRagdoll rag = this.PsyTarget as DeathRagdoll;
			if (rag != null)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				rag.ApplyLocalAngularImpulse(Vector3.Random * 350f);
			}
		}

		// Token: 0x06000620 RID: 1568 RVA: 0x0001C948 File Offset: 0x0001AB48
		private void CrushTarget()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Sound.FromWorld("burer.wave", this.PsyTarget.Position);
			PhysicsGroup physicsGroup = this.PsyTarget.PhysicsGroup;
			Vector3 pos = (physicsGroup != null) ? physicsGroup.Pos : this.PsyTarget.Position;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.PsyTarget.Tags.Remove(this.PsyCrushTag);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Particles.Create("particles/stalker/grav_crush.vpcf", pos);
			Vector3 force = this.Host.Rotation.Forward * 2500f + Vector3.Up * 250f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.PsyTarget.ApplyAbsoluteImpulse(force);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.PsyTarget.OnKilled();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Particles hoverParticles = this.HoverParticles;
			if (hoverParticles != null)
			{
				hoverParticles.Destroy(false);
			}
			DeathRagdoll rag = this.PsyTarget as DeathRagdoll;
			if (rag != null)
			{
				for (int i = 0; i < rag.PhysicsGroup.BodyCount; i++)
				{
					rag.PhysicsGroup.GetBody(i).GravityEnabled = true;
				}
			}
		}

		// Token: 0x04000225 RID: 549
		private float HoverHeight = 100f;

		// Token: 0x04000226 RID: 550
		private float InitialDistance;

		// Token: 0x04000227 RID: 551
		private Vector3 TargetPoint;

		// Token: 0x04000228 RID: 552
		private string PsyCrushTag = "Burer.PsyCrush";

		// Token: 0x04000229 RID: 553
		private Entity PsyTarget;

		// Token: 0x0400022A RID: 554
		private Particles HoverParticles;
	}
}

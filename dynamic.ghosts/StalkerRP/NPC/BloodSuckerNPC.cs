using System;
using System.Runtime.CompilerServices;
using Sandbox;
using SandboxEditor;
using StalkerRP.Debug;

namespace StalkerRP.NPC
{
	// Token: 0x02000077 RID: 119
	[DebugSpawnable(Name = "Bloodsucker", Category = "Mutants")]
	[Title("Bloodsucker")]
	[HammerEntity]
	[Category("Mutants")]
	[EditorModel("models/stalker/monsters/bloodsucker/bloodsucker.vmdl", "white", "white")]
	[Spawnable]
	[NPC]
	public class BloodSuckerNPC : NPCBase
	{
		// Token: 0x1700015F RID: 351
		// (get) Token: 0x06000567 RID: 1383 RVA: 0x00019A21 File Offset: 0x00017C21
		public override Capsule PhysicsCapsule
		{
			get
			{
				return Capsule.FromHeightAndRadius(84f, 15f);
			}
		}

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x06000568 RID: 1384 RVA: 0x00019A32 File Offset: 0x00017C32
		public float SearchRadius
		{
			get
			{
				return 500f;
			}
		}

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x06000569 RID: 1385 RVA: 0x00019A39 File Offset: 0x00017C39
		public virtual float AttackDamage
		{
			get
			{
				return 50f;
			}
		}

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x0600056A RID: 1386 RVA: 0x00019A40 File Offset: 0x00017C40
		public virtual float UncloakRange
		{
			get
			{
				return 500f;
			}
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x0600056B RID: 1387 RVA: 0x00019A47 File Offset: 0x00017C47
		public virtual float VisibilityMinAlpha
		{
			get
			{
				return 0.12f;
			}
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x0600056C RID: 1388 RVA: 0x00019A4E File Offset: 0x00017C4E
		public virtual bool EmitAggroSoundOnLeavingIdle
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x0600056D RID: 1389 RVA: 0x00019A51 File Offset: 0x00017C51
		protected virtual string AggroSound
		{
			get
			{
				return "bloodsucker.aggro";
			}
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x0600056E RID: 1390 RVA: 0x00019A58 File Offset: 0x00017C58
		protected virtual string CloakStartSound
		{
			get
			{
				return "bloodsucker.cloak.start";
			}
		}

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x0600056F RID: 1391 RVA: 0x00019A5F File Offset: 0x00017C5F
		protected virtual string CloakEndSound
		{
			get
			{
				return "bloodsucker.cloak.end";
			}
		}

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x06000570 RID: 1392 RVA: 0x00019A66 File Offset: 0x00017C66
		protected virtual string GrowlSound
		{
			get
			{
				return "bloodsucker.growl";
			}
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x06000571 RID: 1393 RVA: 0x00019A6D File Offset: 0x00017C6D
		protected override float FootStepVolume
		{
			get
			{
				return 0.2f;
			}
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x06000572 RID: 1394 RVA: 0x00019A74 File Offset: 0x00017C74
		protected override string NPCAssetID
		{
			get
			{
				return "bloodsucker";
			}
		}

		// Token: 0x06000573 RID: 1395 RVA: 0x00019A7C File Offset: 0x00017C7C
		protected override void PostSpawn()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Speed = base.Resource.RunSpeed;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Movement = new NPCMovementSlowTurn(this);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.IdleState = new BloodSuckerIdleState(this);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.CircleState = new BloodSuckerCircleState(this);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.AttackState = new BloodSuckerAttackState(this);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.TauntState = new BloodSuckerTauntState(this);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Scale = 0.85f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.SM.SetState(this.IdleState);
		}

		// Token: 0x06000574 RID: 1396 RVA: 0x00019B1A File Offset: 0x00017D1A
		public override void SetToIdleState()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.SM.SetState(this.IdleState);
		}

		// Token: 0x06000575 RID: 1397 RVA: 0x00019B32 File Offset: 0x00017D32
		public void BeginFade()
		{
			if (this.IsFading)
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.IsFading = true;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.timeSinceFadeStarted = 0f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.ClientBeginFade();
		}

		// Token: 0x06000576 RID: 1398 RVA: 0x00019B6C File Offset: 0x00017D6C
		[ClientRpc]
		private void ClientBeginFade()
		{
			if (!this.ClientBeginFade__RpcProxy(null))
			{
				return;
			}
			if (this.IsFading)
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.IsFading = true;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.timeSinceFadeStarted = 0f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Sound.FromEntity(this.CloakStartSound, this);
		}

		// Token: 0x06000577 RID: 1399 RVA: 0x00019BC7 File Offset: 0x00017DC7
		public void EndFade()
		{
			if (!this.IsFading)
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.IsFading = false;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.timeSinceFadeEnded = 0f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.ClientEndFade();
		}

		// Token: 0x06000578 RID: 1400 RVA: 0x00019C00 File Offset: 0x00017E00
		[ClientRpc]
		public void ClientEndFade()
		{
			if (!this.ClientEndFade__RpcProxy(null))
			{
				return;
			}
			if (!this.IsFading)
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.IsFading = false;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.timeSinceFadeEnded = 0f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Sound.FromEntity(this.CloakEndSound, this);
		}

		// Token: 0x06000579 RID: 1401 RVA: 0x00019C5C File Offset: 0x00017E5C
		[ClientRpc]
		public void EmitAggroSound()
		{
			if (!this.EmitAggroSound__RpcProxy(null))
			{
				return;
			}
			if (!string.IsNullOrEmpty(this.AggroSound))
			{
				Sound.FromEntity(this.AggroSound, this);
			}
		}

		// Token: 0x0600057A RID: 1402 RVA: 0x00019C98 File Offset: 0x00017E98
		[ClientRpc]
		public void EmitGrowlSound()
		{
			if (!this.EmitGrowlSound__RpcProxy(null))
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Sound.FromEntity(this.GrowlSound, this);
		}

		// Token: 0x0600057B RID: 1403 RVA: 0x00019CCC File Offset: 0x00017ECC
		[Event.FrameAttribute]
		private void ClientVisibility()
		{
			if (this.IsFading)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				base.RenderColor = base.RenderColor.WithAlpha(1f - (1f - this.VisibilityMinAlpha) * (this.timeSinceFadeStarted / this.timeUntilFullFade).Clamp(0f, 1f));
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.RenderColor = base.RenderColor.WithAlpha(this.VisibilityMinAlpha + 1f * (this.timeSinceFadeEnded / this.timeUntilFullFade));
		}

		// Token: 0x0600057C RID: 1404 RVA: 0x00019D68 File Offset: 0x00017F68
		protected bool ClientBeginFade__RpcProxy(To? toTarget = null)
		{
			if (!Prediction.FirstTime)
			{
				return false;
			}
			if (!Host.IsServer)
			{
				Prediction.Watch("ClientBeginFade", Array.Empty<object>());
				return true;
			}
			using (NetWrite writer = NetWrite.StartRpc(704038658, this))
			{
				writer.SendRpc(toTarget, this);
			}
			return false;
		}

		// Token: 0x0600057D RID: 1405 RVA: 0x00019DC8 File Offset: 0x00017FC8
		protected bool ClientEndFade__RpcProxy(To? toTarget = null)
		{
			if (!Prediction.FirstTime)
			{
				return false;
			}
			if (!Host.IsServer)
			{
				Prediction.Watch("ClientEndFade", Array.Empty<object>());
				return true;
			}
			using (NetWrite writer = NetWrite.StartRpc(1792130778, this))
			{
				writer.SendRpc(toTarget, this);
			}
			return false;
		}

		// Token: 0x0600057E RID: 1406 RVA: 0x00019E28 File Offset: 0x00018028
		protected bool EmitAggroSound__RpcProxy(To? toTarget = null)
		{
			if (!Prediction.FirstTime)
			{
				return false;
			}
			if (!Host.IsServer)
			{
				Prediction.Watch("EmitAggroSound", Array.Empty<object>());
				return true;
			}
			using (NetWrite writer = NetWrite.StartRpc(882073424, this))
			{
				writer.SendRpc(toTarget, this);
			}
			return false;
		}

		// Token: 0x0600057F RID: 1407 RVA: 0x00019E88 File Offset: 0x00018088
		protected bool EmitGrowlSound__RpcProxy(To? toTarget = null)
		{
			if (!Prediction.FirstTime)
			{
				return false;
			}
			if (!Host.IsServer)
			{
				Prediction.Watch("EmitGrowlSound", Array.Empty<object>());
				return true;
			}
			using (NetWrite writer = NetWrite.StartRpc(-1491797203, this))
			{
				writer.SendRpc(toTarget, this);
			}
			return false;
		}

		// Token: 0x06000580 RID: 1408 RVA: 0x00019EE8 File Offset: 0x000180E8
		private void ClientBeginFade(To toTarget)
		{
			this.ClientBeginFade__RpcProxy(new To?(toTarget));
		}

		// Token: 0x06000581 RID: 1409 RVA: 0x00019EF7 File Offset: 0x000180F7
		public void ClientEndFade(To toTarget)
		{
			this.ClientEndFade__RpcProxy(new To?(toTarget));
		}

		// Token: 0x06000582 RID: 1410 RVA: 0x00019F06 File Offset: 0x00018106
		public void EmitAggroSound(To toTarget)
		{
			this.EmitAggroSound__RpcProxy(new To?(toTarget));
		}

		// Token: 0x06000583 RID: 1411 RVA: 0x00019F15 File Offset: 0x00018115
		public void EmitGrowlSound(To toTarget)
		{
			this.EmitGrowlSound__RpcProxy(new To?(toTarget));
		}

		// Token: 0x06000584 RID: 1412 RVA: 0x00019F24 File Offset: 0x00018124
		protected override void OnCallRemoteProcedure(int id, NetRead read)
		{
			if (id <= 704038658)
			{
				if (id == -1491797203)
				{
					if (!Prediction.WasPredicted("EmitGrowlSound", Array.Empty<object>()))
					{
						this.EmitGrowlSound();
					}
					return;
				}
				if (id == 704038658)
				{
					if (!Prediction.WasPredicted("ClientBeginFade", Array.Empty<object>()))
					{
						this.ClientBeginFade();
					}
					return;
				}
			}
			else
			{
				if (id == 882073424)
				{
					if (!Prediction.WasPredicted("EmitAggroSound", Array.Empty<object>()))
					{
						this.EmitAggroSound();
					}
					return;
				}
				if (id == 1792130778)
				{
					if (!Prediction.WasPredicted("ClientEndFade", Array.Empty<object>()))
					{
						this.ClientEndFade();
					}
					return;
				}
			}
			base.OnCallRemoteProcedure(id, read);
		}

		// Token: 0x040001F2 RID: 498
		public BloodSuckerIdleState IdleState;

		// Token: 0x040001F3 RID: 499
		public BloodSuckerCircleState CircleState;

		// Token: 0x040001F4 RID: 500
		public BloodSuckerAttackState AttackState;

		// Token: 0x040001F5 RID: 501
		public BloodSuckerTauntState TauntState;

		// Token: 0x040001F6 RID: 502
		public bool IsFading;

		// Token: 0x040001F7 RID: 503
		private TimeSince timeSinceFadeStarted = 0f;

		// Token: 0x040001F8 RID: 504
		private TimeSince timeSinceFadeEnded = 0f;

		// Token: 0x040001F9 RID: 505
		private readonly float timeUntilFullFade = 0.5f;
	}
}

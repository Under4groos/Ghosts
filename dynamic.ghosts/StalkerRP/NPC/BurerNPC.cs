using System;
using System.Runtime.CompilerServices;
using Sandbox;
using Sandbox.Internal;
using SandboxEditor;
using StalkerRP.Debug;
using StalkerRP.PostProcessing;

namespace StalkerRP.NPC
{
	// Token: 0x02000086 RID: 134
	[DebugSpawnable(Name = "Burer", Category = "Mutants")]
	[Title("Burer")]
	[HammerEntity]
	[Category("Mutants")]
	[EditorModel("models/stalker/monsters/burer/burer.vmdl", "white", "white")]
	[Spawnable]
	[NPC]
	public class BurerNPC : NPCBase, IPsyCreature
	{
		// Token: 0x1700018E RID: 398
		// (get) Token: 0x060005FC RID: 1532 RVA: 0x0001C0B7 File Offset: 0x0001A2B7
		public float TelekinesisSearchRange
		{
			get
			{
				return 1200f;
			}
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x060005FD RID: 1533 RVA: 0x0001C0BE File Offset: 0x0001A2BE
		public float TelekinesisMassLimit
		{
			get
			{
				return 8000f;
			}
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x060005FE RID: 1534 RVA: 0x0001C0C5 File Offset: 0x0001A2C5
		public float TelekinesisThrowForce
		{
			get
			{
				return 200f;
			}
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x060005FF RID: 1535 RVA: 0x0001C0CC File Offset: 0x0001A2CC
		public int TelekinesisMaxObjects
		{
			get
			{
				return 5;
			}
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x06000600 RID: 1536 RVA: 0x0001C0CF File Offset: 0x0001A2CF
		public float TelekinesisCrushDamage
		{
			get
			{
				return 60f;
			}
		}

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x06000601 RID: 1537 RVA: 0x0001C0D6 File Offset: 0x0001A2D6
		public float TelekinesisCrushRange
		{
			get
			{
				return 700f;
			}
		}

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x06000602 RID: 1538 RVA: 0x0001C0DD File Offset: 0x0001A2DD
		public float TelekinesisPushDamage
		{
			get
			{
				return 50f;
			}
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x06000603 RID: 1539 RVA: 0x0001C0E4 File Offset: 0x0001A2E4
		public float TelekinesisPushRange
		{
			get
			{
				return 1400f;
			}
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x06000604 RID: 1540 RVA: 0x0001C0EB File Offset: 0x0001A2EB
		public float TelekinesisPushSpeed
		{
			get
			{
				return 900f;
			}
		}

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x06000605 RID: 1541 RVA: 0x0001C0F2 File Offset: 0x0001A2F2
		public string TelekinesisTag
		{
			get
			{
				return "BurerGrabbed";
			}
		}

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x06000606 RID: 1542 RVA: 0x0001C0F9 File Offset: 0x0001A2F9
		private float PsyOverlayAlphaMax
		{
			get
			{
				return 0.13f;
			}
		}

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x06000607 RID: 1543 RVA: 0x0001C100 File Offset: 0x0001A300
		private float PsyOverlayDistanceMax
		{
			get
			{
				return 1600f;
			}
		}

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x06000608 RID: 1544 RVA: 0x0001C107 File Offset: 0x0001A307
		protected override float PainSoundDelay
		{
			get
			{
				return 1.5f;
			}
		}

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x06000609 RID: 1545 RVA: 0x0001C10E File Offset: 0x0001A30E
		public override Capsule PhysicsCapsule
		{
			get
			{
				return Capsule.FromHeightAndRadius(48f, 28f);
			}
		}

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x0600060A RID: 1546 RVA: 0x0001C11F File Offset: 0x0001A31F
		protected override string NPCAssetID
		{
			get
			{
				return "burer";
			}
		}

		// Token: 0x0600060B RID: 1547 RVA: 0x0001C128 File Offset: 0x0001A328
		protected override void PostSpawn()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Speed = base.Resource.CrippledSpeed;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.EyeLocalPosition = Vector3.Up * 45f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.IdleState = new BurerIdleState(this);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.ChaseState = new BurerChaseState(this);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.MeleeState = new BurerMeleeState(this);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.TelekinesisState = new BurerTelekinesisState(this);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.ShieldState = new BurerShieldState(this);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.PsyCrushState = new BurerPsyCrushState(this);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.PsyWaveState = new BurerPsyWaveState(this);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.FleeState = new BurerFleeState(this);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.SM.SetState(this.IdleState);
		}

		// Token: 0x0600060C RID: 1548 RVA: 0x0001C203 File Offset: 0x0001A403
		public override void SetToIdleState()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.SM.SetState(this.IdleState);
		}

		// Token: 0x0600060D RID: 1549 RVA: 0x0001C21C File Offset: 0x0001A41C
		public override void TakeDamage(DamageInfo info)
		{
			if (info.HitboxIndex == 18)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				info.Damage = 0f;
				Vector3 pos = info.Position;
				Vector3 dir = (info.Attacker.Position - info.Position).Normal;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.DoShieldEffect(pos, dir);
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.TakeDamage(info);
		}

		// Token: 0x0600060E RID: 1550 RVA: 0x0001C287 File Offset: 0x0001A487
		public override void ClientSpawn()
		{
			PostProcessEmitterComponent postProcessEmitterComponent = this.Components.Create<PostProcessEmitterComponent>(true);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			postProcessEmitterComponent.SetUpEmitter(EffectsPostProcessManager.STALKER_PSY_BURER_COLOR, 4000f, 1.4f, "psy_aura_loop", 0.05f, 0f);
		}

		// Token: 0x0600060F RID: 1551 RVA: 0x0001C2C0 File Offset: 0x0001A4C0
		[ClientRpc]
		private void DoShieldEffect(Vector3 position, Vector3 normal)
		{
			BurerNPC.<DoShieldEffect>d__42 <DoShieldEffect>d__;
			<DoShieldEffect>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<DoShieldEffect>d__.<>4__this = this;
			<DoShieldEffect>d__.position = position;
			<DoShieldEffect>d__.normal = normal;
			<DoShieldEffect>d__.<>1__state = -1;
			<DoShieldEffect>d__.<>t__builder.Start<BurerNPC.<DoShieldEffect>d__42>(ref <DoShieldEffect>d__);
		}

		// Token: 0x06000610 RID: 1552 RVA: 0x0001C308 File Offset: 0x0001A508
		protected bool DoShieldEffect__RpcProxy(Vector3 position, Vector3 normal, To? toTarget = null)
		{
			if (!Prediction.FirstTime)
			{
				return false;
			}
			if (!Host.IsServer)
			{
				Prediction.Watch("DoShieldEffect", new object[]
				{
					position,
					normal
				});
				return true;
			}
			using (NetWrite writer = NetWrite.StartRpc(-1123607035, this))
			{
				if (!NetRead.IsSupported(position))
				{
					GlobalGameNamespace.Log.Error("[ClientRpc] DoShieldEffect is not allowed to use Vector3 for the parameter 'position'!");
					return false;
				}
				writer.Write<Vector3>(position);
				if (!NetRead.IsSupported(normal))
				{
					GlobalGameNamespace.Log.Error("[ClientRpc] DoShieldEffect is not allowed to use Vector3 for the parameter 'normal'!");
					return false;
				}
				writer.Write<Vector3>(normal);
				writer.SendRpc(toTarget, this);
			}
			return false;
		}

		// Token: 0x06000611 RID: 1553 RVA: 0x0001C3CC File Offset: 0x0001A5CC
		private void DoShieldEffect(To toTarget, Vector3 position, Vector3 normal)
		{
			this.DoShieldEffect__RpcProxy(position, normal, new To?(toTarget));
		}

		// Token: 0x06000612 RID: 1554 RVA: 0x0001C3E0 File Offset: 0x0001A5E0
		protected override void OnCallRemoteProcedure(int id, NetRead read)
		{
			if (id == -1123607035)
			{
				Vector3 __position = read.ReadData<Vector3>(default(Vector3));
				Vector3 __normal = read.ReadData<Vector3>(default(Vector3));
				if (!Prediction.WasPredicted("DoShieldEffect", new object[]
				{
					__position,
					__normal
				}))
				{
					this.DoShieldEffect(__position, __normal);
				}
				return;
			}
			base.OnCallRemoteProcedure(id, read);
		}

		// Token: 0x0400021D RID: 541
		public BurerIdleState IdleState;

		// Token: 0x0400021E RID: 542
		public BurerChaseState ChaseState;

		// Token: 0x0400021F RID: 543
		public BurerMeleeState MeleeState;

		// Token: 0x04000220 RID: 544
		public BurerTelekinesisState TelekinesisState;

		// Token: 0x04000221 RID: 545
		public BurerShieldState ShieldState;

		// Token: 0x04000222 RID: 546
		public BurerPsyCrushState PsyCrushState;

		// Token: 0x04000223 RID: 547
		public BurerPsyWaveState PsyWaveState;

		// Token: 0x04000224 RID: 548
		public BurerFleeState FleeState;
	}
}

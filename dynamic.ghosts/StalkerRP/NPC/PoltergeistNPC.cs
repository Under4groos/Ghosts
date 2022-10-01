using System;
using System.Runtime.CompilerServices;
using Sandbox;
using SandboxEditor;
using StalkerRP.Debug;
using StalkerRP.PostProcessing;

namespace StalkerRP.NPC
{
	// Token: 0x020000B7 RID: 183
	[DebugSpawnable(Name = "Poltergeist", Category = "Mutants")]
	[Title("Poltergeist")]
	[HammerEntity]
	[Category("Mutants")]
	[EditorModel("models/stalker/monsters/poltergeist/poltergeist.vmdl", "white", "white")]
	[Spawnable]
	public class PoltergeistNPC : NPCBase, IPsyCreature
	{
		// Token: 0x1700022A RID: 554
		// (get) Token: 0x060007E6 RID: 2022 RVA: 0x0002373D File Offset: 0x0002193D
		protected override bool EnableIdleSounds
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x060007E7 RID: 2023 RVA: 0x00023740 File Offset: 0x00021940
		protected override float IdleSoundMinDelay
		{
			get
			{
				return 1f;
			}
		}

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x060007E8 RID: 2024 RVA: 0x00023747 File Offset: 0x00021947
		protected override float IdleSoundMaxDelay
		{
			get
			{
				return 7f;
			}
		}

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x060007E9 RID: 2025 RVA: 0x0002374E File Offset: 0x0002194E
		protected override float PainSoundDelay
		{
			get
			{
				return 1.5f;
			}
		}

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x060007EA RID: 2026 RVA: 0x00023755 File Offset: 0x00021955
		protected override string NPCAssetID
		{
			get
			{
				return "poltergeist";
			}
		}

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x060007EB RID: 2027 RVA: 0x0002375C File Offset: 0x0002195C
		protected virtual string bodyEffect
		{
			get
			{
				return "particles/stalker/monsters/poltergeist_body.vpcf";
			}
		}

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x060007EC RID: 2028 RVA: 0x00023763 File Offset: 0x00021963
		protected virtual string deathEffect
		{
			get
			{
				return "particles/stalker/monsters/poltergeist_death_explode.vpcf";
			}
		}

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x060007ED RID: 2029 RVA: 0x0002376A File Offset: 0x0002196A
		protected virtual string bodyIdleSound
		{
			get
			{
				return "poltergeist.tele.idle";
			}
		}

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x060007EE RID: 2030 RVA: 0x00023771 File Offset: 0x00021971
		protected virtual string effectSound
		{
			get
			{
				return "psy.voices";
			}
		}

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x060007EF RID: 2031 RVA: 0x00023778 File Offset: 0x00021978
		protected virtual Color screenColor
		{
			get
			{
				return EffectsPostProcessManager.STALKER_PSY_BURER_COLOR;
			}
		}

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x060007F0 RID: 2032 RVA: 0x0002377F File Offset: 0x0002197F
		protected virtual float effectRange
		{
			get
			{
				return 3000f;
			}
		}

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x060007F1 RID: 2033 RVA: 0x00023786 File Offset: 0x00021986
		protected virtual float effectStrength
		{
			get
			{
				return 1.8f;
			}
		}

		// Token: 0x060007F2 RID: 2034 RVA: 0x00023790 File Offset: 0x00021990
		protected override void PostSpawn()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.SetupPhysicsFromModel(PhysicsMotionType.Keyframed, false);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Speed = base.Resource.RunSpeed;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.RoamingState = new PoltergeistRoamingState(this);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Movement = new NPCMovementBase(this);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.SM.SetState(this.RoamingState);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.sfx = base.PlaySound(this.bodyIdleSound);
		}

		// Token: 0x060007F3 RID: 2035 RVA: 0x00023810 File Offset: 0x00021A10
		public override void SetToIdleState()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.SM.SetState(this.RoamingState);
		}

		// Token: 0x060007F4 RID: 2036 RVA: 0x00023828 File Offset: 0x00021A28
		public override void ClientSpawn()
		{
			PostProcessEmitterComponent postProcessEmitterComponent = this.Components.Create<PostProcessEmitterComponent>(true);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			postProcessEmitterComponent.SetUpEmitter(this.screenColor, this.effectRange, this.effectStrength, this.effectSound, 1f, 0f);
		}

		// Token: 0x060007F5 RID: 2037 RVA: 0x00023862 File Offset: 0x00021A62
		protected override void OnDestroy()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.OnDestroy();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.sfx.Stop();
		}

		// Token: 0x060007F6 RID: 2038 RVA: 0x00023880 File Offset: 0x00021A80
		public override void OnKilled()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.EmitDeathSound();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Gib();
		}

		// Token: 0x060007F7 RID: 2039 RVA: 0x00023898 File Offset: 0x00021A98
		[ClientRpc]
		public override void GibEffect(Vector3 pos)
		{
			if (!base.GibEffect__RpcProxy(pos, null))
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.GibEffect(pos);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Particles.Create(this.deathEffect, this.Position);
		}

		// Token: 0x060007F8 RID: 2040 RVA: 0x000238DB File Offset: 0x00021ADB
		protected override void OnHidden()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.sfx.Stop();
		}

		// Token: 0x060007F9 RID: 2041 RVA: 0x000238EE File Offset: 0x00021AEE
		protected override void OnRevealed()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.sfx = base.PlaySound(this.bodyIdleSound);
		}

		// Token: 0x060007FA RID: 2042 RVA: 0x00023907 File Offset: 0x00021B07
		public override void GibEffect(To toTarget, Vector3 pos)
		{
			base.GibEffect__RpcProxy(pos, new To?(toTarget));
		}

		// Token: 0x060007FB RID: 2043 RVA: 0x00023918 File Offset: 0x00021B18
		protected override void OnCallRemoteProcedure(int id, NetRead read)
		{
			if (id == -660821229)
			{
				Vector3 __pos = read.ReadData<Vector3>(default(Vector3));
				if (!Prediction.WasPredicted("GibEffect", new object[]
				{
					__pos
				}))
				{
					this.GibEffect(__pos);
				}
				return;
			}
			base.OnCallRemoteProcedure(id, read);
		}

		// Token: 0x0400029E RID: 670
		private Sound sfx;

		// Token: 0x0400029F RID: 671
		public NPCState<PoltergeistNPC> RoamingState;
	}
}

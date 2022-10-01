using System;
using System.Runtime.CompilerServices;
using Sandbox;

namespace StalkerRP.NPC
{
	// Token: 0x020000D4 RID: 212
	public class PsyDogIllusionNPC : PsyDogNPC
	{
		// Token: 0x17000297 RID: 663
		// (get) Token: 0x0600091F RID: 2335 RVA: 0x00027C44 File Offset: 0x00025E44
		public float MaterializeTime
		{
			get
			{
				return 0.5f;
			}
		}

		// Token: 0x17000298 RID: 664
		// (get) Token: 0x06000920 RID: 2336 RVA: 0x00027C4B File Offset: 0x00025E4B
		protected override BaseNPCState StartingState
		{
			get
			{
				return this.CreatedState;
			}
		}

		// Token: 0x17000299 RID: 665
		// (get) Token: 0x06000921 RID: 2337 RVA: 0x00027C53 File Offset: 0x00025E53
		protected override string NPCAssetID
		{
			get
			{
				return "psydog.illusion";
			}
		}

		// Token: 0x06000922 RID: 2338 RVA: 0x00027C5A File Offset: 0x00025E5A
		protected override void PostSpawn()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.CreatedState = new PsyDogCreatedState(this);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Health = 1f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.PostSpawn();
		}

		// Token: 0x06000923 RID: 2339 RVA: 0x00027C88 File Offset: 0x00025E88
		public void DoIllusionCreatedEffect()
		{
		}

		// Token: 0x06000924 RID: 2340 RVA: 0x00027C8A File Offset: 0x00025E8A
		public override bool IsIllusion()
		{
			return true;
		}

		// Token: 0x06000925 RID: 2341 RVA: 0x00027C8D File Offset: 0x00025E8D
		protected override void OnDestroy()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			PsyDogIllusionGroup illusionGroup = this.IllusionGroup;
			if (illusionGroup != null)
			{
				illusionGroup.OnMemberDied(this);
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.OnDestroy();
		}

		// Token: 0x06000926 RID: 2342 RVA: 0x00027CB1 File Offset: 0x00025EB1
		public void DissipateIllusion()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Delete();
		}

		// Token: 0x06000927 RID: 2343 RVA: 0x00027CBE File Offset: 0x00025EBE
		public override void TakeDamage(DamageInfo info)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.DissipateIllusion();
		}

		// Token: 0x06000928 RID: 2344 RVA: 0x00027CCB File Offset: 0x00025ECB
		public override void ClientSpawn()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.TimeSinceSpawned = 0f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Particles.Create("particles/stalker/monsters/psydog_illusion_trail.vpcf", this, true);
		}

		// Token: 0x06000929 RID: 2345 RVA: 0x00027CF4 File Offset: 0x00025EF4
		[Event.FrameAttribute]
		private void RenderSpawn()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			SceneObject sceneObject = base.SceneObject;
			if (sceneObject == null)
			{
				return;
			}
			RenderAttributes attributes = sceneObject.Attributes;
			string text = "fDissolveWeight";
			float num = 1f - this.TimeSinceSpawned / this.MaterializeTime;
			attributes.Set(text, num);
		}

		// Token: 0x040002EE RID: 750
		public PsyDogCreatedState CreatedState;

		// Token: 0x040002EF RID: 751
		private TimeSince TimeSinceSpawned = 0f;
	}
}

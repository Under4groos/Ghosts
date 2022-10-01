using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Sandbox;
using Sandbox.Internal;

namespace StalkerRP.NPC
{
	// Token: 0x0200006A RID: 106
	[GameResource("Limb Target Weight", "limbwgt", "Weights for hitting limbs.")]
	public class LimbTargetResource : StalkerResource
	{
		// Token: 0x17000131 RID: 305
		// (get) Token: 0x060004D2 RID: 1234 RVA: 0x00017F1D File Offset: 0x0001611D
		// (set) Token: 0x060004D3 RID: 1235 RVA: 0x00017F25 File Offset: 0x00016125
		[Category("Weights")]
		public float Head { get; set; }

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x060004D4 RID: 1236 RVA: 0x00017F2E File Offset: 0x0001612E
		// (set) Token: 0x060004D5 RID: 1237 RVA: 0x00017F36 File Offset: 0x00016136
		[Category("Weights")]
		public float Torso { get; set; }

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x060004D6 RID: 1238 RVA: 0x00017F3F File Offset: 0x0001613F
		// (set) Token: 0x060004D7 RID: 1239 RVA: 0x00017F47 File Offset: 0x00016147
		[Category("Weights")]
		public float Stomach { get; set; }

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x060004D8 RID: 1240 RVA: 0x00017F50 File Offset: 0x00016150
		// (set) Token: 0x060004D9 RID: 1241 RVA: 0x00017F58 File Offset: 0x00016158
		[Category("Weights")]
		public float Arms { get; set; }

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x060004DA RID: 1242 RVA: 0x00017F61 File Offset: 0x00016161
		// (set) Token: 0x060004DB RID: 1243 RVA: 0x00017F69 File Offset: 0x00016169
		[Category("Weights")]
		public float Legs { get; set; }

		// Token: 0x060004DC RID: 1244 RVA: 0x00017F72 File Offset: 0x00016172
		protected override void PostLoad()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.PostLoad();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.GenerateLimbWeights();
		}

		// Token: 0x060004DD RID: 1245 RVA: 0x00017F8A File Offset: 0x0001618A
		private void AddWeight(float newWeight, HitboxIndex index)
		{
			if (newWeight <= 0f)
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.totalWeight += newWeight;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.limbWeights.Add(new ValueTuple<float, HitboxIndex>(this.totalWeight, index));
		}

		// Token: 0x060004DE RID: 1246 RVA: 0x00017FC4 File Offset: 0x000161C4
		private void GenerateLimbWeights()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.AddWeight(this.Head, HitboxIndex.Head);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.AddWeight(this.Torso, HitboxIndex.Chest);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.AddWeight(this.Stomach, HitboxIndex.Stomach);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.AddWeight(this.Arms / 2f, HitboxIndex.ArmLeft);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.AddWeight(this.Arms / 2f, HitboxIndex.ArmRight);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.AddWeight(this.Legs / 2f, HitboxIndex.LegLeft);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.AddWeight(this.Legs / 2f, HitboxIndex.LegRight);
		}

		// Token: 0x060004DF RID: 1247 RVA: 0x0001806C File Offset: 0x0001626C
		public HitboxIndex GetRandomLimb()
		{
			float r = Rand.Float(this.totalWeight);
			foreach (ValueTuple<float, HitboxIndex> tuple in this.limbWeights)
			{
				float weight = tuple.Item1;
				if (r <= weight && r <= weight)
				{
					return tuple.Item2;
				}
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			GlobalGameNamespace.Log.Info("FAILED TO RETURN LIMB!");
			return HitboxIndex.Head;
		}

		// Token: 0x040001A7 RID: 423
		private List<ValueTuple<float, HitboxIndex>> limbWeights = new List<ValueTuple<float, HitboxIndex>>();

		// Token: 0x040001A8 RID: 424
		private float totalWeight;
	}
}

using System;
using System.Runtime.CompilerServices;
using Sandbox;
using Sandbox.UI;

namespace StalkerRP.UI
{
	// Token: 0x02000060 RID: 96
	[UseTemplate("ui/limbs/limbhealthhud.html")]
	public class LimbHealthHUD : Panel
	{
		// Token: 0x170000FE RID: 254
		// (get) Token: 0x0600043C RID: 1084 RVA: 0x00016596 File Offset: 0x00014796
		// (set) Token: 0x0600043D RID: 1085 RVA: 0x0001659E File Offset: 0x0001479E
		public Panel Head { get; set; }

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x0600043E RID: 1086 RVA: 0x000165A7 File Offset: 0x000147A7
		// (set) Token: 0x0600043F RID: 1087 RVA: 0x000165AF File Offset: 0x000147AF
		public Panel Torso { get; set; }

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x06000440 RID: 1088 RVA: 0x000165B8 File Offset: 0x000147B8
		// (set) Token: 0x06000441 RID: 1089 RVA: 0x000165C0 File Offset: 0x000147C0
		public Panel ArmLeft { get; set; }

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x06000442 RID: 1090 RVA: 0x000165C9 File Offset: 0x000147C9
		// (set) Token: 0x06000443 RID: 1091 RVA: 0x000165D1 File Offset: 0x000147D1
		public Panel ArmRight { get; set; }

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x06000444 RID: 1092 RVA: 0x000165DA File Offset: 0x000147DA
		// (set) Token: 0x06000445 RID: 1093 RVA: 0x000165E2 File Offset: 0x000147E2
		public Panel Stomach { get; set; }

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x06000446 RID: 1094 RVA: 0x000165EB File Offset: 0x000147EB
		// (set) Token: 0x06000447 RID: 1095 RVA: 0x000165F3 File Offset: 0x000147F3
		public Panel LegLeft { get; set; }

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x06000448 RID: 1096 RVA: 0x000165FC File Offset: 0x000147FC
		// (set) Token: 0x06000449 RID: 1097 RVA: 0x00016604 File Offset: 0x00014804
		public Panel LegRight { get; set; }

		// Token: 0x0600044A RID: 1098 RVA: 0x00016610 File Offset: 0x00014810
		public override void Tick()
		{
			StalkerPlayer player = Local.Pawn as StalkerPlayer;
			if (player == null)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				base.Style.Opacity = new float?(0f);
				return;
			}
			if (player.HealthComponent.TimeSinceLastDamaged < this.showDurationAfterDamage || Input.Down(InputButton.Score))
			{
				base.SetClass("visible", true);
			}
			else
			{
				base.SetClass("visible", false);
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.ColorForLimb(this.Head, player.HealthComponent.Head);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.ColorForLimb(this.Torso, player.HealthComponent.Torso);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.ColorForLimb(this.ArmLeft, player.HealthComponent.ArmLeft);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.ColorForLimb(this.ArmRight, player.HealthComponent.ArmRight);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.ColorForLimb(this.Stomach, player.HealthComponent.Stomach);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.ColorForLimb(this.LegLeft, player.HealthComponent.LegLeft);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.ColorForLimb(this.LegRight, player.HealthComponent.LegRight);
		}

		// Token: 0x0600044B RID: 1099 RVA: 0x0001674C File Offset: 0x0001494C
		private void ColorForLimb(Panel panel, LimbBase limbBase)
		{
			if (limbBase.IsBroken)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				panel.Style.BackgroundTint = new Color?(LimbHealthHUD.NoHealth);
				return;
			}
			float frac = limbBase.GetHealthFraction();
			if (frac > 0.5f)
			{
				float delta = (1f - frac) / 0.5f;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				panel.Style.BackgroundTint = new Color?(Color.Lerp(LimbHealthHUD.FullHealth, LimbHealthHUD.HalfHealth, delta, true));
				return;
			}
			if (frac <= 0f)
			{
				return;
			}
			float delta2 = 1f - frac / 0.5f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			panel.Style.BackgroundTint = new Color?(Color.Lerp(LimbHealthHUD.HalfHealth, LimbHealthHUD.LowHealth, delta2, true));
		}

		// Token: 0x04000145 RID: 325
		private static readonly Color FullHealth = Color.Green;

		// Token: 0x04000146 RID: 326
		private static readonly Color HalfHealth = Color.Yellow;

		// Token: 0x04000147 RID: 327
		private static readonly Color LowHealth = Color.Red;

		// Token: 0x04000148 RID: 328
		private static readonly Color NoHealth = Color.Black;

		// Token: 0x04000150 RID: 336
		private float showDurationAfterDamage = 5f;
	}
}

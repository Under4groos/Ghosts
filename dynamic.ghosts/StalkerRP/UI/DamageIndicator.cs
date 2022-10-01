using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Sandbox;
using Sandbox.UI;

namespace StalkerRP.UI
{
	// Token: 0x0200005F RID: 95
	public class DamageIndicator : Panel
	{
		// Token: 0x0600043A RID: 1082 RVA: 0x0001655A File Offset: 0x0001475A
		public DamageIndicator()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.StyleSheet.Load("/ui/DamageIndicator.scss", true);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			DamageIndicator.Current = this;
		}

		// Token: 0x0600043B RID: 1083 RVA: 0x00016583 File Offset: 0x00014783
		public void OnHit(Vector3 pos)
		{
			Panel panel = new DamageIndicator.HitPoint(pos);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			panel.Parent = this;
		}

		// Token: 0x04000144 RID: 324
		public static DamageIndicator Current;

		// Token: 0x020001FB RID: 507
		public class HitPoint : Panel
		{
			// Token: 0x0600184E RID: 6222 RVA: 0x00064FF6 File Offset: 0x000631F6
			public HitPoint(Vector3 pos)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Position = pos;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.Lifetime();
			}

			// Token: 0x0600184F RID: 6223 RVA: 0x00065018 File Offset: 0x00063218
			public override void Tick()
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				base.Tick();
				Vector3 wpos = CurrentView.Rotation.Inverse * (this.Position.WithZ(0f) - CurrentView.Position.WithZ(0f)).Normal;
				RuntimeHelpers.EnsureSufficientExecutionStack();
				wpos = wpos.WithZ(0f).Normal;
				float angle = MathF.Atan2(wpos.y, -1f * wpos.x);
				PanelTransform pt = default(PanelTransform);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				pt.AddTranslateX(Length.Percent(-50f));
				RuntimeHelpers.EnsureSufficientExecutionStack();
				pt.AddTranslateY(Length.Percent(-100f));
				RuntimeHelpers.EnsureSufficientExecutionStack();
				pt.AddRotation(0f, 0f, angle.RadianToDegree() + 180f);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				base.Style.Transform = new PanelTransform?(pt);
			}

			// Token: 0x06001850 RID: 6224 RVA: 0x00065118 File Offset: 0x00063318
			private Task Lifetime()
			{
				DamageIndicator.HitPoint.<Lifetime>d__3 <Lifetime>d__;
				<Lifetime>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
				<Lifetime>d__.<>4__this = this;
				<Lifetime>d__.<>1__state = -1;
				<Lifetime>d__.<>t__builder.Start<DamageIndicator.HitPoint.<Lifetime>d__3>(ref <Lifetime>d__);
				return <Lifetime>d__.<>t__builder.Task;
			}

			// Token: 0x0400084A RID: 2122
			public Vector3 Position;
		}
	}
}

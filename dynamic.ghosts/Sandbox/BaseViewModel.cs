using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Sandbox
{
	// Token: 0x02000176 RID: 374
	[Category("ViewModel")]
	[Title("ViewModel")]
	[Icon("pan_tool")]
	[Description("A common base we can use for weapons so we don't have to implement the logic over and over again.")]
	public class BaseViewModel : AnimatedEntity
	{
		// Token: 0x06001125 RID: 4389 RVA: 0x00044200 File Offset: 0x00042400
		public BaseViewModel()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			BaseViewModel.AllViewModels.Add(this);
		}

		// Token: 0x06001126 RID: 4390 RVA: 0x00044218 File Offset: 0x00042418
		public override void Spawn()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Spawn();
		}

		// Token: 0x06001127 RID: 4391 RVA: 0x00044225 File Offset: 0x00042425
		protected override void OnDestroy()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.OnDestroy();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			BaseViewModel.AllViewModels.Remove(this);
		}

		// Token: 0x06001128 RID: 4392 RVA: 0x00044243 File Offset: 0x00042443
		public override void OnNewModel(Model model)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.OnNewModel(model);
		}

		// Token: 0x06001129 RID: 4393 RVA: 0x00044251 File Offset: 0x00042451
		public override void PostCameraSetup(ref CameraSetup camSetup)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Position = camSetup.Position;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Rotation = camSetup.Rotation;
		}

		// Token: 0x0600112A RID: 4394 RVA: 0x00044278 File Offset: 0x00042478
		public static void UpdateAllPostCamera(ref CameraSetup camSetup)
		{
			foreach (Entity entity in BaseViewModel.AllViewModels)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				entity.PostCameraSetup(ref camSetup);
			}
		}

		// Token: 0x0600112B RID: 4395 RVA: 0x000442D0 File Offset: 0x000424D0
		public override Sound PlaySound(string soundName, string attachment)
		{
			if (this.Owner.IsValid())
			{
				return this.Owner.PlaySound(soundName, attachment);
			}
			return base.PlaySound(soundName, attachment);
		}

		// Token: 0x04000555 RID: 1365
		public static List<BaseViewModel> AllViewModels = new List<BaseViewModel>();
	}
}

using System;
using System.Runtime.CompilerServices;
using Sandbox;
using Sandbox.UI;

namespace StalkerRP
{
	// Token: 0x02000059 RID: 89
	public class StalkerScopedWeaponBase : StalkerWeaponBase
	{
		// Token: 0x06000384 RID: 900 RVA: 0x00013776 File Offset: 0x00011976
		public override void CreateHudElements()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.CreateHudElements();
			RootPanel hud = Local.Hud;
		}

		// Token: 0x06000385 RID: 901 RVA: 0x00013789 File Offset: 0x00011989
		public override void DestroyHudElements()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.DestroyHudElements();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.RemoveScope();
		}

		// Token: 0x06000386 RID: 902 RVA: 0x000137A1 File Offset: 0x000119A1
		private void RemoveScope()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Panel scopePanel = this.ScopePanel;
			if (scopePanel != null)
			{
				scopePanel.Delete(true);
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.ScopePanel = null;
		}

		// Token: 0x06000387 RID: 903 RVA: 0x000137C8 File Offset: 0x000119C8
		public override void PostCameraSetup(ref CameraSetup camSetup)
		{
			if (base.InSights)
			{
				if (base.ViewModelEntity != null)
				{
					base.ViewModelEntity.EnableDrawing = false;
				}
				if (this.lerpZoomAmount == 0f)
				{
					this.lerpZoomAmount = camSetup.FieldOfView;
				}
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.lerpZoomAmount = this.lerpZoomAmount.LerpTo(4f, 10f * Time.Delta, true);
				RuntimeHelpers.EnsureSufficientExecutionStack();
				camSetup.FieldOfView = this.lerpZoomAmount;
				return;
			}
			if (base.ViewModelEntity != null)
			{
				base.ViewModelEntity.EnableDrawing = true;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.lerpZoomAmount = 0f;
		}

		// Token: 0x04000119 RID: 281
		public Panel ScopePanel;

		// Token: 0x0400011A RID: 282
		private float lerpZoomAmount;
	}
}

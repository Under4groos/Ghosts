using System;
using System.Runtime.CompilerServices;
using Sandbox.Internal;
using Sandbox.UI;

namespace Sandbox
{
	// Token: 0x02000191 RID: 401
	[UseTemplate]
	internal class DofPanel : Panel
	{
		// Token: 0x17000568 RID: 1384
		// (get) Token: 0x06001379 RID: 4985 RVA: 0x0004C84C File Offset: 0x0004AA4C
		// (set) Token: 0x0600137A RID: 4986 RVA: 0x0004C854 File Offset: 0x0004AA54
		[DefaultValue(0f)]
		public float Radius { get; set; }

		// Token: 0x17000569 RID: 1385
		// (get) Token: 0x0600137B RID: 4987 RVA: 0x0004C85D File Offset: 0x0004AA5D
		// (set) Token: 0x0600137C RID: 4988 RVA: 0x0004C865 File Offset: 0x0004AA65
		[DefaultValue(0.16f)]
		public float FStop { get; set; } = 0.16f;

		// Token: 0x1700056A RID: 1386
		// (get) Token: 0x0600137D RID: 4989 RVA: 0x0004C86E File Offset: 0x0004AA6E
		// (set) Token: 0x0600137E RID: 4990 RVA: 0x0004C876 File Offset: 0x0004AA76
		[DefaultValue(64f)]
		public float DofFocalPoint { get; set; } = 64f;

		// Token: 0x0600137F RID: 4991 RVA: 0x0004C880 File Offset: 0x0004AA80
		public void TickSettings()
		{
			DevCamPP pp = GlobalGameNamespace.PostProcess.Get<DevCamPP>();
			if (pp == null)
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			pp.DepthOfField.Enabled = (this.Radius > 0f);
			if (!pp.DepthOfField.Enabled)
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			pp.DepthOfField.Radius = this.Radius;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			pp.DepthOfField.FStop = this.FStop;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			pp.DepthOfField.FocalLength = this.DofFocalPoint + 32f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			pp.DepthOfField.FocalPoint = this.DofFocalPoint;
			if (this.focusMode == "live")
			{
				float raySize = 4f;
				Ray eyeRay = new Ray(CurrentView.Position, CurrentView.Rotation.Forward);
				float num = 10000f;
				Trace trace = Trace.Ray(eyeRay, num).UseHitboxes(true);
				Vector3 vector = raySize;
				TraceResult tr = trace.Size(vector).Run();
				if (tr.Hit)
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.DofFocalPoint = this.DofFocalPoint.LerpTo(tr.Distance + raySize * 0.5f, Time.Delta * 10f, true);
				}
			}
		}

		// Token: 0x06001380 RID: 4992 RVA: 0x0004C9CA File Offset: 0x0004ABCA
		[Event.FrameAttribute]
		public void OnFrame()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.TickSettings();
		}

		// Token: 0x06001381 RID: 4993 RVA: 0x0004C9D7 File Offset: 0x0004ABD7
		public void PickFocalPoint(MousePanelEvent e)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.focusMode = "fixed";
			RuntimeHelpers.EnsureSufficientExecutionStack();
			DevCamSettings.OnMouseClicked = null;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			DevCamSettings.OnMouseMoved = null;
		}

		// Token: 0x06001382 RID: 4994 RVA: 0x0004CA00 File Offset: 0x0004AC00
		public void MoveFocalPoint(MousePanelEvent e)
		{
			if (this.focusMode != "pick")
			{
				return;
			}
			Vector3 dir = Screen.GetDirection(e.LocalPosition);
			Ray eyeRay = new Ray(CurrentView.Position, dir);
			float num = 10000f;
			TraceResult tr = Trace.Ray(eyeRay, num).UseHitboxes(true).Run();
			if (tr.Hit)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.DofFocalPoint = tr.Distance;
			}
		}

		// Token: 0x06001383 RID: 4995 RVA: 0x0004CA76 File Offset: 0x0004AC76
		public void StartFocusTypePick()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.focusMode = "pick";
			RuntimeHelpers.EnsureSufficientExecutionStack();
			DevCamSettings.OnMouseClicked = new Action<MousePanelEvent>(this.PickFocalPoint);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			DevCamSettings.OnMouseMoved = new Action<MousePanelEvent>(this.MoveFocalPoint);
		}

		// Token: 0x06001384 RID: 4996 RVA: 0x0004CAB4 File Offset: 0x0004ACB4
		public void LiveFocus()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.focusMode = "live";
		}

		// Token: 0x0400064A RID: 1610
		private string focusMode = "fixed";
	}
}

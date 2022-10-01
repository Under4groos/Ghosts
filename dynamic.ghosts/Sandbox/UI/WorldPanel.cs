using System;
using System.Runtime.CompilerServices;
using Sandbox.Internal;

namespace Sandbox.UI
{
	// Token: 0x020001D1 RID: 465
	public class WorldPanel : RootPanel
	{
		// Token: 0x1700067A RID: 1658
		// (get) Token: 0x06001770 RID: 6000 RVA: 0x00061FB8 File Offset: 0x000601B8
		// (set) Token: 0x06001771 RID: 6001 RVA: 0x00061FC0 File Offset: 0x000601C0
		public ScenePanelObject SceneObject { get; private set; }

		// Token: 0x1700067B RID: 1659
		// (get) Token: 0x06001772 RID: 6002 RVA: 0x00061FC9 File Offset: 0x000601C9
		// (set) Token: 0x06001773 RID: 6003 RVA: 0x00061FD6 File Offset: 0x000601D6
		public Transform Transform
		{
			get
			{
				return this.SceneObject.Transform;
			}
			set
			{
				this.SceneObject.Transform = value;
			}
		}

		// Token: 0x1700067C RID: 1660
		// (get) Token: 0x06001774 RID: 6004 RVA: 0x00061FE4 File Offset: 0x000601E4
		// (set) Token: 0x06001775 RID: 6005 RVA: 0x00061FF4 File Offset: 0x000601F4
		public Vector3 Position
		{
			get
			{
				return this.Transform.Position;
			}
			set
			{
				this.Transform = this.Transform.WithPosition(value);
			}
		}

		// Token: 0x1700067D RID: 1661
		// (get) Token: 0x06001776 RID: 6006 RVA: 0x00062016 File Offset: 0x00060216
		// (set) Token: 0x06001777 RID: 6007 RVA: 0x00062024 File Offset: 0x00060224
		public Rotation Rotation
		{
			get
			{
				return this.Transform.Rotation;
			}
			set
			{
				this.Transform = this.Transform.WithRotation(value);
			}
		}

		// Token: 0x1700067E RID: 1662
		// (get) Token: 0x06001778 RID: 6008 RVA: 0x00062046 File Offset: 0x00060246
		// (set) Token: 0x06001779 RID: 6009 RVA: 0x00062054 File Offset: 0x00060254
		public float WorldScale
		{
			get
			{
				return this.Transform.Scale;
			}
			set
			{
				this.Transform = this.Transform.WithScale(value);
			}
		}

		// Token: 0x1700067F RID: 1663
		// (get) Token: 0x0600177A RID: 6010 RVA: 0x00062076 File Offset: 0x00060276
		// (set) Token: 0x0600177B RID: 6011 RVA: 0x0006207E File Offset: 0x0006027E
		public float MaxInteractionDistance { get; set; }

		// Token: 0x0600177C RID: 6012 RVA: 0x00062088 File Offset: 0x00060288
		public WorldPanel(SceneWorld world = null)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			if (world == null)
			{
				world = GlobalGameNamespace.Map.Scene;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.SceneObject = new ScenePanelObject(world, this);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.SceneObject.Flags.IsOpaque = false;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.SceneObject.Flags.IsTranslucent = true;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.RenderedManually = true;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.PanelBounds = new Rect(-500f, -500f, 1000f, 1000f);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Scale = 2f;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.MaxInteractionDistance = 1000f;
		}

		// Token: 0x0600177D RID: 6013 RVA: 0x00062140 File Offset: 0x00060340
		[Description("Update the bounds for this panel. We purposely do nothing here because on world panels you can change the bounds by setting PanelBounds.")]
		protected override void UpdateBounds(Rect rect)
		{
			Vector3 right = this.Rotation.Right;
			Vector3 down = this.Rotation.Down;
			Rect panelBounds = base.PanelBounds * 0.05f;
			BBox bounds = new BBox(right * panelBounds.left + down * panelBounds.top);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			bounds = bounds.AddPoint(right * panelBounds.left + down * panelBounds.bottom);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			bounds = bounds.AddPoint(right * panelBounds.right + down * panelBounds.top);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			bounds = bounds.AddPoint(right * panelBounds.right + down * panelBounds.bottom);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.SceneObject.Bounds = bounds + this.Position;
		}

		// Token: 0x0600177E RID: 6014 RVA: 0x0006223C File Offset: 0x0006043C
		[Description("We override this to prevent the scale automatically being set based on screen size changing.. because that's obviously not needed here.")]
		protected override void UpdateScale(Rect screenSize)
		{
		}

		// Token: 0x0600177F RID: 6015 RVA: 0x0006223E File Offset: 0x0006043E
		public override void Delete(bool immediate = false)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Delete(immediate);
		}

		// Token: 0x06001780 RID: 6016 RVA: 0x0006224C File Offset: 0x0006044C
		public override void OnDeleted()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.OnDeleted();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			ScenePanelObject sceneObject = this.SceneObject;
			if (sceneObject != null)
			{
				sceneObject.Delete();
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.SceneObject = null;
		}

		// Token: 0x06001781 RID: 6017 RVA: 0x0006227C File Offset: 0x0006047C
		public override bool RayToLocalPosition(Ray ray, out Vector2 position, out float distance)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			position = default(Vector2);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			distance = 0f;
			Plane plane = new Plane(this.Position, this.Rotation.Forward);
			Vector3? pos = plane.Trace(ray, false, (double)this.MaxInteractionDistance);
			if (pos == null)
			{
				return false;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			distance = Vector3.DistanceBetween(pos.Value, ray.Origin);
			if (distance < 1f)
			{
				return false;
			}
			Vector3 localPos3 = this.Transform.PointToLocal(pos.Value);
			Vector2 localPos4 = new Vector2(localPos3.y, -localPos3.z);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			localPos4 *= 20f / this.WorldScale;
			if (!base.IsInside(localPos4))
			{
				return false;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			position = localPos4;
			return true;
		}
	}
}

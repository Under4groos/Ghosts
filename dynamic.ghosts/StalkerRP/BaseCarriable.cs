using System;
using System.Runtime.CompilerServices;
using Sandbox;

namespace StalkerRP
{
	// Token: 0x0200004D RID: 77
	[Title("Carriable")]
	[Icon("luggage")]
	[Description("An entity that can be carried in the player's inventory and hands.")]
	public class BaseCarriable : AnimatedEntity
	{
		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000313 RID: 787 RVA: 0x000121EA File Offset: 0x000103EA
		public virtual string ViewModelPath
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x06000314 RID: 788 RVA: 0x000121ED File Offset: 0x000103ED
		// (set) Token: 0x06000315 RID: 789 RVA: 0x000121F5 File Offset: 0x000103F5
		public BaseViewModel ViewModelEntity { get; protected set; }

		// Token: 0x06000316 RID: 790 RVA: 0x000121FE File Offset: 0x000103FE
		public override void Spawn()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.Spawn();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.PhysicsEnabled = true;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.UsePhysicsCollision = true;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.EnableHideInFirstPerson = true;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.EnableShadowInFirstPerson = true;
		}

		// Token: 0x06000317 RID: 791 RVA: 0x0001223B File Offset: 0x0001043B
		public virtual bool CanCarry(Entity carrier)
		{
			return true;
		}

		// Token: 0x06000318 RID: 792 RVA: 0x0001223E File Offset: 0x0001043E
		public virtual void OnCarryStart(Entity carrier)
		{
			if (base.IsClient)
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.SetParent(carrier, true);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Owner = carrier;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.EnableAllCollisions = false;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.EnableDrawing = false;
		}

		// Token: 0x06000319 RID: 793 RVA: 0x0001227A File Offset: 0x0001047A
		public virtual void SimulateAnimator(PawnAnimator anim)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			anim.SetAnimParameter("holdtype", 1);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			anim.SetAnimParameter("aim_body_weight", 1f);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			anim.SetAnimParameter("holdtype_handedness", 0);
		}

		// Token: 0x0600031A RID: 794 RVA: 0x000122B3 File Offset: 0x000104B3
		public virtual void SimulateAnimator(CitizenAnimationHelper anim)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			anim.HoldType = CitizenAnimationHelper.HoldTypes.Pistol;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			anim.Handedness = CitizenAnimationHelper.Hand.Both;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			anim.AimBodyWeight = 1f;
		}

		// Token: 0x0600031B RID: 795 RVA: 0x000122E0 File Offset: 0x000104E0
		public virtual void OnCarryDrop(Entity dropper)
		{
			if (base.IsClient)
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.SetParent(null, null, null);
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.Owner = null;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.EnableDrawing = true;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.EnableAllCollisions = true;
		}

		// Token: 0x0600031C RID: 796 RVA: 0x00012330 File Offset: 0x00010530
		[Description("This entity has become the active entity. This most likely means a player was carrying it in their inventory and now has it in their hands.")]
		public virtual void ActiveStart(Entity ent)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.EnableDrawing = true;
			Player player = ent as Player;
			if (player != null)
			{
				PawnAnimator animator = player.GetActiveAnimator();
				if (animator != null)
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					this.SimulateAnimator(animator);
				}
			}
			if (this.IsLocalPawn)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.DestroyViewModel();
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.DestroyHudElements();
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.CreateViewModel();
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.CreateHudElements();
			}
		}

		// Token: 0x0600031D RID: 797 RVA: 0x0001239D File Offset: 0x0001059D
		[Description("This entity has stopped being the active entity. This most likely means a player was holding it but has switched away or dropped it (in which case dropped = true)")]
		public virtual void ActiveEnd(Entity ent, bool dropped)
		{
			if (!dropped)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				base.EnableDrawing = false;
			}
			if (base.IsClient)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.DestroyViewModel();
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.DestroyHudElements();
			}
		}

		// Token: 0x0600031E RID: 798 RVA: 0x000123CC File Offset: 0x000105CC
		protected override void OnDestroy()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			base.OnDestroy();
			if (base.IsClient && this.ViewModelEntity.IsValid())
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.DestroyViewModel();
				RuntimeHelpers.EnsureSufficientExecutionStack();
				this.DestroyHudElements();
			}
		}

		// Token: 0x0600031F RID: 799 RVA: 0x00012404 File Offset: 0x00010604
		[Description("Create the viewmodel. You can override this in your base classes if you want to create a certain viewmodel entity.")]
		public virtual void CreateViewModel()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			Host.AssertClient("CreateViewModel");
			if (string.IsNullOrEmpty(this.ViewModelPath))
			{
				return;
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.ViewModelEntity = new BaseViewModel();
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.ViewModelEntity.Position = this.Position;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.ViewModelEntity.Owner = this.Owner;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.ViewModelEntity.EnableViewmodelRendering = true;
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.ViewModelEntity.SetModel(this.ViewModelPath);
		}

		// Token: 0x06000320 RID: 800 RVA: 0x00012491 File Offset: 0x00010691
		[Description("We're done with the viewmodel - delete it")]
		public virtual void DestroyViewModel()
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			BaseViewModel viewModelEntity = this.ViewModelEntity;
			if (viewModelEntity != null)
			{
				viewModelEntity.Delete();
			}
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.ViewModelEntity = null;
		}

		// Token: 0x06000321 RID: 801 RVA: 0x000124B5 File Offset: 0x000106B5
		public virtual void CreateHudElements()
		{
		}

		// Token: 0x06000322 RID: 802 RVA: 0x000124B7 File Offset: 0x000106B7
		public virtual void DestroyHudElements()
		{
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x06000323 RID: 803 RVA: 0x000124B9 File Offset: 0x000106B9
		[Description("Utility - return the entity we should be spawning particles from etc")]
		public virtual ModelEntity EffectEntity
		{
			get
			{
				if (!this.ViewModelEntity.IsValid() || !this.IsFirstPersonMode)
				{
					return this;
				}
				return this.ViewModelEntity;
			}
		}
	}
}

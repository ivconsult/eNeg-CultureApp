
namespace citPOINT.CultureApp.Data.Web
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Data;
    using System.Linq;
    using System.ServiceModel.DomainServices.EntityFramework;
    using System.ServiceModel.DomainServices.Hosting;
    using System.ServiceModel.DomainServices.Server;


    // Implements application logic using the CultureAppEntities context.
    // TODO: Add your application logic to these methods or in additional methods.
    // TODO: Wire up authentication (Windows/ASP.NET Forms) and uncomment the following to disable anonymous access
    // Also consider adding roles to restrict access as appropriate.
    // [RequiresAuthentication]
    [EnableClientAccess()]
    public partial class CultureAppService : LinqToEntitiesDomainService<CultureAppEntities>
    {

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'ConversationCultures' query.
        [Query(IsDefault = true)]
        public IQueryable<ConversationCulture> GetConversationCultures()
        {
            return this.ObjectContext.ConversationCultures;
        }

        public void InsertConversationCulture(ConversationCulture conversationCulture)
        {
            if ((conversationCulture.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(conversationCulture, EntityState.Added);
            }
            else
            {
                this.ObjectContext.ConversationCultures.AddObject(conversationCulture);
            }
        }

        public void UpdateConversationCulture(ConversationCulture currentConversationCulture)
        {
            this.ObjectContext.ConversationCultures.AttachAsModified(currentConversationCulture, this.ChangeSet.GetOriginal(currentConversationCulture));
        }

        public void DeleteConversationCulture(ConversationCulture conversationCulture)
        {
            if ((conversationCulture.EntityState == EntityState.Detached))
            {
                this.ObjectContext.ConversationCultures.Attach(conversationCulture);
            }
            this.ObjectContext.ConversationCultures.DeleteObject(conversationCulture);
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'CultureFiveDimensions' query.
        [Query(IsDefault = true)]
        public IQueryable<CultureFiveDimension> GetCultureFiveDimensions()
        {
            return this.ObjectContext.CultureFiveDimensions;
        }

        public void InsertCultureFiveDimension(CultureFiveDimension cultureFiveDimension)
        {
            if ((cultureFiveDimension.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(cultureFiveDimension, EntityState.Added);
            }
            else
            {
                this.ObjectContext.CultureFiveDimensions.AddObject(cultureFiveDimension);
            }
        }

        public void UpdateCultureFiveDimension(CultureFiveDimension currentCultureFiveDimension)
        {
            this.ObjectContext.CultureFiveDimensions.AttachAsModified(currentCultureFiveDimension, this.ChangeSet.GetOriginal(currentCultureFiveDimension));
        }

        public void DeleteCultureFiveDimension(CultureFiveDimension cultureFiveDimension)
        {
            if ((cultureFiveDimension.EntityState == EntityState.Detached))
            {
                this.ObjectContext.CultureFiveDimensions.Attach(cultureFiveDimension);
            }
            this.ObjectContext.CultureFiveDimensions.DeleteObject(cultureFiveDimension);
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'DomainCultureMappings' query.
        [Query(IsDefault = true)]
        public IQueryable<DomainCultureMapping> GetDomainCultureMappings()
        {
            return this.ObjectContext.DomainCultureMappings;
        }

        public void InsertDomainCultureMapping(DomainCultureMapping domainCultureMapping)
        {
            if ((domainCultureMapping.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(domainCultureMapping, EntityState.Added);
            }
            else
            {
                this.ObjectContext.DomainCultureMappings.AddObject(domainCultureMapping);
            }
        }

        public void UpdateDomainCultureMapping(DomainCultureMapping currentDomainCultureMapping)
        {
            this.ObjectContext.DomainCultureMappings.AttachAsModified(currentDomainCultureMapping, this.ChangeSet.GetOriginal(currentDomainCultureMapping));
        }

        public void DeleteDomainCultureMapping(DomainCultureMapping domainCultureMapping)
        {
            if ((domainCultureMapping.EntityState == EntityState.Detached))
            {
                this.ObjectContext.DomainCultureMappings.Attach(domainCultureMapping);
            }
            this.ObjectContext.DomainCultureMappings.DeleteObject(domainCultureMapping);
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'NegotiationCultures' query.
        [Query(IsDefault = true)]
        public IQueryable<NegotiationCulture> GetNegotiationCultures()
        {
            return this.ObjectContext.NegotiationCultures;
        }

        public void InsertNegotiationCulture(NegotiationCulture negotiationCulture)
        {
            if ((negotiationCulture.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(negotiationCulture, EntityState.Added);
            }
            else
            {
                this.ObjectContext.NegotiationCultures.AddObject(negotiationCulture);
            }
        }

        public void UpdateNegotiationCulture(NegotiationCulture currentNegotiationCulture)
        {
            this.ObjectContext.NegotiationCultures.AttachAsModified(currentNegotiationCulture, this.ChangeSet.GetOriginal(currentNegotiationCulture));
        }

        public void DeleteNegotiationCulture(NegotiationCulture negotiationCulture)
        {
            if ((negotiationCulture.EntityState == EntityState.Detached))
            {
                this.ObjectContext.NegotiationCultures.Attach(negotiationCulture);
            }
            this.ObjectContext.NegotiationCultures.DeleteObject(negotiationCulture);
        }
    }
}



﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orchard.ContentManagement.Records;
using Orchard.Logging;

namespace Orchard.ContentManagement.Handlers
{
    /// <summary>
    /// 由于移除了ContentManager,handlers暂时不会起作用
    /// </summary>
    public abstract class ContentHandler : IContentHandler
    {
        protected ContentHandler()
        {
            Filters = new List<IContentFilter>();
            Logger = NullLogger.Instance;
        }

        public List<IContentFilter> Filters { get; set; }
        public ILogger Logger { get; set; }


        protected void OnCreating<TContent>(Action<CreateContentContext, TContent> handler) where TContent : ContentItem
        {
            Filters.Add(new InlineStorageFilter<TContent> { OnCreating = handler });
        }

        protected void OnCreated<TContent>(Action<CreateContentContext, TContent> handler) where TContent : ContentItem
        {
            Filters.Add(new InlineStorageFilter<TContent> { OnCreated = handler });
        }

        protected void OnUpdating<TContent>(Action<UpdateContentContext, TContent> handler) where TContent : ContentItem
        {
            Filters.Add(new InlineStorageFilter<TContent> { OnUpdating = handler });
        }

        protected void OnUpdated<TContent>(Action<UpdateContentContext, TContent> handler) where TContent : ContentItem
        {
            Filters.Add(new InlineStorageFilter<TContent> { OnUpdated = handler });
        }



        protected void OnRemoving<TContent>(Action<RemoveContentContext, TContent> handler) where TContent : ContentItem
        {
            Filters.Add(new InlineStorageFilter<TContent> { OnRemoving = handler });
        }

        protected void OnRemoved<TContent>(Action<RemoveContentContext, TContent> handler) where TContent : ContentItem
        {
            Filters.Add(new InlineStorageFilter<TContent> { OnRemoved = handler });
        }

        class InlineStorageFilter<TContent> : StorageFilterBase<TContent> where TContent : ContentItem
        {
            public Action<CreateContentContext, TContent> OnCreating { get; set; }
            public Action<CreateContentContext, TContent> OnCreated { get; set; }
            public Action<UpdateContentContext, TContent> OnUpdating { get; set; }
            public Action<UpdateContentContext, TContent> OnUpdated { get; set; }

            public Action<RemoveContentContext, TContent> OnRemoving { get; set; }
            public Action<RemoveContentContext, TContent> OnRemoved { get; set; }

            protected override void Creating(CreateContentContext context, TContent instance)
            {
                if (OnCreating != null) OnCreating(context, instance);
            }
            protected override void Created(CreateContentContext context, TContent instance)
            {
                if (OnCreated != null) OnCreated(context, instance);
            }

            protected override void Updating(UpdateContentContext context, TContent instance)
            {
                if (OnUpdating != null) OnUpdating(context, instance);
            }
            protected override void Updated(UpdateContentContext context, TContent instance)
            {
                if (OnUpdated != null) OnUpdated(context, instance);
            }

            protected override void Removing(RemoveContentContext context, TContent instance)
            {
                if (OnRemoving != null) OnRemoving(context, instance);
            }
            protected override void Removed(RemoveContentContext context, TContent instance)
            {
                if (OnRemoved != null) OnRemoved(context, instance);
            }
        }


        void IContentHandler.Creating(CreateContentContext context)
        {
            foreach (var filter in Filters.OfType<IContentStorageFilter>())
                filter.Creating(context);
            Creating(context);
        }

        void IContentHandler.Created(CreateContentContext context)
        {
            foreach (var filter in Filters.OfType<IContentStorageFilter>())
                filter.Created(context);
            Created(context);
        }


        void IContentHandler.Updating(UpdateContentContext context)
        {
            foreach (var filter in Filters.OfType<IContentStorageFilter>())
                filter.Updating(context);
            Updating(context);
        }

        void IContentHandler.Updated(UpdateContentContext context)
        {
            foreach (var filter in Filters.OfType<IContentStorageFilter>())
                filter.Updated(context);
            Updated(context);
        }


        void IContentHandler.Removing(RemoveContentContext context)
        {
            foreach (var filter in Filters.OfType<IContentStorageFilter>())
                filter.Removing(context);
            Removing(context);
        }

        void IContentHandler.Removed(RemoveContentContext context)
        {
            foreach (var filter in Filters.OfType<IContentStorageFilter>())
                filter.Removed(context);
            Removed(context);
        }

        protected virtual void Creating(CreateContentContext context) { }
        protected virtual void Created(CreateContentContext context) { }

        protected virtual void Updating(UpdateContentContext context) { }
        protected virtual void Updated(UpdateContentContext context) { }

        protected virtual void Removing(RemoveContentContext context) { }
        protected virtual void Removed(RemoveContentContext context) { }


    }

}

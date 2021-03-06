﻿using Orchard.ContentManagement.MetaData.Models;
using Orchard.ContentManagement.Records;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Orchard.ContentManagement
{
    public class ContentItem : IContent
    {
        public ContentItem()
        {
            _parts = new List<ContentPart>();
        }

        public int Id { get { return Record == null ? 0 : Record.Id; } }

        public override bool Equals(object obj)
        {
            return Equals(obj as ContentItem);
        }

        private readonly IList<ContentPart> _parts;


        private static bool IsTransient(ContentItem obj)
        {
            return obj != null && Equals(obj.Id, default(int));
        }

        private Type GetUnproxiedType()
        {
            return GetType();
        }

        public virtual bool Equals(ContentItem other)
        {
            if (other == null)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            if (!IsTransient(this) &&
                !IsTransient(other) &&
                Equals(Id, other.Id))
            {
                var otherType = other.GetUnproxiedType();
                var thisType = GetUnproxiedType();
                return thisType.IsAssignableFrom(otherType) ||
                        otherType.IsAssignableFrom(thisType);
            }

            return false;
        }

        public override int GetHashCode()
        {
            if (Equals(Id, default(int)))
                return base.GetHashCode();
            return Id.GetHashCode();
        }

        public static bool operator ==(ContentItem x, ContentItem y)
        {
            return Equals(x, y);
        }

        public static bool operator !=(ContentItem x, ContentItem y)
        {
            return !(x == y);
        }

        public string ContentType { get; set; }
        public ContentTypeDefinition TypeDefinition { get; set; }

        public IEnumerable<ContentPart> Parts { get { return _parts; } }

        public bool Has(Type partType)
        {
            return partType == typeof(ContentItem) || _parts.Any(partType.IsInstanceOfType);
        }

        public IContent Get(Type partType)
        {
            if (partType == typeof(ContentItem))
                return this;
            return _parts.FirstOrDefault(partType.IsInstanceOfType);
        }

        public void Weld(ContentPart part)
        {
            part.ContentItem = this;
            _parts.Add(part);
        }

        ContentItem IContent.ContentItem
        {
            get { return this; }
        }

        public IContentManager ContentManager { get; set; }

        public ContentItemRecord Record { get; set; }
    }
}

﻿using Orchard.ContentManagement.Handlers;
using Orchard.ContentManagement.MetaData;
using Orchard.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orchard.ContentManagement.Drivers.Coordinators
{
    /// <summary>
    /// This component coordinates how parts are taking part in the rendering when some content needs to be rendered.
    /// It will dispatch BuildDisplay/BuildEditor to all <see cref="IContentPartDriver"/> implementations.
    /// </summary
    public class ContentPartDriverCoordinator : ContentHandlerBase
    {
        private readonly IEnumerable<IContentPartDriver> _drivers;
        private readonly IContentDefinitionManager _contentDefinitionManager;

        public ContentPartDriverCoordinator(IEnumerable<IContentPartDriver> drivers, IContentDefinitionManager contentDefinitionManager)
        {
            _drivers = drivers;
            _contentDefinitionManager = contentDefinitionManager;
            Logger = NullLogger.Instance;
        }

        public ILogger Logger { get; set; }

        public override void Activating(ActivatingContentContext context)
        {
            var contentTypeDefinition = _contentDefinitionManager.GetTypeDefinition(context.ContentType);
            if (contentTypeDefinition == null)
                return;

            var partInfos = _drivers.SelectMany(cpp => cpp.GetPartInfo()).ToList();
            foreach (var typePartDefinition in contentTypeDefinition.Parts)
            {
                var partName = typePartDefinition.PartName;
                var partInfo = partInfos.FirstOrDefault(pi => pi.PartName == partName);
                var part = partInfo != null
                    ? partInfo.Factory(typePartDefinition)
                    : new ContentPart { TypePartDefinition = typePartDefinition };
                context.Builder.Weld(part);
            }
        }

        public override void BuildDisplay(BuildDisplayContext context)
        {
            _drivers.Invoke(driver =>
            {
                var result = driver.BuildDisplay(context);
                if (result != null)
                    result.Apply(context);
            }, Logger);
        }

        public override void BuildEditor(BuildEditorContext context)
        {
            _drivers.Invoke(driver =>
            {
                var result = driver.BuildEditor(context);
                if (result != null)
                    result.Apply(context);
            }, Logger);
        }

        public override void UpdateEditor(UpdateEditorContext context)
        {
            _drivers.Invoke(driver =>
            {
                var result = driver.UpdateEditor(context);
                if (result != null)
                    result.Apply(context);
            }, Logger);
        }
    }
}

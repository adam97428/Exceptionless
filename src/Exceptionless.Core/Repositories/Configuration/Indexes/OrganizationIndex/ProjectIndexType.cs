﻿using System;
using Exceptionless.Core.Extensions;
using Exceptionless.Core.Models;
using Foundatio.Repositories.Elasticsearch.Configuration;
using Foundatio.Repositories.Elasticsearch.Extensions;
using Foundatio.Repositories.Elasticsearch.Queries.Builders;
using Nest;

namespace Exceptionless.Core.Repositories.Configuration {
    public class ProjectIndexType : IndexTypeBase<Project> {
        public ProjectIndexType(OrganizationIndex index) : base(index, "project") { }

        public override TypeMappingDescriptor<Project> BuildMapping(TypeMappingDescriptor<Project> map) {
            return base.BuildMapping(map)
                .Dynamic(false)
                .Properties(p => p
                    .SetupDefaults()
                    .Keyword(f => f.Name(e => e.OrganizationId))
                    .Text(f => f.Name(e => e.Name).AddKeywordField())
                    .Scalar(f => f.NextSummaryEndOfDayTicks, f => f)
                );
        }
    }
}
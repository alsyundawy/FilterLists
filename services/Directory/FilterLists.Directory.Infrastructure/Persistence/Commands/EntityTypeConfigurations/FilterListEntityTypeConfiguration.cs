﻿using FilterLists.Directory.Domain.Aggregates.FilterLists;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FilterLists.Directory.Infrastructure.Persistence.Commands.EntityTypeConfigurations;

internal class FilterListTypeConfiguration : IEntityTypeConfiguration<FilterList>
{
    public virtual void Configure(EntityTypeBuilder<FilterList> builder)
    {
        builder.Property<int>(nameof(Queries.Entities.FilterList.Id));
        builder.HasMany("_changes").WithOne();
        builder.Navigation("_changes").AutoInclude();
        builder.Navigation(f => f.ViewUrls).AutoInclude();
    }
}
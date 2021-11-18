﻿using System.Globalization;
using EFCore.NamingConventions.Internal;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FilterLists.Directory.Infrastructure.Persistence.Queries.Entities;

public record Merge
{
    public int IncludedInFilterListId { get; init; }
    public FilterList IncludedInFilterList { get; } = null!;
    public int IncludesFilterListId { get; init; }
    public FilterList IncludesFilterList { get; } = null!;
}

internal class MergeTypeConfiguration : IEntityTypeConfiguration<Merge>
{
    public virtual void Configure(EntityTypeBuilder<Merge> builder)
    {
        // TODO: register and resolve INameRewriter
        var nr = new SnakeCaseNameRewriter(CultureInfo.InvariantCulture);

        builder.ToTable($"{nr.RewriteName(nameof(Merge))}s");
        builder.HasKey(m => new { m.IncludedInFilterListId, m.IncludesFilterListId });
        builder.HasOne(m => m.IncludedInFilterList)
            .WithMany(fl => fl.IncludesFilterLists)
            .HasForeignKey(m => m.IncludedInFilterListId);
        builder.HasOne(m => m.IncludesFilterList)
            .WithMany(fl => fl.IncludedInFilterLists)
            .HasForeignKey(m => m.IncludesFilterListId);
        builder.HasDataJsonFile<Merge>();
    }
}

﻿using System.Collections.Immutable;

using Fluxor;

namespace SciMaterials.UI.BWASM.States.ContentTypes;

[FeatureState]
public record FilesContentTypesFilterState(ImmutableArray<ContentTypeState> ContentTypes) : CachedState
{
    public FilesContentTypesFilterState() : this(ImmutableArray<ContentTypeState>.Empty) { }

    public ImmutableArray<ContentTypeState> Selected { get; init; } = ImmutableArray<ContentTypeState>.Empty;
    public string Filter { get; init; } = string.Empty;

    public override bool IsNotTimeToUpdateData() => !ContentTypes.IsDefaultOrEmpty && base.IsNotTimeToUpdateData();
}
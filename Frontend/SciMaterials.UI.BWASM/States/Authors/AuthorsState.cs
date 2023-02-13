﻿using System.Collections.Immutable;

using Fluxor;

namespace SciMaterials.UI.BWASM.States.Authors;

[FeatureState]
public record AuthorsState(ImmutableArray<AuthorState> Authors) : CachedState
{
    public AuthorsState() : this(ImmutableArray<AuthorState>.Empty) { }
}
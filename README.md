# Frontier Map Better Prospecting Compatibility

This Vintage Story compatibility mod forwards completed prospecting density
readings from BetterEr Prospecting to Frontier's Map.

BetterEr Prospecting's new density mode does not call the vanilla
`ItemProspectingPick.PrintProbeResults` method patched by Frontier's Map. This
mod instead listens at `ModSystemOreMap.DidProbe`, where the final reading is
already available, and sends that exact reading through Frontier's existing
network, naming-dialog, storage, and prospecting-pin workflow. Map tooltips
include the vanilla quality label (Very poor through Ultra high) and show two
decimal places, so a value such as 0.03 is retained.

The compatibility applies to density readings. BetterEr's node, proximity,
stone, and borehole search modes are intentionally unchanged.

## Requirements

- Vintage Story 1.22 or later
- BetterEr Prospecting
- Frontier's Map

The manifest checks only that both mods are present; it does not pin their
versions.

## Build

Set `VINTAGE_STORY_PATH` to a Vintage Story installation containing the .NET 10
game assemblies, then run:

```sh
dotnet build src/FrontierMapBetterProspecting.csproj -c Release
```

The bundled reference DLLs are compile-time inputs only and are not copied into
the build output.

## Package

```sh
VINTAGE_STORY_PATH=/path/to/vintagestory ./build/package.sh
```

The finished archive is written to:

```text
artifacts/package/frontiermapbetterprospecting.zip
```

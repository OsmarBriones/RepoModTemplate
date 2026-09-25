# RepoModTemplate Architecture

This document describes the runtime structure, data flow, and design decisions for this mod.

---

## High-Level Concept

Briefly summarize what this mod does:
1. **Trigger / Hook:** What game event or lifecycle point triggers mod logic.
2. **Authority / Networking:** Specify if logic runs host-only (singleplayer/master client) or on all clients.
3. **Outcome:** What changes in the game (item spawned, enemy stunned, stats adjusted, etc.).

---

## Main Data Flow

### 1. Level Start / Initialization
- Entry point: `Patches/ReloadOnLevelStart.cs` (Harmony patch on `EnemyDirector.Start`).
- Checks `SemiFunc.RunIsLevel()` to ensure execution only in playable levels.
- Refreshes configuration via `ConfigurationController.Reload()`.
- Resets any per-level state or counters.

### 2. Gameplay Events & Patches
- Place each Harmony patch in `Patches/<TargetType>_<TargetMethod>_Patch.cs`.
- Follow the host-only pattern where applicable:
  ```csharp
  if (!SemiFunc.IsMasterClientOrSingleplayer()) return;
  ```

### 3. Shared Library Usage (RepoAPI)
- If the mod consumes shared logic (spawning items, item keys, reflection helpers), pull only the required modules via git submodule in `external/RepoAPI` and `<Compile Include>` in `.csproj`.
- Never duplicate generic RepoAPI code locally.

---

## Key Design Decisions & Invariants

- **Standalone build:** This repository builds into a single self-contained DLL (`RepoModTemplate.dll`).
- **Configuration reload:** Settings reload cleanly on level transition without requiring a game restart.

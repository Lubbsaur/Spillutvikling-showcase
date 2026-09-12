# SpillutviklingX-showcase

The gameplay scripts from **SpillutviklingX**, a small dungeon-crawler RPG I built in Unity 6 (C#) — NavMesh-driven enemies, a quest/dialogue system, and the usual core-loop plumbing (health, combat, audio, scene flow).

## Why just the scripts

The full project (private) also contains several third-party asset packs — KayKit's dungeon/character models, an RPG forest environment pack, TextMesh Pro, a UI kit. Even the free ones come with Unity Asset Store licenses that don't permit redistributing the raw assets outside a compiled build, so none of that ships here — just the C# I wrote, which doesn't embed any of it. One auto-generated file (Unity's Input System action bindings) is left out too, since it's generated, not written.

Won't compile as-is: these scripts reference project-specific prefabs, animator parameters, and scene objects wired up in the Unity Editor, which don't travel with plain text files.

## What's in here

- **Player** — `PlayerMovement`, `PlayerAttack`, `PlayerHealth`, `PlayerAnimation`, `PlayerRigidbody` — click-to-move control (new Input System), attack/health with a UI health bar and hit feedback
- **Enemies** — `EnemyAI` (NavMesh patrol → chase → attack state machine, with distance-based aggro and forget ranges), `EnemyAttack`, `EnemyAttackHitbox`, `EnemyHealth`
- **Quests & dialogue** — `CloseQuestDialogue`, `DialogueCloseButton`, `NPCDialogueImage`, `NPCVoiceLine`, `SpeakBubble` — an NPC dialogue system with voice lines and quest completion state
- **World & audio** — `Dungeon1`, `AmbienceLooper`, `BackgroundMusic`, `DungeonMusicController`, `FootstepPlayer`, `Billboard`, `LookAtPlayer`, `CameraFollow`
- **UI & flow** — `PauseMenu`, `GameOverMenu`, `SceneSwitch`, `ScreenFader`, `FadeText`, `FinishGameButton`, `CoinCollector`, `CoinSpin`, `GiveCoinsButton`, `HitFlash`

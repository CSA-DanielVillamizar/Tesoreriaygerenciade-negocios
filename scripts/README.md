# Scripts de automatización (GitHub Backlog)

## Requisitos

- PowerShell 5.1+
- GitHub CLI (`gh`) instalado
- Sesión iniciada: `gh auth login`
- Permisos para crear milestones, labels e issues en el repo

## Script

- `scripts/create_github_backlog.ps1`

Este script:

- Crea milestones (`Phase 0` a `Phase 5` y `Future / Backlog`) si no existen.
- Crea labels de tipo, fase, área y prioridad si no existen.
- Crea issues para todos los archivos `issue-epic-*.md` y `issue-story-*.md`.
- Usa los `.md` como **body** y asigna labels/milestone automáticamente.
- Evita duplicados por título (si ya existe, lo omite).

## Uso rápido

Desde la raíz del repo:

```powershell
powershell -ExecutionPolicy Bypass -File .\scripts\create_github_backlog.ps1 \
  -Repo "CSA-DanielVillamizar/Tesoreriaygerenciade-negocios" \
  -BacklogPath "C:\Users\DanielVillamizar\Sistema Contable L.A.M.A. Medellin\backlog" \
  -BacklogDocPath ".\docs\BACKLOG.md"
```

## Modo simulación (sin crear nada)

```powershell
powershell -ExecutionPolicy Bypass -File .\scripts\create_github_backlog.ps1 \
  -Repo "CSA-DanielVillamizar/Tesoreriaygerenciade-negocios" \
  -BacklogPath "C:\Users\DanielVillamizar\Sistema Contable L.A.M.A. Medellin\backlog" \
  -BacklogDocPath ".\docs\BACKLOG.md" \
  -WhatIf
```

## Notas

- Si omites `-Repo`, el script intenta detectarlo desde `git remote origin`.
- Si omites `-BacklogDocPath`, usa `docs/BACKLOG.md` del repo actual.

param(
    [string]$Repo = "",
    [string]$BacklogPath = "C:\Users\DanielVillamizar\Sistema Contable L.A.M.A. Medellin\backlog",
    [string]$BacklogDocPath = "",
    [switch]$WhatIf
)

$ErrorActionPreference = "Stop"

function Write-Info([string]$Message) {
    Write-Host "[INFO] $Message" -ForegroundColor Cyan
}

function Write-Warn([string]$Message) {
    Write-Host "[WARN] $Message" -ForegroundColor Yellow
}

function Resolve-RepoFromGit {
    $origin = (& git remote get-url origin 2>$null)
    if (-not $origin) {
        throw "No se pudo detectar el repo desde git. Pasa -Repo owner/name."
    }

    if ($origin -match "github.com[:/](?<owner>[^/]+)/(?<name>[^/.]+)(\.git)?$") {
        return "$($Matches.owner)/$($Matches.name)"
    }

    throw "No se pudo parsear origin '$origin' como repo GitHub owner/name."
}

function Ensure-GhReady {
    & gh --version *> $null
    if ($LASTEXITCODE -ne 0) {
        throw "GitHub CLI (gh) no está instalado o no está en PATH."
    }

    & gh auth status *> $null
    if ($LASTEXITCODE -ne 0) {
        throw "No hay sesión activa de gh. Ejecuta: gh auth login"
    }
}

function Get-MilestoneMap {
    return [ordered]@{
        "0" = "Phase 0 - Foundations"
        "1" = "Phase 1 - MVP Contabilidad + Cuotas"
        "2" = "Phase 2 - Donaciones"
        "3" = "Phase 3 - Proyectos"
        "4" = "Phase 4 - Negocios"
        "5" = "Phase 5 - Tributario avanzado"
        "X" = "Future / Backlog"
    }
}

function Ensure-Milestones([string]$RepoName, [hashtable]$MilestoneMap) {
    $existing = & gh api "repos/$RepoName/milestones?state=all&per_page=100" | ConvertFrom-Json
    $existingTitles = @{}
    foreach ($m in $existing) { $existingTitles[$m.title] = $true }

    foreach ($title in $MilestoneMap.Values) {
        if ($existingTitles.ContainsKey($title)) {
            Write-Info "Milestone ya existe: $title"
            continue
        }

        if ($WhatIf) {
            Write-Info "[WhatIf] Crearía milestone: $title"
            continue
        }

        Write-Info "Creando milestone: $title"
        & gh api "repos/$RepoName/milestones" --method POST --field title="$title" *> $null
    }
}

function Get-LabelDefinitions {
    $labels = @(
        @{ name = "epic"; color = "5319e7"; description = "Epic issue" },
        @{ name = "story"; color = "1d76db"; description = "Story issue" },
        @{ name = "task"; color = "0e8a16"; description = "Task issue" },
        @{ name = "bug"; color = "d73a4a"; description = "Bug issue" },

        @{ name = "phase:0"; color = "c5def5"; description = "Phase 0" },
        @{ name = "phase:1"; color = "c5def5"; description = "Phase 1" },
        @{ name = "phase:2"; color = "c5def5"; description = "Phase 2" },
        @{ name = "phase:3"; color = "c5def5"; description = "Phase 3" },
        @{ name = "phase:4"; color = "c5def5"; description = "Phase 4" },
        @{ name = "phase:5"; color = "c5def5"; description = "Phase 5" },

        @{ name = "area:iam"; color = "bfdadc"; description = "Identity and access" },
        @{ name = "area:infra"; color = "bfdadc"; description = "Infrastructure" },
        @{ name = "area:accounting"; color = "bfdadc"; description = "Accounting" },
        @{ name = "area:treasury"; color = "bfdadc"; description = "Treasury" },
        @{ name = "area:members"; color = "bfdadc"; description = "Members" },
        @{ name = "area:donations"; color = "bfdadc"; description = "Donations" },
        @{ name = "area:projects"; color = "bfdadc"; description = "Projects" },
        @{ name = "area:business"; color = "bfdadc"; description = "Business" },
        @{ name = "area:reports"; color = "bfdadc"; description = "Reports" },
        @{ name = "area:docs"; color = "bfdadc"; description = "Documentation" },

        @{ name = "priority:high"; color = "b60205"; description = "High priority" },
        @{ name = "priority:medium"; color = "fbca04"; description = "Medium priority" },
        @{ name = "priority:low"; color = "0e8a16"; description = "Low priority" }
    )

    return $labels
}

function Ensure-Labels([string]$RepoName) {
    $existing = & gh label list --repo $RepoName --limit 200 --json name | ConvertFrom-Json
    $existingNames = @{}
    foreach ($label in $existing) { $existingNames[$label.name] = $true }

    foreach ($def in (Get-LabelDefinitions)) {
        if ($existingNames.ContainsKey($def.name)) {
            Write-Info "Label ya existe: $($def.name)"
            continue
        }

        if ($WhatIf) {
            Write-Info "[WhatIf] Crearía label: $($def.name)"
            continue
        }

        Write-Info "Creando label: $($def.name)"
        & gh label create $def.name --repo $RepoName --color $def.color --description $def.description *> $null
    }
}

function Get-IssueFiles([string]$Path) {
    $epics = Get-ChildItem -Path $Path -Filter "issue-epic-*.md" -File | Sort-Object Name
    $stories = Get-ChildItem -Path $Path -Filter "issue-story-*.md" -File | Sort-Object Name
    return @($epics + $stories)
}

function Get-BacklogTitles([string]$DocPath) {
    $result = @{
        Epic  = @()
        Story = @()
    }

    if (-not $DocPath -or -not (Test-Path $DocPath)) {
        return $result
    }

    $mode = ""
    foreach ($line in (Get-Content -Path $DocPath)) {
        if ($line -match '^##\s+Épicas') {
            $mode = "Epic"
            continue
        }
        if ($line -match '^##\s+Historias') {
            $mode = "Story"
            continue
        }

        if ($mode -eq "Epic" -and $line -match '^\s*\d+\.\s+(?<title>.+)\s*$') {
            $result.Epic += $Matches.title.Trim()
            continue
        }

        if ($mode -eq "Story" -and $line -match '^\s*-\s+\[Story\]\[Phase\s*\d+\]\s+(?<title>.+)\s*$') {
            $result.Story += $Matches.title.Trim()
            continue
        }
    }

    return $result
}

function Get-SectionSingleLine([string[]]$Lines, [string]$SectionHeading) {
    for ($i = 0; $i -lt $Lines.Length; $i++) {
        if ($Lines[$i] -match "^##\s+$SectionHeading\s*$") {
            for ($j = $i + 1; $j -lt $Lines.Length; $j++) {
                $candidate = $Lines[$j].Trim()
                if ($candidate -eq "") { continue }
                if ($candidate -match '^##\s+') { return "" }
                return $candidate
            }
        }
    }
    return ""
}

function Resolve-IssueType([string]$FileName) {
    if ($FileName -like "issue-epic-*.md") { return "epic" }
    if ($FileName -like "issue-story-*.md") { return "story" }
    return ""
}

function Resolve-Phase([string]$FileName, [string]$Title, [string]$BodyText) {
    if ($FileName -match '^issue-story-(?<phase>[0-5])-') {
        return $Matches.phase
    }

    if ($FileName -match 'issue-story-iam-') { return "0" }
    if ($FileName -match 'issue-story-fx-') { return "1" }

    $text = "$Title`n$BodyText"
    if ($text -match '(?i)phase\s*(?<phase>[0-5])') {
        return $Matches.phase
    }

    if ($text -match '(?i)\[phase\s*x\]') {
        return "X"
    }

    return "X"
}

function Resolve-AreaLabel([string]$IssueType, [string]$FileName, [string]$Phase, [string]$Title, [string]$BodyText) {
    $text = ("$FileName`n$Title`n$BodyText").ToLowerInvariant()

    if ($IssueType -eq "story") {
        if ($FileName -match 'issue-story-iam-') { return "area:iam" }
        if ($FileName -match 'issue-story-fx-') { return "area:accounting" }
        if ($FileName -match '^issue-story-0-') { return "area:iam" }
        if ($FileName -match '^issue-story-1-') { return "area:accounting" }
        if ($FileName -match '^issue-story-2-') { return "area:donations" }
        if ($FileName -match '^issue-story-3-') { return "area:projects" }
        if ($FileName -match '^issue-story-4-') { return "area:business" }
        if ($FileName -match '^issue-story-5-') { return "area:reports" }
    }

    if ($IssueType -eq "epic" -and $FileName -match '^issue-epic-(?<idx>\d+)\.md$') {
        $epicIdx = [int]$Matches.idx
        $epicAreaMap = @{
            1  = "area:iam"
            2  = "area:infra"
            3  = "area:accounting"
            4  = "area:accounting"
            5  = "area:treasury"
            6  = "area:members"
            7  = "area:accounting"
            8  = "area:accounting"
            9  = "area:donations"
            10 = "area:projects"
            11 = "area:business"
            12 = "area:reports"
            13 = "area:reports"
        }
        if ($epicAreaMap.ContainsKey($epicIdx)) {
            return $epicAreaMap[$epicIdx]
        }
    }

    if ($Phase -eq "2") { return "area:donations" }
    if ($Phase -eq "3") { return "area:projects" }
    if ($Phase -eq "4") { return "area:business" }
    if ($Phase -eq "5") { return "area:reports" }

    if ($text -match 'donaci[oó]n|donaciones|certificado|campa') { return "area:donations" }
    if ($text -match 'proyecto|beneficiario|rendici[oó]n') { return "area:projects" }
    if ($text -match '\bmerch\b|inventario|\bventa\b|\bventas\b|negocios?') { return "area:business" }
    if ($text -match 'ex[oó]gena|dian|tributari|beneficiarios finales|\breporte\b|\breportes\b') { return "area:reports" }
    if ($text -match 'miembro|afiliaci[oó]n|\bcuota\b|\bcuotas\b|\bcxc\b|cartera') { return "area:members" }
    if ($text -match 'tesorer[ií]a|banco|conciliaci[oó]n|medio de pago|\brecibo\b|\brecibos\b') { return "area:treasury" }
    if ($text -match 'contab|\bpuc\b|comprobante|libros? contables|balance|\bcxp\b|diferencia en cambio|\btrm\b|\busd\b|cierre mensual') { return "area:accounting" }
    if ($text -match '\binfra\b|key vault|blob|app service|azure sql|observabil|insights|devops') { return "area:infra" }
    if ($text -match '\biam\b|entra|oidc|mfa|auth|login|jwt|\brbac\b|permiso|roles?') { return "area:iam" }
    if ($text -match 'docs|documentaci') { return "area:docs" }

    return "area:docs"
}

function Resolve-PriorityLabel([string]$Title, [string]$BodyText) {
    $text = ("$Title`n$BodyText").ToLowerInvariant()
    if ($text -match 'priority:high|prioridad alta|alta prioridad') { return "priority:high" }
    if ($text -match 'priority:low|prioridad baja|baja prioridad') { return "priority:low" }
    return "priority:medium"
}

function Resolve-IssueTitle(
    [System.IO.FileInfo]$File,
    [string]$IssueType,
    [string[]]$BodyLines,
    [hashtable]$BacklogTitles
) {
    if ($IssueType -eq "epic" -and $File.Name -match '^issue-epic-(?<idx>\d+)\.md$') {
        $index = [int]$Matches.idx - 1
        if ($index -ge 0 -and $index -lt $BacklogTitles.Epic.Count) {
            return $BacklogTitles.Epic[$index]
        }
    }

    $rawQuiero = Get-SectionSingleLine -Lines $BodyLines -SectionHeading "Quiero"
    if ($rawQuiero) {
        if ($IssueType -eq "epic") { return "EPIC: $rawQuiero" }
        return "STORY: $rawQuiero"
    }

    $rawObjetivo = Get-SectionSingleLine -Lines $BodyLines -SectionHeading "Objetivo"
    if ($rawObjetivo) {
        if ($IssueType -eq "epic") { return "EPIC: $rawObjetivo" }
        return "STORY: $rawObjetivo"
    }

    if ($IssueType -eq "epic") { return "EPIC: $($File.BaseName)" }
    return "STORY: $($File.BaseName)"
}

function Get-ExistingIssueTitles([string]$RepoName) {
    $issues = & gh issue list --repo $RepoName --state all --limit 500 --json "number,title" | ConvertFrom-Json
    $existing = @{}
    foreach ($i in $issues) {
        $existing[$i.title] = $i.number
    }
    return $existing
}

function Create-IssuesFromFiles(
    [string]$RepoName,
    [System.IO.FileInfo[]]$Files,
    [hashtable]$MilestoneMap,
    [hashtable]$BacklogTitles
) {
    $existingTitles = Get-ExistingIssueTitles -RepoName $RepoName
    $created = 0
    $skipped = 0

    foreach ($file in $Files) {
        $issueType = Resolve-IssueType -FileName $file.Name
        if ($issueType -notin @("epic", "story")) {
            Write-Warn "Saltando archivo no soportado: $($file.Name)"
            continue
        }

        $bodyText = Get-Content -Path $file.FullName -Raw
        $bodyLines = $bodyText -split "`r?`n"
        $title = Resolve-IssueTitle -File $file -IssueType $issueType -BodyLines $bodyLines -BacklogTitles $BacklogTitles

        if ($existingTitles.ContainsKey($title)) {
            Write-Info "Issue ya existe (#$($existingTitles[$title])): $title"
            $skipped++
            continue
        }

        $phase = Resolve-Phase -FileName $file.Name -Title $title -BodyText $bodyText
        $milestoneTitle = $MilestoneMap[$phase]
        if (-not $milestoneTitle) { $milestoneTitle = $MilestoneMap["X"] }

        $areaLabel = Resolve-AreaLabel -IssueType $issueType -FileName $file.Name -Phase $phase -Title $title -BodyText $bodyText
        $priorityLabel = Resolve-PriorityLabel -Title $title -BodyText $bodyText

        $labels = @($issueType)
        if ($phase -match '^[0-5]$') {
            $labels += "phase:$phase"
        }
        $labels += $areaLabel
        $labels += $priorityLabel
        $labelCsv = ($labels | Select-Object -Unique) -join ","

        if ($WhatIf) {
            Write-Info "[WhatIf] Crearía issue: '$title' | milestone='$milestoneTitle' | labels='$labelCsv'"
            $created++
            continue
        }

        Write-Info "Creando issue: $title"
        $args = @(
            "issue", "create",
            "--repo", $RepoName,
            "--title", $title,
            "--body-file", $file.FullName,
            "--label", $labelCsv
        )
        if ($milestoneTitle) {
            $args += @("--milestone", $milestoneTitle)
        }

        $out = & gh @args 2>&1
        if ($LASTEXITCODE -ne 0) {
            Write-Warn "No se pudo crear '$title'. Error: $out"
            continue
        }

        Write-Info "Creado: $out"
        $created++
    }

    Write-Info "Resumen issues -> creados: $created | omitidos: $skipped"
}

if (-not $Repo) {
    $Repo = Resolve-RepoFromGit
}

if (-not $BacklogDocPath) {
    $BacklogDocPath = Join-Path (Get-Location) "docs/BACKLOG.md"
}

if (-not (Test-Path $BacklogPath)) {
    throw "No existe BacklogPath: $BacklogPath"
}

Ensure-GhReady
Write-Info "Repo objetivo: $Repo"
Write-Info "Backlog folder: $BacklogPath"
Write-Info "Backlog doc (títulos): $BacklogDocPath"

$milestoneMap = Get-MilestoneMap
Ensure-Milestones -RepoName $Repo -MilestoneMap $milestoneMap
Ensure-Labels -RepoName $Repo

$titles = Get-BacklogTitles -DocPath $BacklogDocPath
$files = Get-IssueFiles -Path $BacklogPath

if (-not $files -or $files.Count -eq 0) {
    throw "No se encontraron archivos issue-epic-*.md / issue-story-*.md en $BacklogPath"
}

Create-IssuesFromFiles -RepoName $Repo -Files $files -MilestoneMap $milestoneMap -BacklogTitles $titles

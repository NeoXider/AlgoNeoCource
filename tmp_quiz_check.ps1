$files = @()
foreach($d in 'm1','m2','m3','m4','m5','m6') {
    $path = Join-Path 'C:\Git\AlgoNeoCource\lessons2' $d
    if(Test-Path $path) {
        $files += Get-ChildItem -Path $path -Filter '*.md' -Recurse
    }
}

$results = @()
foreach($f in $files) {
    $lines = Get-Content $f.FullName -Encoding UTF8
    for($i=0; $i -lt $lines.Count; $i++) {
        if($lines[$i] -match '^```\s*quiz\s*$') {
            $closeIdx = -1
            $quizId = 'unknown'
            for($j=$i+1; $j -lt [Math]::Min($i+50, $lines.Count); $j++) {
                if($lines[$j].Trim() -eq '```') {
                    $closeIdx = $j
                    break
                }
                $m = [regex]::Match($lines[$j], '^id:\s*(\S+)')
                if($m.Success) { $quizId = $m.Groups[1].Value }
            }
            if($closeIdx -ge 0) {
                $foundExp = $false
                for($k=$closeIdx+1; $k -lt [Math]::Min($closeIdx+20, $lines.Count); $k++) {
                    $s = $lines[$k].Trim()
                    if($s -match '^#{1,6}\s+') {
                        if($s -match 'Пояснение') { $foundExp = $true }
                        break
                    }
                }
                if(-not $foundExp) {
                    $results += [PSCustomObject]@{File=$f.FullName; QuizId=$quizId; Line=$i+1}
                }
                $i = $closeIdx
            }
        }
    }
}

foreach($r in $results) { Write-Host "File: $($r.File), Quiz ID: $($r.QuizId), Line: $($r.Line)" }
Write-Host ''
Write-Host "Total missing explanations: $($results.Count)"
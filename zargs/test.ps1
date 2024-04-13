# param([string[]]$items)
Write-Host -ForegroundColor yellow "[TestIn]"
if ($args -ne $null) {
  foreach($item in $args)
  {
    echo "Arg: $item"
  }
}
if ($items -ne $null) {
  foreach($item in $items)
  {
    echo "item: $item"
  }
}
Write-Host -ForegroundColor yellow "[TestOut]"

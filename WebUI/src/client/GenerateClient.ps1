  $ErrorActionPreference = "Stop"

  $swaggerUrl = "https://localhost:44351/swagger/v1/swagger.json"

  # Output directly into this folder (where the script lives)
  $outputFile = Join-Path $PSScriptRoot "client.ts"

  Write-Host "Fetching Swagger from $swaggerUrl..."
  Write-Host "Generating TypeScript client to $outputFile..."

#  nswag openapi2tsclient `
#    /input:$swaggerUrl `
#    /output:$outputFile `
#    /template:Fetch `
#    /clientBaseClass: "" `
#    /useGetBaseUrlMethod:false `
#    /generateClientClasses:true `
#    /generateOptionalParameters:true `
#    /typeScriptVersion:4.5 
  
  # replaced this command with one that pulls from a config file
  # this means we can see all of the command options instead of
  # having to look it up
  nswag run nswag.json
  
  Write-Host "`nTypeScript API client generated successfully!"

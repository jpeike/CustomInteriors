  $ErrorActionPreference = "Stop"

  $swaggerUrl = "https://localhost:44351/swagger/v1/swagger.json"

  # Output directly into this folder (where the script lives)
  $outputFile = Join-Path $PSScriptRoot "client.ts"

  Write-Host "🔄 Fetching Swagger from $swaggerUrl..."
  Write-Host "🛠️  Generating TypeScript client to $outputFile..."

  nswag openapi2tsclient `
    /input:$swaggerUrl `
    /output:$outputFile `
    /template:Fetch `
    /clientBaseClass: "" `
    /useGetBaseUrlMethod:false `
    /generateClientClasses:true `
    /generateOptionalParameters:true `
    /typeScriptVersion:4.5 `
    /useRequestInterceptor:true `           # <-- ADDED
    /useTransformOptionsMethod:true 

  Write-Host "`n✅ TypeScript API client generated successfully!"

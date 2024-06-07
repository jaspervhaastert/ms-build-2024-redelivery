param name string = 'oai-msbuild24'
param location string = resourceGroup().location

resource account 'Microsoft.CognitiveServices/accounts@2024-04-01-preview' = {
  name: name
  location: location
  kind: 'OpenAI'
  sku: {
    name: 'S0'
  }
  properties: {
    customSubDomainName: toLower(name)
  }
}
